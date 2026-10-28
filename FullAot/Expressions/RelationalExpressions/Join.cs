using FullAot.DataItems;
using FullAot.Expressions.ScalarExpressions;
using FullAot.Expressions.ScalarExpressions.Predicates;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.RelationalExpressions;

public class Join : BinaryExpression
{
    private readonly Predicate _on;
    private readonly int[] _rightKeep;
    public override Schema OutputSchema { get; }

    public Join(RelationalExpression left, RelationalExpression right, IEnumerable<(string col_left, string col_right)> map) : base(left, right)
    {
        if (map.Count() == 0)
        {
            //keine Elemente uebergeben, ein natural join ist also requested
            //dieser geht ueber alle attribute deren column (name + type) identisch ist in beiden Relationen
            var intersectSchema = left.OutputSchema.Intersect(right.OutputSchema);
            foreach (var col in intersectSchema.Columns)
            {
                map.Append((col.Name, col.Name));
            }
        }

        var fixedRemovedColumnsFromRight = map.Select(t => t.col_right);
        OutputSchema = left.OutputSchema.Join(right.OutputSchema, fixedRemovedColumnsFromRight);
        
        //Problem geloest, ich binde das Predicate einfach an ein garantiertes Schema
        Predicate joinPredicate = new Eq(new ColumnReference( $"l.{map.ElementAt(0).col_left}"),
            new ColumnReference($"r.{map.ElementAt(0).col_right}"));

        for (int i = 1; i < map.Count(); i++)
        {
            Predicate brother = new Eq(new ColumnReference( $"l.{map.ElementAt(i).col_left}"),
                new ColumnReference($"r.{map.ElementAt(i).col_right}"));
            joinPredicate = new And(joinPredicate, brother);
        }
        _on = joinPredicate;
        //Erstelle temp join-schema und binde predicate daran
        var joinSchema = Left.OutputSchema.Combine(Right.OutputSchema);
        _on.Bind(joinSchema);
        //Erstelle ein schnelles Lookup array welche index von rechts behalten werden sollen nach dem join
        var exceptSchema = Right.OutputSchema.Except(Left.OutputSchema, fixedRemovedColumnsFromRight);
        _rightKeep = new int[exceptSchema.Count];
        for (int i = 0; i < exceptSchema.Count; i++)
        {
            _rightKeep[i] = exceptSchema.IndexOf(exceptSchema[i].Name);
        }
    }

    public Join(RelationalExpression left, RelationalExpression right, IEnumerable<string> columns) : this(left, right, columns.Select(c => (c,c)))
    {
    }

    public Join(RelationalExpression left, RelationalExpression right) : this(left, right, new List<(string col_left, string col_right)>())
    {
    }

    public override Relation Evaluate()
    {
        //später: Check die Datenmenge und entscheide dann somewhat dynamisch welchen der Algorithmen am besten für den Join benutzt wird
        return EvaluateNaive();
    }

    private Relation EvaluateNaive()
    {
        //Evaluate both children, then return a relation that contains all attributes referenced in On (or a natural join)
        var left = Left.Evaluate();
        var right = Right.Evaluate();
        /*
         * Ablauf (nach neuestem Stand)
         * Erstelle ein gemeinsames schema fuer links + rechts
         * Binde das On-Predicate an das erstellte Schema
         * nested-for-loops ueber linke tuple + rechte tuple
         * erstelle tuple und evaluate predicate auf diesem tuple
         * wenn condition matched, bau den echten tuple und fuege in die return relation hinzu
         */
        var retRelation = new Relation(OutputSchema);
        foreach (var tupleLeft in left)
        {
            var combinedTuple = new Object[left.Schema.Count + right.Schema.Count];
            Array.Copy(tupleLeft.Values, 0, combinedTuple, 0, left.Schema.Count);
            foreach (var tupleRight in right)
            {
                Array.Copy(tupleRight.Values, 0, combinedTuple, left.Schema.Count, right.Schema.Count);
                if (_on.Evaluate(new Tuple(combinedTuple)) is true)
                {
                    //baue den korrekten tuple
                    var outTupleValues = new Object[OutputSchema.Count];
                    //nehme alles von links
                    Array.Copy(tupleLeft.Values, 0, outTupleValues, 0, left.Schema.Count);
                    //nehme nur von rechts was neu ist
                    for (int i = 0; i < _rightKeep.Length; i++)
                    {
                        outTupleValues[i + left.Schema.Count] = tupleRight[_rightKeep[i]];
                    }
                    //insert den Tuple in retRelation
                    retRelation.Insert(new Tuple(outTupleValues));
                }
                else
                {
                    //Predicate was not evaluated to true -> spring zum naechsten Tuple
                    continue;
                }
            }
        }
        return retRelation;
    }
}
using FullAot.DataItems;
using FullAot.Expressions.ScalarExpressions;
using FullAot.Expressions.ScalarExpressions.Predicates;

namespace FullAot.Expressions.RelationalExpressions;

public class Join : BinaryExpression
{
    private IEnumerable<(string col_left, string col_right)> JoinMap { get; }
    public Join(RelationalExpression left, RelationalExpression right, IEnumerable<(string col_left, string col_right)> map) : base(left, right)
    {
        //Das problem ist wirklich, dass das Predicate in meinem aktuellen Design lediglich zur Laufzeit evaluiert werden kann
        //Das waere kein großes issue, aber mein context-system erfordert hier eine klare Trennung
        //ansonsten muesste ich ad-hoc das Schema mutieren und das ist ein komplett anderer Berg
    }

    public Join(RelationalExpression left, RelationalExpression right, IEnumerable<string> columns) : this(left, right, columns.Select(c => (c,c)))
    {
    }

    public Join(RelationalExpression left, RelationalExpression right) : this(left, right, new List<(string col_left, string col_right)>())
    {
    }

    public static Predicate BuildJoinPredicate(Relation left, Relation right, IEnumerable<(string col_left, string col_right)> joinMap)
    {
        Predicate joinPredicate = BuildEqPredicate(left.Schema, right.Schema, joinMap.ElementAt(0));

        for (int i = 1; i < joinMap.Count(); i++)
        {
            Predicate brother = BuildEqPredicate(left.Schema, right.Schema, joinMap.ElementAt(i));
            joinPredicate = new And(joinPredicate, brother);
        }
        
        return joinPredicate;
    }

    public static Predicate BuildEqPredicate(Schema left, Schema right, (string col_left, string col_right) map)
    {
        var cLeft = left[left.IndexOf(map.col_left)];
        var cRight = right[right.IndexOf(map.col_right)];
        if (cLeft.Type != cRight.Type) throw new InvalidOperationException("Operand-Types do not match");
        //Ich find' das echt nicht nice, aber mir faellt gerade nichts anderes ein. Ich denke eher, dass mein Design hier eine Schwachstelle hat
        //aber fuers erste lass' ich das monster hier mal stehen und schau demnaechst nochmal mit einem frischen Auge drauf
        return cLeft.Type switch
        {
            DataType.String => new Eq(new ColumnReference(cLeft.Name),
                new ColumnReference(cRight.Name)),
            DataType.Bool => new Eq(new ColumnReference(cLeft.Name),
                new ColumnReference(cRight.Name)),
            DataType.DateTime => new Eq(new ColumnReference(cLeft.Name),
                new ColumnReference(cRight.Name)),
            DataType.Double => new Eq(new ColumnReference(cLeft.Name),
                new ColumnReference(cRight.Name)),
            DataType.Int => new Eq(new ColumnReference(cLeft.Name),
                new ColumnReference(cRight.Name)),
            DataType.Long => new Eq(new ColumnReference(cLeft.Name),
                new ColumnReference(cRight.Name)),
            _ => throw new ArgumentException($"Column type {cLeft.Type} not supported")
        };
    }


    public override Relation Evaluate()
    {
        //Evaluate both children, then return a relation that contains all attributes referenced in On (or a natural join)
        var left = Left.Evaluate();
        var right = Right.Evaluate();
        //TODO
        //check if JoinMap is actually empty or null and if so build a list that resembles a NaturalJoin
        var predicate = BuildJoinPredicate(left, right, JoinMap);
        //build the resulting Schema after join
        //Aaaaand i am fucked.... predicate still only takes _one_ context, but i actually would have to supply two contexts lol
        
        //alright, es ist diverse Stunden spaeter und ich hab immer noch keine Loesung.
        //aktuell versuche ich im Kopf folgendes umzusetzen: 
        //Gebe ein gemeinsames Tuple an Predicate.Evaluate
        //Dafür muss aber das column mapping angepasst werden UND es müssten alle columns prefixed werden. 
        //Und nach dem prefixen müssen die mappings _nochmal_ angepasst werden. 
        
        /*
         * Der Ablauf wäre dann also:
         * 1) Evaluate left und right
         * 2) Erstelle das endgültige Schema post join via Left.Schema + Right.Schema
         * 3) Iterate über alle Columns in Left.Schema -> Prefixe Names mit "src."
         * 4) Iterate über alle Columns in Right.Schema -> Prefixe Names mit "dst."
         * 5) Iterate über alle items in JoinMap prefixe col_left mit "src." und col_right mit "dst."
         * 6) Bilde den Predicate Tree aus JoinMap
         * 7) Für jeden Tuple in Links:
         *      a) Iterate über jeden Tuple in Rechts:
         *          - Erstelle gemeinsamen Tuple indem rechts an links appended wird
         *          - Predicate.Evaluate(tuple) -> If True füge neuer Relation hinzu
         * 8) Vergleiche finales Schema aus 2) mit dem Schema der neuen Relation
         *      - baue tuple basierend auf dem finalen schema aus 2) und cherry pick objects aus den tuplen der neuen relation
         *      - nacheinander bauen und einfügen um duplikate zu vermeiden
         * 9) Gebe die mittlerweile 4. in-memory erstellte Relation zurück
         */
        
        /*
         * Honestly, da muss es einen _WESENTLICH_ besseren ansatz geben. Es kann nicht angehen, dass ich hier O(4*n*m) produzieren muss
         * und nicht mal die Funktionalität einer _normalen_ Datenbank abbilden kann. 
         */
        throw new NotImplementedException();
    }
}
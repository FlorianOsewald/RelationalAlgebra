using FullAot.DataItems;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.RelationalExpressions;

//Division ist etwas tough ohne dass ich eine GroupBy funktion implementiere. 
//By Konvention, Links ist der Zähler, Rechts der Nenner
public class Division : BinaryExpression
{
    public override Schema OutputSchema { get; }
    private readonly int[] _divisorIndices;
    private readonly int[] _dividentIndices;

    public Division(RelationalExpression left, RelationalExpression right) : base(left, right)
    {
        OutputSchema = left.OutputSchema.Except(right.OutputSchema);
        
        //die array-size entspricht der Anzahl der Columns im Outputschema des Nenners
        _dividentIndices = new int[right.OutputSchema.Count];
        var inDivisor = new HashSet<int>();
        //iteriere über alle Spalten aus dem Outputschema der rechten (Nenner) Relation
        for (int i = 0; i < right.OutputSchema.Count; i++)
        {
            //finde den Index der korrespondierenden Spalte im linken Schema 
            int index = left.OutputSchema.IndexOf(right.OutputSchema[i].Name);
            //falls die Typen unterschiedlich sind, werfe Exception. 
            if (left.OutputSchema[index].Type != right.OutputSchema[i].Type)
            {
                throw new InvalidOperationException("Type Mismatch. Cannot perform Division!");
            }
            //speichere den Lookup für später ab
            _dividentIndices[i] = index;
            inDivisor.Add(index);
        }
        _divisorIndices = new int[OutputSchema.Count];
        //iteriere jetzt über alle Spalten der linken (Zähler) Relation und speichere die Einträge, welche nicht zur rechten (Nenner) Relation gehören
        int divisorIndex = 0;
        for (int i = 0; i < left.OutputSchema.Count; i++)
        {
            //diese Spalte ist nicht in der Nenner Relation enthalten
            if (!inDivisor.Contains(i))
            {
                _divisorIndices[divisorIndex] = i;
                divisorIndex++; //zählen nicht vergessen
            }
        }

        if (divisorIndex == 0)
        {
            //Das heißt keine Spalte aus dem Zähler wurde sich gemerkt, es würde also eine Überdeckung vorliegen von Nenner und Zähler
            throw new InvalidOperationException("Output relation would not contain any columns!");
        }
    }

    public override Relation Evaluate()
    {
        //evaluate both children, then return a relation that only consists of tuples from left that contain all columns from right
        var left = Left.Evaluate();
        var right = Right.Evaluate();

        //Da ich keine GroupBy funktionalität habe, zähle ich die Anzahl der erfüllten 'Bedingungen'
        var hits = new Dictionary<Tuple, int>();
        foreach (var tuple in left)
        {
            //jetzt bauen wir zwei Tuple. Einen für die Daten die exklusiv in links sind und einen der in beiden liegt. 
            //das ganze würde mit Linq wesentlich einfacher sein, aber ich versuche hier explizit zu sein, so dass eine spätere 
            //version in MSIL auf der gleichen Basis aufsetzt. 
            var divisorTupleObjects = new Object[_divisorIndices.Length];
            var dividentTupleObjects = new Object[_dividentIndices.Length];

            //für jede gemerkte Spalte aus den DivisorIndices fügen wir aus dem aktuellen Tupel die entsprechenden Werte in unser Tmp Object[] hinzu
            for (int i = 0; i < _divisorIndices.Length; i++)
            {
                divisorTupleObjects[i] = tuple[_divisorIndices[i]];
            }
            //analog: für jede gemerkte Spalte aus DividentIndices fügen wir aus dem aktuellen Tupel die entsprechenden Werte in usner TMP Object[] hinzu
            for (int i = 0; i < _dividentIndices.Length; i++)
            {
                dividentTupleObjects[i] = tuple[_dividentIndices[i]];
            }
            //jetzt erstellen wir daraus 'echte' Tuple
            var divisorTuple = new Tuple(divisorTupleObjects);
            var dividentTuple = new Tuple(dividentTupleObjects);
            
            //prüfe ob der Wert des divisorTuple bereits im Hits-Dictionary enthalten ist
            hits.TryGetValue(divisorTuple, out var matches);
            
            //falls die rechte Relation den prüf tuple 'divident' enthält, dann können wir die Anzahl der gemerkten hits um eins erhöhen
            if (right.Contains(dividentTuple))
            {
                hits[divisorTuple] = matches + 1;
            }
            else
            {
                //der prüf tuple wurde _nicht_ in der Nenner relation gefunden, wir registrieren trotzdem den tuple, aber mit match-wert unverändert
                hits[divisorTuple] = matches;
            }
        }

        var retRelation = new Relation(OutputSchema);
        //Gehe final durch alle gemerkten Tuple durch und überprüfe die Anzahl der gemerkten erfüllten Bedingungen
        foreach (var (tuple, matches) in hits)
        {
            //wenn der Tuple genau so viele erfüllte Bedingungen hat wie der Nenner Spalten hat, dann ist dies ein erfüllender Tuple
            if (matches == right.Count)
            {
                retRelation.Insert(tuple);
            }
        }
        return retRelation;
    }
}
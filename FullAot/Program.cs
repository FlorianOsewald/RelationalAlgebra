using FullAot.DataItems;
using FullAot.Expressions.RelationalExpressions;
using FullAot.Expressions.ScalarExpressions;
using FullAot.Expressions.ScalarExpressions.Predicates;
using FullAot.FluentExtension;
using FullAot.Items;
using FullAot.Util;
using static FullAot.FluentExtension.ScExpExtensions;
using static FullAot.FluentExtension.RelExpExtensions;
using Tuple = FullAot.DataItems.Tuple;

Console.WriteLine("Hello, World!");

//Bsp: 
//Fluent-API Beispiele
var f_x = Lit(5).Eq(5);
f_x.Bind(null);
Console.WriteLine(f_x.Evaluate(null));

var f_y = Lit(5).Eq(5.0) .And(Lit(7.0).Gt(5.1));
f_y.Bind(null);
Console.WriteLine(f_y.Evaluate(null));

//kleiner test ob jetzt auch ints und doubles verglichen werden können was einer der erhofften benefits von meinem rewrite war
var f_z = Lit(5).Eq(5) .And(Lit(7.0).Gt(5));
f_z.Bind(null);
Console.WriteLine(f_z.Evaluate(null));

var f_add = new BinaryOp(Op.Add, 5, 6.1);
f_add.Bind(null);
Console.WriteLine(f_add.Evaluate(null));

//schema test
Console.WriteLine("Schema Test");
var cols = new List<Column>() {new Column("name", DataType.String), new Column("id", DataType.Long), new Column("age", DataType.Int)};
var cols1 = new List<Column>() {new Column("name", DataType.String), new Column("id", DataType.Long), new Column("age", DataType.Int)};
var cols2 = new List<Column>() {new Column("id", DataType.Long), new Column("age", DataType.Int), new Column("name", DataType.String)};

//cols und cols2 sind identisch vom content, aber nicht identisch in der Auspraegung. Die Frage ist nun, was ergibt ein Equal? 
var schema_1 = new Schema(cols);
var schema_2 = new Schema(cols2);
var schema_3 = new Schema(cols1);
Console.WriteLine(schema_1.Equals(schema_2));
Console.WriteLine(schema_1.Equals(schema_3));
//data test
var relation_1 = new Relation(schema_1);
for (int i = 0; i < 100; i++)
{
    var data = new object[3];
    data[0] = $"name_{i}";
    data[1] = i;
    data[2] = i*10;
    relation_1.Insert(data);
}
var rel_1 = new RelationReference(relation_1);
var q = rel_1.Where(Col("id").Gt(5));
var filtered = q.Evaluate();
Console.WriteLine("data test done");

//cart-prod test aus der Vorlesung
var cols_s = new List<Column>() {new Column("A", DataType.Int), new Column("B", DataType.Int)};
var cols_t = new List<Column>() {new Column("C", DataType.Int), new Column("D", DataType.Int)};
var schema_s = new Schema(cols_s);
var schema_t = new Schema(cols_t);
var relation_s = new Relation(schema_s);
var relation_t = new Relation(schema_t);
relation_s.Insert([1,3]);
relation_s.Insert([2,5]);
relation_t.Insert([7,2]);
relation_t.Insert([3,4]);
Rel(relation_s).X(relation_t).Project([("A","newA"), "B", (Col("C").Add(Col("D")), "C+D")]).Evaluate().Print();
Console.WriteLine("X test done");
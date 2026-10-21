using FullAot.DataItems;
using FullAot.Expressions.RelationalExpressions;
using FullAot.Expressions.ScalarExpressions;
using FullAot.Expressions.ScalarExpressions.Predicates;
using FullAot.FluentExtension;
using FullAot.Items;
using static FullAot.FluentExtension.ScExpExtensions;
using static FullAot.FluentExtension.RelExpExtensions;

Console.WriteLine("Hello, World!");

//Bsp: 
//Fluent-API Beispiele
var f_x = Lit(5).Eq(5);
f_x.Bind(null);
Console.WriteLine(f_x.Evaluate(null));

var f_y = Lit(5).Eq(5) .And(Lit(7.0).Gt(5.1));
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
schema_1.TryGetUnionMapping(schema_2, out var mapping);
foreach (var i in mapping)
{
    Console.WriteLine(i);
}
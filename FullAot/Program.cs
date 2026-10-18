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
var q = new Literal<int>(5);
Console.WriteLine(q.Evaluate(null));

//einfaches Predicate: Eq: 5, 5
var x = new Eq<int>(new Literal<int>(5), new Literal<int>(5));
Console.WriteLine(x.Evaluate(null));

//einfacher Predicate tree: AND, Eq (5,5), Gt(7.0,5.1)
var y = new And(new Eq<int>(new Literal<int>(5), new Literal<int>(5)), new Gt<double>(new Literal<double>(7.5f), new Literal<double>(5.0f), false));
Console.WriteLine(y.Evaluate(null));

Console.WriteLine("-=======-");

//Fluent-API Beispiele
var f_x = new Literal<int>(5).Eq(5);
Console.WriteLine(f_x.Evaluate(null));

var f_y = Lit(5).Eq(5)
    .And(Lit(7.0).Gt(5.1));

Console.WriteLine(f_y.Evaluate(null));


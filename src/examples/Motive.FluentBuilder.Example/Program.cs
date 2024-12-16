
using Motiv.Generator.Attributes;

Console.WriteLine("Hello World!");

var rectangle = Rectangle.WithWidth(10).WithHeight(20).Create();
var rectangleFromShape = Shape.WithWidth(10).WithHeight(20).Create();
var squareFromShape = Shape.WithWidth(10).Create();
var circleFromShape = Shape.WithRadius(5).Create();
var cuboidFromShape = Shape.WithWidth(10).WithHeight(20).WithDepth(30).Create();
// Entry point code here

[FluentFactory]
public partial class Shape;

[FluentFactory]
[FluentConstructor(typeof(Shape))]
public partial record Square([FluentMethod("WithWidth")]int Width);

[FluentFactory]
[FluentConstructor(typeof(Rectangle))]
[FluentConstructor(typeof(Shape))]
public partial record Rectangle([FluentMethod("WithWidth")]int Width, [FluentMethod("WithHeight")]int Height);

[FluentFactory]
[FluentConstructor(typeof(Shape))]
public partial record Circle([FluentMethod("WithRadius")]int Radius);

[FluentFactory]
[FluentConstructor(typeof(Shape))]
public partial record Cuboid([FluentMethod("WithWidth")]int Width, [FluentMethod("WithHeight")]int Height, [FluentMethod("WithDepth")]int Depth);

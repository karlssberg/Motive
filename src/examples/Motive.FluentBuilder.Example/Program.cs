
using Motiv.FluentBuilder.Example;
using Motiv.Generator.Attributes;

Console.WriteLine("Hello World!");

var rectangle = Shape.Width(10).Height(20).Create();
var square = Shape.Width(10).Create();
var circle = Shape.Radius(5).Create();
// Entry point code here

[GenerateFluentFactory("Motiv.FluentBuilder.Example.Shape")]
public record Square(int Width);

[GenerateFluentFactory("Motiv.FluentBuilder.Example.Shape")]
public record Rectangle(int Width, int Height);

[GenerateFluentFactory("Motiv.FluentBuilder.Example.Shape")]
public record Circle(int Radius);


using Motiv.Generator.Attributes;

namespace Motiv.FluentBuilder.Example;

// var rectangle = Shape.Width(10).Height(20).Create();
// var square = Shape.Width(10).Create();
// var circle = Shape.Radius(5).Create();

[GenerateFluentBuilder("Motiv.FluentBuilder.Example.Shape")]
public record Square(int Width);

[GenerateFluentBuilder("Motiv.FluentBuilder.Example.Shape")]
public record Rectangle(int Width, int Height);

[GenerateFluentBuilder("Motiv.FluentBuilder.Example.Shape")]
public record Circle(int Radius);

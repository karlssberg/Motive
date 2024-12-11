using System;

namespace Motiv.FluentBuilder.Example
{
    public static partial class Shape
    {
        public static Square Width(Int32 width)
        {
            return new Square(width);
        }

        public static Circle Radius(Int32 radius)
        {
            return new Circle(radius);
        }
    }
}
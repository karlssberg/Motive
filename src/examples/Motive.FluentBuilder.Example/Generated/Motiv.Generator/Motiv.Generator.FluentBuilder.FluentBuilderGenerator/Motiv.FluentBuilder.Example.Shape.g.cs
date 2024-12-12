using System;

namespace Motiv.FluentBuilder.Example
{
    public static partial class Shape
    {
        public static Step_0 Width(in int width)
        {
            return new Step_0(width);
        }

        public static Step_1 Radius(in int radius)
        {
            return new Step_1(radius);
        }
    }

    public struct Step_0
    {
        private readonly int _width__parameter;
        public Step_0(in int width)
        {
            _width__parameter = width;
        }

        public Square Create()
        {
            return new Square(_width__parameter);
        }

        public Step_2 Height(in int height)
        {
            return new Step_2(_width__parameter, height);
        }
    }

    public struct Step_1
    {
        private readonly int _radius__parameter;
        public Step_1(in int radius)
        {
            _radius__parameter = radius;
        }

        public Circle Create()
        {
            return new Circle(_radius__parameter);
        }
    }

    public struct Step_2
    {
        private readonly int _width__parameter;
        private readonly int _height__parameter;
        public Step_2(in int width, in int height)
        {
            _width__parameter = width;
            _height__parameter = height;
        }

        public Rectangle Create()
        {
            return new Rectangle(_width__parameter, _height__parameter);
        }
    }
}
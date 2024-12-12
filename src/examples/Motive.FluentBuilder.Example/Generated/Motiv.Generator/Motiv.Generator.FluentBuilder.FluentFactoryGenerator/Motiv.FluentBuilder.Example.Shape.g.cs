using System;

namespace Motiv.FluentBuilder.Example
{
    public static partial class Shape
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static Step_0 Width(in int width)
        {
            return new Step_0(width);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Square Create()
        {
            return new Square(_width__parameter);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_2 Height(in int height)
        {
            return new Step_2(_width__parameter, height);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_3 Height(in int height)
        {
            return new Step_3(_width__parameter, height);
        }
    }

    public struct Step_1
    {
        private readonly int _radius__parameter;
        public Step_1(in int radius)
        {
            _radius__parameter = radius;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Rectangle Create()
        {
            return new Rectangle(_width__parameter, _height__parameter);
        }
    }

    public struct Step_3
    {
        private readonly int _width__parameter;
        private readonly int _height__parameter;
        public Step_3(in int width, in int height)
        {
            _width__parameter = width;
            _height__parameter = height;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Step_4 Depth(in int depth)
        {
            return new Step_4(_width__parameter, _height__parameter, depth);
        }
    }

    public struct Step_4
    {
        private readonly int _width__parameter;
        private readonly int _height__parameter;
        private readonly int _depth__parameter;
        public Step_4(in int width, in int height, in int depth)
        {
            _width__parameter = width;
            _height__parameter = height;
            _depth__parameter = depth;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Cuboid Create()
        {
            return new Cuboid(_width__parameter, _height__parameter, _depth__parameter);
        }
    }
}
using System;

public partial class Shape
{
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static Step_0__Shape WithWidth(in int width)
    {
        return new Step_0__Shape(width);
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static Step_0__Shape WithRadius(in int radius)
    {
        return new Step_0__Shape(radius);
    }
}

public struct Step_0__Shape
{
    private readonly int _width__parameter;
    public Step_0__Shape(in int width)
    {
        _width__parameter = width;
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Square Create()
    {
        return new Square(_width__parameter);
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Step_1__Shape WithHeight(in int height)
    {
        return new Step_1__Shape(_width__parameter, height);
    }
}

public struct Step_1__Shape
{
    private readonly int _width__parameter;
    private readonly int _height__parameter;
    public Step_1__Shape(in int width, in int height)
    {
        _width__parameter = width;
        _height__parameter = height;
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Rectangle Create()
    {
        return new Rectangle(_width__parameter, _height__parameter);
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Step_2__Shape WithDepth(in int depth)
    {
        return new Step_2__Shape(_width__parameter, _height__parameter, depth);
    }
}

public struct Step_2__Shape
{
    private readonly int _width__parameter;
    private readonly int _height__parameter;
    private readonly int _depth__parameter;
    public Step_2__Shape(in int width, in int height, in int depth)
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
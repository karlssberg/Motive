using System;

public partial class Shape
{
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static Shape WithWidth(in int width)
    {
        return new Shape(width);
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static Shape WithRadius(in int radius)
    {
        return new Shape(radius);
    }
}

public partial class Shape
{
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static Shape WithWidth(in int width)
    {
        return new Shape(width);
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static Shape WithRadius(in int radius)
    {
        return new Shape(radius);
    }
}

public struct Shape
{
    private readonly int _width__parameter;
    public Step_0__Shape(in int width)
    {
        _width__parameter = width;
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Shape WithHeight(in int height)
    {
        return new Shape(_width__parameter, height);
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Square Create()
    {
        return new Square(_width__parameter);
    }
}

public struct Shape
{
    private readonly int _width__parameter;
    private readonly int _height__parameter;
    public Step_1__Shape(in int width, in int height)
    {
        _width__parameter = width;
        _height__parameter = height;
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Shape WithDepth(in int depth)
    {
        return new Shape(_width__parameter, _height__parameter, depth);
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Rectangle Create()
    {
        return new Rectangle(_width__parameter, _height__parameter);
    }
}

public struct Shape
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

public struct Shape
{
    private readonly int _radius__parameter;
    public Step_3__Shape(in int radius)
    {
        _radius__parameter = radius;
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Circle Create()
    {
        return new Circle(_radius__parameter);
    }
}
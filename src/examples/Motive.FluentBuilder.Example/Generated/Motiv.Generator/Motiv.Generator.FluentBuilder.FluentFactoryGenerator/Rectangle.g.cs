using System;

public partial record Rectangle
{
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static Rectangle WithWidth(in int width)
    {
        return new Rectangle(width);
    }
}

public partial record Rectangle
{
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static Rectangle WithWidth(in int width)
    {
        return new Rectangle(width);
    }
}

public struct Rectangle
{
    private readonly int _width__parameter;
    public Step_0__Rectangle(in int width)
    {
        _width__parameter = width;
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Rectangle WithHeight(in int height)
    {
        return new Rectangle(_width__parameter, height);
    }
}

public struct Rectangle
{
    private readonly int _width__parameter;
    private readonly int _height__parameter;
    public Step_1__Rectangle(in int width, in int height)
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
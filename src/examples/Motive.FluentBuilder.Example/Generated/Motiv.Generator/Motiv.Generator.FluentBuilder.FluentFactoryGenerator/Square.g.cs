using System;

public partial record Square
{
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static Square WithWidth(in int width)
    {
        return new Square(width);
    }
}

public partial record Square
{
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static Square WithWidth(in int width)
    {
        return new Square(width);
    }
}

public struct Square
{
    private readonly int _width__parameter;
    public Step_0__Square(in int width)
    {
        _width__parameter = width;
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Square Create()
    {
        return new Square(_width__parameter);
    }
}
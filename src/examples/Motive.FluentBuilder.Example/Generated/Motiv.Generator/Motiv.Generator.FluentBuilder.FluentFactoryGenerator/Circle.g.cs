using System;

public partial record Circle
{
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static Step_0__Circle WithRadius(in int radius)
    {
        return new Step_0__Circle(radius);
    }
}

public struct Step_0__Circle
{
    private readonly int _radius__parameter;
    public Step_0__Circle(in int radius)
    {
        _radius__parameter = radius;
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public Circle Create()
    {
        return new Circle(_radius__parameter);
    }
}
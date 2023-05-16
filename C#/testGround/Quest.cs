namespace testGround;

internal class Quest
{
    private readonly double _height;
    private readonly double _radius;

    public Quest(double height, double radius)
    {
        _height = height;
        _radius = radius;
    }

    public double Volume()
    {
        return Math.PI * (_radius * _radius) * _height;
    }

    public double Area()
    {
        return Math.PI * _radius * (_radius + _height);
    }
}
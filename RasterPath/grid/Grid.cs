#nullable enable

public interface Grid
{
    public int width {
        get;
    }
    public int height {
        get;
    }
    bool InBounds(Geo id);
    bool Passable(Geo id);
    double Cost(Geo a, Geo b);
}
using System;
using System.Collections.Generic;

#nullable enable
public class SquareGrid : WeightedGraph<Geo>, Grid
{
    // Implementation notes: I made the fields public for convenience,
    // but in a real project you'll probably want to follow standard
    // style and make them private.

    protected static readonly Geo[] DIRS = new[]
        {
            new Geo(1, 0),
            new Geo(0, -1),
            new Geo(-1, 0),
            new Geo(0, 1)
        };

    private readonly int _width, _height;
    public int width => _width;
    public int height => _height;
    
    public HashSet<Geo> walls = new HashSet<Geo>(); //accessed by printGraph
    public HashSet<Geo> forests = new HashSet<Geo>();

    public SquareGrid(int width, int height)
    {
        this._width = width;
        this._height = height;
    }

    //@override WeightedGraph
    public double Cost(Geo a, Geo b)
    {
        return forests.Contains(b) ? 5 : 1;
    }

    //@override WeightedGraph
    public IEnumerable<Geo> Neighbors(Geo id)
    {
        foreach (var dir in DIRS)
        {
            Geo next = new Geo(id.x + dir.x, id.y + dir.y);
            if (InBounds(next) && Passable(next))
            {
                yield return next;
            }
        }
    }


    public bool InBounds(Geo id)
    {
        return 0 <= id.x && id.x < _width
            && 0 <= id.y && id.y < _height;
    }

    public bool Passable(Geo id)
    {
        return !walls.Contains(id);
    }
}
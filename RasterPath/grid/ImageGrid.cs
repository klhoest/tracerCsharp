using System;
using System.Collections.Generic;

#nullable enable

public class ImageGrid : WeightedGraph<Geo>, Grid
{
    private readonly int _width, _height;
    public int width => _width;
    public int height => _height;
    private static readonly Geo[] DIRS = new[]
        {
            new Geo(1, 0),
            new Geo(0, -1),
            new Geo(-1, 0),
            new Geo(0, 1)
        };
    public readonly BitmapModel bitmap;
    private readonly CoefficientCost coef;

    public ImageGrid(BitmapModel bitmap, CoefficientCost coefficientCost)
    {
        this.bitmap = bitmap;
        this.coef = coefficientCost;
        this._width = bitmap.width;
        this._height = bitmap.height;
    }

    //@override WeightedGraph
    public double Cost(Geo a, Geo b)
    {
        double pixelColor = bitmap[b.x, b.y];
        return pixelColor*pixelColor*coef.color;
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
        return 0 <= id.x && id.x < bitmap.width
            && 0 <= id.y && id.y < bitmap.height;
    }

    public bool Passable(Geo id)
    {
        return true;
    }
}
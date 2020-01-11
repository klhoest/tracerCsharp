using System;
using System.Collections.Generic;


// A* needs only a WeightedGraph and a location type L, and does *not*
// have to be a grid. However, in the example code I am using a grid.
public interface WeightedGraph<L>
{
    double Cost(Geo a, Geo b);
    IEnumerable<Geo> Neighbors(Geo id);
}
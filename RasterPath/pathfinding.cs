using System;
using System.Collections.Generic;

#nullable enable

/* NOTE about types: in the main article, in the Python code I just
 * use numbers for costs, heuristics, and priorities. In the C++ code
 * I use a typedef for this, because you might want int or double or
 * another type. In this C# code I use double for costs, heuristics,
 * and priorities. You can use an int if you know your values are
 * always integers, and you can use a smaller size number if you know
 * the values are always small. */

public class AStarSearch
{
    
    protected readonly WeightedGraph<Geo> graph;
    protected readonly Geo start;

    public Dictionary<Geo, Geo> cameFrom
        = new Dictionary<Geo, Geo>();
    protected Dictionary<Geo, double> costSoFar
        = new Dictionary<Geo, double>();
    private PriorityQueue<Geo> frontier = new PriorityQueue<Geo>();

    // Note: a generic version of A* would abstract over Location and
    // also Heuristic
    static public double Heuristic(Geo a, Geo b)
    {
        return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
    }

    public AStarSearch(
     WeightedGraph<Geo> graph,
      Geo start)
    {
        this.graph = graph;
        this.start = start;
        
        cameFrom[start] = start;
        costSoFar[start] = 0;
        frontier.Enqueue(start, 0);
    }

    //the loop will restart exactly where it stopped after an early exit
    public void search(Geo goal)
    {
        while (frontier.Count > 0)
        {
            Geo current = frontier.Dequeue();

            if (current.Equals(goal)) return;

            foreach (Geo next in graph.Neighbors(current))
            {
                double newCost = costSoFar[current]
                    + graph.Cost(current, next);
                if (!costSoFar.ContainsKey(next)
                    || newCost < costSoFar[next])
                {
                    costSoFar[next] = newCost;
                    double priority = newCost + Heuristic(next, goal);
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                }
            }
        }
    }

    //<param name="goal"> must not be null
    public LinkedList<Geo> getFullPathTo(Geo goal) {
        Geo parent = default(Geo);
        if (cameFrom.TryGetValue(goal, out parent) == false) {
            search(goal); //if the node has not parent it has not been found yet
        }
        return recoverFullPathTo(goal);
    }

    private LinkedList<Geo> recoverFullPathTo(Geo goal) {
        LinkedList<Geo> result = new LinkedList<Geo>();
        Geo current = goal;
        do {
            result.AddLast(current);
            Geo parent = default(Geo);
            if (cameFrom.TryGetValue(current, out parent) == false)
                throw new AccessViolationException();
            current = parent;
        } while(start.Equals(current) == false);
        return result;
    }

    //private int getDeltaAngle()
}
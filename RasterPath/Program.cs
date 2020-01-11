using System;
using System.Collections.Generic;

#nullable enable

public struct Geo
{
    // Implementation notes: I am using the default Equals but it can
    // be slow. You'll probably want to override both Equals and
    // GetHashCode in a real project.

    public readonly int x, y;
    public Geo(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return "(" + x + ":" + y + ") ";
    }
}

public class Test
{
    //obsolete
    static void DrawGrid(Grid grid, AStarSearch astar)
    {
        // Print out the cameFrom array
        for (var y = 0; y < 10; y++)
        {
            for (var x = 0; x < 10; x++)
            {
                Geo id = new Geo(x, y);
                Geo ptr = id;
                if (!astar.cameFrom.TryGetValue(id, out ptr))
                {
                    ptr = id;
                }
                if (grid.Passable(id) == false) { Console.Write("##"); }
                /*else if (ptr.x == x+1) { Console.Write("\u2192 "); }
                else if (ptr.x == x-1) { Console.Write("\u2190 "); }
                else if (ptr.y == y+1) { Console.Write("\u2193 "); }
                else if (ptr.y == y-1) { Console.Write("\u2191 "); }*/
                else if (ptr.x == x + 1) { Console.Write("> "); }
                else if (ptr.x == x - 1) { Console.Write("< "); }
                else if (ptr.y == y + 1) { Console.Write("! "); }
                else if (ptr.y == y - 1) { Console.Write("^ "); }
                else { Console.Write("* "); }
            }
            Console.WriteLine();
        }
    }

    static void DrawGrid(ImageGrid grid, AStarSearch astar, RequestInput requestInput)
    {
        // Print out the cameFrom array
        for (var y = 0; y < grid.height; y++)
        {
            for (var x = 0; x < grid.width; x++)
            {
                Geo id = new Geo(x, y);
                Geo ptr = id;
                if (!astar.cameFrom.TryGetValue(id, out ptr))
                {
                    ptr = id;
                }
                if (requestInput.start.Equals(id)) { Console.Write("S "); }
                else if (requestInput.finish.Equals(id)) { Console.Write("F "); }
                else if (grid.bitmap[x, y] > 128) { Console.Write("##"); }
                /*else if (ptr.x == x+1) { Console.Write("\u2192 "); }
                else if (ptr.x == x-1) { Console.Write("\u2190 "); }
                else if (ptr.y == y+1) { Console.Write("\u2193 "); }
                else if (ptr.y == y-1) { Console.Write("\u2191 "); }*/
                else if (ptr.x == x + 1) { Console.Write("->"); }
                else if (ptr.x == x - 1) { Console.Write("<-"); }
                else if (ptr.y == y + 1) { Console.Write("! "); }
                else if (ptr.y == y - 1) { Console.Write("^ "); }
                else { Console.Write("* "); }
            }
            Console.WriteLine();
        }
    }



    static void Main()
    {
        RequestInput requestInput;
        try {
            requestInput = new RequestInput();
        } catch (Exception e) { //image can fail to load
            Console.WriteLine(e.Message);
            return;
        }
        var bitmapModel = BitmapModelAdapter.getBitmapModel(requestInput.image);
        var grid = new ImageGrid(bitmapModel, requestInput.coefficientCost);
        var astar = new AStarSearch(grid, requestInput.start);
        astar.search(requestInput.finish);
        var pathList = astar.getFullPathTo(requestInput.finish);

        /*DrawGrid(grid, astar, requestInput);
        foreach (var item in pathList) {
            Console.Write(item);
        }*/
        FileOutput fileOutput = new FileOutput(pathList, bitmapModel);
        
    }

}
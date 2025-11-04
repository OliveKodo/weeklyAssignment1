using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'printShortestPath' function below.
     *
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER i_start
     *  3. INTEGER j_start
     *  4. INTEGER i_end
     *  5. INTEGER j_end
     */

    public static void printShortestPath(int n, int i_start, int j_start, int i_end, int j_end)
    {
    // Print the distance along with the sequence of moves.
 // Early exit: start == end
        if (i_start == i_end && j_start == j_end)
        {
            Console.WriteLine(0);
            Console.WriteLine();
            return;
        }

        // Move deltas and names in required priority
        var moves = new (int dr, int dc, string name)[]
        {
            (-2, -1, "UL"),
            (-2, +1, "UR"),
            ( 0, +2, "R"),
            (+2, +1, "LR"),
            (+2, -1, "LL"),
            ( 0, -2, "L")
        };

        bool InBounds(int r, int c) => r >= 0 && r < n && c >= 0 && c < n;

        var visited = new bool[n, n];
        // Store previous cell and the move name used to get here
        var prev = new (int pr, int pc, string move)?[n, n];

        var q = new Queue<(int r, int c)>();
        q.Enqueue((i_start, j_start));
        visited[i_start, j_start] = true;

        bool found = false;
        while (q.Count > 0 && !found)
        {
            var (r, c) = q.Dequeue();

            foreach (var (dr, dc, name) in moves)
            {
                int nr = r + dr, nc = c + dc;
                if (!InBounds(nr, nc) || visited[nr, nc]) continue;

                visited[nr, nc] = true;
                prev[nr, nc] = (r, c, name);
                if (nr == i_end && nc == j_end)
                {
                    found = true;
                    break;
                }
                q.Enqueue((nr, nc));
            }
        }

        if (!found)
        {
            Console.WriteLine("Impossible");
            return;
        }

        // Reconstruct path from end to start
        var path = new List<string>();
        int cr = i_end, cc = j_end;
        while (!(cr == i_start && cc == j_start))
        {
            var p = prev[cr, cc];
            if (!p.HasValue) break; // safety
            var (pr, pc, moveName) = p.Value;
            path.Add(moveName);
            cr = pr; cc = pc;
        }
        path.Reverse();

        Console.WriteLine(path.Count);
        Console.WriteLine(string.Join(" ", path));
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int i_start = Convert.ToInt32(firstMultipleInput[0]);

        int j_start = Convert.ToInt32(firstMultipleInput[1]);

        int i_end = Convert.ToInt32(firstMultipleInput[2]);

        int j_end = Convert.ToInt32(firstMultipleInput[3]);

        Result.printShortestPath(n, i_start, j_start, i_end, j_end);
    }
}

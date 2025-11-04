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
     * Complete the 'queensAttack' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER k
     *  3. INTEGER r_q
     *  4. INTEGER c_q
     *  5. 2D_INTEGER_ARRAY obstacles
     */

    public static int queensAttack(int n, int k, int r_q, int c_q, List<List<int>> obstacles)
    {
        // Distances to board edges in all 8 directions
    int up        = n - r_q;
    int down      = r_q - 1;
    int right     = n - c_q;
    int left      = c_q - 1;
    int upRight   = Math.Min(n - r_q, n - c_q);
    int upLeft    = Math.Min(n - r_q, c_q - 1);
    int downRight = Math.Min(r_q - 1, n - c_q);
    int downLeft  = Math.Min(r_q - 1, c_q - 1);

    foreach (var ob in obstacles)
    {
        int r = ob[0];
        int c = ob[1];

        // Same column
        if (c == c_q)
        {
            if (r > r_q) up = Math.Min(up, r - r_q - 1);
            else if (r < r_q) down = Math.Min(down, r_q - r - 1);
        }
        // Same row
        else if (r == r_q)
        {
            if (c > c_q) right = Math.Min(right, c - c_q - 1);
            else if (c < c_q) left = Math.Min(left, c_q - c - 1);
        }
        // Diagonals
        else if (Math.Abs(r - r_q) == Math.Abs(c - c_q))
        {
            if (r > r_q && c > c_q)       upRight = Math.Min(upRight, r - r_q - 1);
            else if (r > r_q && c < c_q)  upLeft  = Math.Min(upLeft,  r - r_q - 1);
            else if (r < r_q && c > c_q)  downRight = Math.Min(downRight, r_q - r - 1);
            else if (r < r_q && c < c_q)  downLeft  = Math.Min(downLeft,  r_q - r - 1);
        }
    }

    return up + down + left + right + upLeft + upRight + downLeft + downRight;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int k = Convert.ToInt32(firstMultipleInput[1]);

        string[] secondMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r_q = Convert.ToInt32(secondMultipleInput[0]);

        int c_q = Convert.ToInt32(secondMultipleInput[1]);

        List<List<int>> obstacles = new List<List<int>>();

        for (int i = 0; i < k; i++)
        {
            obstacles.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(obstaclesTemp => Convert.ToInt32(obstaclesTemp)).ToList());
        }

        int result = Result.queensAttack(n, k, r_q, c_q, obstacles);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}

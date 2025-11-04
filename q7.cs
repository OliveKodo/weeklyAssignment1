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
     * Complete the 'countLuck' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts following parameters:
     *  1. STRING_ARRAY matrix
     *  2. INTEGER k
     */

    public static string countLuck(List<string> matrix, int k)
    {
   int n = matrix.Count;
        int m = matrix[0].Length;

        // Convert to char grid for easy indexing
        char[][] grid = new char[n][];
        for (int i = 0; i < n; i++) grid[i] = matrix[i].ToCharArray();

        // Find start (M)
        int sr = -1, sc = -1;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (grid[i][j] == 'M')
                {
                    sr = i; sc = j;
                    break;
                }
            }
            if (sr != -1) break;
        }

        bool[,] visited = new bool[n, m];

        int? decisions = Dfs(sr, sc);

        // Compare with k
        return (decisions.HasValue && decisions.Value == k) ? "Impressed" : "Oops!";

        // Local functions

        bool InBounds(int r, int c) => r >= 0 && r < n && c >= 0 && c < m;

        bool Passable(int r, int c) => grid[r][c] != 'X';

        int? Dfs(int r, int c)
        {
            if (!InBounds(r, c) || visited[r, c] || !Passable(r, c))
                return null;

            if (grid[r][c] == '*')
                return 0; // Reached target: zero more decisions from here

            visited[r, c] = true;

            // Collect valid next moves
            var nexts = new List<(int r, int c)>();
            int[] dr = { -1, 1, 0, 0 };
            int[] dc = { 0, 0, -1, 1 };

            for (int kdir = 0; kdir < 4; kdir++)
            {
                int nr = r + dr[kdir], nc = c + dc[kdir];
                if (InBounds(nr, nc) && !visited[nr, nc] && Passable(nr, nc))
                    nexts.Add((nr, nc));
            }

            // Decision point if more than one path forward
            int add = (nexts.Count > 1) ? 1 : 0;

            foreach (var (nr, nc) in nexts)
            {
                int? child = Dfs(nr, nc);
                if (child.HasValue)
                {
                    return add + child.Value;
                }
            }

            // No path from here (shouldn't happen given unique path guarantee, but safe)
            return null;
        }
    
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine().Trim());

        for (int tItr = 0; tItr < t; tItr++)
        {
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(firstMultipleInput[0]);

            int m = Convert.ToInt32(firstMultipleInput[1]);

            List<string> matrix = new List<string>();

            for (int i = 0; i < n; i++)
            {
                string matrixItem = Console.ReadLine();
                matrix.Add(matrixItem);
            }

            int k = Convert.ToInt32(Console.ReadLine().Trim());

            string result = Result.countLuck(matrix, k);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}

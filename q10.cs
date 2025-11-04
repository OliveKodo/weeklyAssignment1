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
     * Complete the 'surfaceArea' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY A as parameter.
     */

    public static int surfaceArea(List<List<int>> A)
    {
 int H = A.Count;
        if (H == 0) return 0;
        int W = A[0].Count;

        long area = 0; // use long to be safe during accumulation

        // Directions: up, down, left, right
        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        for (int r = 0; r < H; r++)
        {
            for (int c = 0; c < W; c++)
            {
                int h = A[r][c];
                if (h <= 0) continue;

                // Top and bottom faces
                area += 2;

                // Side faces toward each neighbor
                for (int d = 0; d < 4; d++)
                {
                    int nr = r + dr[d];
                    int nc = c + dc[d];

                    int neighbor = 0;
                    if (nr >= 0 && nr < H && nc >= 0 && nc < W)
                        neighbor = A[nr][nc];

                    int exposed = h - neighbor;
                    if (exposed > 0) area += exposed;
                }
            }
        }

        return (int)area;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int H = Convert.ToInt32(firstMultipleInput[0]);

        int W = Convert.ToInt32(firstMultipleInput[1]);

        List<List<int>> A = new List<List<int>>();

        for (int i = 0; i < H; i++)
        {
            A.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(ATemp => Convert.ToInt32(ATemp)).ToList());
        }

        int result = Result.surfaceArea(A);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}

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

class Solution {

    // Complete the getMinimumCost function below.
    static int getMinimumCost(int k, int[] c) {
// Sort the flower costs in ascending order
        Array.Sort(c);

        long total = 0;     // use long to avoid overflow during accumulation
        int n = c.Length;
        int idx = 0;        // number of purchased flowers so far (0-based)

        // Iterate from the most expensive flower to the cheapest
        for (int i = n - 1; i >= 0; i--, idx++) {
            // Each group of k purchases increases the multiplier by 1
            int multiplier = (idx / k) + 1;
            total += multiplier * (long)c[i];
        }

        return (int)total;

    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nk = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nk[0]);

        int k = Convert.ToInt32(nk[1]);

        int[] c = Array.ConvertAll(Console.ReadLine().Split(' '), cTemp => Convert.ToInt32(cTemp))
        ;
        int minimumCost = getMinimumCost(k, c);

        textWriter.WriteLine(minimumCost);

        textWriter.Flush();
        textWriter.Close();
    }
}

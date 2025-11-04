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
     * Complete the 'biggerIsGreater' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING w as parameter.
     */

    public static string biggerIsGreater(string w)
    {
char[] a = w.ToCharArray();
    int n = a.Length;

    // 1) find pivot: rightmost i with a[i] < a[i+1]
    int i = n - 2;
    while (i >= 0 && a[i] >= a[i + 1]) i--;
    if (i < 0) return "no answer"; // already the highest permutation

    // 2) find rightmost j > i with a[j] > a[i]
    int j = n - 1;
    while (a[j] <= a[i]) j--;

    // 3) swap pivot and successor
    char tmp = a[i]; a[i] = a[j]; a[j] = tmp;

    // 4) reverse the suffix to get the minimal larger permutation
    Array.Reverse(a, i + 1, n - (i + 1));

    return new string(a);
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int T = Convert.ToInt32(Console.ReadLine().Trim());

        for (int TItr = 0; TItr < T; TItr++)
        {
            string w = Console.ReadLine();

            string result = Result.biggerIsGreater(w);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}

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
     * Complete the 'encryption' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     */

    public static string encryption(string s)
    {
 // 1) remove spaces
    string t = new string(s.Where(ch => ch != ' ').ToArray());
    int L = t.Length;

    // 2) choose rows and cols
    int rows = (int)Math.Floor(Math.Sqrt(L));
    int cols = (int)Math.Ceiling(Math.Sqrt(L));
    if (rows * cols < L) rows++; // ensure grid fits all chars (and rows <= cols)

    // 3) read column-wise and join with spaces
    var parts = new List<string>();
    for (int c = 0; c < cols; c++)
    {
        var sb = new StringBuilder();
        for (int r = 0; r < rows; r++)
        {
            int idx = r * cols + c;
            if (idx < L) sb.Append(t[idx]);
        }
        parts.Add(sb.ToString());
    }

    return string.Join(" ", parts);
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string result = Result.encryption(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}

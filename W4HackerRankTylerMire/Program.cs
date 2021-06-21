using System;

class Result
{
    public static void staircase(int n)
    {
        for(int i = 0; i <= n; i++)
        {
            string stairStep = new String(' ', (n - i)) + new String('#', i);

            Console.WriteLine(stairStep);
        }
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        Result.staircase(n);
    }
}
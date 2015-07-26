using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    public class Letter
    {
        private char[] sb;

        public Letter(int width, int height)
        {
            sb = new char[width * height + height];
        }

        public int Value { get; set; }

        public char[] Container { get { return sb; } }

        public static int Width { get; set; }
        public static int Height { get; set; }

        public void SetRow(int row, char[] arr)
        {
            if (arr.Length > Width)
                return;
            Array.Copy(arr, 0, sb, row * Width, Height);
            arr[row * Width + Height + 1] = '\n';
        }

        public bool Equals(Letter letter)
        {
            return sb.SequenceEqual(letter.Container);
        }

        public override string ToString()
        {

            return new string(sb);
        }
    }

    public class Number
    {
        private List<Letter> container = new List<Letter>();
        private static Letter[] letters = new Letter[20];

        public Number() { }

        public Number(long value)
        {
            Value = value;
        }

        public static int Width { get; set; }
        public static int Height { get; set; }

        public List<Letter> Letters { get; }
        public long Value { get; set; }

        public static Number Create(IList<Letter> arr)
        {
            if (arr == null || arr.Count == 0)
                return null;
            Number nr = new Number();
            int currentNumber = 0;
            long number = 0;
            nr.Letters.AddRange(arr);
            for (int i = arr.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < letters.Length; j++)
                    if (letters[j].Equals(arr[i]))
                    {

                    }
            }
            return nr;
        }


        public static Number operator +(Number n1, Number n2)
        {
            return new Number(n1.Value + n2.Value);
        }

        public static Number operator -(Number n1, Number n2)
        {
            return new Number(n1.Value - n2.Value);
        }

        public static Number operator *(Number n1, Number n2)
        {
            return new Number(n1.Value * n2.Value);
        }

        public static Number operator /(Number n1, Number n2)
        {
            return new Number(n1.Value / n2.Value);
        }



        public override string ToString()
        {
            return base.ToString();
        }
    }


    static void Main(string[] args)
    {
        string[] inputs = Console.ReadLine().Split(' ');
        int L = int.Parse(inputs[0]);
        int H = int.Parse(inputs[1]);
        for (int i = 0; i < H; i++)
        {
            string numeral = Console.ReadLine();
        }
        int S1 = int.Parse(Console.ReadLine());
        for (int i = 0; i < S1; i++)
        {
            string num1Line = Console.ReadLine();
        }
        int S2 = int.Parse(Console.ReadLine());
        for (int i = 0; i < S2; i++)
        {
            string num2Line = Console.ReadLine();
        }
        string operation = Console.ReadLine();

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine("result");
    }
}
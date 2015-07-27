using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    public class Letter
    {
        private char[] sb;

        public Letter()
        {
            sb = new char[Width * Height + Height];
        }

        public int Value { get; set; }

        public char[] Container { get { return sb; } }

        public static int Width { get; set; }
        public static int Height { get; set; }

        public void SetRow(int row, char[] arr)
        {
            if (arr.Length > Width)
                return;
            Array.Copy(arr, 0, sb, row * (Width + 1), Width);
            sb[row * (Width + 1) + Width] = '\n';
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
        private long _value;
        private static Letter[] letters = new Letter[20];
        private static int width;
        private static int height;
        public Number() { }

        public Number(long value)
        {
            Value = value;
        }

        public static int Width
        {
            get { return width; }
            set { width = value; Letter.Width = value; }
        }

        public static int Height
        {
            get { return height; }
            set { height = value; Letter.Height = value; }
        }


        public static Letter[] AllLetters { get { return letters; } }

        public List<Letter> Letters { get { return container; } }
        public long Value
        {
            get { return _value; }
            set
            {
                _value = value;
                CalculateValue();
            }
        }

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
                        number += letters[j].Value * (long)Math.Pow(20, currentNumber++);
                        break;
                    }
            }
            nr.Value = number;
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
            StringBuilder sb = new StringBuilder();
            foreach (Letter l in container)
                sb.Append(l.ToString());
            return sb.ToString();
        }

        private void CalculateValue()
        {
            long current = Value;
            container.Clear();
            while (current > 0)
            {
                container.Add(AllLetters[current % 20]);
                current /= 20;
            }
            container.Reverse();
        }
    }

    static Number ReadNumber(int n)
    {
        var arr = new List<Letter>();
        var letter = new Letter();
        for (int i = 0; i < n; i++)
        {
            if (i != 0 && i % Number.Height == 0)
            {
                arr.Add(letter);
                letter = new Letter();
            }
            string num1Line = Console.ReadLine();
            letter.SetRow(i % Number.Height, num1Line.ToArray());
        }
        arr.Add(letter);
        return Number.Create(arr);
    }

    static Number ReadNumberDebug(int n, string[] lines, ref int offset)
    {
        var arr = new List<Letter>();
        var letter = new Letter();
        for (int i = 0; i < n; i++)
        {
            if (i != 0 && i % Number.Height == 0)
            {
                arr.Add(letter);
                letter = new Letter();
            }
            string num1Line = lines[offset++];
            letter.SetRow(i % Number.Height, num1Line.ToArray());
        }
        arr.Add(letter);
        return Number.Create(arr);
    }

    public static void debug()
    {
        var lines = File.ReadAllLines("file.txt");
        string[] inputs = lines[0].Split(' ');
        int L = int.Parse(inputs[0]);
        int H = int.Parse(inputs[1]);
        int offset = 1;
        Number n1 = null, n2 = null, res = null;
        Number.Width = L;
        Number.Height = H;
        for (int i = 0; i < 20; i++)
            Number.AllLetters[i] = new Letter() { Value = i };
        for (int i = 0; i < H; i++)
        {
            string numeral = lines[offset++];
            for (int j = 0; j < 20; j++)
                Number.AllLetters[j].SetRow(i, numeral.Substring(j * L, L).ToArray());
        }

        n1 = ReadNumberDebug(int.Parse(lines[offset++]), lines, ref offset);
        n2 = ReadNumberDebug(int.Parse(lines[offset++]), lines, ref offset);
        string operation = lines[offset];

        switch (operation)
        {
            case "*":
                res = n1 * n2;
                break;
            case "/":
                res = n1 / n2;
                break;
            case "+":
                res = n1 + n2;
                break;
            case "-":
                res = n1 + n2;
                break;
        }
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(res.ToString());
    }

    public static void codingame()
    {
        string[] inputs = Console.ReadLine().Split(' ');
        int L = int.Parse(inputs[0]);
        int H = int.Parse(inputs[1]);
        Number n1 = null, n2 = null, res = null;
        Number.Width = L;
        Number.Height = H;
        for (int i = 0; i < 20; i++)
            Number.AllLetters[i] = new Letter() { Value = i };

        for (int i = 0; i < H; i++)
        {
            string numeral = Console.ReadLine();
            for (int j = 0; j < 20; j++)
                Number.AllLetters[j].SetRow(i, numeral.Substring(j * L, L).ToArray());
        }

        n1 = ReadNumber(int.Parse(Console.ReadLine()));
        n2 = ReadNumber(int.Parse(Console.ReadLine()));

        string operation = Console.ReadLine();

        switch (operation)
        {
            case "*":
                res = n1 * n2;
                break;
            case "/":
                res = n1 / n2;
                break;
            case "+":
                res = n1 + n2;
                break;
            case "-":
                res = n1 + n2;
                break;
        }
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(res.ToString());
    }

    static void Main(string[] args)
    {
        debug();
    }
}
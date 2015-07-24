using System;
using System.Text;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    class Letter
    {
        private char[] sb;

        public Letter(int width, int height)
        {
            sb = new char[width * height];
            Width = width;
            Height = height;
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public string LetterString { get { return ToString(); } }

        public string GetRow(int index)
        {
            return index >= 0 && index < Height ? new string(sb, index * Width, Width) : "";
        }

        public char this[int height, int width]
        {
            set
            {
                if (height >= 0 && height < Height || width >= 0 && width < Width)
                    sb[height * Width + width] = value;
            }
        }

        public override string ToString()
        {
            char[] arr = new char[Width * Height + Height];
            int offsetsrc = 0, offsetdest = 0;
            for (int i = 0; i < Height; i++)
            {
                Array.Copy(sb, offsetsrc, arr, offsetdest, Width);
                offsetsrc += Width;
                offsetdest += (Width + 1);
                arr[offsetdest - 1] = '\n';
            }

            return new string(arr);
        }
    }

    class Storage
    {
        Letter[] container = new Letter[27];

        public Storage(int width, int height)
        {
            Width = width;
            Height = height;
            for (int i = 0; i < 27; i++)
                container[i] = new Letter(width, height);
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public string GetValue(string str)
        {
            int index = 0;
            if (str.Length == 1)
            {
                index = str.ToUpper()[0] - 65;
                return index >= 0 && index < 26 ?
                    container[index].ToString() :
                    container[26].ToString();
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Height; i++)
            {
                foreach (char c in str.ToUpper())
                {
                    index = c - 65;
                    sb.Append(index >= 0 && index < 26 ?
                        container[index].GetRow(i) :
                        container[26].GetRow(i));
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }

        public Letter this[int index]
        {
            get
            {
                return (index >= 0 && index < 27) ? container[index] : null;
            }
            set
            {
                if (index >= 0 && index < 27)
                    container[index] = value;
            }
        }
    }

    static void Main(string[] args)
    {
        int L = int.Parse(Console.ReadLine());
        int H = int.Parse(Console.ReadLine());
        int RL = L * 27;
        string T = Console.ReadLine();
        Storage storage = new Storage(L, H);
        for (int i = 0; i < H; i++)
        {
            string ROW = Console.ReadLine();
            int offset = 0;
            if (ROW.Length == RL)
                for (int j = 0; j < 27; j++)
                {
                    string str = ROW.Substring(offset, L);
                    offset += L;
                    for (int ii = 0; ii < str.Length; ii++)
                        storage[j][i, ii] = str[ii];
                }
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");      
        Console.WriteLine(storage.GetValue(T));
    }
}
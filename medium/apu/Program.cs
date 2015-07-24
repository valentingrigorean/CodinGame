using System;
using System.Collections.Generic;

/**
 * Don't let the machines win. You are humanity's last hope...
 **/
class Player
{
    class Pair
    {
        public Pair()
        {
            X = -1;
            Y = -1;
        }

        public int X { get; set; }
        public int Y { get; set; }


        public override string ToString()
        {
            return string.Format("{0} {1}", X, Y);
        }
    }

    class Node
    {
        public Node()
        {
            Right = new Pair();
            Bottom = new Pair();
        }

        public Pair Self { get; set; }
        public Pair Right { get; set; }
        public Pair Bottom { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Self, Right, Bottom);
        }
    }



    static void Main(string[] args)
    {
        int width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
        int height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
        var vars = new bool[height, width];
        var dict = new Dictionary<int, IList<Node>>();

        for (int i = 0; i < height; i++)
        {
            string line = Console.ReadLine();
            var list = new List<Node>();
            Node prev = null;
            for (int ii = 0; ii < line.Length; ii++)
                if (line[ii] == '0')
                {
                    Node curr = new Node() { Self = new Pair() { X = ii, Y = i } };
                    vars[i, ii] = true;
                    if (prev != null)
                        prev.Right = curr.Self;
                    prev = curr;
                    list.Add(curr);
                    if (i > 0)
                    {
                        for (int j = i - 1; j >= 0; j--)
                            if (vars[j, ii])
                            {
                                foreach (Node n in dict[j])
                                    if (n.Self.X == ii)
                                    {
                                        n.Bottom = curr.Self;
                                        break;
                                    }
                                break;
                            }
                    }
                }
            if (list.Count > 0)
                dict.Add(i, list);
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");
        foreach (var pair in dict)
            foreach (var p in pair.Value)
                Console.WriteLine(p.ToString());

    }
}
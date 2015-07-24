using System;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static Node root = new Node();

    public class Node
    {
        private List<Node> nodes = new List<Node>();
        public Node()
        {
        }
        public Node(short number)
        {
            Number = number;
        }
        public short Number { get; private set; }
        public void AddNode(Node node)
        {
            nodes.Add(node);
        }
        public Node Contains(short number)
        {
            foreach (var n in nodes)
                if (n.Number == number)
                    return n;
            return null;
        }
        public string Name { get; set; }
        public int Count
        {
            get
            {
                int count = 1;
                foreach (Node n in nodes)
                    count += n.Count;
                return count;
            }
        }
    }

    static public void AddNode(string number)
    {
        Node curr = root;
        foreach (char c in number)
        {
            short n = (short)Char.GetNumericValue(c);
            if (curr.Contains(n) == null)
            {
                Node nod = new Node(n);
                curr.AddNode(nod);
                curr = nod;
                continue;
            }
            curr = curr.Contains(n);
        }
    }

    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; i++)
        {
            string telephone = Console.ReadLine();
            AddNode(telephone);
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(root.Count - 1); // The number of elements (referencing a number) stored in the structure.
    }
}
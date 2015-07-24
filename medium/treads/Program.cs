using System;
using System.Collections.Generic;
using System.Linq;

public static class DictionarHelper
{
    public static void CreateIfNotExist(this IDictionary<int, Solution.Node> dict, int key, ref Solution.Node value)
    {
        if (dict.ContainsKey(key))
        {
            value = dict[key];
            return;
        }
        value = new Solution.Node(key);
        dict.Add(key, value);
    }
}


public class Solution
{


    public class Node
    {
        private LinkedList<Node> container;

        public Node(int id)
        {
            ID = id;
            container = new LinkedList<Node>();
        }

        public int ID { get; set; }
        public bool IsLeaf { get { return container.Count == 1; } }

        public void Add(Node node)
        {
            if (!container.Contains(node))
                container.AddLast(node);

        }

        public void Remove(Node node)
        {
            if (container.Contains(node))
                container.Remove(node);
        }

        public void RemoveFromAllNodes()
        {
            foreach (Node node in container)
                node.Remove(this);
            container.Clear();
        }

        public override string ToString()
        {
            return ID.ToString();
        }
    }


    static int Propagate(IDictionary<int, Node> graph)
    {
        int level = 0;
        while (graph.Count > 1)
        {
            level++;
            var leaves = graph.Where(e => e.Value.IsLeaf).ToArray();
            for (int i = leaves.Length - 1; i >= 0; i--)
            {
                var value = leaves[i].Value;
                value.RemoveFromAllNodes();
                graph.Remove(value.ID);
            }
        }
        return level;
    }

    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine()); // the number of adjacency relations
        var graph = new Dictionary<int, Node>();
        for (int i = 0; i < n; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int xi = int.Parse(inputs[0]); // the ID of a person which is adjacent to yi
            int yi = int.Parse(inputs[1]); // the ID of a person which is adjacent to xi
            Node x = null;
            Node y = null;
            graph.CreateIfNotExist(xi, ref x);
            graph.CreateIfNotExist(yi, ref y);

            x.Add(y);
            y.Add(x);
        }
        Console.WriteLine(Propagate(graph).ToString()); // The minimal amount of steps required to completely propagate the advertisement
    }
}
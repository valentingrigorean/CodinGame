using MyGraph;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MyGraph
{
    using System.Collections.Generic;

    public interface INode<T>
    {
        T Key { get; }
        bool IsLeaf { get; }
        IList<INode<T>> Nodes { get; }
        void AddNode(INode<T> node);
        void RemoveNode(INode<T> node);
        void RemoveFromAll();
    }

    public interface IGraph<T>
    {
        void AddConnection(T n1, T n2);
        void RemoveConnection(T n1, T n2);
        bool IsDirected { get; }
        INode<T> GetNode(T key);
    }

    public abstract class Node<T> : INode<T>
    {
        private List<INode<T>> nodes = new List<INode<T>>();

        public Node() { }
        public Node(T key)
        {
            Key = key;
        }

        public IList<INode<T>> Nodes { get { return nodes; } }

        public bool IsLeaf { get { return nodes.Count == 1; } }


        public T Key { get; private set; }


        public void AddNode(INode<T> node)
        {
            if (!nodes.Contains(node))
                nodes.Add(node);
        }

        public void RemoveFromAll()
        {
            foreach (INode<T> node in nodes)
                node.RemoveNode(this);
            nodes.Clear();
        }

        public void RemoveNode(INode<T> node)
        {
            if (nodes.Contains(node))
                nodes.Remove(node);
        }

        public override string ToString()
        {
            return Key.ToString();
        }
    }

    public delegate INode<T> CreateNode<T>(T key);

    public class Graph<T> : IGraph<T>
    {
        private IDictionary<T, INode<T>> container;
        private CreateNode<T> createFunction;

        public Graph(CreateNode<T> createFunct, bool directed = false)
        {
            if (createFunct == null)
                throw new ArgumentNullException("You need to provide a function.");
            createFunction = createFunct;
            IsDirected = directed;
            container = new SortedDictionary<T, INode<T>>();
        }

        public bool IsDirected { get; private set; }

        public INode<T> GetNode(T key)
        {
            return container[key];
        }

        public void AddConnection(T n1, T n2)
        {
            var node1 = CreateIfNotExist(n1);
            var node2 = CreateIfNotExist(n2);
            if (!IsDirected)
            {
                node1.AddNode(node2);
                node2.AddNode(node1);
            }
            else
                node1.AddNode(node2);
        }

        public void RemoveConnection(T n1, T n2)
        {
            if (container.ContainsKey(n1) && container.ContainsKey(n2))
            {
                var node1 = container[n1];
                var node2 = container[n2];

                if (!IsDirected)
                {
                    node1.RemoveNode(node2);
                    node2.RemoveNode(node1);
                }
                else
                    node1.RemoveNode(node2);
            }
        }

        private INode<T> CreateIfNotExist(T key)
        {
            if (container.ContainsKey(key))
                return container[key];
            var node = createFunction(key);
            container.Add(key, node);
            return node;
        }
    }
}

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    public class MyNode : Node<int>
    {
        public MyNode(int id) : base(id)
        {

        }

        public bool IsGateway { get; set; }
    }

    public static List<MyNode> DFS(MyNode si, ref List<MyNode> gateways, List<MyNode> path, List<MyNode> shortest)
    {
        if (!path.Contains(si))
            path.Add(si);
        if (gateways.Contains(si))
            return path;
        foreach (MyNode n in si.Nodes)
            if (!path.Contains(n))
            {
                if (shortest == null || path.Count < shortest.Count)
                {
                    var newPath = DFS(n, ref gateways, new List<MyNode>(path), shortest);
                    if (newPath != null)
                        shortest = newPath;
                }
            }
        return shortest;
    }

    public static List<MyNode> DFS(MyNode from, MyNode to, List<MyNode> path, List<MyNode> shortest)
    {
        if (!path.Contains(from))
            path.Add(from);
        if (from == to)
            return path;
        foreach (MyNode n in from.Nodes)
            if (!path.Contains(n))
            {
                if (shortest == null || path.Count < shortest.Count)
                {
                    var newPath = DFS(n, to, new List<MyNode>(path), shortest);
                    if (newPath != null)
                        shortest = newPath;
                }
            }
        return shortest;
    }

    static void debug(ref Graph<int> g, ref List<MyNode> gw)
    {
        g.AddConnection(0, 9);
        g.AddConnection(0, 7);
        g.AddConnection(0, 8);
        g.AddConnection(7, 1);
        g.AddConnection(7, 9);
        g.AddConnection(9, 1);
        g.AddConnection(9, 2);
        g.AddConnection(9, 8);
        g.AddConnection(1, 4);
        g.AddConnection(1, 3);
        g.AddConnection(1, 2);
        g.AddConnection(2, 5);
        g.AddConnection(2, 6);

        ((MyNode)g.GetNode(4)).IsGateway = true;
        ((MyNode)g.GetNode(3)).IsGateway = true;
        ((MyNode)g.GetNode(5)).IsGateway = true;
        ((MyNode)g.GetNode(6)).IsGateway = true;
        gw.Add((MyNode)g.GetNode(4));
        gw.Add((MyNode)g.GetNode(3));
        gw.Add((MyNode)g.GetNode(5));
        gw.Add((MyNode)g.GetNode(6));
    }

    static void codingame(ref Graph<int> g, ref List<MyNode> gw)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        int L = int.Parse(inputs[1]); // the number of links
        int E = int.Parse(inputs[2]); // the number of exit gateways
        for (int i = 0; i < L; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            int N2 = int.Parse(inputs[1]);
            g.AddConnection(N1, N2);
        }
        for (int i = 0; i < E; i++)
        {
            int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
            var node = ((MyNode)g.GetNode(EI));
            node.IsGateway = true;
            gw.Add(node);
        }
    }

    static void Main(string[] args)
    {

        var graph = new Graph<int>(id => new MyNode(id));
        List<MyNode> gateway = new List<MyNode>();
        gateway.Sort((i, j) => {
            if (i.Key == j.Key)
                return 0;
            return i.Key > j.Key ? 1 : -1;
        });
        debug(ref graph, ref gateway);
        // game loop
        while (true)
        {
        reset:
            int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn
            var si = ((MyNode)graph.GetNode(SI));
            var gww = si.Nodes.Where(ii => ((MyNode)ii).IsGateway);
            foreach(MyNode n in gww)
            {
                Console.WriteLine(si.ToString() +" " + n.ToString());
                goto reset;
            }
            List<MyNode> shortestPath = null;
            foreach (MyNode n in gateway)
            {
                foreach (MyNode nn in n.Nodes)
                {
                    var gw = nn.Nodes.Where(i => ((MyNode)i).IsGateway && i != n);
                    foreach(MyNode j in gw)
                    {
                        var temp = DFS(si, n, new List<MyNode>(), null);
                        if (shortestPath == null || temp.Count < shortestPath.Count)
                            shortestPath = temp;
                    }
                }         
                
            }
            if (shortestPath == null)
                shortestPath = DFS(si, ref gateway, new List<MyNode>(), null);
            if (shortestPath != null)
            {
                string s1, s2;
                if (shortestPath.Count == 2)
                {
                    graph.RemoveConnection(shortestPath[1].Key, si.Key);
                    s1 = shortestPath[1].ToString();
                    s2 = si.ToString();
                }
                else
                {
                    int count = shortestPath.Count;
                    graph.RemoveConnection(shortestPath[count - 1].Key, shortestPath[count - 2].Key);
                    s1 = shortestPath[count - 1].ToString();
                    s2 = shortestPath[count - 2].ToString();
                }
                Console.WriteLine(s1 + " " + s2);               
            }           
        }
    }
}
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
        bool Path(T n1, T n2);
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
            container = new Dictionary<T, INode<T>>();
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

        public bool Path(T n1, T n2)
        {
            var node1 = GetNode(n1);
            var node2 = GetNode(n2);
            if (node1 == null || node2 == null)
                return false;
           
            Queue<INode<T>> queue = new Queue<INode<T>>();
            List<INode<T>> visited = new List<INode<T>>();
            queue.Enqueue(node1);
            while (queue.Count != 0)
            {
                var n = queue.Dequeue();

            }
            return false;
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
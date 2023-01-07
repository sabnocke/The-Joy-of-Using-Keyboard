using System.Runtime.InteropServices;

namespace Node
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node node = new Node(10);          
            node.Peak();
            node.Push(5);
            node.Peak();
            node.Push(12);
            node.Push(8);
            node.Peak();
            node.Pop();
            node.Peak();
            node.integCck();
            node.Print();
        }
    }
}
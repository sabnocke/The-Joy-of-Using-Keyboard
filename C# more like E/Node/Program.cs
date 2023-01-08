using System.Runtime.InteropServices;

namespace Node
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node node = new Node(6);
            node.Push(9);
            node.Push(3);
            node.Push(4);
            node.Push(2);
            node.Push(10);
            node.Push(1);
            node.Print();
            node.bubble_sort();
            node.Print();
            node.integCck();
        }
    }
}
using System.Runtime.InteropServices;

namespace Node
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node node = new Node();
            node.Inception(10);
            node.Inception(2);
            node.Inception(3);
            node.Inception(4);
            node.Peak();
            node.Pop();
            node.Peak();
            node.Print();

        }
    }
}
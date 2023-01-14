namespace stack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stack stack = new Stack(1);
            stack.Push(9);
            stack.Push(10);
            stack.Peak();
            stack.Pop();
            stack.Print();
        }
    }
}
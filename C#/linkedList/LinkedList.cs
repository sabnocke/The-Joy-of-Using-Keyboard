using static System.Console;


var llist = new LinkedList<Int32>();
var item = new Node<Int32>(1);
var two = new Node<Int32>(2);
llist.Push(ref item);
llist.Push(ref two);
WriteLine(llist.Top);



class Node<T>(T input)
{
    public T Data { get; set; } = input;
    public Node<T>? Next { get; set; } = null;
    public Node<T>? Previous { get; set; } = null;
}

class LinkedList<T>()
{
    public Node<T>? First { get; private set; } = null;
    private Node<T>? Last { get; set; } = null;
    public ulong Length { get; private set; } = 0;
    

    public void Push(ref Node<T> node)
    {
        if (this.First == null) { 
            this.First = node; 
            this.Last = node; 
            this.Length = 1; 
        } else
        {
            node.Previous = this.Last;
            this.Last = node;
            node.Next = null;
            this.Length++;
        }
    }
    
    public T? Top { 
        get { return this.Last.Data; }
        private set { _ = value; } 
    }
}

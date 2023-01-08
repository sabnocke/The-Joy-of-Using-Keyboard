namespace Node
{
#pragma warning disable CS8618 // Pole, které nemůže být null, musí při ukončování konstruktoru obsahovat hodnotu, která není null. Zvažte možnost deklarovat ho jako pole s možnou hodnotou null.
#pragma warning disable CS8625 // Literál null nejde převést na odkazový typ, který nemůže mít hodnotu null.
    internal class Node
    {
        /* 
         * .push = vklada hodnotu
         * .peak = vraci nejvyssi hodnotu
         * .pop = odstrani nejvyssi hodnotu
         * .print = vytiskne celou serii
         * !bacha na "pragma warning disable", mohlo by to delat bordel!
         */

        public int value;
        public Node ancestor;   // drzi predka, "starsi clanek"
        public Node descendant; // drzi potomka, "mladsi clanek"
        public List<int> chain = new List<int>(); // bude drzet vsechny hodnoty a treba se bude hodit
        public Node frontier;
        // drzi posledni vygenerovany clanek
        public int key = 1;


        public Node(int data = 0) // prvni v rade, pak uz se nepouziva

        {
            value = data;
            ancestor = null;
            descendant = null;
            // generic prirazeni
            frontier = this;
            // nastavi frontier na sebe
        }

        public Node(int new_data, Node older, int key_value) //2. az x clanek
        {
            value = new_data;
            ancestor = older;
            key = key_value;

        }

        public bool Push(int new_data)
        {
            Node new_node = new Node(new_data, frontier, key++);
            frontier.descendant = new_node;
            frontier = new_node;
            return true;
        }

        public bool Peak()
        {
            Console.WriteLine($"peak value: {frontier.value}");
            return true;
        }

        public bool Pop()
        {
            frontier = frontier.ancestor;
            frontier.descendant = null;
            return true;
        }

        public bool Print()
        {
            Node temp = frontier;
            if (temp.ancestor == null) { Console.WriteLine("empty"); }
            Console.Write("Print: ");
            while (temp.ancestor != null)
            {
                Console.Write(temp.value + " ");
                temp = temp.ancestor;
            }
            Console.WriteLine();
            return true;
        }

        public bool integCck()
        {
            Node temp = frontier;
            int run = 0;
            int run2 = 0;
            while (temp.ancestor != null)
            {
                temp = temp.ancestor;
                run++;
            }
            if (temp.descendant != null)
            {
                while (temp.descendant != null)
                {
                    temp = temp.descendant;
                    run2++;
                }
            }
            Console.WriteLine((run == run2) ? true : false);
            return true;
        }

        public bool swap(List<int> lst, int one, int two)
        // krasny swap pro list
        {
            int temp = lst[one];
            lst[one] = lst[two];
            lst[two] = temp;
            return true;
        }

        public bool ToList()
        // prevede obsah retezu do listu (technicky by sel i array)
        {
            Node temp = frontier;
            for (int i = 0; i < key; i++)
            {
                chain.Add(temp.value);
                temp = temp.ancestor;
            }
            return true;
        }

        public bool bubble_sort()
        // generic bubble sort
        {
            ToList();
            for (int i = 0; i < chain.Count; i++)
            {
                for (int a = 0; a < chain.Count - 1; a++)
                {
                    if (chain[a] > chain[a + 1])
                    {
                        swap(chain, a, a + 1);
                    }
                }
            }
            Returnal();
            return true;
        }
        public bool Returnal()
        {
            Cleanse();
            int[] arr = chain.ToArray(); Array.Reverse(arr);
            foreach (int i in arr) { Push(i); }
            return true;
        }
        public bool Cleanse()
        // mohl by mazat cely retez (?)
        {

            frontier.ancestor = null;

            return true;
        }
    }
#pragma warning restore CS8625 // Literál null nejde převést na odkazový typ, který nemůže mít hodnotu null.
#pragma warning restore CS8618 // Pole, které nemůže být null, musí při ukončování konstruktoru obsahovat hodnotu, která není null. Zvažte možnost deklarovat ho jako pole s možnou hodnotou null.
}

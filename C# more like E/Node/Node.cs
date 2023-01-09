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
        public List<int> chain = new List<int>(); // bude drzet vsechny hodnoty
        public Node frontier;
        // drzi posledni vygenerovany clanek
        public int key = 1;
        public int[] arr;

        /// <summary>
        /// Vzdy pouzit tuto verzi!!
        /// Je to jednodussi.
        /// </summary>
        /// <param name="data"> int, ktery se ma vlozit do stacku</param>
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

        /// <summary>
        /// Kontroluje, jestli na sebe jednotlive stacky navazuji ze vsech stran
        /// </summary>
        /// <returns>true, pokud projde, jinak false</returns>
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
        // prevede obsah retezu do listu (to o kus niz dela to stejny ale s array)
        {
            Node temp = frontier;
            for (int i = 0; i < key; i++)
            {
                chain.Add(temp.value);
                temp = temp.ancestor;
            }
            return true;
        }

        public bool ToArr()
        {
            Node temp = frontier;
            arr = new int[key];
            for (int i = 0; i < key; i++)
            {
                arr[i] = temp.value;
                temp = temp.ancestor;
            }

            return true;
        }

        /// <summary>
        /// Spusti bubblesort
        /// </summary>
        /// <param name="sw">true = mel by se to pokusit udelat, pres pole (tbf nevim jestli to funguje). false jede pres list</param>
        /// <returns></returns>
        public bool bubble_sort(bool sw = false)
        {
            if (sw == true) { _ = ToArr(); }
            else { _ = ToList(); }
            for (int i = 0; i < chain.Count; i++)
            {
                for (int a = 0; a < chain.Count - 1; a++)
                {
                    if (chain[a] > chain[a + 1])
                    {
                        _ = swap(chain, a, a + 1);
                    }
                }
            }
            _ = Returnal();
            return true;
        }

        public bool Returnal()
        {
            _ = Cleanse();
            int[] arr = chain.ToArray(); Array.Reverse(arr);
            foreach (int i in arr) { _ = Push(i); }
            return true;
        }

        public bool Cleanse()
        // mohl by mazat cely retez (?)
        // ale proc?
        {

            frontier.ancestor = null;

            return true;
        }

        public int Partition(List<int> arr, int left, int right)
        {
            int pivot = arr[(left + right) / 2];
            while (left <= right)
            {
                while (arr[left] < pivot) { left++; }
                while (arr[right] > pivot) { right--; }
                if (left <= right)
                {
                    _ = swap(arr, left, right);
                    left++;
                    right--;
                }
            }
            return left;
        }

        public void Quicksort(List<int> arr, int left, int right)
        {
            if (left >= right) { return; }
            int pivot = Partition(arr, left, right);
            Quicksort(arr, left, pivot - 1);
            Quicksort(arr, pivot, right);
            _ = Returnal();
        }

        /// <summary>
        /// Spusti quicksort
        /// </summary>
        /// <param name="sw">true = mel by se to pokusit udelat pres pole (tbf nevim jestli to funguje). false jede pres list</param>
        /// <returns></returns>
        public bool callQuicksort(bool sw = false)
        {
            if (sw == true) { ToArr(); }
            else { ToList(); }
            int start = 0; int end = chain.Count() - 1;
            Quicksort(chain, start, end);

            return true;
        }

        /// <summary>
        /// Vytvori stack obsahujici nahodne hodnoty pres List
        /// </summary>
        /// <param name="n">Pocet cisel ve stacku.</param>
        /// <param name="min_value">Nejmensi mozna hodnota.</param>
        /// <param name="max_value">Nejvetsi mozna hodnota.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool GenerateLst(int n = 10, int min_value = 1, int max_value = 100000)
        {
            if (n <= 0) { throw new Exception("Nejde vytvorit pole s nula a mene prvky!"); }
            Random rnd = new Random();
            List<int> list = new List<int>(Enumerable.Range(1, n).Select(i => rnd.Next(min_value, max_value)));
            return true;
        }

        /// <summary>
        /// Vytvori stack obsahujici nahodne hodnoty pres array
        /// </summary>
        /// <param name="n">Pocet cisel ve stacku.</param>
        /// <param name="min_value">Nejmensi mozna hodnota.</param>
        /// <param name="max_value">Nejvetsi mozna hodnota.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool GenerateArr(int n = 10, int min_value = 1, int max_value = 100000)
        {
            if (n <= 0) { Exception ValueError = new("Nejde vytvorit pole s nula a mene prvky!"); throw ValueError; }
            Random rnd = new Random();
            int[] array = new int[n];
            for (int a = 0; a < n; a++) { array[a] = rnd.Next(min_value, max_value); }
            foreach (int i in array) { _ = Push(i); }
            return true;
        }
    }
#pragma warning restore CS8625 // Literál null nejde převést na odkazový typ, který nemůže mít hodnotu null.
#pragma warning restore CS8618 // Pole, které nemůže být null, musí při ukončování konstruktoru obsahovat hodnotu, která není null. Zvažte možnost deklarovat ho jako pole s možnou hodnotou null.
}

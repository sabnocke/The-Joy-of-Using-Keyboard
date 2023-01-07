using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Node
{
    internal class Node
    {
        /* 
         * .push = vklada hodnotu
         * .peak = vraci nejvyssi hodnotu
         * .pop = odstrani nejvyssi hodnotu
         * .print = vytiskne celou serii
         */

        public int value;
        public Node ancestor;   // drzi predka, "starsi clanek"
        public Node descendant; // drzi potomka, "mladsi clanek"
        public List<int> chain = new List<int>(); // bude drzet vsechny hodnoty a treba se bude hodit
        public Node frontier;
        // drzi posledni vygenerovany clanek

        public Node(int data = 0) // prvni v rade
        {
            value = data;
            ancestor = null;
            descendant = null;
            // generic prirazeni
            frontier = this;
            // nastavi frontier na sebe
            chain.Add(data);
            // prida svoji hodnotu do retezu
        }

        public Node(int new_data, Node older, Node newer) //2. az x clanek
        {
            value = new_data;
            ancestor = older;
            descendant = newer;
        }
        
        public bool Push(int new_data)
        {
            Node new_node = new Node(new_data, frontier, null);
            frontier.descendant = new_node;
            frontier = new_node;
            chain.Add(new_node.value);
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
            chain.RemoveAt(chain.Count - 1);
            return true;
        }

        public bool Print()
        {
            Console.WriteLine($"{((chain.Count > 0) ? string.Join(", ", chain) : "null")}");
            return true;
        }

        public bool integCck()
        {
            Node temp = frontier;
            int run = 0;
            int run2 = 0;
            while(temp.ancestor != null)
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
            Console.WriteLine($"amount of runs down: {run}\namount of runs up: {run2}");
            return true;
        }
    }
}

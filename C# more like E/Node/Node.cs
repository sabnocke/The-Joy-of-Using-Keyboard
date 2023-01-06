using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Node
#pragma warning disable CS8618 // Pole, které nemůže být null, musí při ukončování konstruktoru obsahovat hodnotu, která není null. Zvažte možnost deklarovat ho jako pole s možnou hodnotou null.
#pragma warning disable CS8625 // Literál null nejde převést na odkazový typ, který nemůže mít hodnotu null.
{
    internal class Node
    {

        public int value;
        public Node ancestor;
        public List<int> chain = new List<int>(); // bude drzet vsechny hodnoty a treba se bude hodit
        public Node frontier;
        // drzi posledni vygenerovany clanek


        public Node()

        {
            value= 0;

            ancestor = null;

        }
        // .push = vklada hodnotu
        // .peak = vraci nejvyssi hodnotu
        // .pop = odstrani nejvyssi hodnotu
        // .print = vytiskne celou serii
        public bool Inception(int new_data)
        // vytvori prvni clanek; navaze ancestor na frontier (null == null) a na frontier novy clanek
        // *frontier tak drzi prvni clanek
        // v pripade posunu pote dochazi k vytvoreni noveho clanku
        // zatim nejvetsi uspech
        {
            Node new_node = new Node();
            new_node.value = new_data;
            if (ancestor == null)
            {
                new_node.ancestor = frontier;
                frontier = new_node;
            } 
            chain.Add(new_node.value);
            return true;
        }
        

        public bool Peak()
        {
            Console.WriteLine(frontier.value);
            return true;
        }

        public bool Pop()
        {
            frontier = frontier.ancestor;
            return true;
        }

        public bool Print()
        {
            string h = (chain.Count > 0) ? string.Join(", ", chain) : "null";
            Console.WriteLine($"{h}");
            return true;
        }

    }
#pragma warning restore CS8618 // Pole, které nemůže být null, musí při ukončování konstruktoru obsahovat hodnotu, která není null. Zvažte možnost deklarovat ho jako pole s možnou hodnotou null.
#pragma warning restore CS8625 // Literál null nejde převést na odkazový typ, který nemůže mít hodnotu null.
}

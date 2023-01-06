using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace stack
{
    internal class Stack
    {
        public int[] pole;
        public int top;
        public int length;
        //stack.pop
        //stack.print
        //stack.push
        //stack.peak
        public Stack(int len = 10) 
        {
            pole = new int[len];
            top = 0;
            length = pole.Length;
        }
        public bool Push(int pushed = 1)
        {
            if(top < length)
            {
                pole[top] = pushed;
            }
            else
            {
                int[] pole2 = new int[length * 2];
                //pole.CopyTo(pole2, 0); // lepsi zpusob, ale kolis trval.
                for(int i = 0; i < length; i++)
                {
                    pole2[i] = pole[i];
                }
                pole2[top] = pushed;
                length= pole2.Length;
                pole = pole2;
            }
            top++;
            return true;
        }
        public bool Pop()
        {
            if(length > 0)
            {
                pole[top - 1] = 0;
                top--;
            }
            return true;
        }
        public bool Print()
        {
            // dela to stejny jako 
            //foreach(int i in pole) { Console.Write($"{i}, "); }
            for(int i = 0; i < top; i++) { Console.Write($"{pole[i]} "); }
            //Console.WriteLine($"{string.Join(", ", pole)}");
            return true;
        }
        public int Peak()
        {
            if(length > 0 )
            {
                return pole[top - 1];
            }
            return -1;
        }
    }
}

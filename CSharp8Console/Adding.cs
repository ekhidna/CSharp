using CSharp8Console.Interfaces;
using System;

namespace CSharp8Console
{
    public class Adding : IAdding
    {   
        public void Add(int x, int y)
        {
            int z = x + y;
            Console.WriteLine($"{x} + {y} = {z} ");
        }

        public void WriteSomething()
        {
            Console.WriteLine("Something");
        }      
    }
}

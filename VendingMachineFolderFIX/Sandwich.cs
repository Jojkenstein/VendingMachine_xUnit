using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    internal class Sandwich : Product
    {
        public Sandwich(string name, int sizeVal, string sizeUnit, int price) : base(name, sizeVal, sizeUnit, price)
        {
        }
                
        public override void Use()
        {
            Console.WriteLine($"Remove the plastic film and eat your {Name} sandwich.");
        }
    }
}

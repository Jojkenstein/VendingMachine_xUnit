using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    internal class Chocolate : Product
    {
        public Chocolate(string name, int sizeVal, string sizeUnit, int price) : base(name, sizeVal, sizeUnit, price)
        {
        }
                
        public override void Use()
        {
            Console.WriteLine($"Remove the cover and eat your {Name} product.");
        }
    }
}

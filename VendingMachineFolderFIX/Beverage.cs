using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    internal class Beverage : Product
    {
        public Beverage(string name, int sizeVal, string sizeUnit, int price) : base(name, sizeVal, sizeUnit, price)
        {
        }
                
        public override void Use()
        {
            Console.WriteLine($"Open the container and drink your {Name} product.");
        }
    }
}

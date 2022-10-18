using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public abstract class Product
    {
        public Product(string name, int sizeVal, string sizeUnit, int price)
        {
            Name = name;
            SizeVal = sizeVal;
            SizeUnit = sizeUnit;
            Price = price;
            _index++;
            Id = _index;
        }

        private static int _index = 0;

        public int Id { get; }

        public string Name { get; set; }
        public int SizeVal { get; set; }
        public string SizeUnit { get; set; }
        public int Price { get; set; }
        public void Examine()
        {
            Console.WriteLine($"Product: {Name} - Size: {SizeVal} {SizeUnit} - Price: {Price} SEK");
        }
        public abstract void Use();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    internal interface IVending
    {
        public void Purchace();                 // Purchase, to buy a product.
        public void ShowAll();                  // ShowAll, show all products.
        public void InsertMoney(int amount);    // InsertMoney, add money to the pool.
        public void EndTransaction();           // EndTransaction, returns money left in appropriate amount of change.
    }
}





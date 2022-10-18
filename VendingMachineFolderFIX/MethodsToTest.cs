using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class MethodsToTest
    {
        
        public readonly int[] denomination = { 1, 5, 10, 20, 50, 100, 500, 1000 };

        public (int[] billcoinAmount, int availableFunds) EndTransactionTEST(int availableFunds)
        {
            Console.Clear();
            Console.WriteLine($"Your remaning funds ({availableFunds} SEK) will be payed back in bills and/or coins.\n");

            int mod, rest;
            string billcoins;
            int[] billcoinAmount = new int[8];
            for (int i = denomination.Length - 1; i >= 0; i--)
            {
                rest = availableFunds % denomination[i];
                mod = (availableFunds - rest) / denomination[i];
                availableFunds = availableFunds - mod * denomination[i];
                billcoinAmount[i] = mod;
                if (denomination[i] > 10)
                {
                    if (mod > 1)
                    { billcoins = "bills"; }
                    else
                    { billcoins = "bill"; }
                }
                else
                {
                    if (mod > 1)
                    { billcoins = "coins"; }
                    else
                    { billcoins = "coin"; }
                }
                if (mod > 0)
                {
                    Console.WriteLine($"{mod} pcs of {denomination[i]} SEK {billcoins}\r");
                }
            }
            int fundsOut = availableFunds;
            return (billcoinAmount, fundsOut);
            Console.ReadKey();
        }
    }
}

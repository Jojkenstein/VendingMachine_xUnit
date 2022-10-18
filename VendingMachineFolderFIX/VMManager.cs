using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VendingMachine
{
    public class VMManager : IVending
    {
        public VMManager()
        {
            Products = new List<Product>();
            Purchases = new List<Product>();
            CreateProductList();
            availableFunds = 0;
        }

        public void RunMe()
        {
            _customer = true;
            while (_customer)
            {
                MainMenu();
            }
        }

        public List<Product> Products { get; set; }
        public List<Product> Purchases { get; set; }
        public int availableFunds;
        private int _idForPurchase;
        private bool _customer;
        public readonly int[] denomination = { 1, 5, 10, 20, 50, 100, 500, 1000 };


        public void CreateProductList()
        {
            Chocolate choc = new Chocolate("Kexchoklad", 60, "g", 9);
            Products.Add(choc);
            choc = new Chocolate("Snickers", 50, "g", 8);
            Products.Add(choc);
            choc = new Chocolate("Mjölkchoklad", 100, "g", 14);
            Products.Add(choc);
            Beverage bev = new Beverage("Coca Cola", 33, "cl", 9);
            Products.Add(bev);
            bev = new Beverage("Pepsi Max", 50, "cl", 12);
            Products.Add(bev);
            bev = new Beverage("Pepsi Max Mango", 150, "cl", 17);
            Products.Add(bev);
            Sandwich sand = new Sandwich("Club", 170, "g", 40);
            Products.Add(sand);
            sand = new Sandwich("BLT", 140, "g", 39);
            Products.Add(sand);
            sand = new Sandwich("Creamy Chicken", 159, "g", 42);
            Products.Add(sand);
        }

        public void ShowAll()
        {
            Console.WriteLine("Products available for purchase\n");
            foreach (Product item in Products)
            {
                Console.WriteLine(
                  $"Id: {item.Id,2} - " +
                  $"{item.Name,-15} - " +
                  $"{item.SizeVal,3} " +
                  $"{item.SizeUnit,-2} - " +
                  $"{item.Price,2} SEK");
            }
        }
        public void ShowPurchases()
        {
            if (Purchases.Count > 0)
            {
                int i = 0;
                Console.WriteLine("Your unconsumed purchases:");
                foreach (Product item in Purchases)
                {
                    i++;
                    Console.WriteLine($"Item-#:{i,3} - {item.Name}");
                }
                Console.WriteLine();
            }
        }


        public void InsertMoney(int amount)
        {
            availableFunds += amount;
            ShowAvailableFunds();
        }
        public void ShowAvailableFunds()
        {
            Console.WriteLine($"Available credit: {availableFunds} SEK\n");
        }

        public void Purchace()
        {
            bool purchaseActive = true;
            while (purchaseActive)
            {
                Console.Clear();
                Console.WriteLine($"Available funds: {availableFunds} SEK\n");
                ShowAll();
                Console.WriteLine();
                ShowPurchases();
                Console.WriteLine();
                ShowAvailableFunds();
                Console.WriteLine("\nType the Id of your product of choice");
                Console.WriteLine("\nType \"r\" to return to Main Menu\n");
                Console.Write("Input: ");
                string chosenIdStr = Console.ReadLine();
                if (chosenIdStr == "r")
                {
                    purchaseActive = false;
                    chosenIdStr = "0";
                }

                int chosenId = int.Parse(chosenIdStr);

                bool idFound = false; // Bara till för de fall användaren skriver ett ogiltigt Id
                foreach (Product item in Products)
                {
                    if (item.Id == chosenId)
                    {
                        idFound = true;
                        if (item.Price <= availableFunds)
                        {
                            _idForPurchase = item.Id;
                            availableFunds -= item.Price;
                            Purchases.Add(item);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Not enough funds available. Insert more money, please.");
                            break;
                        }
                    }
                }
                if (!idFound && (chosenId != 0))
                {
                    Console.WriteLine("Your chosen product Id is not in the list. Please try again.");
                    Console.ReadKey();
                }
            }
        }
        public void UseAndExamine()
        {
            bool isActive = true;
            while (isActive)
            {
                Console.Clear();
                Console.WriteLine("Examine and/or use your purchased products\n");
                ShowPurchases();
                Console.Write("Choose your product with index-#: ");
                int itemNr = int.Parse(Console.ReadLine());
                Console.Write("\nUse (u) or examine (e) item [or (r) to return]: ");
                string action = Console.ReadLine();
                Console.WriteLine();

                switch (action)
                {
                    case "u":
                        Purchases[itemNr - 1].Use();
                        Purchases.RemoveAt(itemNr - 1);
                        break;
                    case "e":
                        Purchases[itemNr - 1].Examine();
                        Console.WriteLine();
                        break;
                    case "r":
                        isActive = false;
                        break;
                    default:
                        Console.WriteLine("You pressed the wrong button. Please retry.");
                        break;
                }
                Console.WriteLine("\nPress any key to continue");
                Console.ReadKey();
            }
        }
        public void EndTransaction()
        {
            Console.Clear();
            Console.WriteLine($"Your remaning funds ({availableFunds} SEK) will be payed back in bills and/or coins.\n");

            int mod, rest;
            string billcoins;
            for (int i = denomination.Length - 1; i >= 0; i--)
            {
                rest = availableFunds % (denomination[i]);
                mod = (availableFunds - rest) / (denomination[i]);
                availableFunds = availableFunds - mod * denomination[i];
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
            Console.ReadKey();
        }

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Vending Machine - Main Menu\n");

            Console.WriteLine("1/I: Insert money");
            Console.WriteLine("2/S: Show all products");
            Console.WriteLine("3/P: Purchase a product");
            Console.WriteLine("4/E: End transaction");
            Console.WriteLine("5/U: Use & examine");
            Console.WriteLine("6/L: Leave program");
            Console.Write("\nMenu choice: ");
            string choice = Console.ReadLine();
            choice = choice.ToUpper();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                case "I":
                    InsertMoneyMenu();
                    break;
                case "2":
                case "S":
                    Console.Clear();
                    ShowAll();
                    Console.WriteLine("\nPress any key to return to Main Menu");
                    Console.ReadKey();
                    break;
                case "3":
                case "P":
                    Purchace();
                    break;
                case "4":
                case "E":
                    EndTransaction();
                    break;
                case "5":
                case "U":
                    UseAndExamine();
                    break;
                    case "6":
                case "L":
                    _customer = false;
                    break;
                default:
                    Console.WriteLine("\nInvalid input. Please press any key.");
                    Console.ReadKey();
                    break;
            }
        }
        public void InsertMoneyMenu()
        {
            Boolean isActive = true;
            while (isActive)
            {
                Console.Clear();
                Console.WriteLine("Vending Machine - Insert Money Menu\n");

                Console.WriteLine("Insert coins and/or bills according to below\r");

                Console.WriteLine(" A:    1 SEK (coin)");
                Console.WriteLine(" S:    5 SEK (coin)");
                Console.WriteLine(" D:   10 SEK (coin)");
                Console.WriteLine(" F:   20 SEK (bill)");
                Console.WriteLine(" G:   50 SEK (bill)");
                Console.WriteLine(" H:  100 SEK (bill)");
                Console.WriteLine(" J:  500 SEK (bill)");
                Console.WriteLine(" K: 1000 SEK (bill)");
                Console.WriteLine();
                Console.WriteLine(" R: Return to Main Menu");

                ShowAvailableFunds();

                Console.Write("Insert with key: ");
                string insertKey = Console.ReadLine();
                insertKey.ToLower();

                switch (insertKey)
                {
                    case "a":
                        InsertMoney(denomination[0]);
                        break;
                    case "s":
                        InsertMoney(denomination[1]);
                        break;
                    case "d":
                        InsertMoney(denomination[2]);
                        break;
                    case "f":
                        InsertMoney(denomination[3]);
                        break;
                    case "g":
                        InsertMoney(denomination[4]);
                        break;
                    case "h":
                        InsertMoney(denomination[5]);
                        break;
                    case "j":
                        InsertMoney(denomination[6]);
                        break;
                    case "k":
                        InsertMoney(denomination[7]);
                        break;
                    case "r":
                        isActive = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input. Please press the ANY key to redo ;-)");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}

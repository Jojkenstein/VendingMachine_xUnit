using VendingMachine;

//VMManager appMan = new VMManager();
//appMan.RunMe();

MethodsToTest methods = new MethodsToTest();
int inputMoney = 2999;
(int[] billcoinAmount, int availableFunds) = methods.EndTransactionTEST(inputMoney);

Console.WriteLine("Available funds: " + availableFunds);
Console.WriteLine("billcoinAmount[7]: " + billcoinAmount[7]);
Console.WriteLine("billcoinAmount[6]: " + billcoinAmount[6]);
Console.WriteLine("billcoinAmount[5]: " + billcoinAmount[5]);
Console.WriteLine("billcoinAmount[4]: " + billcoinAmount[4]);
Console.WriteLine("billcoinAmount[3]: " + billcoinAmount[3]);
Console.WriteLine("billcoinAmount[2]: " + billcoinAmount[2]);
Console.WriteLine("billcoinAmount[1]: " + billcoinAmount[1]);
Console.WriteLine("billcoinAmount[0]: " + billcoinAmount[0]);
Console.ReadKey();
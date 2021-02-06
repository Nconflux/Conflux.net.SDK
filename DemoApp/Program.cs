using System;
using System.Threading.Tasks;
using Conflux.API;
namespace DemoApp
{
    class Program
    {
        static string url = "http://test.confluxrpc.org";
        static string privateKey = "6B616116762AE9FE57DBD11B971D62F74ECD2BAF8B2633658BB85F691B6D4E43";
        static NConflux conflux = new NConflux(url);
        static void Main(string[] args)
        {

            Console.WriteLine($@"Get Epoch Number:{ GetEpochNumber().Result}");
            Console.WriteLine($@"Get Balance:{ GetBalance().Result}");
            Transfer();
            Console.WriteLine($@"Transfer Completed");
            var contractAddress = DeployContract().Result.ContractAddress;

            Console.WriteLine($@"Deploy Contract:{contractAddress}"); ;
            CallContract(contractAddress);
            Console.WriteLine($@"Called Contract"); ;
            Console.ReadLine();
        }
        public static async Task<dynamic> GetEpochNumber()
        {
            return await conflux.GetEpochNumber();
        }
        public static async Task<dynamic> GetBalance()
        {
            return await conflux.GetBalance("0x1a4149e2ed6aceadd375f946ebee64c71724e01b");
        }
        public static void Transfer()
        {
            conflux = new NConflux(url, privateKey);
            conflux.Transfer("0x129Ba07FD62F3e50e727566311B447D3E19A3E7D", 12).Wait();
        }

        public static async Task<dynamic> DeployContract()
        {
            conflux = new NConflux(url, "6B616116762AE9FE57DBD11B971D62F74ECD2BAF8B2633658BB85F691B6D4E43");

            //var contractInfo = await conflux.DeployContract(abi, byteCode.Trim());
            return await conflux.DeployContract($@"608060405234801561001057600080fd5b506102d7806100206000396000f30060806040526004361061004c576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff1680634ed3885e146100515780636d4ce63c146100ba575b600080fd5b34801561005d57600080fd5b506100b8600480360381019080803590602001908201803590602001908080601f016020809104026020016040519081016040528093929190818152602001838380828437820191505050505050919291929050505061014a565b005b3480156100c657600080fd5b506100cf610164565b6040518080602001828103825283818151815260200191508051906020019080838360005b8381101561010f5780820151818401526020810190506100f4565b50505050905090810190601f16801561013c5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b8060009080519060200190610160929190610206565b5050565b606060008054600181600116156101000203166002900480601f0160208091040260200160405190810160405280929190818152602001828054600181600116156101000203166002900480156101fc5780601f106101d1576101008083540402835291602001916101fc565b820191906000526020600020905b8154815290600101906020018083116101df57829003601f168201915b5050505050905090565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061024757805160ff1916838001178555610275565b82800160010185558215610275579182015b82811115610274578251825591602001919060010190610259565b5b5090506102829190610286565b5090565b6102a891905b808211156102a457600081600090555060010161028c565b5090565b905600a165627a7a7230582059693bf86b17e86ee8a4642d91c7e648e799745cd855b24d1b12cde75e3c5dfc0029");
        }

        public static void CallContract(string contractAddress)
        {
            conflux = new NConflux(url, privateKey.Trim());
            conflux.CallContract(CallType.Gas, " [ { \"constant\": false, \"inputs\": [ { \"name\": \"field\", \"type\": \"string\" } ], \"name\": \"set\", \"outputs\": [], \"payable\": false, \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [], \"name\": \"get\", \"outputs\": [ { \"name\": \"\", \"type\": \"string\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" } ]", contractAddress, "set", new object[] { "test conflux" }).Wait();
        }
    }
}

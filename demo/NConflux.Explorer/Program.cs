using Conflux.Web3;
using System;
using System.IO;
using System.Threading.Tasks;
using Conflux.Contracts;
using System.Linq;
using System.Collections.Generic;
using Conflux.ABI.FunctionEncoding;
using NConflux.Explorer.DemoContract.Hina.ContractDefinition;
using Conflux.Contracts.ContractHandlers;
using Conflux.Web3.Accounts;

namespace NConflux.Explorer
{
    class Program
    {
        private const string RPCEndpoint = "https://testnet-rpc.conflux-chain.org.cn/v2";
        private const string ABIPath = "./DemoContract/Hina/Compiled/Hina.abi";
        private const string ContractAddress = "cfxtest:type.contract:acau4jk947es5cr01a790yj00yx4pfxcbp0jf0v12j";

        private const string privateKey = "0x5d9ff9a5b313d1171b79e29451637160ef636bac68c828173cf6342de9defb9c";

        static async Task Main(string[] args)
        {
            await HinaTranscationsAsync();

            await Task.CompletedTask;
        }

        static async Task HinaCallsAsync()
        {
            string abi = await File.ReadAllTextAsync(ABIPath);

            Web3 web3 = new Web3(RPCEndpoint);

            Contract contract = web3.Cfx.GetContract(abi, ContractAddress);

            //call by func name
            //      Function func00 = contract.GetFunction("addresses");
            //       dynamic ret00 = await func00.CallAsync<dynamic>();




            // Using contract handler
            ContractHandler contractHandler = web3.Cfx.GetContractHandler(ContractAddress);

            AddressesOutputDTO addressesOutputDTO = await contractHandler.QueryAsync<AddressesFunction, AddressesOutputDTO>();
            List<string> addressesOutputListString = await contractHandler.QueryAsync<AddressesFunction, List<string>>();
            string transaction = await contractHandler.SendRequestAsync(new LeaveMessageFunction { Message = "Second" });

            //call by func name
            //      Function func00 = contract.GetFunction("addresses");
            //       dynamic ret00 = await func00.CallAsync<dynamic>(); 
        }

        static async Task HinaTranscationsAsync()
        {
            string abi = await File.ReadAllTextAsync(ABIPath);

            Account account = new Account(privateKey, Conflux.Signer.Chain.Test);
            Web3 web3 = new Web3(account, RPCEndpoint);

            Contract contract = web3.Cfx.GetContract(abi, ContractAddress);
            Function funcLeaveMsg = contract.GetFunction("leaveMessage");
            string transaction = await funcLeaveMsg.SendTransactionAsync(account.Address, "Second");

            //call by func name
            //      Function func00 = contract.GetFunction("addresses");
            //       dynamic ret00 = await func00.CallAsync<dynamic>();




            // Using contract handler
            ContractHandler contractHandler = web3.Cfx.GetContractHandler(ContractAddress);

            AddressesOutputDTO addressesOutputDTO = await contractHandler.QueryAsync<AddressesFunction, AddressesOutputDTO>();
            List<string> addressesOutputListString = await contractHandler.QueryAsync<AddressesFunction, List<string>>();
            transaction = await contractHandler.SendRequestAsync(new LeaveMessageFunction { Message = "Second" });

            //call by func name
            //      Function func00 = contract.GetFunction("addresses");
            //       dynamic ret00 = await func00.CallAsync<dynamic>(); 
        }
    }

}

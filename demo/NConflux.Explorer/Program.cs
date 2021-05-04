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
using Conflux.Hex.HexTypes;
using Conflux.API.Builders;

namespace NConflux.Explorer
{
    class Program
    {
        private const string RPCEndpoint = "https://testnet-rpc.conflux-chain.org.cn/v2";
        private const string ABIPath = "./DemoContract/Hina/Compiled/Hina.abi";
        private const string ContractAddress = "cfxtest:ace3dzw17vy0zcuckvg52ed47taayha3aajjw0pdgd";
        // put your private key here to make a transaction
        private const string privateKey = "";

        /// <summary>
        /// This is demostration program to demostrate how to using new Conflux.net SDK.
        /// The contract it self is in ./DemoContract/Hina.sol
        /// DTO code generation using Visual Studio Code with Solidity plugin OR http://codegen.nethereum.com
        /// For more information about DTO code generation, see http://docs.nethereum.com/en/latest/nethereum-code-generation/
        /// After DTO code generation, REPLACE 'using Nethereum.XXXX' to 'using Conflux.XXXX' statments in the file.
        /// Then enjoy yourself in conflux.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main(string[] args)
        {
            // demo for deploy a contract
            // mark: this contract already deployed during development stage, so it cannot been 'redepolyed' in Testnet 
 //           await DeployAsync();
            // demo for sending a transaction
            await SendTranscationsAsync();
            // demo for a call
            await CallsAsync();

            // new demo that using TrascationBuilder and ContractCaller
            // send transaction
            await TranscationBuilerAsync();
            // call a function
            await ContractCallerAsync();
            await Task.CompletedTask;
        } 

        static async Task DeployAsync()
        {
            Account account = new Account(privateKey, Conflux.Signer.Chain.Test);
            Web3 web3 = new Web3(account, RPCEndpoint);
            var deploymentHandler = web3.Cfx.GetContractDeploymentHandler<HinaDeployment>(); 
            var receipt = await deploymentHandler.SendRequestAndWaitForReceiptAsync(new HinaDeployment());  
        }

        static async Task CallsAsync()
        {
            string abi = await File.ReadAllTextAsync(ABIPath);

            Web3 web3 = new Web3(RPCEndpoint); 
            Contract contract = web3.Cfx.GetContract(abi, ContractAddress);

            // call using function name
            Function funcAddresses = contract.GetFunction("addresses");
            var ret00 = await funcAddresses.CallAsync<List<string>>();

            Function funcMessageWithSender = contract.GetFunction("messageWithSender");
            var ret01 = await funcMessageWithSender.CallDeserializingToObjectAsync<MessageWithSenderOutputDTO>();



            // Or
            // Using contract handler(recommended)
            ContractHandler contractHandler = web3.Cfx.GetContractHandler(ContractAddress);

            AddressesOutputDTO addressesOutputDTO = await contractHandler.QueryDeserializingToObjectAsync<AddressesFunction, AddressesOutputDTO>();
            List<string> addressesOutputListString = await contractHandler.QueryAsync<AddressesFunction, List<string>>();

            MessageWithSenderOutputDTO messageWithSenderOutputDTO = await contractHandler.QueryDeserializingToObjectAsync<MessageWithSenderFunction, MessageWithSenderOutputDTO>();
        }

        static async Task SendTranscationsAsync()
        {
            string abi = await File.ReadAllTextAsync(ABIPath);

            Account account = new Account(privateKey, Conflux.Signer.Chain.Test);
            Web3 web3 = new Web3(account, RPCEndpoint);


            // send transcation using function name
            Contract contract = web3.Cfx.GetContract(abi, ContractAddress);
            Function funcLeaveMsg = contract.GetFunction("leaveMessage");
            string transaction = await funcLeaveMsg.SendTransactionAsync(account.Address, "I shall leave a message");

            // Or
            // Using contract handler(recommended)
            ContractHandler contractHandler = web3.Cfx.GetContractHandler(ContractAddress);
            transaction = await contractHandler.SendRequestAsync(new LeaveMessageFunction { Message = "I shall leave a message" });
        }


        static async Task ContractCallerAsync()
        {
            string abi = await File.ReadAllTextAsync(ABIPath);

            IContractCaller<List<string>> caller0 =
                CallBuilder.CreateAsNoParameterSimpleResult<List<string>>(RPCEndpoint, ContractAddress, abi, "addresses")
                .Build();
            var result0 = await caller0.CallAsync();

            IContractCaller<AddressesOutputDTO> caller1 =
                CallBuilder.CreateAsNoParameterObjectResult<AddressesOutputDTO>(RPCEndpoint, ContractAddress, abi, "addresses")
                .Build();
            var result1 = await caller1.CallAsync();

            IContractCaller<List<string>, AddressesFunction> caller2 =
                CallBuilder.CreateAsSimpleResult<List<string>, AddressesFunction>(RPCEndpoint, ContractAddress)
                .Build();
            var result2 = await caller2.CallAsync(new AddressesFunction());

            IContractCaller<AddressesOutputDTO, AddressesFunction> caller3 =
                CallBuilder.CreateAsObjectResult<AddressesOutputDTO, AddressesFunction>(RPCEndpoint, ContractAddress)
                .Build();
            var result3 = await caller3.CallAsync(new AddressesFunction());



            IContractCaller<MessageWithSenderOutputDTO, object[]> caller4 =
                CallBuilder.CreateAsObjectResult<MessageWithSenderOutputDTO>(RPCEndpoint, ContractAddress, abi, "messageWithSender")
                .Build();
            var result4 = await caller4.CallAsync(null);


            IContractCaller<MessageWithSenderOutputDTO, MessageWithSenderFunction> caller5 =
                CallBuilder.CreateAsObjectResult<MessageWithSenderOutputDTO, MessageWithSenderFunction>(RPCEndpoint, ContractAddress)
                .Build();
            var result5 = await caller5.CallAsync(new MessageWithSenderFunction());
        }

        static async Task TranscationBuilerAsync()
        {
            string abi = await File.ReadAllTextAsync(ABIPath);
            Account account = new Account(privateKey, Conflux.Signer.Chain.Test);

            // send transcation using function name
            ITranscationSender sender0 = TranscationBuilder.Create(RPCEndpoint, ContractAddress, abi, "leaveMessage")
                .GasLimit(96800)        // you can set transcation parameters using builder function
                .StorageLimit(1024)
                .Build(account);
            string transaction0 = await sender0.SendTranscationAsync("I shall leave a message");
            var reciption0 = await sender0.SendRequestAndWaitForReceiptAsync("I shall leave a message");

            // Or
            // Using contract handler(recommended)
            ITranscationSender<LeaveMessageFunction> sender1 = TranscationBuilder.Create<LeaveMessageFunction>(RPCEndpoint, ContractAddress)
                .Build(account);
            string transaction1 = await sender1.SendTranscationAsync();
            var reciption1 = await sender1.SendRequestAndWaitForReceiptAsync(new LeaveMessageFunction { Message = "I shall leave a message" });
        }
    }

}

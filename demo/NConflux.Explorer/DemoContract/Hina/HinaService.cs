using Conflux.Contracts.ContractHandlers;
using Conflux.RPC.Eth.DTOs;
using Conflux.Web3;
using NConflux.Explorer.DemoContract.Hina.ContractDefinition;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NConflux.Explorer.DemoContract.Hina
{
    public partial class HinaService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Conflux.Web3.Web3 web3, HinaDeployment hinaDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Cfx.GetContractDeploymentHandler<HinaDeployment>().SendRequestAndWaitForReceiptAsync(hinaDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Conflux.Web3.Web3 web3, HinaDeployment hinaDeployment)
        {
            return web3.Cfx.GetContractDeploymentHandler<HinaDeployment>().SendRequestAsync(hinaDeployment);
        }

        public static async Task<HinaService> DeployContractAndGetServiceAsync(Conflux.Web3.Web3 web3, HinaDeployment hinaDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, hinaDeployment, cancellationTokenSource);
            return new HinaService(web3, receipt.ContractAddress);
        }

        protected Web3 Web3 { get; }

        public ContractHandler ContractHandler { get; }

        public HinaService(Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Cfx.GetContractHandler(contractAddress);
        }

        public Task<List<string>> AddressesQueryAsync(AddressesFunction addressesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressesFunction, List<string>>(addressesFunction, blockParameter);
        }


        public Task<List<string>> AddressesQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AddressesFunction, List<string>>(null, blockParameter);
        }

        public Task<string> GetContentQueryAsync(GetContentFunction getContentFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetContentFunction, string>(getContentFunction, blockParameter);
        }


        public Task<string> GetContentQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetContentFunction, string>(null, blockParameter);
        }

        public Task<string> GetContentWithIndentifierQueryAsync(GetContentWithIndentifierFunction getContentWithIndentifierFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetContentWithIndentifierFunction, string>(getContentWithIndentifierFunction, blockParameter);
        }


        public Task<string> GetContentWithIndentifierQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetContentWithIndentifierFunction, string>(null, blockParameter);
        }

        public Task<string> LeaveMessageRequestAsync(LeaveMessageFunction leaveMessageFunction)
        {
            return ContractHandler.SendRequestAsync(leaveMessageFunction);
        }

        public Task<TransactionReceipt> LeaveMessageRequestAndWaitForReceiptAsync(LeaveMessageFunction leaveMessageFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(leaveMessageFunction, cancellationToken);
        }

        public Task<string> LeaveMessageRequestAsync(string message)
        {
            var leaveMessageFunction = new LeaveMessageFunction();
            leaveMessageFunction.Message = message;

            return ContractHandler.SendRequestAsync(leaveMessageFunction);
        }

        public Task<TransactionReceipt> LeaveMessageRequestAndWaitForReceiptAsync(string message, CancellationTokenSource cancellationToken = null)
        {
            var leaveMessageFunction = new LeaveMessageFunction();
            leaveMessageFunction.Message = message;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(leaveMessageFunction, cancellationToken);
        }

        public Task<MessageWithSenderOutputDTO> MessageWithSenderQueryAsync(MessageWithSenderFunction messageWithSenderFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<MessageWithSenderFunction, MessageWithSenderOutputDTO>(messageWithSenderFunction, blockParameter);
        }

        public Task<MessageWithSenderOutputDTO> MessageWithSenderQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<MessageWithSenderFunction, MessageWithSenderOutputDTO>(null, blockParameter);
        }
    }
}

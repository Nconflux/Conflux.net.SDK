using System.Threading;
using System.Threading.Tasks;
using Conflux.Contracts.ContractHandlers;
using Conflux.ENS.FIFSRegistrar.ContractDefinition;
using Conflux.RPC.Eth.DTOs;

namespace Conflux.ENS
{

    public partial class FIFSRegistrarService
    {
    
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Conflux.Web3.Web3 web3, FIFSRegistrarDeployment fIFSRegistrarDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Cfx.GetContractDeploymentHandler<FIFSRegistrarDeployment>().SendRequestAndWaitForReceiptAsync(fIFSRegistrarDeployment, cancellationTokenSource);
        }
        public static Task<string> DeployContractAsync(Conflux.Web3.Web3 web3, FIFSRegistrarDeployment fIFSRegistrarDeployment)
        {
            return web3.Cfx.GetContractDeploymentHandler<FIFSRegistrarDeployment>().SendRequestAsync(fIFSRegistrarDeployment);
        }
        public static async Task<FIFSRegistrarService> DeployContractAndGetServiceAsync(Conflux.Web3.Web3 web3, FIFSRegistrarDeployment fIFSRegistrarDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, fIFSRegistrarDeployment, cancellationTokenSource);
            return new FIFSRegistrarService(web3, receipt.ContractAddress);
        }
    
        protected Conflux.Web3.Web3 Web3{ get; }
        
        public ContractHandler ContractHandler { get; }
        
        public FIFSRegistrarService(Conflux.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Cfx.GetContractHandler(contractAddress);
        }
    
        public Task<string> RegisterRequestAsync(RegisterFunction registerFunction)
        {
             return ContractHandler.SendRequestAsync(registerFunction);
        }

        public Task<TransactionReceipt> RegisterRequestAndWaitForReceiptAsync(RegisterFunction registerFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerFunction, cancellationToken);
        }

        public Task<string> RegisterRequestAsync(byte[] subnode, string owner)
        {
            var registerFunction = new RegisterFunction();
                registerFunction.Subnode = subnode;
                registerFunction.Owner = owner;
            
             return ContractHandler.SendRequestAsync(registerFunction);
        }

        public Task<TransactionReceipt> RegisterRequestAndWaitForReceiptAsync(byte[] subnode, string owner, CancellationTokenSource cancellationToken = null)
        {
            var registerFunction = new RegisterFunction();
                registerFunction.Subnode = subnode;
                registerFunction.Owner = owner;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(registerFunction, cancellationToken);
        }
    }
}

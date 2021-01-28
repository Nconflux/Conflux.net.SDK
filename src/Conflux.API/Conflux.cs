using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;
using ConfluxWeb3 = Conflux.Web3;
using System;
using System.Threading.Tasks;

namespace Conflux.API
{
    public enum CallType
    {
        Gas,
        NoGas,
    }
    public class NConflux
    {
        ConfluxWeb3.Web3 web3 = null;
        public string address = "";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="privateKey"></param>
        public NConflux(string url, string privateKey = null)
        {
            if (privateKey != null)
            {
                var chainId = url.Contains("main") ? 1029 : 1;
                var account = new ConfluxWeb3.Accounts.Account(privateKey, chainId);
                address = account.Address;
                web3 = new ConfluxWeb3.Web3(account, url);
            }
            else
            {
                web3 = new ConfluxWeb3.Web3(url);
            }
        }
        //001
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetEpochNumber()
        {
            var ret = await web3.Cfx.GetEpochNumber.SendRequestAsync();
            return Convert.ToInt32(ret.Value.ToString());
        }
        //002
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
        public async Task<decimal> GetBalance(string wallet)
        {
            var ret = await web3.Cfx.GetBalance.SendRequestAsync(wallet);
            return ConfluxWeb3.Web3.Convert.FromDrip(ret.Value);
        }
        //003
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wallet"></param>
        /// <returns></returns>
        public async Task<int> GetNextNonce(string wallet)
        {
            var ret = await web3.Cfx.GetNextNonce.SendRequestAsync(wallet, null);
            return Convert.ToInt32(ret.Value.ToString());
        }
        //004
        /// <summary>
        /// 
        /// </summary>
        /// <param name="receiptWallet"></param>
        /// <param name="amount"></param>
        /// <param name="gasPrice"></param>
        /// <param name="gas"></param>
        /// <returns></returns>
        public async Task Transfer(string receiptWallet, decimal amount, int gasPrice = 1000000, int gas = 21000)
        {
            var epochNumber = await GetEpochNumber();
            var nextNonce = await GetNextNonce(address);
            await web3.Cfx.GetEtherTransferService().TransferEtherAndWaitForReceiptAsync(receiptWallet, amount, gasPrice, new HexBigInteger(epochNumber), new HexBigInteger(nextNonce), gas);
        }
        //005
        /// <summary>
        /// Deploy Contract
        /// </summary>
        /// <param name="byteCode"></param>
        /// <returns></returns>
        public async Task<TransactionReceipt> DeployContract(string byteCode)
        {
            Contract.BYTECODE = byteCode.Trim();
            var deploymentMessage = new Contract
            {
                Nonce = await web3.Cfx.GetNextNonce.SendRequestAsync(address, null),
                EpochNumber = await web3.Cfx.GetEpochNumber.SendRequestAsync(),
                StorageLimit = 2605,
            };

            var deploymentHandler = web3.Cfx.GetContractDeploymentHandler<Contract>();
            return await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
        }

        public async Task<string> DeployContract(string abi, string byteCode)
        {
            //Contract.BYTECODE = byteCode;
            //var deploymentMessage = new Contract
            //{
            //    Nonce = await web3.Cfx.GetNextNonce.SendRequestAsync(address, null),
            //    EpochNumber = await web3.Cfx.GetEpochNumber.SendRequestAsync(),
            //    StorageLimit = 2605,
            //};

            //var deploymentHandler = web3.Cfx.GetContractDeploymentHandler<Contract>();
            return await web3.Cfx.DeployContract.SendRequestAsync(abi: abi, contractByteCode: byteCode, from: address, values: null);
        }
        /// <summary>
        /// Call Contract
        /// </summary>
        /// <param name="callType"></param>
        /// <param name="abi"></param>
        /// <param name="contract"></param>
        /// <param name="funcName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<dynamic> CallContract(CallType callType, string abi, string contract, string funcName, object[] parameters = null)
        {
            var myContract = web3.Cfx.GetContract(abi, contract);
            var function = myContract.GetFunction(funcName);
            dynamic result;
            if (callType == CallType.Gas)
            {

                result = await function.SendTransactionAsync(address, await web3.Cfx.GetEpochNumber.SendRequestAsync(), await web3.Cfx.GetNextNonce.SendRequestAsync(address, null), parameters);


            }
            else
            {
                if (parameters == null)
                {
                    result = await function.CallAsync<dynamic>();
                }
                else
                {
                    result = await function.CallAsync<dynamic>(parameters);
                }
            }
            return result;
        }

        public static string GeneratePrivateKey()
        {
            var key = Conflux.Signer.EthECKey.GenerateKey();
            return BitConverter.ToString(key.GetPrivateKeyAsBytes()).Replace("-", string.Empty);
        }

    }

}

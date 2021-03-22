using Conflux.Hex.HexTypes;
using Conflux.RPC.Eth.DTOs;
using ConfluxWeb3 = Conflux.Web3;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Conflux.ABI.FunctionEncoding;
using System.Linq;

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
                uint chainId = url.Contains("main") ? (uint)1029 : 1;
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
            Contract.BYTECODE = byteCode;
            var deploymentMessage = new Contract
            {
                Nonce = await web3.Cfx.GetNextNonce.SendRequestAsync(address, null),
                EpochNumber = await web3.Cfx.GetEpochNumber.SendRequestAsync()
            };

            var deploymentHandler = web3.Cfx.GetContractDeploymentHandler<Contract>();
            return await deploymentHandler.SendRequestAndWaitForReceiptAsync(deploymentMessage);
        }

        [Obsolete("Using other async function instead")]
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

        /// <summary>
        /// Call function in a contract and return as an object array
        /// </summary>
        /// <param name="abi">Abi of contract</param>
        /// <param name="contract">Address of contract</param>
        /// <param name="funcName">Function entrypoint</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>An object array of function returns</returns>
        public async Task<object[]> CallContractAsync(string abi, string contract, string funcName, params object[] parameters)
        {
            var myContract = web3.Cfx.GetContract(abi, contract);
            var function = myContract.GetFunction(funcName);
            return (await function.CallDecodingToDefaultAsync(parameters)).OrderBy(p => p.Parameter.Order).Select(p => p.Result).ToArray();
        }

        /// <summary>
        /// Call function in a contract and return as an object 
        /// </summary>
        /// <typeparam name="TReturn">Return object type</typeparam>
        /// <param name="abi">Abi of contract</param>
        /// <param name="contract">Address of contract</param>
        /// <param name="funcName">Function entrypoint</param>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public async Task<TReturn> CallContractAsync<TReturn>(string abi, string contract, string funcName, params object[] parameters) where TReturn : new()
        {
            var myContract = web3.Cfx.GetContract(abi, contract);
            var function = myContract.GetFunction(funcName);
            return await function.CallDeserializingToObjectAsync<TReturn>(parameters);
        }

        /// <summary>
        /// Call function in a contract and return the first object
        /// </summary>
        /// <typeparam name="TReturn">Return object type</typeparam>
        /// <param name="abi">Abi of contract</param>
        /// <param name="contract">Address of contract</param>
        /// <param name="funcName">Function entrypoint</param>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public async Task<TReturn> CallContractSimpleAsync<TReturn> (string abi, string contract, string funcName, params object[] parameters)  
        {
            var myContract = web3.Cfx.GetContract(abi, contract);
            var function = myContract.GetFunction(funcName);
            return await function.CallAsync<TReturn>(parameters);
        }

        /// <summary>
        /// Call function in a contract and return the first object as a dynamique object
        /// </summary>
        /// <typeparam name="TReturn">Return object type</typeparam>
        /// <param name="abi">Abi of contract</param>
        /// <param name="contract">Address of contract</param>
        /// <param name="funcName">Function entrypoint</param>
        /// <param name="parameters">Parameters</param>
        /// <returns></returns>
        public async Task<dynamic> CallContractSimpleAsync(string abi, string contract, string funcName, params object[] parameters)
        {
            var myContract = web3.Cfx.GetContract(abi, contract);
            var function = myContract.GetFunction(funcName);
            return await function.CallAsync<dynamic>(parameters);
        }

        public static string GeneratePrivateKey()
        {
            var key = Conflux.Signer.EthECKey.GenerateKey();
            return BitConverter.ToString(key.GetPrivateKeyAsBytes()).Replace("-", string.Empty);
        }

    }

}

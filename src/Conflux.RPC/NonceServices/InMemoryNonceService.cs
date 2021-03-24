using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Conflux.Hex.HexTypes;
using Conflux.JsonRpc.Client;
using Conflux.RPC.Eth;
using Conflux.RPC.Eth.DTOs;
using Conflux.RPC.Eth.Transactions;

namespace Conflux.RPC.NonceServices
{
#if !DOTNET35
    public class InMemoryNonceService: INonceService
    {
        public BigInteger CurrentNonce { get; set; } = -1;
        public IClient Client { get; set; }
        private readonly string _account;
        private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1,1);

        public InMemoryNonceService(string account, IClient client)
        {
            Client = client;
            _account = account;
        }

        public async Task<HexBigInteger> GetNextNonceAsync()
        {

            if (Client == null) throw new NullReferenceException("Client not configured");
            var ethEthGetNextNonce = new EthGetNextNonce(Client);
            await _semaphoreSlim.WaitAsync();
            try
            {
                var nonce = await ethEthGetNextNonce.SendRequestAsync(_account, BlockParameter.CreatePending())
                    .ConfigureAwait(false);
                if (nonce.Value <= CurrentNonce)
                {
                    CurrentNonce = CurrentNonce + 1;
                    nonce = new HexBigInteger(CurrentNonce);
                }
                else
                {
                    CurrentNonce = nonce.Value - 1;
                }
                return nonce;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task ResetNonce()
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                CurrentNonce = -1;
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }
#endif
}
using System.Numerics;
using Conflux.KeyStore;
using Conflux.RPC.Accounts;
using Conflux.RPC.NonceServices;
using Conflux.RPC.TransactionManagers;
using Conflux.Signer;
using Conflux.Util;

namespace Conflux.Web3.Accounts
{
    public class Account : IAccount
    {
        public uint? ChainId { get; } 

#if !PCL
        public static Account LoadFromKeyStoreFile(string filePath, string password)
        {
            var keyStoreService = new Conflux.KeyStore.KeyStoreService();
            var key = keyStoreService.DecryptKeyStoreFromFile(password, filePath);
            return new Account(key);
        }
#endif
        public static Account LoadFromKeyStore(string json, string password, uint? chainId = null)
        {
            var keyStoreService = new KeyStoreService();
            var key = keyStoreService.DecryptKeyStoreFromJson(password, json);
            return new Account(key, chainId);
        }

        public string PrivateKey { get; private set; }

        public Account(EthECKey key, uint? chainId = null)
        {
            ChainId = chainId;
            Initialise(key);
        }

        public Account(string privateKey, uint? chainId = null)
        {
            ChainId = chainId;
            Initialise(new EthECKey(privateKey));
        }

        public Account(byte[] privateKey, uint? chainId = null)
        {
            ChainId = chainId;
            Initialise(new EthECKey(privateKey, true));
        }

        public Account(EthECKey key, Chain chain) : this(key, (uint)chain)
        {
        }

        public Account(string privateKey, Chain chain) : this(privateKey, (uint)chain)
        {
        }

        public Account(byte[] privateKey, Chain chain) : this(privateKey, (uint)chain)
        {
        }

        private void Initialise(EthECKey key)
        {
            this.PrivateKey = key.GetPrivateKey();
            this.Hex40Address = key.GetPublicAddress();
            this.Address = CIP37.Hex40ToCIP37(this.Hex40Address, ChainId); 
            InitialiseDefaultTransactionManager();
        }

        protected virtual void InitialiseDefaultTransactionManager()
        {
            TransactionManager = new AccountSignerTransactionManager(null, this, ChainId);
        } 
        public string Hex40Address { get; protected set; }
        public string Address { get; protected set; }
        public ITransactionManager TransactionManager { get; protected set; }
        public INonceService NonceService { get; set; }
    }
}
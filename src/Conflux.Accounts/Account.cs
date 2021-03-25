using System.Numerics;
using Conflux.KeyStore;
using Conflux.RPC.Accounts;
using Conflux.RPC.NonceServices;
using Conflux.RPC.TransactionManagers;
using Conflux.Signer;
using Conflux.Address;

namespace Conflux.Web3.Accounts
{
    public class Account : IAccount
    {
        public BigInteger? ChainId { get; }

#if !PCL
        public static Account LoadFromKeyStoreFile(string filePath, string password)
        {
            var keyStoreService = new Conflux.KeyStore.KeyStoreService();
            var key = keyStoreService.DecryptKeyStoreFromFile(password, filePath);
            return new Account(key);
        }
#endif
        public static Account LoadFromKeyStore(string json, string password, BigInteger? chainId = null)
        {
            var keyStoreService = new KeyStoreService();
            var key = keyStoreService.DecryptKeyStoreFromJson(password, json);
            return new Account(key, chainId);
        }

        public string PrivateKey { get; private set; }

        public Account(CfxECKey key, BigInteger? chainId = null)
        {
            ChainId = chainId;
            Initialise(key);
        }

        public Account(string privateKey, BigInteger? chainId = null)
        {
            ChainId = chainId;
            Initialise(new CfxECKey(privateKey));
        }

        public Account(byte[] privateKey, BigInteger? chainId = null)
        {
            ChainId = chainId;
            Initialise(new CfxECKey(privateKey, true));
        }

        public Account(CfxECKey key, Chain chain) : this(key, (int)chain)
        {
        }

        public Account(string privateKey, Chain chain) : this(privateKey, (int)chain)
        {
        }

        public Account(byte[] privateKey, Chain chain) : this(privateKey, (int)chain)
        {
        }

        private void Initialise(CfxECKey key)
        {
            PrivateKey = key.GetPrivateKey();
            Address = key.GetPublicAddress();
            Address = Base32.Encode(Address, NetworkType.cfxtest);
            InitialiseDefaultTransactionManager();
        }

        protected virtual void InitialiseDefaultTransactionManager()
        {
            TransactionManager = new AccountSignerTransactionManager(null, this, ChainId);
        }

        public string Address { get; protected set; }
        public ITransactionManager TransactionManager { get; protected set; }
        public INonceService NonceService { get; set; }
    }
}
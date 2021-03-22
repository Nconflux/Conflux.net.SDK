namespace Conflux.Web3.Accounts.Managed
{
    using Conflux.RPC.Accounts;
    using Conflux.RPC.Eth;
    using Conflux.RPC.NonceServices;
    using Conflux.RPC.TransactionManagers;
    using Conflux.Util;

    public class ManagedAccount : IAccount
    {
        public uint? ChainId { get; }

        public ManagedAccount(string accountAddress, string password, uint? chainId = null)
        {
            this.ChainId = chainId;
            this.Hex40Address = accountAddress;
            this.Address = CIP37.Hex40ToCIP37(Hex40Address, this.ChainId);
            this.Password = password;
            this.InitialiseDefaultTransactionManager();
        }

        public ManagedAccount(string accountAddress, string password,
            ManagedAccountTransactionManager transactionManager, uint? chainId = null)
        {
            this.ChainId = chainId;
            this.Hex40Address = accountAddress;
            this.Address = CIP37.Hex40ToCIP37(Hex40Address, this.ChainId);
            this.Password = password;
            this.TransactionManager = transactionManager;
            transactionManager.SetAccount(this);
        }

        public string Password { get; protected set; }

        public string Hex40Address { get; protected set; }
        public string Address { get; protected set; }

        public ITransactionManager TransactionManager { get; protected set; }

        public INonceService NonceService { get; set; } 

        protected virtual void InitialiseDefaultTransactionManager()
        {
            TransactionManager = new ManagedAccountTransactionManager(null, this);
        }
    }
}
using Conflux.RPC.Accounts;
using Conflux.RPC.NonceServices;
using Conflux.RPC.TransactionManagers;

namespace Conflux.Quorum
{
    public class UnlockedAccount : IAccount
    {
        public UnlockedAccount(string accountAddress)
        {
            Address = accountAddress;
            InitialiseDefaultTransactionManager();
        }

        public UnlockedAccount(string accountAddress,
            UnlockedAcountTransactionManager transactionManager)
        {
            Address = accountAddress;
            TransactionManager = transactionManager;
            transactionManager.SetAccount(this);
        }

        public string Address { get; protected set; }

        public ITransactionManager TransactionManager { get; protected set; }

        public INonceService NonceService { get; set; }

        protected virtual void InitialiseDefaultTransactionManager()
        {
            TransactionManager = new UnlockedAcountTransactionManager(null, this);
        }
    }
}
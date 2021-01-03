using Conflux.Signer;
using Conflux.Web3.Accounts;

namespace Conflux.Quorum
{
    public class QuorumAccount : Account
    {

        public QuorumAccount(EthECKey key):base(key)
        {
            
        }

        public QuorumAccount(string privateKey):base(privateKey)
        {
       
        }

        public QuorumAccount(byte[] privateKey):base(privateKey)
        {
         
        }

        protected override void InitialiseDefaultTransactionManager()
        {
            TransactionManager = new QuorumTransactionManager(null, null, this);
        }
    }
}

using System.Collections.Generic;
using Conflux.BlockchainProcessing.BlockStorage.Entities;

namespace Conflux.BlockchainProcessing.BlockStorage.Repositories
{
    public class InMemoryBlockchainStorageRepositoryContext
    {
        public List<IAddressTransactionView> AddressTransactions = new List<IAddressTransactionView>();
        public List<IBlockView> Blocks = new List<IBlockView>();
        public List<IContractView> Contracts = new List<IContractView>();
        public List<ITransactionLogView> TransactionLogs = new List<ITransactionLogView>();
        public List<ITransactionView> Transactions = new List<ITransactionView>();
        public List<ITransactionVmStackView> VmStacks = new List<ITransactionVmStackView>();
    }
}

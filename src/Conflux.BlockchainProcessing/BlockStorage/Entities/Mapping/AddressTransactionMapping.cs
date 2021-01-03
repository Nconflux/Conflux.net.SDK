namespace Conflux.BlockchainProcessing.BlockStorage.Entities.Mapping
{
    public static class AddressTransactionMapping
    {
        public static void Map(this AddressTransaction to, Conflux.RPC.Eth.DTOs.Transaction @from, string address)
        {
            to.BlockNumber = @from.BlockNumber.Value.ToString();
            to.Hash = @from.TransactionHash;
            to.Address = address;
        }

        public static AddressTransaction MapToStorageEntityForUpsert(this Conflux.RPC.Eth.DTOs.TransactionReceiptVO @from, string address)
        {
            return from.MapToStorageEntityForUpsert<AddressTransaction>(address);
        }

        public static TEntity MapToStorageEntityForUpsert<TEntity>(this Conflux.RPC.Eth.DTOs.TransactionReceiptVO @from, string address)
            where TEntity : AddressTransaction, new()
        {
            return MapToStorageEntityForUpsert<TEntity>(new TEntity(), from, address);
        }

        public static TEntity MapToStorageEntityForUpsert<TEntity>(this TEntity to, Conflux.RPC.Eth.DTOs.TransactionReceiptVO @from, string address)
            where TEntity : AddressTransaction
        {
            to.Map(from.Transaction, address);
            to.UpdateRowDates();
            return to;
        }
    }
}

using System.Numerics;
using Nethereum.Signer;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.XUnitEthereumClients;
using Xunit;

namespace Nethereum.Accounts.IntegrationTests
{
    [Collection(EthereumClientIntegrationFixture.ETHEREUM_CLIENT_COLLECTION_DEFAULT)]
    public class TransferEtherTests
    {
        private readonly EthereumClientIntegrationFixture _ethereumClientIntegrationFixture;

        public TransferEtherTests(EthereumClientIntegrationFixture ethereumClientIntegrationFixture)
        {
            _ethereumClientIntegrationFixture = ethereumClientIntegrationFixture;
        }

        [Fact]
        public async void ShouldTransferEtherWithGasPrice()
        {
            
            var web3 = _ethereumClientIntegrationFixture.GetWeb3();
            
            var toAddress = "0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BAe";
            var balanceOriginal = await web3.Cfx.GetBalance.SendRequestAsync(toAddress);
            var balanceOriginalEther = Web3.Web3.Convert.FromDrip(balanceOriginal.Value);
            var receipt = await web3.Cfx.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(toAddress, 1.11m, 2);

            var balance = await web3.Cfx.GetBalance.SendRequestAsync(toAddress);
            Assert.Equal(balanceOriginalEther + 1.11m, Web3.Web3.Convert.FromDrip(balance));
        }

        [Fact]
        public async void ShouldTransferEther()
        {
            var web3 = _ethereumClientIntegrationFixture.GetWeb3();
            var toAddress = "0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BA1";
            var balanceOriginal = await web3.Cfx.GetBalance.SendRequestAsync(toAddress);
            var balanceOriginalEther = Web3.Web3.Convert.FromDrip(balanceOriginal.Value);

            var receipt = await web3.Cfx.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(toAddress, 1.11m);

            var balance = await web3.Cfx.GetBalance.SendRequestAsync(toAddress);
            Assert.Equal(balanceOriginalEther + 1.11m, Web3.Web3.Convert.FromDrip(balance));
        }

        [Fact]
        public async void ShouldTransferEtherWithGasPriceAndGasAmount()
        {
            var web3 = _ethereumClientIntegrationFixture.GetWeb3();
            var toAddress = "0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BA1";
            var balanceOriginal = await web3.Cfx.GetBalance.SendRequestAsync(toAddress);
            var balanceOriginalEther = Web3.Web3.Convert.FromDrip(balanceOriginal.Value);

            var receipt = await web3.Cfx.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(toAddress, 1.11m, 2, new BigInteger(25000));

            var balance = await web3.Cfx.GetBalance.SendRequestAsync(toAddress);
            Assert.Equal(balanceOriginalEther + 1.11m, Web3.Web3.Convert.FromDrip(balance));
        }

        [Fact]
        public async void ShouldTransferEtherEstimatingAmount()
        {
            var web3 = _ethereumClientIntegrationFixture.GetWeb3();
            var toAddress = "0xde0B295669a9FD93d5F28D9Ec85E40f4cb697BA1";
            var balanceOriginal = await web3.Cfx.GetBalance.SendRequestAsync(toAddress);
            var balanceOriginalEther = Web3.Web3.Convert.FromDrip(balanceOriginal.Value);
            var transferService = web3.Cfx.GetEtherTransferService();
            var estimate = await transferService.EstimateGasAsync(toAddress, 1.11m);
            var receipt = await transferService
                .TransferEtherAndWaitForReceiptAsync(toAddress, 1.11m, 2, estimate);

            var balance = await web3.Cfx.GetBalance.SendRequestAsync(toAddress);
            Assert.Equal(balanceOriginalEther + 1.11m, Web3.Web3.Convert.FromDrip(balance));
        }

        [Fact]
        public async void ShouldTransferWholeBalanceInEther()
        {
            var web3 = _ethereumClientIntegrationFixture.GetWeb3();
            var privateKey = EthECKey.GenerateKey();
            var newAccount = new Account(privateKey.GetPrivateKey());
            var toAddress = newAccount.Address;
            var transferService = web3.Cfx.GetEtherTransferService();

            var receipt = await web3.Cfx.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(toAddress, 1000, 2);

            var balance = await web3.Cfx.GetBalance.SendRequestAsync(toAddress);
            Assert.Equal(1000, Web3.Web3.Convert.FromDrip(balance));

            var totalAmountBack =
                await transferService.CalculateTotalAmountToTransferWholeBalanceInEther(toAddress, 2);

            var web32 = new Web3.Web3(newAccount, web3.Client);
            var receiptBack = await web32.Cfx.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(AccountFactory.Address, totalAmountBack, 2);

            var balanceFrom = await web32.Cfx.GetBalance.SendRequestAsync(toAddress);
            Assert.Equal(0, Web3.Web3.Convert.FromDrip(balanceFrom));
        }
    }
}
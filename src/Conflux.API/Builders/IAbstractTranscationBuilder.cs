using Common.Logging;
using Conflux.Web3.Accounts;
using System.Net.Http.Headers;
using System.Numerics;

namespace Conflux.API.Builders
{
    public interface IAbstractTranscationBuilder<TInput> where TInput : class
    {
       
        AbstractTranscationBuilder<TInput> Authentication(AuthenticationHeaderValue auth);
        ITranscationSender<TInput> Build(Account account);
        AbstractTranscationBuilder<TInput> GasLimit(BigInteger? gas);
        AbstractTranscationBuilder<TInput> LogWith(ILog log);
        AbstractTranscationBuilder<TInput> StorageLimit(BigInteger? storageLimit);
        AbstractTranscationBuilder<TInput> WithEpochNumber(BigInteger? epochNumber);
        AbstractTranscationBuilder<TInput> WithGasPrice(BigInteger? gasPrice);
        AbstractTranscationBuilder<TInput> WithNonce(BigInteger? nonce);
        AbstractTranscationBuilder<TInput> WithValue(BigInteger value);
    }

    public interface IAbstractTranscationBuilder
    {  
        AbstractTranscationBuilder Authentication(AuthenticationHeaderValue auth);
        ITranscationSender Build(Account account);
        AbstractTranscationBuilder GasLimit(BigInteger? gas);
        AbstractTranscationBuilder LogWith(ILog log);
        AbstractTranscationBuilder StorageLimit(BigInteger? storageLimit);
        AbstractTranscationBuilder WithEpochNumber(BigInteger? epochNumber);
        AbstractTranscationBuilder WithGasPrice(BigInteger? gasPrice);
        AbstractTranscationBuilder WithNonce(BigInteger? nonce);
        AbstractTranscationBuilder WithValue(BigInteger value);
    }
}

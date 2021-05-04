using Common.Logging;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Conflux.API.Builders
{
    public interface IAbstractCallBuilder<TReturn, TInput>
    {
        AbstractCallBuilder<TReturn, TInput> Authentication(AuthenticationHeaderValue auth);
        IContractCaller<TReturn, TInput> Build();
        AbstractCallBuilder<TReturn, TInput> LogWith(ILog log);
    }

    public interface IAbstractCallBuilder<TReturn>
    {
        AbstractCallBuilder<TReturn> Authentication(AuthenticationHeaderValue auth);
        IContractCaller<TReturn> Build();
        AbstractCallBuilder<TReturn> LogWith(ILog log);
    } 
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Conflux.API.Builders
{
    public interface IContractCaller<TReturn, TInput>
    {
        Task<TReturn> CallAsync(TInput input);
    }

    public interface IContractCaller<TReturn>
    {
        Task<TReturn> CallAsync();
    }
}

using Conflux.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conflux.API.Builders
{
    public interface ITranscationSender<TInput> where TInput : class
    {
        Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(TInput parameters = null);
        Task<string> SendTranscationAsync(TInput parameters = null);
    }

    public interface ITranscationSender
    {
        Task<TransactionReceipt> SendRequestAndWaitForReceiptAsync(params object[] parameters);
        Task<string> SendTranscationAsync(params object[] parameters);
    }
}

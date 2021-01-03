using Conflux.ABI.FunctionEncoding.Attributes;
using Conflux.Contracts;

namespace Conflux.GSN.DTOs
{
    [Function("depositFor")]
    public class DepositForFunction : FunctionMessage
    {
        [Parameter("address", "target", 1)]
        public string Target { get; set; }
    }
}

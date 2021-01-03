using Conflux.ABI.FunctionEncoding.Attributes;
using Conflux.Contracts;

namespace Conflux.GSN.DTOs
{
    [Function("getHubAddr", "address")]
    public class GetHubAddrFunction : FunctionMessage
    {
    }
}

using Conflux.ABI.FunctionEncoding.Attributes;
using Conflux.Contracts;

namespace Conflux.GSN.DTOs
{
    [Function("version", "string")]
    public class VersionFunction : FunctionMessage
    {
    }
}

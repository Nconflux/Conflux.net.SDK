using System;

namespace Conflux.Quorum.Enclave
{
    public class QuorumEnclaveRequestException : Exception
    {
        public QuorumEnclaveRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
using System;


namespace Conflux.JsonRpc.UnityClient
{
    public class UnityRequest<TResult>
    {
        public TResult Result { get; set; }
        public Exception Exception { get; set; }
    }
}  

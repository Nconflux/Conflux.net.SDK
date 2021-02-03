namespace Conflux.JsonRpc.Client
{
    public class RpcRequest
    {
        public RpcRequest(object id, string method, params object[] parameterList)
        {
            Id = id;
            Method = method;
            try
            {
                //existed embeded.
                var x = ((object[])parameterList[0]);

                RawParameters = (object[])parameterList[0];
            }
            catch (System.Exception ex)
            {
                RawParameters = parameterList;
            }
        }

        public object Id { get; set; }
        public string Method { get; private set; }
        public object[] RawParameters { get; private set; }
    }
}
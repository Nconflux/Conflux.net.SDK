using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Conflux.JsonRpc.Client;
using Conflux.JsonRpc.Client.Streaming;
using Conflux.JsonRpc.WebSocketStreamingClient;

namespace Conflux.RPC.Reactive.RpcStreaming
{
    public class RpcStreamingResponseObservableHandler<TResponse> : RpcStreamingRequestResponseHandler<TResponse>
    {
        protected Subject<TResponse> ResponseSubject { get; set; }

        protected RpcStreamingResponseObservableHandler(IStreamingClient streamingClient):base(streamingClient)
        { 
            ResponseSubject = new Subject<TResponse>();
        }

        public IObservable<TResponse> GetResponseAsObservable()
        {
            return ResponseSubject.AsObservable();
        }

        protected override void HandleResponse(TResponse subscriptionDataResponse)
        {
            ResponseSubject.OnNext(subscriptionDataResponse);
            ResponseSubject.OnCompleted();
        }

        protected override void HandleResponseError(RpcResponseException exception)
        {
            ResponseSubject.OnError(exception);
        }
    }
}

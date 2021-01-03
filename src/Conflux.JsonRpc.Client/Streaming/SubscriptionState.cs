namespace Conflux.JsonRpc.Client.Streaming
{

    public enum SubscriptionState
    {
        Idle,
        Subscribing,
        Subscribed,
        Unsubscribing,
        Unsubscribed
    }
}
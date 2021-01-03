﻿using Conflux.Geth.RPC.Admin;

namespace Conflux.Geth
{
    public interface IAdminApiService
    {
        IAdminAddPeer AddPeer { get; }
        IAdminDatadir Datadir { get; }
        IAdminNodeInfo NodeInfo { get; }
        IAdminPeers Peers { get; }
        IAdminSetSolc SetSolc { get; }
        IAdminStartRPC StartRPC { get; }
        IAdminStartWS StartWS { get; }
        IAdminStopRPC StopRPC { get; }
        IAdminStopWS StopWS { get; }
    }
}
﻿namespace Conflux.GSN.Models
{
    public enum RelayCallStatus
    {
        OK,
        RelayedCallFailed,
        PreRelayedFailed,
        PostRelayedFailed,
        RecipientBalanceChanged
    }
}

using Conflux.ABI.FunctionEncoding.Attributes;
using Conflux.Contracts;
using System.Collections.Generic;
using System.Numerics;

namespace NConflux.Explorer.DemoContract.Hina.ContractDefinition
{


    public partial class HinaDeployment : HinaDeploymentBase
    {
        public HinaDeployment() : base(BYTECODE) { }
        public HinaDeployment(string byteCode) : base(byteCode) { }
    }

    public class HinaDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b5061065b806100206000396000f3fe60806040526004361061004a5760003560e01c80634e7f12641461004f57806359016c79146100c157806393c6b517146100c1578063cd324bfd1461014b578063da0321cd146101e8575b600080fd5b6100bf6004803603602081101561006557600080fd5b81019060208101813564010000000081111561008057600080fd5b82018360208201111561009257600080fd5b803590602001918460018302840111640100000000831117156100b457600080fd5b50909250905061024d565b005b3480156100cd57600080fd5b506100d6610373565b6040805160208082528351818301528351919283929083019185019080838360005b838110156101105781810151838201526020016100f8565b50505050905090810190601f16801561013d5780820380516001836020036101000a031916815260200191505b509250505060405180910390f35b34801561015757600080fd5b50610160610409565b6040518080602001836001600160a01b03168152602001828103825284818151815260200191508051906020019080838360005b838110156101ac578181015183820152602001610194565b50505050905090810190601f1680156101d95780820380516001836020036101000a031916815260200191505b50935050505060405180910390f35b3480156101f457600080fd5b506101fd6104b8565b60408051602080825283518183015283519192839290830191858101910280838360005b83811015610239578181015183820152602001610221565b505050509050019250505060405180910390f35b61025960008383610592565b50600180546001600160a01b0319163317808255604080516001600160a01b03929092168083523491830182905260606020840181815260008054600260001998821615610100029890980116969096049185018290527f5a8bc123059083b625bed1b7e3bb220ace57a184a65d86f92dda51dfdac2b5239592949293929160808301908590801561032c5780601f106103015761010080835404028352916020019161032c565b820191906000526020600020905b81548152906001019060200180831161030f57829003601f168201915b505094505050505060405180910390a17fd12e2a3d98cbefa162c9e2377c4c27e40ae9421c596d4a2d2e08f713a26268b95a60408051918252519081900360200190a15050565b60008054604080516020601f60026000196101006001881615020190951694909404938401819004810282018101909252828152606093909290918301828280156103ff5780601f106103d4576101008083540402835291602001916103ff565b820191906000526020600020905b8154815290600101906020018083116103e257829003601f168201915b5050505050905090565b6001805460008054604080516020601f600260001998861615610100029890980190941696909604928301869004860281018601909152818152606094929384936001600160a01b039091169284918301828280156104a95780601f1061047e576101008083540402835291602001916104a9565b820191906000526020600020905b81548152906001019060200180831161048c57829003601f168201915b50505050509150915091509091565b60408051600480825260a082019092526060916020820160808036833701905050905033816000815181106104e957fe5b6001600160a01b039283166020918202929092010152600180548351921691839190811061051357fe5b60200260200101906001600160a01b031690816001600160a01b031681525050328160028151811061054157fe5b60200260200101906001600160a01b031690816001600160a01b031681525050308160038151811061056f57fe5b60200260200101906001600160a01b031690816001600160a01b03168152505090565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f106105d35782800160ff19823516178555610600565b82800160010185558215610600579182015b828111156106005782358255916020019190600101906105e5565b5061060c929150610610565b5090565b5b8082111561060c576000815560010161061156fea2646970667358221220cf14ed06b11327f3c7c7ab44f8902dd20f16c013e5776e1b8f18b6f6615f58e864736f6c634300060c0033";
        public HinaDeploymentBase() : base(BYTECODE) { }
        public HinaDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AddressesFunction : AddressesFunctionBase { }

    [Function("addresses", "address[]")]
    public class AddressesFunctionBase : FunctionMessage
    {

    }

    public partial class GetContentFunction : GetContentFunctionBase { }

    [Function("getContent", "string")]
    public class GetContentFunctionBase : FunctionMessage
    {

    }

    public partial class GetContentWithIndentifierFunction : GetContentWithIndentifierFunctionBase { }

    [Function("getContentWithIndentifier", "string")]
    public class GetContentWithIndentifierFunctionBase : FunctionMessage
    {

    }

    public partial class LeaveMessageFunction : LeaveMessageFunctionBase { }

    [Function("leaveMessage")]
    public class LeaveMessageFunctionBase : FunctionMessage
    {
        [Parameter("string", "_message", 1)]
        public virtual string Message { get; set; }
    }

    public partial class MessageWithSenderFunction : MessageWithSenderFunctionBase { }

    [Function("messageWithSender", typeof(MessageWithSenderOutputDTO))]
    public class MessageWithSenderFunctionBase : FunctionMessage
    {

    }

    public partial class GasInfoEventDTO : GasInfoEventDTOBase { }

    [Event("GasInfo")]
    public class GasInfoEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "msgGas", 1, false)]
        public virtual BigInteger MsgGas { get; set; }
    }

    public partial class LeaveMessageEventDTO : LeaveMessageEventDTOBase { }

    [Event("LeaveMessage")]
    public class LeaveMessageEventDTOBase : IEventDTO
    {
        [Parameter("address", "from", 1, false)]
        public virtual string From { get; set; }
        [Parameter("string", "message", 2, false)]
        public virtual string Message { get; set; }
        [Parameter("uint256", "payed", 3, false)]
        public virtual BigInteger Payed { get; set; }
    }

    public partial class AddressesOutputDTO : AddressesOutputDTOBase { }

    [FunctionOutput]
    public class AddressesOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address[]", "ret", 1)]
        public virtual List<string> Ret { get; set; }
    }

    public partial class GetContentOutputDTO : GetContentOutputDTOBase { }

    [FunctionOutput]
    public class GetContentOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("string", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class GetContentWithIndentifierOutputDTO : GetContentWithIndentifierOutputDTOBase { }

    [FunctionOutput]
    public class GetContentWithIndentifierOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("string", "message", 1)]
        public virtual string Message { get; set; }
    }



    public partial class MessageWithSenderOutputDTO : MessageWithSenderOutputDTOBase { }

    [FunctionOutput]
    public class MessageWithSenderOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("string", "message", 1)]
        public virtual string Message { get; set; }
        [Parameter("address", "", 2)]
        public virtual string ReturnValue2 { get; set; }
    }
}

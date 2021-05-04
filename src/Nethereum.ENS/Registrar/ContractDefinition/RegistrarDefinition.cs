using System.Collections.Generic;
using System.Numerics;
using Conflux.ABI.FunctionEncoding.Attributes;
using Conflux.Contracts;

namespace Conflux.ENS.Registrar.ContractDefinition
{
    
    
    public partial class RegistrarDeployment:RegistrarDeploymentBase
    {
        public RegistrarDeployment():base(BYTECODE) { }
        
        public RegistrarDeployment(string byteCode):base(byteCode) { }
    }

    public class RegistrarDeploymentBase:ContractDeploymentMessage
    {
        
        public static string BYTECODE = "608060405234801561001057600080fd5b5060405160608061299483398101604090815281516020830151919092015160008054600160a060020a031916600160a060020a0385161781556001839055811161005b574261005d565b805b600455505050612922806100726000396000f3006080604052600436106101195763ffffffff60e060020a6000350416630230a07c811461011e57806313c89a8f1461013857806315f733311461016257806322ec1244146101bb5780632525f5c1146101e5578063267b6922146102095780633f15457f1461026957806347872b421461029a5780635ddae283146102b85780635e431709146102d057806361d585da146102f457806379ce9fac146103305780639350333714610354578063983b94fb146103835780639c67f06f1461039b578063ae1a0b0c146103b0578063ce92dced146103de578063de10f04b146103e9578063e27fe50f1461043e578063ea9e107a14610493578063ede8acdb146104ba578063faff50a8146104d2578063febefd61146104e7575b600080fd5b34801561012a57600080fd5b50610136600435610531565b005b34801561014457600080fd5b5061015060043561077f565b60408051918252519081900360200190f35b34801561016e57600080fd5b506040805160206004803580820135601f81018490048402850184019095528484526101369436949293602493928401919081908401838280828437509497506107a39650505050505050565b3480156101c757600080fd5b50610150600435600160a060020a0360243516604435606435610b22565b3480156101f157600080fd5b50610136600160a060020a0360043516602435610b66565b34801561021557600080fd5b50610221600435610d58565b6040518086600581111561023157fe5b60ff168152600160a060020a0390951660208601525060408085019390935260608401919091526080830152519081900360a0019150f35b34801561027557600080fd5b5061027e610da4565b60408051600160a060020a039092168252519081900360200190f35b3480156102a657600080fd5b50610136600435602435604435610db3565b3480156102c457600080fd5b50610136600435611389565b3480156102dc57600080fd5b5061027e600160a060020a0360043516602435611621565b34801561030057600080fd5b5061030c600435611647565b6040518082600581111561031c57fe5b60ff16815260200191505060405180910390f35b34801561033c57600080fd5b50610136600435600160a060020a03602435166116bd565b34801561036057600080fd5b5061036f600435602435611823565b604080519115158252519081900360200190f35b34801561038f57600080fd5b50610136600435611839565b3480156103a757600080fd5b50610150611afb565b3480156103bc57600080fd5b506103c5611b01565b6040805163ffffffff9092168252519081900360200190f35b610136600435611b08565b3480156103f557600080fd5b506040805160206004803580820135838102808601850190965280855261013695369593946024949385019291829185019084908082843750949750611bf39650505050505050565b34801561044a57600080fd5b506040805160206004803580820135838102808601850190965280855261013695369593946024949385019291829185019084908082843750949750611c509650505050505050565b34801561049f57600080fd5b50610136600435600160a060020a0360243516604435611c88565b3480156104c657600080fd5b50610136600435611c8d565b3480156104de57600080fd5b50610150611dea565b60408051602060048035808201358381028086018501909652808552610136953695939460249493850192918291850190849080828437509497505093359450611df09350505050565b60008082600261054082611647565b600581111561054b57fe5b1480156105e05750600081815260026020908152604080832054815160e060020a638da5cb5b0281529151600160a060020a0390911693638da5cb5b93600480850194919392918390030190829087803b1580156105a857600080fd5b505af11580156105bc573d6000803e3d6000fd5b505050506040513d60208110156105d257600080fd5b5051600160a060020a031633145b15156105eb57600080fd5b600084815260026020526040902080546001820154919450600160a060020a031692506301e1338001421015806106ad5750600080546001546040805160e060020a6302571be30281526004810192909252513093600160a060020a03909316926302571be392602480820193602093909283900390910190829087803b15801561067557600080fd5b505af1158015610689573d6000803e3d6000fd5b505050506040513d602081101561069f57600080fd5b5051600160a060020a031614155b15156106b857600080fd5b60006002840181905560038401558254600160a060020a03191683556106dd84611e02565b81600160a060020a031663bbe427716103e86040518263ffffffff1660e060020a02815260040180828152602001915050600060405180830381600087803b15801561072857600080fd5b505af115801561073c573d6000803e3d6000fd5b505050600284015460408051918252518692507f292b79b9246fa2c8e77d3fe195b251f9cb839d7d038e667c069ee7708c631e169181900360200190a250505050565b6004547001000000000000000000000000000000006249d400818404020401919050565b600080826040518082805190602001908083835b602083106107d65780518252601f1990920191602091820191016107b7565b5181516020939093036101000a6000190180199091169216919091179052604051920182900390912092506002915081905061081183611647565b600581111561081c57fe5b1461082657600080fd5b600661083186612039565b111561083c57600080fd5b846040518082805190602001908083835b6020831061086c5780518252601f19909201916020918201910161084d565b51815160209384036101000a60001901801990921691161790526040805192909401829003909120600081815260029092529290209197509095506108b69250869150611e029050565b8254600160a060020a031615610a61576108db8360020154662386f26fc100006120e3565b60028481018290558454604080517fb0c809720000000000000000000000000000000000000000000000000000000081529290930460048301526000602483018190529251600160a060020a039091169263b0c80972926044808201939182900301818387803b15801561094e57600080fd5b505af1158015610962573d6000803e3d6000fd5b50508454604080517f13af40350000000000000000000000000000000000000000000000000000000081523360048201529051600160a060020a0390921693506313af4035925060248082019260009290919082900301818387803b1580156109ca57600080fd5b505af11580156109de573d6000803e3d6000fd5b50508454604080517fbbe427710000000000000000000000000000000000000000000000000000000081526103e860048201529051600160a060020a03909216935063bbe42771925060248082019260009290919082900301818387803b158015610a4857600080fd5b505af1158015610a5c573d6000803e3d6000fd5b505050505b846040518082805190602001908083835b60208310610a915780518252601f199092019160209182019101610a72565b51815160209384036101000a60001901801990921691161790526040805192909401829003822060028a015460018b01549084529183019190915283519095508994507f1f9c649fe47e58bb60f4e52f0d90e4c47a526c9f90c5113df842c025970b66ad93918190039091019150a3505060006002820181905560038201558054600160a060020a03191690555050565b60408051948552600160a060020a03939093166c01000000000000000000000000026020850152603484019190915260548301525160749181900391909101902090565b600160a060020a038083166000908152600360209081526040808320858452909152902054168015801590610c1457506206978063ffffffff1681600160a060020a03166305b344106040518163ffffffff1660e060020a028152600401602060405180830381600087803b158015610bde57600080fd5b505af1158015610bf2573d6000803e3d6000fd5b505050506040513d6020811015610c0857600080fd5b50510162127500014210155b1515610c1f57600080fd5b604080517f13af40350000000000000000000000000000000000000000000000000000000081523360048201529051600160a060020a038316916313af403591602480830192600092919082900301818387803b158015610c7f57600080fd5b505af1158015610c93573d6000803e3d6000fd5b5050505080600160a060020a031663bbe4277160056040518263ffffffff1660e060020a02815260040180828152602001915050600060405180830381600087803b158015610ce157600080fd5b505af1158015610cf5573d6000803e3d6000fd5b50505050600160a060020a038316600081815260036020908152604080832086845282528083208054600160a060020a03191690558051928352600591830191909152805185926000805160206128d783398151915292908290030190a3505050565b60008181526002602052604081208190819081908190610d7787611647565b815460018301546002840154600390940154929a600160a060020a03909216995097509195509350915050565b600054600160a060020a031681565b600080600080600080610dc889338a8a610b22565b336000908152600360209081526040808320848452909152902054909650600160a060020a03169450841515610dfd57600080fd5b33600090815260036020908152604080832089845282528083208054600160a060020a03191690558b83526002825280832081517f3fa4f2450000000000000000000000000000000000000000000000000000000081529151909750610ebf938c93600160a060020a038b1693633fa4f2459360048083019491928390030190829087803b158015610e8e57600080fd5b505af1158015610ea2573d6000803e3d6000fd5b505050506040513d6020811015610eb857600080fd5b50516120fb565b604080517fb0c8097200000000000000000000000000000000000000000000000000000000815260048101839052600160248201529051919450600160a060020a0387169163b0c809729160448082019260009290919082900301818387803b158015610f2b57600080fd5b505af1158015610f3f573d6000803e3d6000fd5b50505050610f4c89611647565b91506002826005811115610f5c57fe5b1415610ff65784600160a060020a031663bbe4277160056040518263ffffffff1660e060020a02815260040180828152602001915050600060405180830381600087803b158015610fac57600080fd5b505af1158015610fc0573d6000803e3d6000fd5b5050604080518681526001602082015281513394508d93506000805160206128d7833981519152929181900390910190a361137e565b600482600581111561100457fe5b1461100e57600080fd5b662386f26fc1000083108061109a57506202a30063ffffffff1684600101540385600160a060020a03166305b344106040518163ffffffff1660e060020a028152600401602060405180830381600087803b15801561106c57600080fd5b505af1158015611080573d6000803e3d6000fd5b505050506040513d602081101561109657600080fd5b5051115b156111345784600160a060020a031663bbe427716103e36040518263ffffffff1660e060020a02815260040180828152602001915050600060405180830381600087803b1580156110ea57600080fd5b505af11580156110fe573d6000803e3d6000fd5b5050604080518681526000602082015281513394508d93506000805160206128d7833981519152929181900390910190a361137e565b836003015483111561122e578354600160a060020a0316156111d157508254604080517fbbe427710000000000000000000000000000000000000000000000000000000081526103e360048201529051600160a060020a0390921691829163bbe4277191602480830192600092919082900301818387803b1580156111b857600080fd5b505af11580156111cc573d6000803e3d6000fd5b505050505b600384018054600280870191909155908490558454600160a060020a031916600160a060020a038716178555604080518581526020810192909252805133928c926000805160206128d783398151915292918290030190a361137e565b83600201548311156112ed5760028401839055604080517fbbe427710000000000000000000000000000000000000000000000000000000081526103e360048201529051600160a060020a0387169163bbe4277191602480830192600092919082900301818387803b1580156112a357600080fd5b505af11580156112b7573d6000803e3d6000fd5b5050604080518681526003602082015281513394508d93506000805160206128d7833981519152929181900390910190a361137e565b84600160a060020a031663bbe427716103e36040518263ffffffff1660e060020a02815260040180828152602001915050600060405180830381600087803b15801561133857600080fd5b505af115801561134c573d6000803e3d6000fd5b5050604080518681526004602082015281513394508d93506000805160206128d7833981519152929181900390910190a35b505050505050505050565b60008082600261139882611647565b60058111156113a357fe5b1480156114385750600081815260026020908152604080832054815160e060020a638da5cb5b0281529151600160a060020a0390911693638da5cb5b93600480850194919392918390030190829087803b15801561140057600080fd5b505af1158015611414573d6000803e3d6000fd5b505050506040513d602081101561142a57600080fd5b5051600160a060020a031633145b151561144357600080fd5b600080546001546040805160e060020a6302571be3028152600481019290925251600160a060020a03909216926302571be3926024808401936020939083900390910190829087803b15801561149857600080fd5b505af11580156114ac573d6000803e3d6000fd5b505050506040513d60208110156114c257600080fd5b50519250600160a060020a0383163014156114dc57600080fd5b600084815260026020526040808220805482517ffaab9d39000000000000000000000000000000000000000000000000000000008152600160a060020a038881166004830152935192965092169263faab9d39926024808201939182900301818387803b15801561154c57600080fd5b505af1158015611560573d6000803e3d6000fd5b505083546001850154604080517fea9e107a000000000000000000000000000000000000000000000000000000008152600481018a9052600160a060020a039384166024820152604481019290925251918716935063ea9e107a925060648082019260009290919082900301818387803b1580156115dd57600080fd5b505af11580156115f1573d6000803e3d6000fd5b50508354600160a060020a0319168455505060006001830181905560028301819055600390920191909155505050565b6003602090815260009283526040808420909152908252902054600160a060020a031681565b600081815260026020526040812061165f8342611823565b151561166e57600591506116b7565b806001015442101561169e5760018101546202a2ff190142101561169557600191506116b7565b600491506116b7565b600381015415156116b257600091506116b7565b600291505b50919050565b60008260026116cb82611647565b60058111156116d657fe5b14801561176b5750600081815260026020908152604080832054815160e060020a638da5cb5b0281529151600160a060020a0390911693638da5cb5b93600480850194919392918390030190829087803b15801561173357600080fd5b505af1158015611747573d6000803e3d6000fd5b505050506040513d602081101561175d57600080fd5b5051600160a060020a031633145b151561177657600080fd5b600160a060020a038316151561178b57600080fd5b600084815260026020526040808220805482517f13af4035000000000000000000000000000000000000000000000000000000008152600160a060020a03888116600483015293519296509216926313af4035926024808201939182900301818387803b1580156117fb57600080fd5b505af115801561180f573d6000803e3d6000fd5b5050505061181d848461210c565b50505050565b600061182e8361077f565b821190505b92915050565b600081600261184782611647565b600581111561185257fe5b1480156118e75750600081815260026020908152604080832054815160e060020a638da5cb5b0281529151600160a060020a0390911693638da5cb5b93600480850194919392918390030190829087803b1580156118af57600080fd5b505af11580156118c3573d6000803e3d6000fd5b505050506040513d60208110156118d957600080fd5b5051600160a060020a031633145b15156118f257600080fd5b60008381526002602081905260409091209081015490925061191b90662386f26fc100006120e3565b600283018190558254604080517fb0c8097200000000000000000000000000000000000000000000000000000000815260048101939093526001602484015251600160a060020a039091169163b0c8097291604480830192600092919082900301818387803b15801561198d57600080fd5b505af11580156119a1573d6000803e3d6000fd5b50505050611a2f838360000160009054906101000a9004600160a060020a0316600160a060020a0316638da5cb5b6040518163ffffffff1660e060020a028152600401602060405180830381600087803b1580156119fe57600080fd5b505af1158015611a12573d6000803e3d6000fd5b505050506040513d6020811015611a2857600080fd5b505161210c565b81546040805160e060020a638da5cb5b0281529051600160a060020a0390921691638da5cb5b916004808201926020929091908290030181600087803b158015611a7857600080fd5b505af1158015611a8c573d6000803e3d6000fd5b505050506040513d6020811015611aa257600080fd5b5051600283015460018401546040805192835260208301919091528051600160a060020a039093169286927f0f0c27adfd84b60b6f456b0e87cdccb1e5fb9603991588d87fa99f5b6b61e67092908290030190a3505050565b60045481565b6249d40081565b336000908152600360209081526040808320848452909152812054600160a060020a031615611b3657600080fd5b662386f26fc10000341015611b4a57600080fd5b3433611b546123f1565b600160a060020a039091168152604051908190036020019082f080158015611b80573d6000803e3d6000fd5b503360008181526003602090815260408083208884528252918290208054600160a060020a031916600160a060020a0386161790558151348152915193955091935085927fb556ff269c1b6714f432c36431e2041d28436a73b6c3f19c021827bbdc6bfc29929181900390910190a35050565b80511515611c0057600080fd5b6002611c26826001845103815181101515611c1757fe5b90602001906020020151611647565b6005811115611c3157fe5b1415611c3c57600080fd5b611c4d600182510382600154612212565b50565b60005b8151811015611c8457611c7c8282815181101515611c6d57fe5b90602001906020020151611c8d565b600101611c53565b5050565b505050565b6000806004544210158015611caa5750600454630784ce00014211155b8015611d405750600080546001546040805160e060020a6302571be30281526004810192909252513093600160a060020a03909316926302571be392602480820193602093909283900390910190829087803b158015611d0957600080fd5b505af1158015611d1d573d6000803e3d6000fd5b505050506040513d6020811015611d3357600080fd5b5051600160a060020a0316145b1515611d4b57600080fd5b611d5483611647565b91506001826005811115611d6457fe5b1415611d6f57611c88565b6000826005811115611d7d57fe5b14611d8757600080fd5b50600082815260026020818152604080842042620697800160018201819055938101859055600381019490945580519283525185927f87e97e825a1d1fa0c54e1d36c7506c1dea8b1efd451fe68b000cf96f7cf4000392908290030190a2505050565b60015481565b611df982611c50565b611c8481611b08565b600080546001546040805160e060020a6302571be30281526004810192909252513092600160a060020a0316916302571be391602480830192602092919082900301818887803b158015611e5557600080fd5b505af1158015611e69573d6000803e3d6000fd5b505050506040513d6020811015611e7f57600080fd5b5051600160a060020a03161415611c845760008054600154604080517f06ab592300000000000000000000000000000000000000000000000000000000815260048101929092526024820186905230604483015251600160a060020a03909216926306ab59239260648084019382900301818387803b158015611f0157600080fd5b505af1158015611f15573d6000803e3d6000fd5b5050600154604080519182526020820186905280519182900381018220600080547f1896f70a00000000000000000000000000000000000000000000000000000000855260048501839052602485018290529251919650600160a060020a039092169450631896f70a935060448084019382900301818387803b158015611f9b57600080fd5b505af1158015611faf573d6000803e3d6000fd5b505060008054604080517f5b0fc9c300000000000000000000000000000000000000000000000000000000815260048101879052602481018490529051600160a060020a039092169450635b0fc9c39350604480820193929182900301818387803b15801561201d57600080fd5b505af1158015612031573d6000803e3d6000fd5b505050505050565b805160009060018381019184010182805b828410156120da5750825160ff16608081101561206c576001840193506120cf565b60e08160ff161015612083576002840193506120cf565b60f08160ff16101561209a576003840193506120cf565b60f88160ff1610156120b1576004840193506120cf565b60fc8160ff1610156120c8576005840193506120cf565b6006840193505b60019091019061204a565b50949350505050565b6000818311156120f4575081611833565b5080611833565b6000818310156120f4575081611833565b600080546001546040805160e060020a6302571be30281526004810192909252513093600160a060020a03909316926302571be392602480820193602093909283900390910190829087803b15801561216457600080fd5b505af1158015612178573d6000803e3d6000fd5b505050506040513d602081101561218e57600080fd5b5051600160a060020a03161415611c845760008054600154604080517f06ab5923000000000000000000000000000000000000000000000000000000008152600481019290925260248201869052600160a060020a03858116604484015290519216926306ab59239260648084019382900301818387803b15801561201d57600080fd5b6000548251600160a060020a03909116906306ab592390839085908790811061223757fe5b602090810290910101516040805160e060020a63ffffffff86160281526004810193909352602483019190915230604483015251606480830192600092919082900301818387803b15801561228b57600080fd5b505af115801561229f573d6000803e3d6000fd5b505050508082848151811015156122b257fe5b60209081029091018101516040805193845291830152805191829003019020905060008311156122ea576122ea600184038383612212565b60008054604080517f1896f70a00000000000000000000000000000000000000000000000000000000815260048101859052602481018490529051600160a060020a0390921692631896f70a9260448084019382900301818387803b15801561235257600080fd5b505af1158015612366573d6000803e3d6000fd5b505060008054604080517f5b0fc9c300000000000000000000000000000000000000000000000000000000815260048101879052602481018490529051600160a060020a039092169450635b0fc9c39350604480820193929182900301818387803b1580156123d457600080fd5b505af11580156123e8573d6000803e3d6000fd5b50505050505050565b6040516104d58061240283390190560060806040526040516020806104d5833981016040525160018054600160a060020a03909216600160a060020a0319928316178155600080549092163317909155426003556005805460ff1916909117905534600455610472806100636000396000f3006080604052600436106100a35763ffffffff7c010000000000000000000000000000000000000000000000000000000060003504166305b3441081146100a85780630b5ab3d5146100cf57806313af4035146100e65780632b20e397146101075780633fa4f24514610138578063674f220f1461014d5780638da5cb5b14610162578063b0c8097214610177578063bbe4277114610194578063faab9d39146101ac575b600080fd5b3480156100b457600080fd5b506100bd6101cd565b60408051918252519081900360200190f35b3480156100db57600080fd5b506100e46101d3565b005b3480156100f257600080fd5b506100e4600160a060020a0360043516610218565b34801561011357600080fd5b5061011c6102b6565b60408051600160a060020a039092168252519081900360200190f35b34801561014457600080fd5b506100bd6102c5565b34801561015957600080fd5b5061011c6102cb565b34801561016e57600080fd5b5061011c6102da565b34801561018357600080fd5b506100e460043560243515156102e9565b3480156101a057600080fd5b506100e4600435610369565b3480156101b857600080fd5b506100e4600160a060020a0360043516610400565b60035481565b60055460ff16156101e357600080fd5b600154604051600160a060020a0390911690303180156108fc02916000818181858888f19350505050156102165761deadff5b565b600054600160a060020a0316331461022f57600080fd5b600160a060020a038116151561024457600080fd5b600180546002805473ffffffffffffffffffffffffffffffffffffffff19908116600160a060020a03808516919091179092559084169116811790915560408051918252517fa2ea9883a321a3e97b8266c2b078bfeec6d50c711ed71f874a90d500ae2eaf369181900360200190a150565b600054600160a060020a031681565b60045481565b600254600160a060020a031681565b600154600160a060020a031681565b600054600160a060020a0316331461030057600080fd5b60055460ff16151561031157600080fd5b60045482111561032057600080fd5b6004829055600154604051600160a060020a0390911690303184900380156108fc02916000818181858888f193505050508061035a575080155b151561036557600080fd5b5050565b600054600160a060020a0316331461038057600080fd5b60055460ff16151561039157600080fd5b6005805460ff1916905560405161dead906103e83031848203020480156108fc02916000818181858888f1935050505015156103cc57600080fd5b6040517fbb2ce2f51803bba16bc85282b47deeea9a5c6223eabea1077be696b3f265cf1390600090a16103fd6101d3565b50565b600054600160a060020a0316331461041757600080fd5b6000805473ffffffffffffffffffffffffffffffffffffffff1916600160a060020a03929092169190911790555600a165627a7a7230582035d2001b67491c12934109329348e960c6740bd46d7e626189aec1deb86867a700297b6c4b278d165a6b33958f8ea5dfb00c8c9d4d0acf1985bef5d10786898bc3e7a165627a7a723058202edc7779938442c58c2504db9a374d437392e818ab9694c876299c7192fbe86d0029";
        
        public RegistrarDeploymentBase():base(BYTECODE) { }
        
        public RegistrarDeploymentBase(string byteCode):base(byteCode) { }
        
        [Parameter("address", "_ens", 1)]
        public virtual string Ens {get; set;}
        [Parameter("bytes32", "_rootNode", 2)]
        public virtual byte[] RootNode {get; set;}
        [Parameter("uint256", "_startDate", 3)]
        public virtual BigInteger StartDate {get; set;}
    }    
    
    public partial class ReleaseDeedFunction:ReleaseDeedFunctionBase{}

    [Function("releaseDeed")]
    public class ReleaseDeedFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "_hash", 1)]
        public virtual byte[] Hash {get; set;}
    }    
    
    public partial class GetAllowedTimeFunction:GetAllowedTimeFunctionBase{}

    [Function("getAllowedTime", "uint256")]
    public class GetAllowedTimeFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "_hash", 1)]
        public virtual byte[] Hash {get; set;}
    }    
    
    public partial class InvalidateNameFunction:InvalidateNameFunctionBase{}

    [Function("invalidateName")]
    public class InvalidateNameFunctionBase:FunctionMessage
    {
        [Parameter("string", "unhashedName", 1)]
        public virtual string UnhashedName {get; set;}
    }    
    
    public partial class ShaBidFunction:ShaBidFunctionBase{}

    [Function("shaBid", "bytes32")]
    public class ShaBidFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "hash", 1)]
        public virtual byte[] Hash {get; set;}
        [Parameter("address", "owner", 2)]
        public virtual string Owner {get; set;}
        [Parameter("uint256", "value", 3)]
        public virtual BigInteger Value {get; set;}
        [Parameter("bytes32", "salt", 4)]
        public virtual byte[] Salt {get; set;}
    }    
    
    public partial class CancelBidFunction:CancelBidFunctionBase{}

    [Function("cancelBid")]
    public class CancelBidFunctionBase:FunctionMessage
    {
        [Parameter("address", "bidder", 1)]
        public virtual string Bidder {get; set;}
        [Parameter("bytes32", "seal", 2)]
        public virtual byte[] Seal {get; set;}
    }    
    
    public partial class EntriesFunction:EntriesFunctionBase{}

    [Function("entries", typeof(EntriesOutputDTO))]
    public class EntriesFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "_hash", 1)]
        public virtual byte[] Hash {get; set;}
    }    
    
    public partial class EnsFunction:EnsFunctionBase{}

    [Function("ens", "address")]
    public class EnsFunctionBase:FunctionMessage
    {

    }    
    
    public partial class UnsealBidFunction:UnsealBidFunctionBase{}

    [Function("unsealBid")]
    public class UnsealBidFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "_hash", 1)]
        public virtual byte[] Hash {get; set;}
        [Parameter("uint256", "_value", 2)]
        public virtual BigInteger Value {get; set;}
        [Parameter("bytes32", "_salt", 3)]
        public virtual byte[] Salt {get; set;}
    }    
    
    public partial class TransferRegistrarsFunction:TransferRegistrarsFunctionBase{}

    [Function("transferRegistrars")]
    public class TransferRegistrarsFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "_hash", 1)]
        public virtual byte[] Hash {get; set;}
    }    
    
    public partial class SealedBidsFunction:SealedBidsFunctionBase{}

    [Function("sealedBids", "address")]
    public class SealedBidsFunctionBase:FunctionMessage
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 {get; set;}
        [Parameter("bytes32", "", 2)]
        public virtual byte[] ReturnValue2 {get; set;}
    }    
    
    public partial class StateFunction:StateFunctionBase{}

    [Function("state", "uint8")]
    public class StateFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "_hash", 1)]
        public virtual byte[] Hash {get; set;}
    }    
    
    public partial class TransferFunction:TransferFunctionBase{}

    [Function("transfer")]
    public class TransferFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "_hash", 1)]
        public virtual byte[] Hash {get; set;}
        [Parameter("address", "newOwner", 2)]
        public virtual string NewOwner {get; set;}
    }    
    
    public partial class IsAllowedFunction:IsAllowedFunctionBase{}

    [Function("isAllowed", "bool")]
    public class IsAllowedFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "_hash", 1)]
        public virtual byte[] Hash {get; set;}
        [Parameter("uint256", "_timestamp", 2)]
        public virtual BigInteger Timestamp {get; set;}
    }    
    
    public partial class FinalizeAuctionFunction:FinalizeAuctionFunctionBase{}

    [Function("finalizeAuction")]
    public class FinalizeAuctionFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "_hash", 1)]
        public virtual byte[] Hash {get; set;}
    }    
    
    public partial class RegistryStartedFunction:RegistryStartedFunctionBase{}

    [Function("registryStarted", "uint256")]
    public class RegistryStartedFunctionBase:FunctionMessage
    {

    }    
    
    public partial class LaunchLengthFunction:LaunchLengthFunctionBase{}

    [Function("launchLength", "uint32")]
    public class LaunchLengthFunctionBase:FunctionMessage
    {

    }    
    
    public partial class NewBidFunction:NewBidFunctionBase{}

    [Function("newBid")]
    public class NewBidFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "sealedBid", 1)]
        public virtual byte[] SealedBid {get; set;}
    }    
    
    public partial class EraseNodeFunction:EraseNodeFunctionBase{}

    [Function("eraseNode")]
    public class EraseNodeFunctionBase:FunctionMessage
    {
        [Parameter("bytes32[]", "labels", 1)]
        public virtual List<byte[]> Labels {get; set;}
    }    
    
    public partial class StartAuctionsFunction:StartAuctionsFunctionBase{}

    [Function("startAuctions")]
    public class StartAuctionsFunctionBase:FunctionMessage
    {
        [Parameter("bytes32[]", "_hashes", 1)]
        public virtual List<byte[]> Hashes {get; set;}
    }    
    
    public partial class AcceptRegistrarTransferFunction:AcceptRegistrarTransferFunctionBase{}

    [Function("acceptRegistrarTransfer")]
    public class AcceptRegistrarTransferFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "hash", 1)]
        public virtual byte[] Hash {get; set;}
        [Parameter("address", "deed", 2)]
        public virtual string Deed {get; set;}
        [Parameter("uint256", "registrationDate", 3)]
        public virtual BigInteger RegistrationDate {get; set;}
    }    
    
    public partial class StartAuctionFunction:StartAuctionFunctionBase{}

    [Function("startAuction")]
    public class StartAuctionFunctionBase:FunctionMessage
    {
        [Parameter("bytes32", "_hash", 1)]
        public virtual byte[] Hash {get; set;}
    }    
    
    public partial class RootNodeFunction:RootNodeFunctionBase{}

    [Function("rootNode", "bytes32")]
    public class RootNodeFunctionBase:FunctionMessage
    {

    }    
    
    public partial class StartAuctionsAndBidFunction:StartAuctionsAndBidFunctionBase{}

    [Function("startAuctionsAndBid")]
    public class StartAuctionsAndBidFunctionBase:FunctionMessage
    {
        [Parameter("bytes32[]", "hashes", 1)]
        public virtual List<byte[]> Hashes {get; set;}
        [Parameter("bytes32", "sealedBid", 2)]
        public virtual byte[] SealedBid {get; set;}
    }    
    
    public partial class AuctionStartedEventDTO:AuctionStartedEventDTOBase{}

    [Event("AuctionStarted")]
    public class AuctionStartedEventDTOBase: IEventDTO
    {
        [Parameter("bytes32", "hash", 1, true )]
        public virtual byte[] Hash {get; set;}
        [Parameter("uint256", "registrationDate", 2, false )]
        public virtual BigInteger RegistrationDate {get; set;}
    }    
    
    public partial class NewBidEventDTO:NewBidEventDTOBase{}

    [Event("NewBid")]
    public class NewBidEventDTOBase: IEventDTO
    {
        [Parameter("bytes32", "hash", 1, true )]
        public virtual byte[] Hash {get; set;}
        [Parameter("address", "bidder", 2, true )]
        public virtual string Bidder {get; set;}
        [Parameter("uint256", "deposit", 3, false )]
        public virtual BigInteger Deposit {get; set;}
    }    
    
    public partial class BidRevealedEventDTO:BidRevealedEventDTOBase{}

    [Event("BidRevealed")]
    public class BidRevealedEventDTOBase: IEventDTO
    {
        [Parameter("bytes32", "hash", 1, true )]
        public virtual byte[] Hash {get; set;}
        [Parameter("address", "owner", 2, true )]
        public virtual string Owner {get; set;}
        [Parameter("uint256", "value", 3, false )]
        public virtual BigInteger Value {get; set;}
        [Parameter("uint8", "status", 4, false )]
        public virtual byte Status {get; set;}
    }    
    
    public partial class HashRegisteredEventDTO:HashRegisteredEventDTOBase{}

    [Event("HashRegistered")]
    public class HashRegisteredEventDTOBase: IEventDTO
    {
        [Parameter("bytes32", "hash", 1, true )]
        public virtual byte[] Hash {get; set;}
        [Parameter("address", "owner", 2, true )]
        public virtual string Owner {get; set;}
        [Parameter("uint256", "value", 3, false )]
        public virtual BigInteger Value {get; set;}
        [Parameter("uint256", "registrationDate", 4, false )]
        public virtual BigInteger RegistrationDate {get; set;}
    }    
    
    public partial class HashReleasedEventDTO:HashReleasedEventDTOBase{}

    [Event("HashReleased")]
    public class HashReleasedEventDTOBase: IEventDTO
    {
        [Parameter("bytes32", "hash", 1, true )]
        public virtual byte[] Hash {get; set;}
        [Parameter("uint256", "value", 2, false )]
        public virtual BigInteger Value {get; set;}
    }    
    
    public partial class HashInvalidatedEventDTO:HashInvalidatedEventDTOBase{}

    [Event("HashInvalidated")]
    public class HashInvalidatedEventDTOBase: IEventDTO
    {
        [Parameter("bytes32", "hash", 1, true )]
        public virtual byte[] Hash {get; set;}
        [Parameter("string", "name", 2, true )]
        public virtual string Name {get; set;}
        [Parameter("uint256", "value", 3, false )]
        public virtual BigInteger Value {get; set;}
        [Parameter("uint256", "registrationDate", 4, false )]
        public virtual BigInteger RegistrationDate {get; set;}
    }    
    
    
    
    public partial class GetAllowedTimeOutputDTO:GetAllowedTimeOutputDTOBase{}

    [FunctionOutput]
    public class GetAllowedTimeOutputDTOBase :IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 {get; set;}
    }    
    
    
    
    public partial class ShaBidOutputDTO:ShaBidOutputDTOBase{}

    [FunctionOutput]
    public class ShaBidOutputDTOBase :IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 {get; set;}
    }    
    
    
    
    public partial class EntriesOutputDTO:EntriesOutputDTOBase{}

    [FunctionOutput]
    public class EntriesOutputDTOBase :IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 {get; set;}
        [Parameter("address", "", 2)]
        public virtual string ReturnValue2 {get; set;}
        [Parameter("uint256", "", 3)]
        public virtual BigInteger ReturnValue3 {get; set;}
        [Parameter("uint256", "", 4)]
        public virtual BigInteger ReturnValue4 {get; set;}
        [Parameter("uint256", "", 5)]
        public virtual BigInteger ReturnValue5 {get; set;}
    }    
    
    public partial class EnsOutputDTO:EnsOutputDTOBase{}

    [FunctionOutput]
    public class EnsOutputDTOBase :IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 {get; set;}
    }    
    
    
    
    
    
    public partial class SealedBidsOutputDTO:SealedBidsOutputDTOBase{}

    [FunctionOutput]
    public class SealedBidsOutputDTOBase :IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 {get; set;}
    }    
    
    public partial class StateOutputDTO:StateOutputDTOBase{}

    [FunctionOutput]
    public class StateOutputDTOBase :IFunctionOutputDTO 
    {
        [Parameter("uint8", "", 1)]
        public virtual byte ReturnValue1 {get; set;}
    }    
    
    
    
    public partial class IsAllowedOutputDTO:IsAllowedOutputDTOBase{}

    [FunctionOutput]
    public class IsAllowedOutputDTOBase :IFunctionOutputDTO 
    {
        [Parameter("bool", "allowed", 1)]
        public virtual bool Allowed {get; set;}
    }    
    
    
    
    public partial class RegistryStartedOutputDTO:RegistryStartedOutputDTOBase{}

    [FunctionOutput]
    public class RegistryStartedOutputDTOBase :IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 {get; set;}
    }    
    
    public partial class LaunchLengthOutputDTO:LaunchLengthOutputDTOBase{}

    [FunctionOutput]
    public class LaunchLengthOutputDTOBase :IFunctionOutputDTO 
    {
        [Parameter("uint32", "", 1)]
        public virtual uint ReturnValue1 {get; set;}
    }    
    
    
    
    
    
    
    
    
    
    
    
    public partial class RootNodeOutputDTO:RootNodeOutputDTOBase{}

    [FunctionOutput]
    public class RootNodeOutputDTOBase :IFunctionOutputDTO 
    {
        [Parameter("bytes32", "", 1)]
        public virtual byte[] ReturnValue1 {get; set;}
    }    
    

}
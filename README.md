# Nconflux
Test word
[![Join the chat at https://gitter.im/michaelzhang/Conflux.RPC](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/juanfranblanco/Conflux.RPC?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge) [![Documentation Status](https://readthedocs.org/projects/nconflux/badge/?version=latest)](https://nconflux.readthedocs.io/en/latest/) [![NuGet version](https://badge.fury.io/nu/nconflux.web3.svg)](https://badge.fury.io/nu/nconflux.web3)

Azure DevOps CI [![CI Build / Test Status](https://dev.azure.com/nconfluxproject/Nconflux/_apis/build/status/nconflux-CI)](https://dev.azure.com/nconfluxproject/Nconflux/_build/latest?definitionId=2)


Azure DevOps CI Code Gen: [![CI Code gen Build / Test Status](https://dev.azure.com/nconfluxproject/Nconflux/_apis/build/status/nconflux%20ci%20codegen)](https://dev.azure.com/nconfluxproject/Nconflux/_build/latest?definitionId=3)
CI dev packages: [![MyGet Pre Release](https://img.shields.io/myget/nconflux/vpre/nconflux.web3.svg?style=plastic)](https://www.myget.org/feed/nconflux/package/nuget/Nconflux.Web3)


# What is Nconflux?

Nconflux is the .Net integration library for Conflux, simplifying the access and smart contract interaction with Conflux nodes both public or permissioned like Gcfx, [Parity](https://www.parity.io/) or [Quorum](https://www.jpmorgan.com/global/Quorum).

Nconflux is developed targeting netstandard 1.1, netstandard 2.0, netcore 2.1, netcore 3.1, net451 and also as a portable library, hence it is compatible with all the operating systems (Windows, Linux, MacOS, Android and OSX) and has been tested on cloud, mobile, desktop, Xbox, hololens and windows IoT.

# Nconflux Playground. Try Nconflux now in your browser.
Go to http://playground.nconflux.com to browse and execute all the different samples on how to use Nconflux directly in your browser. 
The same version is hosted in IPFS at http://playground.nconflux.cfx.link/ or the same https://gateway.pinata.cloud/ipfs/QmPgWmX3HsxCBCDVv8adEhLzeJd2VstcyGh1T9ipKrvU4Y/

[![Nconflux Playground](screenshots/playground.png)](http://playground.nconflux.com)

## Issues, Requests and help

Please join the chat at:  [![Join the chat at https://gitter.im/juanfranblanco/Conflux.RPC](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/juanfranblanco/Conflux.RPC?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

We should be able to answer there any simple queries, general comments or requests, everyone is welcome. In a similar feel free to raise any issue or pull request.

## Documentation
The documentation and guides can be found at [Read the docs](https://nconflux.readthedocs.io/en/latest/). 

## Features

* JSON RPC / IPC Conflux core methods.
* Gcfx management API (admin, personal, debugging, miner).
* [Parity](https://www.parity.io/) management API.
* [Quorum](https://www.jpmorgan.com/global/Quorum) integration.
* Simplified smart contract interaction for deployment, function calling, transaction and event filtering and decoding of topics.
* [Unity 3d](https://unity3d.com/) Unity integration.
* ABI to .Net type encoding and decoding, including attribute based for complex object deserialization.
* Hd Wallet
* Transaction, RLP and message signing, verification and recovery of accounts.
* Libraries for standard contracts Token, [ENS](https://ens.domains/) and [Uport](https://www.uport.me/)
* Integrated TestRPC testing to simplify TDD and BDD (Specflow) development.
* Key storage using Web3 storage standard, compatible with Gcfx and Parity.
* Simplified account life cycle for both managed by third party client (personal) or stand alone (signed transactions).
* Low level Interception of RPC calls.
* Code generation of smart contracts services.

## Quick installation

Nconflux provides two types of packages. Standalone packages targeting Netstandard 1.1, net451 and where possible net351 to support Unity3d. There is also a Nconflux.Portable library which combines all the packages into a single portable library. As netstandard evolves and is more widely supported, the portable library might be eventually deprecated.

To install the latest version:

#### Windows users

To install the main packages you can either:

```
PM > Install-Package Nconflux.Web3
```
or 
```
PM > Install-Package Nconflux.Portable
```

#### Mac/Linux users

```
dotnet add package Nconflux.Web3 
``` 
or 
```
dotnet add package Nconflux.Portable
```

## Main Libraries

|  Project Source | Nuget_Package |  Description |
| ------------- |--------------------------|-----------|
| Nconflux.Portable    | [![NuGet version](https://badge.fury.io/nu/nconflux.portable.svg)](https://badge.fury.io/nu/nconflux.portable)| Portable class library combining all the different libraries in one package |
| [Nconflux.Web3](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.Web3)    | [![NuGet version](https://badge.fury.io/nu/nconflux.web3.svg)](https://badge.fury.io/nu/nconflux.web3)| Conflux Web3 Class Library simplifying the interaction via RPC. Includes contract interaction, deployment, transaction, encoding / decoding and event filters |
| [Nconflux.Unity](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.Unity) |  | Unity3d integration, libraries can be found in the Nconflux[releases](https://github.com/Nconflux/Nconflux/releases) |
| [Nconflux.Gcfx](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.Gcfx)    | [![NuGet version](https://badge.fury.io/nu/nconflux.gcfx.svg)](https://badge.fury.io/nu/nconflux.gcfx)| Nconflux.Gcfx is the extended Web3 library for Gcfx. This includes the non-generic RPC API client methods to interact with the Go Conflux Client (Gcfx) like Admin, Debug, Miner|
| [Nconflux.Quorum](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.Quorum)| [![NuGet version](https://badge.fury.io/nu/Nconflux.quorum.svg)](https://badge.fury.io/nu/Nconflux.quorum)| Extension to interact with Quorum, the permissioned implementation of Conflux supporting data privacy created by JP Morgan|
| [Nconflux.Parity](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.Parity)| [![NuGet version](https://badge.fury.io/nu/nconflux.parity.svg)](https://badge.fury.io/nu/nconflux.parity)| Nconflux.Parity is the extended Web3 library for Parity. Including the non-generic RPC API client methods to interact with Parity. (WIP)|

## Core Libraries

|  Project Source | Nuget_Package |  Description |
| ------------- |--------------------------|-----------|
| [Nconflux.ABI](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.ABI) | [![NuGet version](https://badge.fury.io/nu/nconflux.abi.svg)](https://badge.fury.io/nu/nconflux.abi)| Encoding and decoding of ABI Types, functions, events of Conflux contracts |
| [Nconflux.EVM](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.EVM) | |Conflux Virtual Machine API|
| [Nconflux.Hex](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.Hex) | [![NuGet version](https://badge.fury.io/nu/nconflux.hex.svg)](https://badge.fury.io/nu/nconflux.hex)| HexTypes for encoding and decoding String, BigInteger and different Hex helper functions|
| [Nconflux.RPC](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.RPC)   | [![NuGet version](https://badge.fury.io/nu/nconflux.rpc.svg)](https://badge.fury.io/nu/nconflux.rpc) | Core RPC Class Library to interact via RCP with an Conflux client |
| [Nconflux.JsonRpc.Client](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.JsonRpc.Client)   | [![NuGet version](https://badge.fury.io/nu/nconflux.jsonrpc.client.svg)](https://badge.fury.io/nu/nconflux.jsonrpc.client) | Nconflux JsonRpc.Client core library to use in conjunction with either the JsonRpc.RpcClient, the JsonRpc.IpcClient or other custom Rpc provider |
| [Nconflux.JsonRpc.RpcClient](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.JsonRpc.RpcClient)   | [![NuGet version](https://badge.fury.io/nu/nconflux.jsonrpc.rpcclient.svg)](https://badge.fury.io/nu/nconflux.jsonrpc.rpcclient) | JsonRpc Rpc Client using Http|
| [Nconflux JsonRpc IpcClient](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.JsonRpc.IpcClient)| [![NuGet version](https://badge.fury.io/nu/nconflux.jsonRpc.ipcclient.svg)](https://badge.fury.io/nu/nconflux.jsonRpc.ipcclient) |JsonRpc IpcClient provider for Windows, Linux and Unix|
| [Nconflux.RLP](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.RLP)  | [![NuGet version](https://badge.fury.io/nu/nconflux.rlp.svg)](https://badge.fury.io/nu/nconflux.rlp) | RLP encoding and decoding |
| [Nconflux.KeyStore](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.KeyStore)  | [![NuGet version](https://badge.fury.io/nu/nconflux.keystore.svg)](https://badge.fury.io/nu/nconflux.keystore) | Keystore generation, encryption and decryption for Conflux key files using the Web3 Secret Storage definition, https://github.com/conflux/wiki/wiki/Web3-Secret-Storage-Definition |
| [Nconflux.Signer](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.Signer)  | [![NuGet version](https://badge.fury.io/nu/nconflux.signer.svg)](https://badge.fury.io/nu/nconflux.signer) | nconflux signer library to sign and verify messages, RLP and transactions using an Conflux account private key |
| [Nconflux.Contracts](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.Contracts)  | [![NuGet version](https://badge.fury.io/nu/nconflux.contracts.svg)](https://badge.fury.io/nu/nconflux.contracts) | Core library to interact via RPC with Smart contracts in Conflux |
| [Nconflux.IntegrationTesting](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.IntegrationTesting)  |   | Integration testing module |
| [Nconflux.HDWallet](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.HDWallet)  | [![NuGet version](https://badge.fury.io/nu/nconflux.HDWallet.svg)](https://badge.fury.io/nu/nconflux.HDWallet) | Generates an HD tree of Conflux compatible addresses from a randomly generated seed phrase (using BIP32 and BIP39) |

Note: IPC is supported for Windows, Unix and Linux but is only available using Nconflux.Web3 not Nconflux.Portable
 
## Smart contract API Libraries

|  Project Source | Nuget_Package |  Description |
| ------------- |--------------------------|-----------
| [Nconflux.StandardTokenEIP20](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.StandardTokenEIP20)| [![NuGet version](https://badge.fury.io/nu/nconflux.standardtokeneip20.svg)](https://badge.fury.io/nu/nconflux.nconflux.standardtokeneip20)| ``` Nconflux.StandardTokenEIP20 ``` Conflux Service to interact with ERC20 compliant contracts |
| [Nconflux.Uport](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.Uport)| [![NuGet version](https://badge.fury.io/nu/nconflux.uport.svg)](https://badge.fury.io/nu/nconflux.uport)| Uport registry library |
| [Nconflux.ENS](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.ENS)| [![NuGet version](https://badge.fury.io/nu/nconflux.ens.svg)](https://badge.fury.io/nu/nconflux.ens)| Conflux Name service library (original ENS) WIP to upgrade to latest ENS |

## Utilities

|  Project Source |  Description |
| ------------- |--------------------------|
| [Nconflux.Generator.Console](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.Generator.Console) |  |
| [Nconflux.Console](https://github.com/Nconflux/Nconflux.Console) | A collection of command line utilities to interact with Conflux and account management | |
[Testchains](https://github.com/Nconflux/TestChains) | Pre-configured devchains for fast response (PoA) | | 
[DappHybrid](https://github.com/Nconflux/Nconflux.DappHybrid) | A cross-platform hybrid hosting mechanism for web based decentralised applications |
## Training modules

|  Project Source |  Description |
| ------------- |--------------------------|
|[Nconflux.Workbooks](https://github.com/Nconflux/Nconflux.Workbooks) | Xamarin Workbook tutorials including executable code | 
|[Nconflux.Tutorials](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.Tutorials) | Tutorials to run on VS Studio |

## Code samples

|  Source |  Description |
| ------------- |------------|
[Keystore generator](https://github.com/Nconflux/Nconflux/tree/master/src/Nconflux.KeyStore.Console.Sample)| Keystore file generator|
[Faucet](https://github.com/Nconflux/Nconflux.Faucet)| Web application template for an Cfx faucet |
[Nconflux Flappy](https://github.com/Nconflux/Nconflux.Flappy)| The source code files for the Unity3d game integrating with Conflux |
[Nconflux Game Sample](https://github.com/Nconflux/nconflux.game.sample)| Sample game demonstrating how to integrate Nconflux with [UrhoSharp's SamplyGame](https://github.com/xamarin/urho-samples/tree/master/SamplyGame) to build a cross-platform game interacting with Conflux |
[Nconflux UI wallet sample](https://github.com/Nconflux/nconflux.UI.wallet.sample)| Cross platform wallet example using Nconflux, Xamarin.Forms and MvvmCross, targeting: Android, iOS, Windows Mobile, Desktop (windows 10 uwp), IoT with the Raspberry PI and Xbox. |
|[Nconflux Windows wallet sample](https://github.com/Nconflux/Nconflux.SimpleWindowsWallet) | Windows forms wallet sample providing the core functionality for Loading accounts from different mediums, Cfx transfer, Standard token interaction. This is going to be the basis for the future cross-platform wallet / dapp |
[Nconflux Windows wallet sample](https://github.com/Nconflux/Nconflux.SimpleWindowsWallet) | Windows forms wallet sample providing the core functionality for Loading accounts from different mediums, Cfx transfer, Standard token interaction. This is going to be the basis for the future cross-platform wallet / dapp |
[Blazor/Blockchain Explorer](https://github.com/Nconflux/NconfluxBlazor) | Wasm blockchain explorer based on [Blazor](https://github.com/aspnet/Blazor) and [ReactiveUI](https://github.com/reactiveui/ReactiveUI)|

### Video guides
There a few video guides, which might be helpful to get started. 
**NOTE: These videos are for version 1.0, so some areas have changed.**

Please use the Nconflux playground for the latest samples.

#### Introductions

These are two videos that can take you through all the initial steps from creating a contract to deployment, one in the classic windows, visual studio environment and another in a cross platform mac and visual studio code. 

##### Windows, Visual Studio
This video takes you through the steps of creating the a smart contract, compile it, start a private chain and deploy it using Nconflux.

[![Smart contracts, private test chain and deployment to Conflux with Nconflux](http://img.youtube.com/vi/4t5Z3eX59k4/0.jpg)](http://www.youtube.com/watch?v=4t5Z3eX59k4 "Smart contracts, private test chain and deployment to Conflux with Nconflux")


#### Introduction to Calls, Transactions, Events, Filters and Topics

This hands on demo provides an introduction to calls, transactions, events filters and topics

[![Introduction to Calls, Transactions, Events, Filters and Topics](http://img.youtube.com/vi/Yir_nu5mmw8/0.jpg)](https://www.youtube.com/watch?v=Yir_nu5mmw8 "Introduction to Calls, Transactions, Events, Filters and Topics")
 
#### Mappings, Structs, Arrays and complex Functions Output (DTOs) 

This video provides an introduction on how to store and retrieve data from structs, mappings and arrays decoding multiple output parameters to Data Transfer Objects

[![Mappings, Structs, Arrays and complex Functions Output (DTOs)](http://img.youtube.com/vi/o8UC96K0rg8/0.jpg)](https://www.youtube.com/watch?v=o8UC96K0rg8 "Mappings, Structs, Arrays and complex Functions Output (DTOs)")


## Thanks and Credits

* Many thanks to Cass for the fantastic logo (https://github.com/cassiopaia) and everyone in Maker for providing very early feedback.
* Many thanks to everyone who has submitted a request for extra features, help or bugs either here in github, gitter or other channels, you are continuously shaping this project. 
  A big shout out specially to @slothbag, @matt.tan, @knocte, @TrekDev, @raymens, @rickzanux, @naddison36, @bobsummerwill, @brendan87, @dylanmckendry that were using Nconflux and providing great feedback from the beginning.
  @djsowa Marcin Sowa for his help on IPC in Linux.
* Everyone in the Conflux, Consensys and the blockchain community. 
* Huge shout out to everyone developing all the different Conflux implementations Gcfx, Parity, ConfluxJ, CfxCpp, conflux-js (and every other utility around it), python (in the different shapes), ruby (digix guys), solidity, vyper, serpent, web3 implementations (web3js the first) and cfxjs, web3j, etc, etc and last but not least the .Net Bitcoin implementation.

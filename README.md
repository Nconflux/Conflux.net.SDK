# Nconflux

 
 
# What is Nconflux?

Nconflux is the .Net integration library for Conflux, simplifying the access and smart contract interaction with Conflux nodes.

Nconflux is developed targeting   netcore 3.1 and .net 5, hence it is compatible with all the operating systems (Windows, Linux, MacOS, Android and OSX) and has been tested on cloud, mobile, desktop, Xbox, hololens and windows IoT.
 

## Issues, Requests and help

Forum: https://forum.conflux.fun/
Wechat group: please join our support's wechat account 1725325128,  he will invite you into the wechat group.

We should be able to answer there any simple queries, general comments or requests, everyone is welcome. In a similar feel free to raise any issue or pull request.

## Documentation
The documentation and guides can be found at [Read the docs](https://xxxx/). 

## Features

* JSON RPC / IPC Conflux core methods. 
* Simplified smart contract interaction for deployment, function calling, transaction and event filtering and decoding of topics.
* ABI to .Net type encoding and decoding, including attribute based for complex object deserialization.
* Transaction, RLP and message signing, verification and recovery of accounts.
* Code generation of smart contracts services.

## Quick Installation

 
To install the latest version:

 
```
Install Visual Studio 2019
Open Tools/Nuget Package Manager/Manager Nuget Package for Solution...
In tab Browse, search Conflux.API
Install it
```
 
## Code Samples
### Create an NConflux Instance

NConflux conflux = new NConflux("http://test.confluxrpc.org");// no need gas 
NConflux conflux = new NConflux("http://test.confluxrpc.org",privateKey);//need gas
### Get Epoch Number
var epochNumber = await conflux.GetEpochNumber();
### Get Balance
var balance = await conflux.GetBalance("0x1****");
### Get Next Nonce
var nextNonce =  await conflux.GetNextNonce("0x1****");
### Transfer 
await conflux.Transfer("0x1****", 1234);
### Deploy Contract
var contractInfo = await conflux.DeployContract("0x60**");
### Call Contract
await conflux.CallContract(CallType.Gas, ABI, contractInfo.ContractAddress, "set", new object[] { "user1" });
## Thanks  

* Many thanks to [Conflux Foundation ](https://confluxnetwork.org/ "Conflux Foundation ")

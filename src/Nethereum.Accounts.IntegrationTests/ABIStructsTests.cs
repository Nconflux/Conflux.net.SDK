﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.Contracts.CQS;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3.Accounts;
using Nethereum.XUnitEthereumClients;
using Xunit;

namespace Nethereum.Accounts.IntegrationTests
{
    [Collection(EthereumClientIntegrationFixture.ETHEREUM_CLIENT_COLLECTION_DEFAULT)]
    public class ABIStructsTests
    {
        /*
     pragma solidity "0.4.25";
pragma experimental ABIEncoderV2;

contract TestV2
{
   
        uint256 public id1 = 1;
        uint256 public id2;
        uint256 public id3;
        string  public id4;
        TestStruct public testStructStorage;


        event TestStructStorageChanged(address sender, TestStruct testStruct);

        struct SubSubStruct {
            uint256 id;
        }

        struct SubStruct {
            uint256 id;
            SubSubStruct sub;
            string id2;
        }

        struct TestStruct {
            uint256 id;
            SubStruct subStruct1;
            SubStruct subStruct2;
            string id2;
        }

        struct SimpleStruct{
            uint256 id;
            uint256 id2;
        }

        function TestArray() pure public returns (SimpleStruct[2] structArray) {
            structArray[0] = (SimpleStruct(1, 100));
            structArray[1] = (SimpleStruct(2, 200));
            return structArray;
        }

        function Test(TestStruct testScrut) public {
            id1 = testScrut.id;
            id2 = testScrut.subStruct1.id;
            id3 = testScrut.subStruct2.sub.id;
            id4 = testScrut.subStruct2.id2;
    
        }

        function SetStorageStruct(TestStruct testStruct) public {
            testStructStorage = testStruct;
            emit TestStructStorageChanged(msg.sender, testStruct);
        }

        function GetTest() public view returns(TestStruct testStruct, int test1, int test2){
            testStruct.id = 1;
            testStruct.id2 = "hello";
            testStruct.subStruct1.id = 200;
            testStruct.subStruct1.id2 = "Giraffe";
            testStruct.subStruct1.sub.id = 20;
            testStruct.subStruct2.id = 300;
            testStruct.subStruct2.id2 = "Elephant";
            testStruct.subStruct2.sub.id = 30000;
            test1 = 5;
            test2 = 6;
        }
    
        struct Empty{
    
        }

        function TestEmpty(Empty empty) public {
    
        }
}
        
}
        */
        [Fact]
        public async Task StructTests()
        {
            await SolidityV2StructTests();
        }


        [Function("id1", "uint256")]
        public class Id1Function : FunctionMessage
        {

        }

        [Function("id2", "uint256")]
        public class Id2Function : FunctionMessage
        {

        }

        [Function("id3", "uint256")]
        public class Id3Function : FunctionMessage
        {

        }

        [Function("id4", "string")]
        public class Id4Function : FunctionMessage
        {

        }

        [Function("GetTest")]
        public class GetTestFunction : FunctionMessage
        {
            
        }

        [Function("testStructStorage")]
        public class GetTestStructStorageFunction : FunctionMessage
        {

        }

        public class SimpleStruct
        {
            [Parameter("uint256", "id1", 1)]
            public BigInteger Id1 { get; set; }

            [Parameter("uint256", "id2", 2)]
            public BigInteger Id2 { get; set; }
        }

        [Function("TestArray", typeof(TestArrayOuputDTO))]
        public class TestArray : FunctionMessage
        {
        }

        [FunctionOutput]
        public class TestArrayOuputDTO:IFunctionOutputDTO
        {
            [Parameter("tuple[2]", "simpleStruct", 1)]
            public List<SimpleStruct> SimpleStructs { get; set;}
        }

        [FunctionOutput]
        public class GetTestFunctionOuptputDTO:IFunctionOutputDTO
        {
            [Parameter("tuple")]
            public TestStructStrings TestStruct { get; set; }


            [Parameter("int256", "test1", 2)]
            public BigInteger Test1 { get; set; }


            [Parameter("int256", "test2", 3)]
            public BigInteger Test2 { get; set; }
        }

        [Function("Test")]
        public class TestFunction : FunctionMessage
        {
            [Parameter("tuple", "testStruct")]
            public TestStructStrings TestStruct { get; set; }
        }

        [Function("SetStorageStruct")]
        public class SetStorageStructFunction : FunctionMessage
        {
            [Parameter("tuple", "testStruct")]
            public TestStructStrings TestStruct { get; set; }
        }

        [Event("TestStructStorageChanged")]
        public class TestStructStorageChangedEvent: IEventDTO
        {
            [Parameter("address", "sender", 1)]
            public string Address { get; set; }

            [Parameter("tuple", "testStruct", 2)]
            public TestStructStrings TestStruct { get; set; }
        }


        [FunctionOutput]
        public class TestStructStrings: IFunctionOutputDTO
        {
            [Parameter("uint256", "id", 1)]
            public BigInteger Id { get; set; }

            [Parameter("tuple", "subStruct1", 2)]
            public SubStructUintString SubStruct1 { get; set; }

            [Parameter("tuple", "subStruct2", 3)]
            public SubStructUintString SubStruct2 { get; set; }

            [Parameter("string", "id2", 4)]
            public string Id2 { get; set; }
        }


        public class SubStructUintString
        {
            [Parameter("uint256", "id", 1)]
            public BigInteger Id { get; set; }

            [Parameter("tuple", "sub", 2)]
            public SubStructUInt Sub { get; set; }

            [Parameter("string", "id2", 3)]
            public String Id2 { get; set; }
        }

        public class SubStructUInt
        {
            [Parameter("uint256", "id", 1)]
            public BigInteger Id { get; set; }
        }

        public class TestContractDeployment : ContractDeploymentMessage
        {
            public const string BYTE_CODE = "0x6080604052600160005534801561001557600080fd5b50610d7a806100256000396000f3006080604052600436106100a35763ffffffff7c010000000000000000000000000000000000000000000000000000000060003504166319dcec3d81146100a857806326145698146100d55780632abda41e146100f75780634d8f11fa14610117578063517999bc1461013957806363845ba31461015b578063a55001a614610180578063e5207eaa146101a0578063e9159d64146101b5578063f6ee7d9f146101d7575b600080fd5b3480156100b457600080fd5b506100bd6101ec565b6040516100cc93929190610bf9565b60405180910390f35b3480156100e157600080fd5b506100f56100f0366004610a06565b6102d1565b005b34801561010357600080fd5b506100f56101123660046109e1565b61039c565b34801561012357600080fd5b5061012c61039f565b6040516100cc9190610bd4565b34801561014557600080fd5b5061014e6103db565b6040516100cc9190610c26565b34801561016757600080fd5b506101706103e1565b6040516100cc9493929190610c34565b34801561018c57600080fd5b506100f561019b366004610a06565b6105f5565b3480156101ac57600080fd5b5061014e610628565b3480156101c157600080fd5b506101ca61062e565b6040516100cc9190610be8565b3480156101e357600080fd5b5061014e6106bc565b6101f46106c2565b6001815260408051808201825260058082527f68656c6c6f0000000000000000000000000000000000000000000000000000006020808401919091526060850192909252818401805160c8905283518085018552600781527f47697261666665000000000000000000000000000000000000000000000000008185015281518501525182015160149052828401805161012c905283518085018552600881527f456c657068616e74000000000000000000000000000000000000000000000000818501528151909401939093529151015161753090529091600690565b8051600490815560208083015180516005908155818301515160065560408201518051869594610306926007929101906106f8565b5050506040828101518051600484019081556020808301515160058601559282015180519293919261033e92600687019201906106f8565b5050506060820151805161035c9160078401916020909101906106f8565b509050507fc4948cf046f20c08b2b7f5b0b6de7bdbe767d009d512c8440b98eb424bdb9ad83382604051610391929190610bb4565b60405180910390a150565b50565b6103a7610776565b60408051808201825260018152606460208083019190915290835281518083019092526002825260c8828201528201525b90565b60005481565b6004805460408051606081018252600580548252825160208181018552600654825280840191909152600780548551601f6002600019610100600186161502019093169290920491820184900484028101840187528181529697969495939493860193928301828280156104965780601f1061046b57610100808354040283529160200191610496565b820191906000526020600020905b81548152906001019060200180831161047957829003601f168201915b5050509190925250506040805160608101825260048501805482528251602080820185526005880154825280840191909152600687018054855160026001831615610100026000190190921691909104601f81018490048402820184018752808252979897949650929486019390918301828280156105565780601f1061052b57610100808354040283529160200191610556565b820191906000526020600020905b81548152906001019060200180831161053957829003601f168201915b5050509190925250505060078201805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815293949392918301828280156105eb5780601f106105c0576101008083540402835291602001916105eb565b820191906000526020600020905b8154815290600101906020018083116105ce57829003601f168201915b5050505050905084565b8051600055602080820151516001556040808301518083015151600255015180516106249260039201906106f8565b5050565b60025481565b6003805460408051602060026001851615610100026000190190941693909304601f810184900484028201840190925281815292918301828280156106b45780601f10610689576101008083540402835291602001916106b4565b820191906000526020600020905b81548152906001019060200180831161069757829003601f168201915b505050505081565b60015481565b61010060405190810160405280600081526020016106de6107a4565b81526020016106eb6107a4565b8152602001606081525090565b828054600181600116156101000203166002900490600052602060002090601f016020900481019282601f1061073957805160ff1916838001178555610766565b82800160010185558215610766579182015b8281111561076657825182559160200191906001019061074b565b506107729291506107bf565b5090565b6080604051908101604052806002905b61078e6107d9565b8152602001906001900390816107865790505090565b606060405190810160405280600081526020016106eb6107f0565b6103d891905b8082111561077257600081556001016107c5565b604080518082019091526000808252602082015290565b60408051602081019091526000815290565b6000601f8201831361081357600080fd5b813561082661082182610cad565b610c86565b9150808252602083016020830185838301111561084257600080fd5b61084d838284610cfe565b50505092915050565b600080828403121561086757600080fd5b6108716000610c86565b9392505050565b60006060828403121561088a57600080fd5b6108946060610c86565b905060006108a284846109d5565b82525060206108b3848483016108eb565b602083015250604082013567ffffffffffffffff8111156108d357600080fd5b6108df84828501610802565b60408301525092915050565b6000602082840312156108fd57600080fd5b6109076020610c86565b9050600061091584846109d5565b82525092915050565b60006080828403121561093057600080fd5b61093a6080610c86565b9050600061094884846109d5565b825250602082013567ffffffffffffffff81111561096557600080fd5b61097184828501610878565b602083015250604082013567ffffffffffffffff81111561099157600080fd5b61099d84828501610878565b604083015250606082013567ffffffffffffffff8111156109bd57600080fd5b6109c984828501610802565b60608301525092915050565b600061087182356103d8565b60008082840312156109f257600080fd5b60006109fe8484610856565b949350505050565b600060208284031215610a1857600080fd5b813567ffffffffffffffff811115610a2f57600080fd5b6109fe8482850161091e565b610a4481610ce5565b82525050565b610a5381610cd5565b610a5c826103d8565b60005b82811015610a8c57610a72858351610ad1565b610a7b82610cdf565b604095909501949150600101610a5f565b5050505050565b610a44816103d8565b6000610aa782610cdb565b808452610abb816020860160208601610d0a565b610ac481610d36565b9093016020019392505050565b80516040830190610ae28482610a93565b506020820151610af56020850182610a93565b50505050565b80516000906060840190610b0f8582610a93565b506020830151610b226020860182610b43565b5060408301518482036040860152610b3a8282610a9c565b95945050505050565b80516020830190610af58482610a93565b80516000906080840190610b688582610a93565b5060208301518482036020860152610b808282610afb565b91505060408301518482036040860152610b9a8282610afb565b91505060608301518482036060860152610b3a8282610a9c565b60408101610bc28285610a3b565b81810360208301526109fe8184610b54565b60808101610be28284610a4a565b92915050565b602080825281016108718184610a9c565b60608082528101610c0a8186610b54565b9050610c196020830185610a93565b6109fe6040830184610a93565b60208101610be28284610a93565b60808101610c428287610a93565b8181036020830152610c548186610afb565b90508181036040830152610c688185610afb565b90508181036060830152610c7c8184610a9c565b9695505050505050565b60405181810167ffffffffffffffff81118282101715610ca557600080fd5b604052919050565b600067ffffffffffffffff821115610cc457600080fd5b506020601f91909101601f19160190565b50600290565b5190565b60200190565b73ffffffffffffffffffffffffffffffffffffffff1690565b82818337506000910152565b60005b83811015610d25578181015183820152602001610d0d565b83811115610af55750506000910152565b601f01601f1916905600a265627a7a723058201a204c8fd11b9facac01a86aaac24ebbc6159e540ae80a3dfe6fa745070a73516c6578706572696d656e74616cf50037";

            public TestContractDeployment() : base(BYTE_CODE)
            {
            }
        }

        public static async Task SolidityV2StructTests()
        {

            var address = "0x12890d2cce102216644c59daE5baed380d84830c";
            var privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
            var web3 = new Web3.Web3(new Account(privateKey));
            var deploymentReceipt = await web3.Cfx.GetContractDeploymentHandler<TestContractDeployment>()
                .SendRequestAndWaitForReceiptAsync();
            
            var functionTest = new TestFunction();
            var input = new TestStructStrings()
            {
                Id = 1,
                Id2 = "hello",
                SubStruct1 = new SubStructUintString()
                {
                    Id = 200,
                    Id2 = "Giraffe",
                    Sub = new SubStructUInt()
                    {
                        Id = 20
                    }
                },
                SubStruct2 = new SubStructUintString()
                {
                    Id = 300,
                    Id2 = "Elephant",
                    Sub = new SubStructUInt()
                    {
                        Id = 30000
                    }
                },
            };

            functionTest.TestStruct = input;

            var contractHandler = web3.Cfx.GetContractHandler(deploymentReceipt.ContractAddress);

            var testStructArrayResult =
                await contractHandler.QueryDeserializingToObjectAsync<TestArray, TestArrayOuputDTO>();

            Assert.Equal(1, testStructArrayResult.SimpleStructs[0].Id1);
            Assert.Equal(2, testStructArrayResult.SimpleStructs[1].Id1);

            var id1Before = await contractHandler.QueryAsync<Id1Function, BigInteger>();
            Assert.Equal(1, id1Before);

            var receiptTransaction = await contractHandler.SendRequestAndWaitForReceiptAsync(functionTest);

            var id1After = await contractHandler.QueryAsync<Id1Function, BigInteger>();
            Assert.Equal(1, id1After);
            var id2After = await contractHandler.QueryAsync<Id2Function, BigInteger>();
            Assert.Equal(200, id2After);
            var id3After = await contractHandler.QueryAsync<Id3Function, BigInteger>();
            Assert.Equal(30000, id3After);
            var id4After = await contractHandler.QueryAsync<Id4Function, string>();
            Assert.Equal("Elephant", id4After);
            var testDataFromContract = await contractHandler.QueryDeserializingToObjectAsync<GetTestFunction, GetTestFunctionOuptputDTO>();
            Assert.Equal(5, testDataFromContract.Test1);
            var functionStorage = new SetStorageStructFunction {TestStruct = input};
            var receiptSending = await contractHandler.SendRequestAndWaitForReceiptAsync(functionStorage);

            var storageData =  await contractHandler.QueryDeserializingToObjectAsync<GetTestStructStorageFunction, TestStructStrings>();
            Assert.Equal("hello", storageData.Id2);
            var eventStorage = contractHandler.GetEvent<TestStructStorageChangedEvent>();
            var eventOutputs = eventStorage.DecodeAllEventsForEvent(receiptSending.Logs);
            Assert.Equal(1, eventOutputs[0].Event.TestStruct.Id);

            var eventUntyped = new Event(web3.Client, deploymentReceipt.ContractAddress, eventStorage.EventABI);
            var eventOutputs2 = eventUntyped.DecodeAllEventsDefaultForEvent(receiptSending.Logs);
            Assert.Equal("0x12890d2cce102216644c59dae5baed380d84830c", eventOutputs2[0].Event[0].Result);
            Assert.Equal("sender", eventOutputs2[0].Event[0].Parameter.Name);

        }
    }
}
﻿using Moq;
using Nethereum.Generator.Console.Commands;
using Nethereum.Generator.Console.Generation;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Nethereum.Generator.Console.UnitTests.CommandTests
{
    public class GenerateFromAbiCommandTests
    {
        private readonly GenerateFromAbiCommand _command;
        private readonly Mock<ICodeGenerationWrapper> _mockCodeGenerationWrapper;

        public GenerateFromAbiCommandTests()
        {
            _mockCodeGenerationWrapper = new Mock<ICodeGenerationWrapper>();
            _command = new GenerateFromAbiCommand(){CodeGenerationWrapper = _mockCodeGenerationWrapper.Object};
        }

        [Fact]
        public void InstantiatesDefaultCodeGenerationWrapper()
        {
            Assert.Equal(typeof(CodeGenerationWrapper), new GenerateFromAbiCommand().CodeGenerationWrapper.GetType());
        }

        [Fact]
        public void HasExpectedCommandName()
        {
            Assert.Equal("from-abi", _command.Name);
        }

        [Fact]
        public void HasExpectedArgs()
        {
            var expectedArgs = new Dictionary<string, string>
            {
                {"cn", "contractName"},
                {"abi", "abiPath"},
                {"bin", "binPath"},
                {"o", "outputPath"},
                {"ns", "namespace"},
                {"sf", "SingleFile"}
            };

            _command.HasArgs(expectedArgs);
        }

        [Fact]
        public void ExecuteInvokesCodeGeneratorWithExpectedValues()
        {
            Assert.Equal(0, _command.Execute(
                "-cn", "StandardContract", 
                "-abi", "StandardContract.abi", 
                "-bin", "StandardContract.bin", 
                "-o", "c:/Temp", 
                "-ns", "DefaultNamespace"));

            var singleFile = true;

            _mockCodeGenerationWrapper
                .Verify(w => w.FromAbi("StandardContract", "StandardContract.abi", "StandardContract.bin", "DefaultNamespace", "c:/Temp", singleFile));
        }

        [Fact]
        public void Accepts_And_Passes_Single_File_Parameter()
        {
            Assert.Equal(0, _command.Execute(
                "-cn", "StandardContract", 
                "-abi", "StandardContract.abi", 
                "-bin", "StandardContract.bin", 
                "-o", "c:/Temp", 
                "-ns", "DefaultNamespace",
                "-sf", "false"));

            var singleFile = false;

            _mockCodeGenerationWrapper
                .Verify(w => w.FromAbi("StandardContract", "StandardContract.abi", "StandardContract.bin", "DefaultNamespace", "c:/Temp", singleFile));
        }

        [Fact]
        public void ContractNameIsOptional()
        {
            Assert.Equal(0, _command.Execute(
                "-cn", string.Empty, 
                "-abi", "StandardContract", 
                "-bin", "StandardContract.bin", 
                "-o", "c:/Temp", 
                "-ns", "DefaultNamespace"));
        }

        [Fact]
        public void AbiFileIsMandatory()
        {
            Assert.Equal(1, _command.Execute(
                "-cn", "StandardContract", 
                "-abi", string.Empty, 
                "-bin", "StandardContract.bin", 
                "-o", "c:/Temp", 
                "-ns", "DefaultNamespace"));
        }

        [Fact]
        public void OuputPathIsMandatory()
        {
            Assert.Equal(1, _command.Execute(
                "-cn", "StandardContract", 
                "-abi", "StandardContract.abi", 
                "-bin", "StandardContract.bin", 
                "-o", null, 
                "-ns", "DefaultNamespace"));
        }

        [Fact]
        public void NamespaceIsMandatory()
        {
            Assert.Equal(1, _command.Execute(
                "-cn", "StandardContract", 
                "-abi", string.Empty, 
                "-bin", "StandardContract.bin", 
                "-o", "c:/Temp", 
                "-ns", null));
        }

        [Fact]
        public void SupportsHelpArgs()
        {
            _command.EnsureHelpArgs();
        }
    }
}

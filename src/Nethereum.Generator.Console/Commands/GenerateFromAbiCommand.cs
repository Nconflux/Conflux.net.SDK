﻿using Microsoft.Extensions.CommandLineUtils;
using System;
using Nethereum.Generator.Console.Generation;

namespace Nethereum.Generator.Console.Commands
{
    public class GenerateFromAbiCommand : CommandLineApplication
    {
        private readonly CommandOption _contractName;
        private readonly CommandOption _abiFilePath;
        private readonly CommandOption _binCodeFilePath;
        private readonly CommandOption _outputFolder;
        private readonly CommandOption _baseNamespace;
        private readonly CommandOption _singleFile;
        public ICodeGenerationWrapper CodeGenerationWrapper {get; set; }

        public GenerateFromAbiCommand()
        {
            Name = "from-abi";
            Description = "Generates Nethereum code based based on a single abi.";
            _contractName = Option("-cn | --contractName", "The contract name (Optional)", CommandOptionType.SingleValue);
            _abiFilePath = Option("-abi | --abiPath", "The abi file and path (Mandatory)", CommandOptionType.SingleValue);
            _binCodeFilePath = Option("-bin | --binPath", "The bin file and path (Optional)", CommandOptionType.SingleValue);
            _outputFolder = Option("-o | --outputPath", "The output path for the generated code (Mandatory)", CommandOptionType.SingleValue);
            _baseNamespace = Option("-ns | --namespace", "The base namespace for the generated code (Mandatory)", CommandOptionType.SingleValue);
            _singleFile = Option("-sf | --SingleFile", "Generate the message definition in a single file (Optional - default is true)", CommandOptionType.SingleValue);
            OnExecute((Func<int>)RunCommand);
            CodeGenerationWrapper = new CodeGenerationWrapper();

            this.AddHelpOption();
        }

        private int RunCommand()
        {
            var abiFilePath = _abiFilePath.Value();
            if (string.IsNullOrWhiteSpace(abiFilePath))
            {
                System.Console.WriteLine("An abi file must be specified");
                return 1;
            }

            var outputFolder = (_outputFolder).Value();
            if (string.IsNullOrWhiteSpace(outputFolder))
            {
                System.Console.WriteLine("An output folder must be specified");
                return 1;
            }

            var baseNamespace = _baseNamespace.Value();
            if (string.IsNullOrWhiteSpace(baseNamespace))
            {
                System.Console.WriteLine("A base namespace must be specified");
                return 1;
            }

            var contractName = _contractName.Value();

            bool singleFile = true;

            if (_singleFile.HasValue())
            {
                bool.TryParse(_singleFile.Value(), out singleFile);
            }

            CodeGenerationWrapper.FromAbi(contractName, abiFilePath, _binCodeFilePath.Value(), baseNamespace, outputFolder, singleFile);

            return 0;
        }
    }
}

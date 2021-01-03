﻿namespace Nethereum.Generator.Console.Generation
{
    public interface ICodeGenerationWrapper
    {
        void FromAbi(string contractName, string abiFilePath, string binFilePath, string baseNamespace, string outputFolder, bool singleFile);
        void FromProject(string projectPath, string assemblyName);
        void FromTruffle(string inputDirectory, string baseNamespace, string outputFolder, bool singleFile);
    }
}
using System;
using Nethereum.Generators.Model;

namespace Nethereum.Generators.Core
{
    public class FunctionABIModel
    {
        public FunctionABI FunctionABI { get; }
        private ITypeConvertor _abiTypeToDotnetTypeConvertor;

        public FunctionABIModel(FunctionABI functionABI, ITypeConvertor abiTypeToDotnetTypeConvertor)
        {
            FunctionABI = functionABI;
            this._abiTypeToDotnetTypeConvertor = abiTypeToDotnetTypeConvertor;
            
        }

        public string GetSingleOutputReturnType()
        {
            if (FunctionABI.OutputParameters != null && FunctionABI.OutputParameters.Length == 1)
            {
                var parameterModel = new ParameterABIModel(FunctionABI.OutputParameters[0]);
                
                return _abiTypeToDotnetTypeConvertor.Convert(parameterModel.Parameter.Type,  
                    parameterModel.GetStructTypeClassName(), true);
            }
            return null;
        }

        public string GetSingleAbiReturnType()
        {
            if (FunctionABI.OutputParameters != null && FunctionABI.OutputParameters.Length == 1)
            {
                return FunctionABI.OutputParameters[0].Type;
            }
            return null;
        }

        public bool IsMultipleOutput()
        {
            return (FunctionABI.OutputParameters != null && FunctionABI.OutputParameters.Length > 1) || 
                (FunctionABI.OutputParameters != null && FunctionABI.OutputParameters.Length ==1 && 
                FunctionABI.OutputParameters[0].Type.StartsWith("tuple")) ;
        }

        public bool IsSingleOutput()
        {
            return FunctionABI.OutputParameters != null && FunctionABI.OutputParameters.Length == 1 && !FunctionABI.OutputParameters[0].Type.StartsWith("tuple");
        }
  
        public bool HasNoInputParameters()
        {
            return FunctionABI.InputParameters == null || FunctionABI.InputParameters.Length == 0;
        }

        public bool HasNoReturn()
        {
            return FunctionABI.OutputParameters == null || FunctionABI.OutputParameters.Length == 0;
        }

        public bool IsTransaction()
        {
            return FunctionABI.Constant == false;
        }
    }
}
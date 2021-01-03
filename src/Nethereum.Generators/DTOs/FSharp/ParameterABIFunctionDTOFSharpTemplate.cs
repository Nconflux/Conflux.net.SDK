using System;
using System.Linq;
using Nethereum.Generators.Core;
using Nethereum.Generators.Model;

namespace Nethereum.Generators.DTOs
{
    public class ParameterABIFunctionDTOFSharpTemplate
    {
        private ParameterABIModel parameterModel;
        private ParameterABIModelTypeMap parameterAbiModelTypeMap;

        public ParameterABIFunctionDTOFSharpTemplate()
        {
            var typeMapper = new ABITypeToFSharpType();
            parameterAbiModelTypeMap = new ParameterABIModelTypeMap(typeMapper);
        }

        public string GenerateAllProperties(ParameterABI[] parameters)
        {
            return string.Join(Environment.NewLine, parameters.Select(GenerateProperty));
        }

        public string GenerateProperty(ParameterABI parameter)
        {
            var parameterModel = new ParameterABIModel(parameter);
            return
                $@"{SpaceUtils.ThreeTabs}[<Parameter(""{parameter.Type}"", ""{@parameter.Name}"", {parameter.Order})>]
{SpaceUtils.ThreeTabs}member val public {parameterModel.GetPropertyName()} = Unchecked.defaultof<{parameterAbiModelTypeMap.GetParameterDotNetOutputMapType(parameter)}> with get, set";
        }


        public string GenerateAllFunctionParameters(ParameterABI[] parameters)
        {
            return string.Join(", ", parameters.Select(GenerateFunctionParameter));
        }

        public string GenerateFunctionParameter(ParameterABI parameter)
        {
            var parameterModel = new ParameterABIModel(parameter);
            return $@"{parameterModel.GetVariableName()}: {parameterAbiModelTypeMap.GetParameterDotNetOutputMapType(parameter)}";
        }

        public string GenerateAssigmentFunctionParametersToProperties(ParameterABI[] parameters, string objectName, string spacing)
        {
            return string.Join(Environment.NewLine, parameters.Select(x => GenerateAssigmentFunctionParameterToProperty(x, objectName, spacing)));
        }

        public string GenerateAssigmentFunctionParameterToProperty(ParameterABI parameter, string objectName, string spacing)
        {
            var parameterModel = new ParameterABIModel(parameter);
            return $@"{spacing}{objectName}.{parameterModel.GetPropertyName()} <- {parameterModel.GetVariableName()}";
        }
    }
}
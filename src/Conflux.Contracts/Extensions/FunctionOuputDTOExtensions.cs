using Conflux.ABI.FunctionEncoding;
using Conflux.ABI.FunctionEncoding.Attributes;

namespace Conflux.Contracts
{
    public static class FunctionOuputDTOExtensions
    {
        private static FunctionCallDecoder _functionCallDecoder = new FunctionCallDecoder();

        public static TFunctionOutputDTO DecodeOutput<TFunctionOutputDTO>(this TFunctionOutputDTO functionOuputDTO, string output) where TFunctionOutputDTO: IFunctionOutputDTO {
            return _functionCallDecoder.DecodeFunctionOutput(functionOuputDTO, output);
        }
    }
}
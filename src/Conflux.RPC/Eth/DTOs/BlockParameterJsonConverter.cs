using System;
using Newtonsoft.Json;

namespace Conflux.RPC.Eth.DTOs
{
    public class BlockParameterJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var blockParameter = (BlockParameter) value;
            if (blockParameter.WalletAddress!=null)
            {
                writer.WriteValue(blockParameter.WalletAddress);
            }
           
            writer.WriteValue(blockParameter.GetRPCParam());
        
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (BlockParameter);
        }
    }
}
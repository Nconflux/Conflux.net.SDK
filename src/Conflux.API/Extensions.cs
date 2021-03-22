using Conflux.ABI.FunctionEncoding;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;

namespace Conflux.API
{
    public static class Extensions
    {
        //Convert outputs to List<Type>
        public static List<Type> AsList<Type>(this ParameterOutput parameter) => (parameter.Result as List<object>).Cast<Type>().ToList(); 
    }
}

using Conflux.ABI.FunctionEncoding.Attributes;
using Conflux.Contracts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Conflux.API
{

    public class Contract : ContractDeploymentMessage
    {

        public static string BYTECODE = " ";

        public Contract() : base(BYTECODE) { } 
      
    }

}

using DemoWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Conflux.API;
namespace DemoWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private string url = "http://main.confluxrpc.org";
        NConflux conflux = new NConflux("http://main.confluxrpc.org");


        //private string url = "http://gpu100.mistgpu.xyz:30214";
        //NConflux conflux = new NConflux("http://gpu100.mistgpu.xyz:30214");
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<JsonResult> GetBalance(string wallet)
        {
            //var xxx = await conflux.GetGasPrice();
            return Json(await conflux.GetBalance(wallet));
        }

        public async Task<JsonResult> Transfer(string privateKey, string acceptWallet, decimal amount)
        {
            try
            {
                conflux = new NConflux(url, privateKey);
                await conflux.Transfer(acceptWallet, amount);
                return Json("ok");
            }
            catch (Exception ex)
            {
                return Json("网络异常，请稍后重试！");
            }

        }

        public async Task<JsonResult> DeployContract(string abi, string byteCode, string privateKey)
        {

            conflux = new NConflux(url, privateKey.Trim());

            //var contractInfo = await conflux.DeployContract(abi, byteCode.Trim());
            var c = await conflux.DeployContract(byteCode.Trim());
            return Json(c.ContractAddress);
        }

        public async Task<JsonResult> ApplyContractSetValue(string value, string privateKey, string contractAddress, string abi)
        {
            conflux = new NConflux(url, privateKey.Trim());
            await conflux.CallContract(CallType.Gas, abi, contractAddress, "set", new object[] { value });
            return Json("ok");
        }

        public async Task<JsonResult> ApplyContractGetValue(string contractAddress, string abi)
        {
          var getValue=   await conflux.CallContract(CallType.NoGas, abi, contractAddress, "get");
            return Json(getValue);
        }

        public JsonResult Random()
        {
            Random random = new Random();
            List<int> sumResult = new List<int>();
            for (int j = 0; j < 100; j++)
            {
                List<int> result = new List<int>();
                for (int i = 0; i < 540; i++)
                {
                    var x = random.Next(1, 55);
                    if (!result.Contains(x))
                    {
                        result.Add(x);
                    }
                    if (x == 54)
                    {
                        break;
                    }
                }
                sumResult.AddRange(result);
            }
          

            return Json(sumResult);
        }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Conflux.API;
using System.IO;
using System.Numerics;
namespace GetTreasure
{

    public partial class frmForm : Form
    {
        public class Outer
        {
            public int total { get; set; }
            public List<Record> list { get; set; }
        }
        public class Record
        {
            public string transactionHash { get; set; }
            public string from { get; set; }
            public long timestamp { get; set; }
            public string value { get; set; }
        }
        HttpClient http = new HttpClient();
        public frmForm()
        {
            InitializeComponent();
        }
        bool run = false;
        bool _lock = false;

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                run = true;
                Go();
            }
            catch (Exception ex)
            {
                Tool.Log(ex.StackTrace);
            }
        }
        int count = 1;
        public async Task<int> GetAmount()
        {
            var privateKey = txtKey.Text;
            if (!privateKey.Contains("0x"))
            {
                privateKey = "0x" + privateKey;
            }
            var ABI = @"
 [
    {
    ""inputs"": [],
    ""name"": ""getTicketAmount"",
    ""outputs"": [
      {
                ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  }
] 
";
            NConflux c = new NConflux("http://main.confluxrpc.org", privateKey);
            var amout = await c.CallContract(CallType.NoGas, ABI, txtAllIn.Text, "getTicketAmount");
            return Convert.ToInt32(((decimal)amout / 1000000000000000000));
        }
        async void Go()
        {
            try
            {
                var amount = await GetAmount();
                txtAmount.Text = amount.ToString();
                lblStatus.Text = $@"第{count}次," + "运行开始";
                Tool.Log(lblStatus.Text);
                var privateKey = txtKey.Text;
                if (!privateKey.Contains("0x"))
                {
                    privateKey = "0x" + privateKey;
                }
                NConflux c = new NConflux("http://main.confluxrpc.org", privateKey);
                _lock = true;
                var api = $@"https://confluxscan.io/v1/transfer?accountAddress=0x815ba914df462565179567d651b24ef5567ae20d&limit=100&skip=0";
                var result = http.GetAsync(api).Result;
                var data = result.Content.ReadAsStringAsync().Result;
                var parsedData = JsonConvert.DeserializeObject<Outer>(data);
                Record record = null;
                foreach (var pd in parsedData.list)
                {
                    record = pd;
                    if (pd.from.StartsWith("0x1") && pd.value == $@"{amount}000000000000000000")
                    {
                        break;
                    }
                }
                if (record == null)
                {
                    return;
                }
                var createdAt = Tool.UnixTimeStampToDateTime(record.timestamp);

                List<string> ourAddress = new List<string>();
                ourAddress.Add(c.address);
 
                ourAddress.AddRange(txtWe.Text.Replace(" ", "").Split('|'));
                var notMyAddress = !ourAddress.Contains(record.from);
                lblPublicAddress.Text = "钱包地址：" + c.address;
                try
                {
                    lblStatus.Text = $@"上一个人的地址：{record.from} 下注时间 ：{ createdAt},时间过去了：{DateTime.Now.Subtract(createdAt).TotalMinutes }分钟";
                    Tool.Log(lblStatus.Text);
                    if (DateTime.Now.Subtract(createdAt).TotalSeconds > Convert.ToInt32(txtTime.Text) * 60 && notMyAddress)
                    {
                        lblStatus.Text = $@"第{count}次," + "开始发送转账";
                        Tool.Log(lblStatus.Text);
                        #region ABI
                        var FCContract = "0x8e2f2e68eb75bb8b18caafe9607242d4748f8d98";

                        var ABI = @"[
  {
    ""inputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""constructor""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""owner"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""spender"",
        ""type"": ""address""
      },
      {
        ""indexed"": false,
        ""internalType"": ""uint256"",
        ""name"": ""value"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""Approval"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""operator"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""tokenHolder"",
        ""type"": ""address""
      }
    ],
    ""name"": ""AuthorizedOperator"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""operator"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""from"",
        ""type"": ""address""
      },
      {
        ""indexed"": false,
        ""internalType"": ""uint256"",
        ""name"": ""amount"",
        ""type"": ""uint256""
      },
      {
        ""indexed"": false,
        ""internalType"": ""bytes"",
        ""name"": ""data"",
        ""type"": ""bytes""
      },
      {
        ""indexed"": false,
        ""internalType"": ""bytes"",
        ""name"": ""operatorData"",
        ""type"": ""bytes""
      }
    ],
    ""name"": ""Burned"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""operator"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""to"",
        ""type"": ""address""
      },
      {
        ""indexed"": false,
        ""internalType"": ""uint256"",
        ""name"": ""amount"",
        ""type"": ""uint256""
      },
      {
        ""indexed"": false,
        ""internalType"": ""bytes"",
        ""name"": ""data"",
        ""type"": ""bytes""
      },
      {
        ""indexed"": false,
        ""internalType"": ""bytes"",
        ""name"": ""operatorData"",
        ""type"": ""bytes""
      }
    ],
    ""name"": ""Minted"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": false,
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""Paused"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""operator"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""tokenHolder"",
        ""type"": ""address""
      }
    ],
    ""name"": ""RevokedOperator"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""internalType"": ""bytes32"",
        ""name"": ""role"",
        ""type"": ""bytes32""
      },
      {
        ""indexed"": true,
        ""internalType"": ""bytes32"",
        ""name"": ""previousAdminRole"",
        ""type"": ""bytes32""
      },
      {
        ""indexed"": true,
        ""internalType"": ""bytes32"",
        ""name"": ""newAdminRole"",
        ""type"": ""bytes32""
      }
    ],
    ""name"": ""RoleAdminChanged"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""internalType"": ""bytes32"",
        ""name"": ""role"",
        ""type"": ""bytes32""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""sender"",
        ""type"": ""address""
      }
    ],
    ""name"": ""RoleGranted"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""internalType"": ""bytes32"",
        ""name"": ""role"",
        ""type"": ""bytes32""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""sender"",
        ""type"": ""address""
      }
    ],
    ""name"": ""RoleRevoked"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""operator"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""from"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""to"",
        ""type"": ""address""
      },
      {
        ""indexed"": false,
        ""internalType"": ""uint256"",
        ""name"": ""amount"",
        ""type"": ""uint256""
      },
      {
        ""indexed"": false,
        ""internalType"": ""bytes"",
        ""name"": ""data"",
        ""type"": ""bytes""
      },
      {
        ""indexed"": false,
        ""internalType"": ""bytes"",
        ""name"": ""operatorData"",
        ""type"": ""bytes""
      }
    ],
    ""name"": ""Sent"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": false,
        ""internalType"": ""uint256"",
        ""name"": ""id"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""Snapshot"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""from"",
        ""type"": ""address""
      },
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""to"",
        ""type"": ""address""
      },
      {
        ""indexed"": false,
        ""internalType"": ""uint256"",
        ""name"": ""value"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""Transfer"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": false,
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""Unpaused"",
    ""type"": ""event""
  },
  {
    ""anonymous"": false,
    ""inputs"": [
      {
        ""indexed"": true,
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      },
      {
        ""indexed"": false,
        ""internalType"": ""uint256"",
        ""name"": ""CPool"",
        ""type"": ""uint256""
      },
      {
        ""indexed"": false,
        ""internalType"": ""uint256"",
        ""name"": ""PPool"",
        ""type"": ""uint256""
      },
      {
        ""indexed"": false,
        ""internalType"": ""uint256"",
        ""name"": ""PPoolLocked"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""Write"",
    ""type"": ""event""
  },
  {
    ""inputs"": [],
    ""name"": ""CAP"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""DEFAULT_ADMIN_ROLE"",
    ""outputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": """",
        ""type"": ""bytes32""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""MINTER_ROLE"",
    ""outputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": """",
        ""type"": ""bytes32""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""PAUSER_ROLE"",
    ""outputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": """",
        ""type"": ""bytes32""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""RECORDER_ROLE"",
    ""outputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": """",
        ""type"": ""bytes32""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""SPONSOR"",
    ""outputs"": [
      {
        ""internalType"": ""contract SponsorWhitelistControl"",
        ""name"": """",
        ""type"": ""address""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": """",
        ""type"": ""address""
      }
    ],
    ""name"": ""_balances"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""_totalSupply"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""addAdmin"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""addMinter"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""addPauser"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""addRecorder"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""holder"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""address"",
        ""name"": ""spender"",
        ""type"": ""address""
      }
    ],
    ""name"": ""allowance"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""spender"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""value"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""approve"",
    ""outputs"": [
      {
        ""internalType"": ""bool"",
        ""name"": """",
        ""type"": ""bool""
      }
    ],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""operator"",
        ""type"": ""address""
      }
    ],
    ""name"": ""authorizeOperator"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""tokenHolder"",
        ""type"": ""address""
      }
    ],
    ""name"": ""balanceOf"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""snapshotId"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""balanceOfAt"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": ""amount"",
        ""type"": ""uint256""
      },
      {
        ""internalType"": ""bytes"",
        ""name"": ""data"",
        ""type"": ""bytes""
      }
    ],
    ""name"": ""burn"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""decimals"",
    ""outputs"": [
      {
        ""internalType"": ""uint8"",
        ""name"": """",
        ""type"": ""uint8""
      }
    ],
    ""stateMutability"": ""pure"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""defaultOperators"",
    ""outputs"": [
      {
        ""internalType"": ""address[]"",
        ""name"": """",
        ""type"": ""address[]""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": ""role"",
        ""type"": ""bytes32""
      }
    ],
    ""name"": ""getRoleAdmin"",
    ""outputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": """",
        ""type"": ""bytes32""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": ""role"",
        ""type"": ""bytes32""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""index"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""getRoleMember"",
    ""outputs"": [
      {
        ""internalType"": ""address"",
        ""name"": """",
        ""type"": ""address""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": ""role"",
        ""type"": ""bytes32""
      }
    ],
    ""name"": ""getRoleMemberCount"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": ""role"",
        ""type"": ""bytes32""
      },
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""grantRole"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""granularity"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": ""role"",
        ""type"": ""bytes32""
      },
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""hasRole"",
    ""outputs"": [
      {
        ""internalType"": ""bool"",
        ""name"": """",
        ""type"": ""bool""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""isAdmin"",
    ""outputs"": [
      {
        ""internalType"": ""bool"",
        ""name"": """",
        ""type"": ""bool""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""isMinter"",
    ""outputs"": [
      {
        ""internalType"": ""bool"",
        ""name"": """",
        ""type"": ""bool""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""operator"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""address"",
        ""name"": ""tokenHolder"",
        ""type"": ""address""
      }
    ],
    ""name"": ""isOperatorFor"",
    ""outputs"": [
      {
        ""internalType"": ""bool"",
        ""name"": """",
        ""type"": ""bool""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""isPauser"",
    ""outputs"": [
      {
        ""internalType"": ""bool"",
        ""name"": """",
        ""type"": ""bool""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""isRecorder"",
    ""outputs"": [
      {
        ""internalType"": ""bool"",
        ""name"": """",
        ""type"": ""bool""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""name"",
    ""outputs"": [
      {
        ""internalType"": ""string"",
        ""name"": """",
        ""type"": ""string""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""onlyAdminMock"",
    ""outputs"": [],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""onlyMinterMock"",
    ""outputs"": [],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""onlyPauserMock"",
    ""outputs"": [],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""onlyRecorderMock"",
    ""outputs"": [],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""amount"",
        ""type"": ""uint256""
      },
      {
        ""internalType"": ""bytes"",
        ""name"": ""data"",
        ""type"": ""bytes""
      },
      {
        ""internalType"": ""bytes"",
        ""name"": ""operatorData"",
        ""type"": ""bytes""
      }
    ],
    ""name"": ""operatorBurn"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""sender"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""address"",
        ""name"": ""recipient"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""amount"",
        ""type"": ""uint256""
      },
      {
        ""internalType"": ""bytes"",
        ""name"": ""data"",
        ""type"": ""bytes""
      },
      {
        ""internalType"": ""bytes"",
        ""name"": ""operatorData"",
        ""type"": ""bytes""
      }
    ],
    ""name"": ""operatorSend"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""paused"",
    ""outputs"": [
      {
        ""internalType"": ""bool"",
        ""name"": """",
        ""type"": ""bool""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""removeMinter"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""removePauser"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""removeRecorder"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""renounceAdmin"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""renounceMinter"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""renouncePauser"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""renounceRecorder"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": ""role"",
        ""type"": ""bytes32""
      },
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""renounceRole"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""operator"",
        ""type"": ""address""
      }
    ],
    ""name"": ""revokeOperator"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""bytes32"",
        ""name"": ""role"",
        ""type"": ""bytes32""
      },
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      }
    ],
    ""name"": ""revokeRole"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""recipient"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""amount"",
        ""type"": ""uint256""
      },
      {
        ""internalType"": ""bytes"",
        ""name"": ""data"",
        ""type"": ""bytes""
      }
    ],
    ""name"": ""send"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""setupExpiryTime"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""symbol"",
    ""outputs"": [
      {
        ""internalType"": ""string"",
        ""name"": """",
        ""type"": ""string""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""totalSupply"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": ""snapshotId"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""totalSupplyAt"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""view"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""recipient"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""amount"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""transfer"",
    ""outputs"": [
      {
        ""internalType"": ""bool"",
        ""name"": """",
        ""type"": ""bool""
      }
    ],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""holder"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""address"",
        ""name"": ""recipient"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""amount"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""transferFrom"",
    ""outputs"": [
      {
        ""internalType"": ""bool"",
        ""name"": """",
        ""type"": ""bool""
      }
    ],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""cap"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""pure"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address[]"",
        ""name"": ""accounts"",
        ""type"": ""address[]""
      },
      {
        ""internalType"": ""uint256[]"",
        ""name"": ""ConfluxPools"",
        ""type"": ""uint256[]""
      },
      {
        ""internalType"": ""uint256[]"",
        ""name"": ""Personals"",
        ""type"": ""uint256[]""
      },
      {
        ""internalType"": ""uint256[]"",
        ""name"": ""Lockeds"",
        ""type"": ""uint256[]""
      }
    ],
    ""name"": ""batchSetStateOf"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""ConfluxPool"",
        ""type"": ""uint256""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""Personal"",
        ""type"": ""uint256""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""Locked"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""setStateOf"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": ""total"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""setTotalSupply"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [
      {
        ""internalType"": ""address"",
        ""name"": ""account"",
        ""type"": ""address""
      },
      {
        ""internalType"": ""uint256"",
        ""name"": ""value"",
        ""type"": ""uint256""
      }
    ],
    ""name"": ""mint"",
    ""outputs"": [
      {
        ""internalType"": ""bool"",
        ""name"": """",
        ""type"": ""bool""
      }
    ],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""pause"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""unpause"",
    ""outputs"": [],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  },
  {
    ""inputs"": [],
    ""name"": ""snapshot"",
    ""outputs"": [
      {
        ""internalType"": ""uint256"",
        ""name"": """",
        ""type"": ""uint256""
      }
    ],
    ""stateMutability"": ""nonpayable"",
    ""type"": ""function""
  }
] ";
                        //var balance = await c.CallContract(CallType.NoGas, ABI, FCContract, "balanceOf", new object[] { "0x14a540499C6e0A98EAC18D4c195e59FB75E8d824" });
                        decimal b = 1000000000000000000m;
                        var contractResult = await c.CallContract(CallType.Gas, ABI, FCContract, "transfer", new object[] { txtAllIn.Text, new BigInteger(b * amount) });

                        lblStatus.Text = $@"****第{count}次," + $@"已经完成转账, hash:{contractResult} ,我们的地址包括：{txtWe.Text}";
                        Tool.Log(lblStatus.Text);
                        #endregion
                    }
                    else
                    {
                        if (DateTime.Now.Subtract(createdAt).TotalSeconds < Convert.ToInt32(txtTime.Text) * 60)
                        {
                            lblStatus.Text = $@"第{count}次," + "时间阈值未到";
                        }
                        if (!notMyAddress)
                        {
                            lblStatus.Text += $@"第{count}次," + $@"是我们的地址{record.from}，不用下注,我们的地址包括：{txtWe.Text}";
                        }
                        Tool.Log(lblStatus.Text);
                    }
                }
                catch (Exception ex)
                {
                    lblStatus.Text = $@"第{count}次," + ex.StackTrace;
                    _lock = false;
                    Tool.Log(lblStatus.Text);
                }

                _lock = false;
                count++;
            }
            catch (Exception ex)
            {
                Tool.Log(ex.StackTrace);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (run)
            {
                try
                {
                    Go();
                }
                catch (Exception)
                {

                }

            }
        }
    }
}

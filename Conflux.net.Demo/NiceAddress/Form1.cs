using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Conflux.API;
using Conflux.Web3.Accounts;
using Conflux.Address;
using System.Threading;

namespace NiceAddress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            txtResult.Text = await Run();
        }
        async Task<string> Run()
        {
            var result = "";
            var count = 0;
            while (count < 1)
            {
                var prefix = txtPrefix.Text.Trim();
                var suffix = txtSuffix.Text.Trim();

                if (prefix.Contains("o") || prefix.Contains("i") || prefix.Contains("l") || prefix.Contains("q")
                    || suffix.Contains("o") || suffix.Contains("i") || suffix.Contains("l") || suffix.Contains("q")
                    )
                {
                    MessageBox.Show("不能含有oilq");
                }
                var privateKey = NConflux.GeneratePrivateKey();
                var account = new Account(privateKey);
                var newAddress = Base32.Encode(account.Address, "cfx");
                var newAddressRemoveCFX = newAddress.Replace("cfx:aa", "");
                if (prefix != string.Empty)
                {
                    if (suffix != string.Empty)
                    {
                        if (newAddressRemoveCFX.StartsWith(prefix) && newAddressRemoveCFX.EndsWith(suffix))
                        {
                            result += $@"Address: {newAddress} , Key:{account.PrivateKey}";
                            count++;
                        }
                    }
                    else
                    {
                        if (newAddressRemoveCFX.StartsWith(prefix))
                        {
                            result += $@"Address: {newAddress} , Key:{account.PrivateKey}";
                            count++;
                        }
                    }
                }
                else
                {
                    if (suffix != string.Empty)
                    {
                        if (newAddressRemoveCFX.EndsWith(suffix))
                        {
                            result += $@"Address: {newAddress} , Key:{account.PrivateKey}";
                            count++;
                        }
                    }
                    else
                    {
                    }
                }
            }
            return result;

        }
    }
}

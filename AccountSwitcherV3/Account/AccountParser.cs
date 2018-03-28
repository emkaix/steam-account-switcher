using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using AccountSwitcherV3.Properties;

namespace AccountSwitcherV3
{
    public static class AccountParser
    {
        public static IEnumerable<Account> GetAll()
        {
            var jsonstring = string.Empty;

            try
            {
                jsonstring = File.ReadAllText(Environment.CurrentDirectory + Resources.AccountFilePath);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("'accounts.json'.file not found.\nIs it placed in the same directory?", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            //var jsonstring = File.ReadAllText(@"C:\Users\Klaus\Documents\Visual Studio 2015\Projects\AccountSwitcherV3\AccountSwitcherV3\Resources\accounts.json");

            var rootobject = new AccountRootObject();
            try
            {
                rootobject = JsonConvert.DeserializeObject<AccountRootObject>(jsonstring);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Error at deserializing 'accounts.json'.\nFile possibly wrong formatted?", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                Environment.Exit(0);
            }
            
            return rootobject.AccountList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountSwitcherV3
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string VisibleName { get; set; }
        public long SteamID { get; set; }

        public Account(string _username, string _password, string _visiblename)
        {
            Username = _username;
            Password = _password;
            VisibleName = _visiblename;            
        }

        public void Login()
        {
            SteamInstanceHelper.KillCurrentSession();
            SteamInstanceHelper.LoginAccount(this);
        }
    }

    class AccountRootObject
    {
        public IEnumerable<Account> AccountList { get; set; }
    }
}

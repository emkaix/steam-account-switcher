using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountSwitcherV3
{
    class TrayIcon
    {
        
        public event MouseEventHandler Click;

        public NotifyIcon Control { get; set; }

        private ContextMenuStrip menu;
        
        public TrayIcon()
        {
            Control = new NotifyIcon();

            menu = new ContextMenuStrip();
            menu.Items.Add("Exit");

            Control.ContextMenuStrip = menu;
            

            Control.Visible = true;
            Control.Icon = global::AccountSwitcherV3.Properties.Resources.steam_icon_AM3_icon;
            Control.MouseClick += Icon_MouseClick;
            Control.Text = "AccountSwitcherV3";
            
        }

        private void Icon_MouseClick(object sender, MouseEventArgs e)
        {
            OnClick(e);
        }

        

        protected virtual void OnClick(MouseEventArgs e)
        {
            if (Click == null) return;
            Click(this, e);
        }
        
        public void RemoveFromTrayBar()
        {
            Control.Dispose();
        }



    }
}

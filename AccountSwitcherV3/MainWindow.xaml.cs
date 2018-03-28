using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.IO;

namespace AccountSwitcherV3
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        TrayIcon icon = new TrayIcon();
        private bool _imagesloaded;
        private List<EntryControl> lsentries = new List<EntryControl>();


        public MainWindow()
        {
            InitializeComponent();

            icon.Control.ContextMenuStrip.ItemClicked += (s, e) => { this.Close(); };
            
            
            this.Deactivated += HandleIconFocusLoss;
            icon.Click += HandleIconClick;
            

        }

        private async void HandleIconClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SetupWindow();

                while (!this.IsActive)
                {
                    await Task.Delay(10);
                }

                CreateEntries();
            }            
        }
        private void HandleIconFocusLoss(object sender, EventArgs e)
        {

            this.Visibility = Visibility.Hidden;
            if (!_imagesloaded)
            {
                panel.Children.Clear();
            }
            
            scrollviewer.ScrollToHome();

            foreach (var entry in lsentries)
            {
                entry.ResetMargin();
            }

        }

        public void SetupWindow()
        {
            var loc = System.Windows.Forms.Cursor.Position;
            this.Left = loc.X - this.Width / 2;
            this.Top = loc.Y - (this.Height + 10);
            this.Activate();

            this.Visibility = Visibility.Visible;

            
            
        }

        public async void CreateEntries()
        {
            var accs = AccountParser.GetAll();

            if (!_imagesloaded)
            {
                foreach (var acc in accs)
                {
                    var entry = new EntryControl(acc, this);
                    ControlCreator.CreateEntry(panel, entry);
                    lsentries.Add(entry);
                }
            }
            

            var sb = AnimationFactory.GetEntryCreateAnimation(lsentries, 0.1);
            sb.Begin();

            if (!_imagesloaded)
            {
                var t = await SteamWebAPIHelper.FetchPlayerDataAsync(accs);
                
                foreach (var entry in lsentries)
                {
                    var accordingimg = t.Where(x => x.Key == entry.Account.SteamID).FirstOrDefault();
                    entry.ProfileImage.Source = accordingimg.Value;
                    

                }

                _imagesloaded = true;
            }
            
        }

        

        private void Window_Closed(object sender, EventArgs e)
        {
            icon.Control.Dispose();
        }
    }
}

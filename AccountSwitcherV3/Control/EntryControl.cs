using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace AccountSwitcherV3
{
    public class EntryControl
    {
        public Grid Control { get; set; }
        public Account Account { get; set; }
        public Image ProfileImage { get { return this._image; }  }



        public EventHandler MouseDown;

        private Grid _grid;
        private Image _image;
        private TextBlock _textblock;

        private Thickness _margin = new Thickness(5, 400, 5, 5);

        private Storyboard _mousehoverenter;
        private Storyboard _mousehoverleave;


        public EntryControl(Account _acc, MainWindow _wnd)
        {

            _grid = new Grid()
            {
                Margin = _margin,
                Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x6C, 0x68, 0x68)),
                Height = 38
            };

            _grid.MouseDown += _grid_MouseDown;

            _image = new Image()
            {
                Margin = new System.Windows.Thickness(3, 3, 126, 3),
                Source = new BitmapImage(new Uri("/AccountSwitcherV3;component/Resources/default.jpg", UriKind.Relative))
            };

            _textblock = new TextBlock()
            {
                Margin = new System.Windows.Thickness(33, 10, 0, 10),
                Text = _acc.VisibleName,
                VerticalAlignment = System.Windows.VerticalAlignment.Center,
                FontFamily = new FontFamily(new Uri("pack://application:,,,/Fonts/#Roboto"), "Roboto"),
                FontSize = 13

            };

            _grid.Children.Add(_image);
            _grid.Children.Add(_textblock);

            this.Control = _grid;
            this.Account = _acc;

            Control.MouseDown += (s, e) => {
                this.Account.Login();
                _wnd.Hide();
            };

            _mousehoverenter = AnimationFactory.GetMouseHoverEnterEntryAnimation(this, 0.1);
            _mousehoverleave = AnimationFactory.GetMouseHoverLeaveEntryAnimation(this, 0.1);

            Control.MouseEnter += (s, e) => { _mousehoverenter.Begin(); };
            Control.MouseLeave += (s, e) => { _mousehoverleave.Begin(); };
        }

        private void _grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (MouseDown == null) return;
            MouseDown(this, e);
        }

        public void SetImage(BitmapImage _img)
        {
           _image.Dispatcher.Invoke(() => { this._image.Source = _img; });
        }

        public void ResetMargin()
        {
            _grid.SetCurrentValue(Grid.MarginProperty, new Thickness(5, 400, 5, 5));
        }

    }
}

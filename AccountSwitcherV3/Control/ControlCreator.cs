using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace AccountSwitcherV3
{
    public static class ControlCreator
    {

        public static void CreateEntry(StackPanel sp, EntryControl entry)
        {
            sp.Children.Add(entry.Control);
        }

    }
}

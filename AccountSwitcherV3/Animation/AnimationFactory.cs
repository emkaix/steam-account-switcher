using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace AccountSwitcherV3
{
    public static class AnimationFactory
    {
        public static Storyboard GetEntryCreateAnimation(IEnumerable<EntryControl> _entries, double _sequenceduration)
        {
            var begintime = 0.0d;
            var sb = new Storyboard();
            

            foreach (var entry in _entries)
            {
                var flyin = new ThicknessAnimation() { To = new System.Windows.Thickness(5, 5, 5, 5), Duration = TimeSpan.FromSeconds(_sequenceduration), BeginTime = TimeSpan.FromSeconds(begintime) };
                Storyboard.SetTarget(flyin, entry.Control);
                Storyboard.SetTargetProperty(flyin, new System.Windows.PropertyPath(Grid.MarginProperty));

                var fadein = new DoubleAnimation() { From = 0, To = 1, Duration = TimeSpan.FromSeconds(_sequenceduration), BeginTime = TimeSpan.FromSeconds(begintime) };
                Storyboard.SetTarget(fadein, entry.Control);
                Storyboard.SetTargetProperty(fadein, new System.Windows.PropertyPath(Grid.OpacityProperty));
                
                sb.Children.Add(flyin);
                sb.Children.Add(fadein);

                begintime += _sequenceduration;
            }

            return sb;
        }

        public static Storyboard GetMouseHoverEnterEntryAnimation(EntryControl _entry, double _sequenceduration)
        {
            var sb = new Storyboard();

            var animmargin = new ThicknessAnimation() { To = new System.Windows.Thickness(5, 2, 5, 2), Duration = TimeSpan.FromSeconds(_sequenceduration), BeginTime = TimeSpan.FromSeconds(0) };
            Storyboard.SetTarget(animmargin, _entry.Control);
            Storyboard.SetTargetProperty(animmargin, new System.Windows.PropertyPath(Grid.MarginProperty));

            var heightanim = new DoubleAnimation() { To = 44, Duration = TimeSpan.FromSeconds(_sequenceduration), BeginTime = TimeSpan.FromSeconds(0) };
            Storyboard.SetTarget(heightanim, _entry.Control);
            Storyboard.SetTargetProperty(heightanim, new System.Windows.PropertyPath(Grid.HeightProperty));

            var coloranim = new ColorAnimation() { To = Color.FromArgb(0xFF, 0x90, 0x8C, 0x8C), Duration = TimeSpan.FromSeconds(_sequenceduration), BeginTime = TimeSpan.FromSeconds(0) };
            Storyboard.SetTarget(coloranim, _entry.Control);
            Storyboard.SetTargetProperty(coloranim, new System.Windows.PropertyPath("Background.Color"));

            sb.Children.Add(animmargin);
            sb.Children.Add(heightanim);
            sb.Children.Add(coloranim);

            return sb;
        }

        public static Storyboard GetMouseHoverLeaveEntryAnimation(EntryControl _entry, double _sequenceduration)
        {
            var sb = new Storyboard();

            var anim = new ThicknessAnimation() { To = new System.Windows.Thickness(5, 5, 5, 5), Duration = TimeSpan.FromSeconds(_sequenceduration), BeginTime = TimeSpan.FromSeconds(0) };
            Storyboard.SetTarget(anim, _entry.Control);
            Storyboard.SetTargetProperty(anim, new System.Windows.PropertyPath(Grid.MarginProperty));

            var heightanim = new DoubleAnimation() { To = 38, Duration = TimeSpan.FromSeconds(_sequenceduration), BeginTime = TimeSpan.FromSeconds(0) };
            Storyboard.SetTarget(heightanim, _entry.Control);
            Storyboard.SetTargetProperty(heightanim, new System.Windows.PropertyPath(Grid.HeightProperty));

            var coloranim = new ColorAnimation() { To = Color.FromArgb(0xFF, 0x6C, 0x68, 0x68), Duration = TimeSpan.FromSeconds(_sequenceduration), BeginTime = TimeSpan.FromSeconds(0) };
            Storyboard.SetTarget(coloranim, _entry.Control);
            Storyboard.SetTargetProperty(coloranim, new System.Windows.PropertyPath("Background.Color"));

            sb.Children.Add(anim);
            sb.Children.Add(heightanim);
            sb.Children.Add(coloranim);

            return sb;
        }
    }
}

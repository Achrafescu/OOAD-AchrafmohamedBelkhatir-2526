using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfControlsOefenblad.Helpers;

namespace WpfControlsOefenblad.Exercises
{
    [NavPage(Title = "Volume Control", Description = "Slider gebruiken met ValueChanged en property-aanpassing.", Order = 7, IsVisible = true)]
    public partial class VolumeControl : Page
    {
        public VolumeControl()
        {
            InitializeComponent();
        }

        private void Sld1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (tblVolume == null || brdColor == null) return;

            tblVolume.Text = $"Volume: {sld1.Value:F0}%";

            brdColor.Width = sld1.Value * 5;

            if (sld1.Value < 20)
            {
                brdColor.Background = Brushes.Green;
            }
            else if (sld1.Value < 40)
            {
                brdColor.Background = Brushes.Yellow;
            }
            else if (sld1.Value < 60)
            {
                brdColor.Background = Brushes.Orange;
            }
            else if (sld1.Value < 80)
            {
                brdColor.Background = Brushes.Red;
            }
            else
            {
                brdColor.Background = Brushes.DarkRed;
            }
        }
    }
}

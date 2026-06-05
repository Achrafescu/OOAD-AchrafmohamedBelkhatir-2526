using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfControlsOefenblad.Helpers;
using System.Linq;

namespace WpfControlsOefenblad.Exercises
{
    [NavPage(Title = "Live Form Validation", Description = "TextChanged gebruiken voor live validatie en IsEnabled.", Order = 4, IsVisible = true)]
    public partial class LiveFormValidation : Page
    {
        public LiveFormValidation()
        {
            InitializeComponent();
        }

        private void tch_txtPaswoord(object sender, TextChangedEventArgs e)
        {
            int requirements = 0;

            if (string.IsNullOrWhiteSpace(txtPaswoord.Text))
            {
                btnSave.IsEnabled = false;
                txtStatus.Text = "...";
                requirements = 0;
            }

            if (txtPaswoord.Text.Length < 8)
            {
                txtPaswoord.BorderBrush = Brushes.Red;
                txtPaswoord.ToolTip = "Paswoord moet minstens 8 tekens bevatten.";
                txtStatus.Text = "Ongeldig paswoord:" + Environment.NewLine;
                txtStatus.Text += "Minstens 8 tekens vereist" + Environment.NewLine;
                txtStatus.Foreground = Brushes.Red;
            }
            else
            {
                requirements++;
                txtPaswoord.ClearValue(Border.BorderBrushProperty);
                txtPaswoord.ToolTip = null;
            }

            if (txtPaswoord.Text.Any(char.IsUpper))
            {
                requirements++;
            }
            else
            {
                txtPaswoord.BorderBrush = Brushes.Red;
                txtPaswoord.ToolTip = "Paswoord moet een hoofdletter bevatten.";
                txtStatus.Text += "Minstens één hoofdletter vereist" + Environment.NewLine;
                txtStatus.Foreground = Brushes.Red;
            }

            if (txtPaswoord.Text.Any(char.IsDigit))
            {
                requirements++;
            }
            else
            {
                txtPaswoord.BorderBrush = Brushes.Red;
                txtPaswoord.ToolTip = "Paswoord moet een cijfer bevatten.";
                txtStatus.Text += "Minstens één cijfer vereist" + Environment.NewLine;
                txtStatus.Foreground = Brushes.Red;
            }

            if (requirements >= 3 && txtPaswoord.Text.Length >= 8)
            {
                btnSave.IsEnabled = true;
                txtStatus.Text = "Geldig paswoord!";
                txtStatus.Foreground = Brushes.Green;

            }
        }
    }
}

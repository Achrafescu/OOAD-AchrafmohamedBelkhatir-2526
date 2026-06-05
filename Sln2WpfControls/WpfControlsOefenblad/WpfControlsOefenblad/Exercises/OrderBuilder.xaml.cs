using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfControlsOefenblad.Helpers;

namespace WpfControlsOefenblad.Exercises
{
    [NavPage(Title = "Order Builder", Description = "CheckBox + RadioButton met samenvatting en reset.", Order = 5, IsVisible = true)]
    public partial class OrderBuilder : Page
    {
        public OrderBuilder()
        {
            InitializeComponent();
        }

        private void chbKaas_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnBevestig_Click(object sender, RoutedEventArgs e)
        {
            if (rbnAfhalen.IsChecked == false && rbnLevering.IsChecked == false && rbnTerPlaatse.IsChecked == false)
            {
                tbSamenvatting.Text = "Kies eerst een leveringsmethode";
                return;
            } 

            List<string> selectedExtras = new List<string>();

            if (chbKaas.IsChecked == true)
                selectedExtras.Add(chbKaas.Content.ToString());

            if (chbSpek.IsChecked == true)
                selectedExtras.Add(chbSpek.Content.ToString());

            if (chbExtraSaus.IsChecked == true)
                selectedExtras.Add(chbExtraSaus.Content.ToString());

            if (chbUi.IsChecked == true)
                selectedExtras.Add(chbUi.Content.ToString());

            string levering = rbnAfhalen.IsChecked == true ? "Afhalen" : rbnLevering.IsChecked == true ? "Levering" : "Ter Plaatse";


            tbSamenvatting.Text = $"Levering: {levering}" 
                + Environment.NewLine 
                + $" Extras: {string.Join(", ", selectedExtras)}";

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tbSamenvatting.Text = "...";

            chbKaas.IsChecked = true;
            chbSpek.IsChecked = false;
            chbExtraSaus.IsChecked = true;
            chbUi.IsChecked = false;

            rbnAfhalen.IsChecked = false;
            rbnLevering.IsChecked = false;
            rbnTerPlaatse.IsChecked = false;
        }
    }
}

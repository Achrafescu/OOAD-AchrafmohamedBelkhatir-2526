using System;
using System.Windows;
using System.Windows.Controls;
using WpfControlsOefenblad.Helpers;

namespace WpfControlsOefenblad.Exercises
{
    [NavPage(Title = "Welkom", Description = "TextBox met eenvoudige validatie.", Order = 2, IsVisible = true)]
    public partial class Welkom : Page
    {
        public Welkom()
        {
            InitializeComponent();
        }

        private void Click_btnZegHallo(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNaam.Text))
            {
                tblFoutmelding.Visibility = Visibility.Visible;
                tblResultaat.Text = "...";
                return;
            }
            else
            {
                tblFoutmelding.Visibility = Visibility.Hidden;
                tblResultaat.Text = $"Welkom {txtNaam.Text.Trim()}!";
            }
        }
    }
}

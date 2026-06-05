using System.Windows;
using System.Windows.Controls;
using WpfControlsOefenblad.Helpers;

namespace WpfControlsOefenblad.Exercises
{
    [NavPage(Title = "Select And Move", Description = "Items verplaatsen tussen twee ListBoxes.", Order = 9, IsVisible = true)]
    public partial class SelectAndMove : Page
    {
        public SelectAndMove()
        {
            InitializeComponent();
        }

        private void lbxAvailable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnToSelected.IsEnabled = lbxAvailable.SelectedItems.Count > 0;
        }

        private void lbxSelected_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnToAvailable.IsEnabled = lbxSelected.SelectedItems.Count > 0;
        }

        private void btnToSelected_Click(object sender, RoutedEventArgs e)
        {
            var items = new string[lbxAvailable.SelectedItems.Count];
            lbxAvailable.SelectedItems.CopyTo(items, 0);

            foreach (string item in items)
            {
                lbxSelected.Items.Add(item);
                lbxAvailable.Items.Remove(item);
            }

            btnToSelected.IsEnabled = false;
        }

        private void btnToAvailable_Click(object sender, RoutedEventArgs e)
        {
            var items = new string[lbxSelected.SelectedItems.Count];
            lbxSelected.SelectedItems.CopyTo(items, 0);

            foreach (string item in items)
            {
                lbxAvailable.Items.Add(item);
                lbxSelected.Items.Remove(item);
            }

            btnToAvailable.IsEnabled = false;
        }
    }
}
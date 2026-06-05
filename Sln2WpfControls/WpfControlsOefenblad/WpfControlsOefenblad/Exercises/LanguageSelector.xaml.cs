using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfControlsOefenblad.Helpers;
using System.Linq;

namespace WpfControlsOefenblad.Exercises;

[NavPage(Title = "Language Selector", Description = "ComboBox SelectionChanged event en ComboBoxItem", Order = 6, IsVisible = true)]
public partial class LanguageSelector : Page
{
    public LanguageSelector()
    {
        InitializeComponent();

        ComboBoxItem defaultItem = new ComboBoxItem
        {
            Content = "Kies je taal...",
            FontSize = 14,
            Padding = new Thickness(5)
        };
        cbTaal.Items.Add(defaultItem);

        string[] languages = {"Nederlands", "English", "Français"};

        foreach (string language in languages)
        {
            ComboBoxItem item = new ComboBoxItem
            {
                Content = language,
                FontSize = 14,
                Padding = new Thickness(5)
            };
            cbTaal.Items.Add(item);
        }

        cbTaal.SelectedIndex = 0;
    }

    private void cbTaal_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        string begroeting;

        switch (cbTaal.SelectedIndex)
        {
            case 0:
                begroeting = "Hallo!";
                break;
            case 1:
                begroeting = "Hallo!";
                break;
            case 2:
                begroeting = "Hello!";
                break;
            default:
                begroeting = "Bonjour!";
                break;
        }

        txtGeselecteerdeTaal.Text = $"{begroeting}";

        ComboBoxItem selectedItem = (ComboBoxItem)cbTaal.SelectedItem;
        selectedItem.FontWeight = FontWeights.Bold;

        var nonSelectedItems = cbTaal.Items.Cast<ComboBoxItem>().Where(item => item != selectedItem).ToList();
        foreach (ComboBoxItem item in nonSelectedItems)
        {
            item.FontWeight = FontWeights.Normal;
        }
    }
}

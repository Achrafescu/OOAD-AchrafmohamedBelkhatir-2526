using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WpfBestandenOefenblad.Helpers;

namespace WpfBestandenOefenblad.Exercises;

[NavPage(Title = "Pad builder", Description = "Paden samenstellen uit basispad, map en bestandsnaam", Order = 1, IsVisible = true)]
public partial class PadBuilder : Page
{
    public PadBuilder()
    {
        InitializeComponent();
    }

    private string selectedMapPath = string.Empty;
    private string selectedMapName = string.Empty;

    private void rdbCommon_Checked(object sender, RoutedEventArgs e)
    {
        RadioButton rb = sender as RadioButton;
        selectedMapName = rb.Content.ToString();

        switch (selectedMapName)
        {
            case "Documenten":
                selectedMapPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                break;
            case "Afbeeldingen":
                selectedMapPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                break;
            case "Desktop":
                selectedMapPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                break;
            default:
                selectedMapPath = AppDomain.CurrentDomain.BaseDirectory;
                break;
        }
    }

    private void btnGenereerPad_Click(object sender, RoutedEventArgs e)
    {
        txtResultaat.Text = string.Empty;

        string fileDirectory = System.IO.Path.Combine(txtPad.Text, txtBestandsnaam.Text);
        string filePath = System.IO.Path.Combine(selectedMapPath, fileDirectory);

        txtResultaat.Text = filePath.Replace('\\', '/');

    }


}

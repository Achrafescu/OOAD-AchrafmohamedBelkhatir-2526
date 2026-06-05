using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using WpfBestandenOefenblad.Helpers;

namespace WpfBestandenOefenblad.Exercises;

[NavPage(Title = "Lees speciale map", Description = "Drie knoppen: Desktop, Documenten, Afbeeldingen; bestanden met grootte en aanmaakdatum in TextBlock", Order = 6, IsVisible = true)]
public partial class LeesSpecialeMap : Page
{
    public LeesSpecialeMap()
    {
        InitializeComponent();
    }

    private void btnCommon_Click(object sender, RoutedEventArgs e)
    {
        Button button = sender as Button;

        string specialFolderName = button.Tag.ToString();

        string specialFolderPath = GetSpecialFolderPath(specialFolderName);
        /*
        string path = Environment.GetFolderPath((Environment.SpecialFolder)Enum.Parse(typeof(Environment.SpecialFolder), specialFolderName));
        */

        string[] files = Directory.GetFiles(specialFolderPath);

        foreach (string file in files)
        {
            FileInfo fileInfo = new FileInfo(file);
            txtBestanden.Text += $"{fileInfo.Name} ({fileInfo.Length} bytes) - Aanmaakdatum: {fileInfo.CreationTime}\n";
        }

    }

    private string GetSpecialFolderPath(string folderName)
    {
        switch (folderName)
        {
            case "MyDocuments":
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            case "Desktop":
                return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            case "MyPictures":
                return Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            case "MyMusic":
                return Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            case "MyVideos":
                return Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            default:
                return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }

}

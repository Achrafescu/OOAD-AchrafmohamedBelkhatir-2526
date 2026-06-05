using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using WpfBestandenOefenblad.Helpers;

namespace WpfBestandenOefenblad.Exercises;

[NavPage(Title = "Kies afbeelding", Description = "OpenFileDialog om een jpg/jpeg te kiezen en in een Image te tonen", Order = 3, IsVisible = true)]
public partial class KiesAfbeelding : Page
{
    public KiesAfbeelding()
    {
        InitializeComponent();
    }

    private void btnKiesAfbeelding_Click(object sender, RoutedEventArgs e)
    {
        imgAfbeelding.Source = null;

        OpenFileDialog dialog = new OpenFileDialog();
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        dialog.Title = "Kies een afbeelding";
        dialog.Filter = "Images|*.jpg;*.jpeg";

        string chosenFileName = string.Empty;
        bool? dialogResult = dialog.ShowDialog();

        if (dialogResult == true)
        {
            chosenFileName = dialog.FileName;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(chosenFileName, UriKind.Absolute);
            bitmap.EndInit();
            imgAfbeelding.Source = bitmap;

            tblKies.Text = $"Gekozen afbeelding: {Path.GetFileName(chosenFileName)}";
        }
        else
        {
            tblKies.Text = "Kiezen afbeelding geannuleerd";
        }
    }
}

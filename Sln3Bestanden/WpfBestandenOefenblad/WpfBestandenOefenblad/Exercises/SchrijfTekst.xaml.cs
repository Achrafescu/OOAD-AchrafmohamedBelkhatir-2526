using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WpfBestandenOefenblad.Helpers;

namespace WpfBestandenOefenblad.Exercises;

[NavPage(Title = "Schrijf tekst", Description = "Tekst intikken in een TextBox en opslaan naar een bestand", Order = 4, IsVisible = true)]
public partial class SchrijfTekst : Page
{
    public SchrijfTekst()
    {
        InitializeComponent();
    }

    private void btnSchrijfTekst_Click(object sender, RoutedEventArgs e)
    {
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        dialog.Filter = "Tekstbestanden (*.txt)|*.txt";
        dialog.FileName = "untitled.txt";

        if (dialog.ShowDialog() == true)
        {
            File.WriteAllText(dialog.FileName, txtInhoud.Text);
        }
        else
        {
            MessageBox.Show("Bestand opslaan geannuleerd.");
        }

    }
}

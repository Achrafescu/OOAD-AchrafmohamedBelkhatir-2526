using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using WpfBestandenOefenblad.Helpers;

namespace WpfBestandenOefenblad.Exercises;

[NavPage(Title = "Lees CSV", Description = "CSV inlezen (Product;Quantity;Price), regels tonen en totaal verkoopbedrag", Order = 2, IsVisible = true)]
public partial class LeesCsv : Page
{
    public LeesCsv()
    {
        InitializeComponent();
    }

    private void btnLaad_Click(object sender, RoutedEventArgs e)
    {
        txtTotaal.Text = string.Empty;
        lstVerkoopcijfers.Items.Clear();

        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filePath = System.IO.Path.Combine(folderPath, "GitHub", "OOAD-AchrafmohamedBelkhatir-2526", "Sln3Bestanden", "WpfBestandenOefenblad", "WpfBestandenOefenblad", "Exercises", "Files", "verkoop.csv");

        if (!File.Exists(filePath))
        {
            MessageBox.Show($"Bestand niet gevonden: {filePath}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filePath);
            double totaal = 0;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] parts = line.Split(';');

                if (parts.Length != 3)
                    continue;

                if (!int.TryParse(parts[1].Trim(), out int quantityInt)) continue;
                if (!double.TryParse(parts[2].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double priceDouble)) continue;


                {
                    string productString = parts[0].Trim();
                    double subtotal = quantityInt * priceDouble;
                    totaal += subtotal;

                    lstVerkoopcijfers.Items.Add($"{productString} x{quantityInt} aan {priceDouble:c} = {subtotal:c}");
                }
            }

            txtTotaal.Text = $"Totaal verkoopbedrag: {totaal:c}";
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Fout bij het lezen van het bestand: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
        }


    }
}

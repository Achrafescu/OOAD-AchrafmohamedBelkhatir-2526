using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace WpfZalaStock
{

    enum PasvormType
    {
        Slim,
        Fitted,
        Straight,
        Relaxed,
        Regular,
        Baggy,
        Bootcut,
        Tapered,
        Flared
    }

    enum LengteType
    {
        Kort,
        Normaal,
        Lang
    }

    enum BreedteType
    {
        Smal,
        Normaal,
        Wijd
    }

    enum SluitingType
    {
        Veters,
        Rits,
        Klittenband
    }

    enum NeusType
    {
        Rond,
        Puntig,
        Vierkant
    }

    enum MateriaalType
    {
        Goud,
        Zilver,
        Brons,
        Platinum,
        Hout
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                LoadCSVFromResource();
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText(@"C:\Users\molef\crash.txt",
                    ex.GetType().Name + "\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }

        List<Kleding> kledingList = new List<Kleding>();
        List<Schoen> schoenList = new List<Schoen>();
        List<Sieraad> sieraadList = new List<Sieraad>();
        List<Product> productList = new List<Product>();

        double somVerkopen = 0;
        double somGeretourneerd = 0;
        double somNetto = 0;



        private void LoadCSVFromResource()
        {
            try
            {
                string resourceName = "producten.csv";
                StreamResourceInfo resourceInfo = Application.GetResourceStream(new Uri(resourceName, UriKind.Relative));

                if (resourceInfo == null)
                {
                    MessageBox.Show("CSV-bestand niet gevonden in de resources.");
                    return;
                }
                if (resourceInfo.Stream == null)
                {
                    MessageBox.Show("Fout bij het openen van de CSV-stream.");
                    return;
                }

                if (resourceInfo.Stream.CanRead)
                {
                    using (StreamReader reader = new StreamReader(resourceInfo.Stream))
                    {
                        string content = reader.ReadToEnd();
                        ProcessCsvContent(content);
                    }
                }
                else
                {
                    MessageBox.Show("De CSV-stream is niet leesbaar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het laden van de CSV: {ex.Message}");
            }
        }

        private void ProcessCsvContent(string content)
        {

            string[] lines = content.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<string> nonEmptyLines = new List<string>();

            foreach (string line in lines)
            {

                if (!string.IsNullOrWhiteSpace(line))
                {
                    nonEmptyLines.Add(line);
                }
            }

            foreach (string line in nonEmptyLines)
            {
                string[] fields = line.Split(';');

                string categorie = fields[0].Trim();
                string naam = fields[1].Trim();
                string merk = fields[2].Trim();
                double prijs = double.Parse(fields[3].Trim().Replace(',', '.'));
                string kleur = fields[4].Trim();
                int aantalInStock = int.Parse(fields[5].Trim());


                if (double.TryParse(prijs.ToString(), System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out prijs) && int.TryParse(aantalInStock.ToString(), out aantalInStock))
                {
                    // Hier kun je de producten aanmaken en toevoegen aan je collectie
                    // Bijvoorbeeld: Product product = new Schoen(naam, merk, prijs, kleur, aantalInStock, ...);
                    if (categorie == "Kleding")
                    {
                        string pasvorm = fields[6].Trim();
                        string lengte = fields[7].Trim();
                        Kleding kledingUnit = new Kleding(
                            naam: naam,
                            merk: merk,
                            prijs: prijs,
                            kleur: kleur,
                            aantalInStock: aantalInStock,
                            pasvorm: Enum.Parse<PasvormType>(pasvorm),
                            lengte: Enum.Parse<LengteType>(lengte)
                        );
                        kledingList.Add(kledingUnit);
                    }

                    if (categorie == "Schoenen")
                    {
                        string breedte = fields[6].Trim();
                        string sluiting = fields[7].Trim();
                        string neus = fields[8].Trim();
                        Schoen schoenUnit = new Schoen(

                            naam: naam,
                            merk: merk,
                            prijs: prijs,
                            kleur: kleur,
                            aantalInStock: aantalInStock,
                            breedte: Enum.Parse<BreedteType>(breedte),
                            sluiting: Enum.Parse<SluitingType>(sluiting),
                            neus: Enum.Parse<NeusType>(neus)
                        );
                        schoenList.Add(schoenUnit);
                    }

                    if (categorie == "Sieraden")
                    {
                        string materiaal = fields[6].Trim();
                        Sieraad sieraadUnit = new Sieraad(

                            naam: naam,
                            merk: merk,
                            prijs: prijs,
                            kleur: kleur,
                            aantalInStock: aantalInStock,
                            materiaal: Enum.Parse<MateriaalType>(materiaal)
                        );
                        sieraadList.Add(sieraadUnit);
                    }

                    Product productAlle = new Product(

                        naam: naam,
                        merk: merk,
                        prijs: prijs,
                        kleur: kleur,
                        aantalInStock: aantalInStock
                    );
                    productList.Add(productAlle);

                }
                else
                {
                    MessageBox.Show($"Fout bij het parsen van prijs of aantal in stock voor lijn: {line}");
                }
            }
        }

        private void cbxCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBeschikbareProducten == null) return;
            if (cbxCategorie.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedCategory = selectedItem.Content.ToString();
                switch (selectedCategory)
                {
                    case "Alle":
                        // Toon alle producten
                        lstBeschikbareProducten.Items.Clear();
                        foreach (Product productUnit in productList)
                        {
                            lstBeschikbareProducten.Items.Add(productUnit);
                        }
                        break;

                    case "Kleding":
                        // Toon alleen kleding
                        lstBeschikbareProducten.Items.Clear();
                        foreach (Kleding kledingUnit in kledingList)
                        {
                            lstBeschikbareProducten.Items.Add(kledingUnit);
                        }
                        break;

                    case "Schoenen":
                        // Toon alleen schoenen
                        lstBeschikbareProducten.Items.Clear();
                        foreach (Schoen schoenUnit in schoenList)
                        {
                            lstBeschikbareProducten.Items.Add(schoenUnit);
                        }
                        break;

                    case "Sieraden":
                        // Toon alleen sieraden
                        lstBeschikbareProducten.Items.Clear();
                        foreach (Sieraad sieraadUnit in sieraadList)
                        {
                            lstBeschikbareProducten.Items.Add(sieraadUnit);
                        }
                        break;
                }
            }
        }

        private void lstBeschikbareProducten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tblProductdetails.Text = string.Empty;
            tblProductdetails.Text = $"{lstBeschikbareProducten.SelectedItem?.ToString()}\nIn stock: {((Product)lstBeschikbareProducten.SelectedItem)?.AantalInStock}";
        }

        private void btnVerkopen_Click(object sender, RoutedEventArgs e)
        {

            object selectedObject = lstBeschikbareProducten.SelectedItem;
            Product selectedProduct = (Product)selectedObject;


            if (!(lstBeschikbareProducten.SelectedItem == null) && int.TryParse(txtAantal.Text, out int aantalTeVerkopen) && aantalTeVerkopen > 0)
            {
                double totaal = selectedProduct.Prijs * aantalTeVerkopen;
                if (selectedProduct.AantalInStock >= aantalTeVerkopen)
                {
                    lstVerkochtProducten.Items.Add($"{lstBeschikbareProducten.SelectedItem.ToString()} x {txtAantal.Text} - Totaal: {totaal:F2}");
                } 
                else 
                {
                    MessageBox.Show($"Niet genoeg voorraad.\nBeschikbaar: {selectedProduct.AantalInStock}","Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Voer een geldig aantal in (groter dan 0).", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedProduct != null)
            {
                selectedProduct.AantalInStock -= aantalTeVerkopen;
            }
            lstBeschikbareProducten.Items.Refresh();
            tblProductdetails.Text = $"{selectedProduct}\nIn stock: {selectedProduct?.AantalInStock}";

            somVerkopen += selectedProduct.Prijs * aantalTeVerkopen;
            somNetto = somVerkopen - somGeretourneerd;

            tblTotaalVerkopen.Text = $"Totaalbedrag verkopen: € {somVerkopen:F2}";
            tblTotaal.Text = $"Totaalbedrag: € {somNetto:F2}";
        }

        private void btnRetourneren_Click(object sender, RoutedEventArgs e)
        {
            

            object selectedObject = lstBeschikbareProducten.SelectedItem;
            Product selectedProduct = (Product)selectedObject;

            if (lstBeschikbareProducten.SelectedItem == null)
            {
                MessageBox.Show("Selecteer eerst een product.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtAantal.Text, out int aantalTeRetourneren) || aantalTeRetourneren <= 0)
            {
                MessageBox.Show("Voer een geldig aantal in (groter dan 0).", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            double totaal = selectedProduct.Prijs * aantalTeRetourneren;

            lstGeretourneerdProducten.Items.Add($"{lstBeschikbareProducten.SelectedItem.ToString()} x {txtAantal.Text} - Totaal: {totaal:F2}");

            if (selectedProduct != null) {
                selectedProduct.AantalInStock += aantalTeRetourneren;
            }
            lstBeschikbareProducten.Items.Refresh();
            tblProductdetails.Text = $"{selectedProduct}\nIn stock: {selectedProduct?.AantalInStock}";

            somGeretourneerd += selectedProduct.Prijs * aantalTeRetourneren;
            somNetto = somVerkopen - somGeretourneerd;

            tblTotaalRetours.Text = $"Totaalbedrag retours: -€ {somGeretourneerd:F2}";
            tblTotaal.Text = $"Totaalbedrag: € {somNetto:F2}";
        }
    }
}
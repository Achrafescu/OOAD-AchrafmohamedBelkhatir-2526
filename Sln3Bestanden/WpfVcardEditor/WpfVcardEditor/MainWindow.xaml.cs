using Microsoft.Win32;
using System;
using System.Globalization;
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
using System.Windows.Shapes;

namespace WpfVcardEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool _hasUnsavedChanges = false;
        private string _currentFilePath = string.Empty;

        public MainWindow()
        {
            InitializeComponent();

            Card_Changed();

            UpdateStatusBar();
        }

        private void Card_Changed()
        {
            txtAchternaam.TextChanged += (s, ev) => { _hasUnsavedChanges = true; UpdateStatusBar(); };
            txtVoornaam.TextChanged += (s, ev) => { _hasUnsavedChanges = true; UpdateStatusBar(); };
            dpGeboortedatum.SelectedDateChanged += (s, ev) => { _hasUnsavedChanges = true; UpdateStatusBar(); };
            rdbMan.Checked += (s, ev) => { _hasUnsavedChanges = true; UpdateStatusBar(); };
            rdbVrouw.Checked += (s, ev) => { _hasUnsavedChanges = true; UpdateStatusBar(); };
            rdbOnbekend.Checked += (s, ev) => { _hasUnsavedChanges = true; UpdateStatusBar(); };
            txtEmail.TextChanged += (s, ev) => { _hasUnsavedChanges = true; UpdateStatusBar(); };
            txtTelefoon.TextChanged += (s, ev) => { _hasUnsavedChanges = true; UpdateStatusBar(); };
            if (imgFoto.Source != null)
            {
                _hasUnsavedChanges = true;
            }

        }

        private void MarkImageAsChanged(object sender, RoutedEventArgs e)
        {
            _hasUnsavedChanges = true;
        }



        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openVcard = new OpenFileDialog();
            openVcard.Title = "Open vCard File";
            openVcard.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openVcard.Filter = "vCard files (*.vcf)|*.vcf|All files (*.*)|*.*";

            string chosenFile = string.Empty;

            if (openVcard.ShowDialog() == true)
            {
                chosenFile = openVcard.FileName;
                _currentFilePath = openVcard.FileName;
                // Handle the selected file


                if (!File.Exists(chosenFile))
                {
                    MessageBox.Show($"Bestand niet gevonden: {chosenFile}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    string[] vcardLines = System.IO.File.ReadAllLines(chosenFile, Encoding.UTF8);

                    foreach (string line in vcardLines)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        if (line == "BEGIN:VCARD" || line == "END:VCARD") continue;

                        int colonIndex = line.IndexOf(':');
                        if (colonIndex < 0) continue;

                        string rawKey = line.Substring(0, colonIndex).ToUpper();
                        string key = rawKey.Split(';')[0]; // ✅ ignore les paramètres comme CHARSET=UTF-8
                        string value = line.Substring(colonIndex + 1);

                        System.Diagnostics.Debug.WriteLine($"KEY: '{key}' | VALUE: '{value}'");

                        switch (key)
                        {
                            case "N":
                                string[] nameParts = value.Split(';');
                                if (nameParts.Length >= 2)
                                {
                                    txtAchternaam.Text = nameParts[0];
                                    txtVoornaam.Text = nameParts[1];
                                }
                                break;

                            case "BDAY":
                                // Format attendu : 1990-05-15
                                if (DateTime.TryParseExact(value, "yyyyMMdd",
                                System.Globalization.CultureInfo.InvariantCulture,
                                System.Globalization.DateTimeStyles.None, out DateTime bday))
                                {
                                    dpGeboortedatum.SelectedDate = bday;
                                }
                                break;

                            case "GENDER":
                                if (value == "M") rdbMan.IsChecked = true;
                                else if (value == "F") rdbVrouw.IsChecked = true;
                                else rdbOnbekend.IsChecked = true;
                                break;

                            case "TEL":
                                txtTelefoon.Text = value;
                                break;

                            case "EMAIL":
                                txtEmail.Text = value;
                                break;

                            case "PHOTO":
                                // ignoré pour l'instant
                                System.Diagnostics.Debug.WriteLine($"PHOTO length: {value.Length}");
                                System.Diagnostics.Debug.WriteLine($"PHOTO preview: {value.Substring(0, Math.Min(50, value.Length))}");
                                if (!string.IsNullOrWhiteSpace(value))
                                    ShowBase64Image(value);
                                break;
                        }

                    }




                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fout bij het lezen van het bestand: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);


                }
                UpdateStatusBar();
            }

        }

        private void ShowBase64Image(string base64Data)
        {
            try
            {
                string cleanBase64 = base64Data.Replace("\r", "").Replace("\n", "").Replace(" ", "");
                byte[] imageBytes = Convert.FromBase64String(cleanBase64);
                BitmapImage bitmap = new BitmapImage();
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    bitmap.Freeze(); // pour éviter les problèmes de thread
                }
                imgFoto.Source = bitmap;
                _hasUnsavedChanges = true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Le format Base64 n'est pas valide.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het laden van de afbeelding: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFoto = new OpenFileDialog();
            openFoto.Title = "Select Foto File";
            openFoto.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFoto.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";

            if (openFoto.ShowDialog() == true)
            {
                string fotoPath = openFoto.FileName;
                try
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(fotoPath);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    imgFoto.Source = bitmap;
                    tblFoto.Text = System.IO.Path.GetFileName(fotoPath);
                    _hasUnsavedChanges = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fout bij het laden van de afbeelding: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string ImageToBase64()
        {
            if (imgFoto.Source == null) return string.Empty;
            try
            {
                BitmapSource? bitmapSource = imgFoto.Source as BitmapSource;
                if (bitmapSource == null) return string.Empty;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    byte[] imageBytes = ms.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fout bij het converteren van de afbeelding: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return string.Empty;
            }
        }

        private void menuOpslaanAls_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveVcard = new SaveFileDialog();
            saveVcard.Title = "Save vCard File";
            saveVcard.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveVcard.Filter = "vCard files (*.vcf)|*.vcf|All files (*.*)|*.*";
            saveVcard.DefaultExt = "vcf";
            saveVcard.FileName = "contact.vcf";

            if (saveVcard.ShowDialog() == true)
            {
                _currentFilePath = saveVcard.FileName;
                string savePath = saveVcard.FileName;
                try
                {
                    StringBuilder vcfContent = new StringBuilder();
                    vcfContent.AppendLine("BEGIN:VCARD");
                    vcfContent.AppendLine("VERSION:3.0");

                    if (!string.IsNullOrWhiteSpace(txtAchternaam.Text) && !string.IsNullOrWhiteSpace(txtVoornaam.Text))
                    { 
                        vcfContent.AppendLine($"N;CHARSET=UTF-8:{txtAchternaam.Text};{txtVoornaam.Text};;;");
                    }
                    else if (!string.IsNullOrWhiteSpace(txtAchternaam.Text))
                    {
                        vcfContent.AppendLine($"N;CHARSET=UTF-8:{txtAchternaam.Text};;;;"); 
                    }
                    else if (!string.IsNullOrWhiteSpace(txtVoornaam.Text))
                    {
                        vcfContent.AppendLine($"N;CHARSET=UTF-8:;{txtVoornaam.Text};;;"); 
                    }

                    if (dpGeboortedatum.SelectedDate.HasValue)
                    {
                        string birthDate = dpGeboortedatum.SelectedDate.Value.ToString("yyyyMMdd");
                        vcfContent.AppendLine($"BDAY:{birthDate}");
                    }
                        
                    if (rdbMan.IsChecked == true) vcfContent.AppendLine("GENDER:M");
                    if (rdbVrouw.IsChecked == true) vcfContent.AppendLine("GENDER:F");
                    if (rdbOnbekend.IsChecked == true) vcfContent.AppendLine("GENDER:X");

                    if (!string.IsNullOrWhiteSpace(txtEmail.Text))
                    {
                        vcfContent.AppendLine($"EMAIL;CHARSET=UTF-8;TYPE=HOME,INTERNET:{txtEmail.Text}");
                    }

                    if (!string.IsNullOrWhiteSpace(txtTelefoon.Text))
                    {
                        vcfContent.AppendLine($"TEL;TYPE=HOME,VOICE:{txtTelefoon.Text}");
                    }

                    if (imgFoto.Source != null && !string.IsNullOrEmpty(ImageToBase64()))
                    {
                        string photoBase64 = ImageToBase64();
                        vcfContent.AppendLine($"PHOTO;ENCODING=BASE64;TYPE=JPEG:{photoBase64}");
                    }

                    vcfContent.AppendLine("END:VCARD");

                    File.WriteAllText(savePath, vcfContent.ToString(), Encoding.UTF8);

                    MessageBox.Show($"vCard succesvol opgeslagen!\nBestand: {saveVcard.FileName}",
                          "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fout bij het opslaan van het bestand: {ex.Message}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                UpdateStatusBar();
            }
        }

        private void menuNieuw_Click(object sender, RoutedEventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                MessageBoxResult result = MessageBox.Show("Er zijn niet-opgeslagen wijzigingen. Wilt u doorgaan?", "Waarschuwing", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }

            txtAchternaam.Clear();
            txtVoornaam.Clear();
            txtEmail.Clear();
            txtTelefoon.Clear();
            dpGeboortedatum.SelectedDate = null;
            rdbMan.IsChecked = false;
            rdbVrouw.IsChecked = false;
            rdbOnbekend.IsChecked = false;
            imgFoto.Source = null;
            _hasUnsavedChanges = false;
            _currentFilePath = string.Empty;
            UpdateStatusBar();
        }

        private void menuOpslaan_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gebruik 'Opslaan als...' om een vCard-bestand op te slaan.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void menuAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            if (_hasUnsavedChanges)
            {
                MessageBoxResult result = MessageBox.Show("Er zijn niet-opgeslagen wijzigingen. Wilt u afsluiten?", "Waarschuwing", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    return;
                }
            }
            Application.Current.Shutdown();
        }

        private void menuOver_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().Show();
        }

        private void UpdateStatusBar()
        {
            // 1. Mettre à jour le nom du fichier
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                tblCurrentFile.Text = "📄 huidige kaart: (geen geopend)";
            }
            else
            {
                string fileName = System.IO.Path.GetFileName(_currentFilePath);
                tblCurrentFile.Text = $"📄 huidige kaart: {fileName}";
            }

            // 2. Calculer le pourcentage de champs remplis
            int filledFields = 0;
            int totalFields = 0;

            // Définir les champs à vérifier
            var fields = new List<Func<bool>>
    {
        () => !string.IsNullOrWhiteSpace(txtAchternaam.Text),
        () => !string.IsNullOrWhiteSpace(txtVoornaam.Text),
        () => dpGeboortedatum.SelectedDate.HasValue,
        () => rdbMan.IsChecked == true || rdbVrouw.IsChecked == true || rdbOnbekend.IsChecked == true,
        () => !string.IsNullOrWhiteSpace(txtEmail.Text),
        () => !string.IsNullOrWhiteSpace(txtTelefoon.Text),
        () => imgFoto.Source != null
    };

            totalFields = fields.Count;
            foreach (var field in fields)
            {
                if (field())
                    filledFields++;
            }

            int percentage = (int)((double)filledFields / totalFields * 100);
            tblPercentage.Text = $"📊 ingevuld: {percentage}%";
        }




    }
}

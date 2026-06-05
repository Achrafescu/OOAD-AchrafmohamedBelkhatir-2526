using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System;
using System.Windows;
using System.Windows.Controls;
using CLHelpdesk;


namespace WpfHelpdesk
{
    /// <summary>
    /// Interaction logic for TicketToevoegenWindow.xaml
    /// </summary>
    public partial class TicketToevoegenWindow : Window
    {
        // ── Velden ───────────────────────────────────────────────
        private TicketBeheer _beheer;

        // ── Constructor ──────────────────────────────────────────
        public TicketToevoegenWindow(TicketBeheer beheer)
        {
            InitializeComponent();
            _beheer = beheer;
        }

        // ════════════════════════════════════════════════════════
        // EVENTS
        // ════════════════════════════════════════════════════════

        // Pas het label aan naargelang het type
        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblToestel == null) return;

            string type = (cmbType.SelectedItem as ComboBoxItem)?.Content.ToString();
            lblToestel.Content = (type == "Software") ? "Applicatie *" : "Toestel *";
        }

        // Sluit het venster zonder op te slaan
        private void btnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        // Valideert en slaat het nieuwe ticket op
        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            // ── Validatie ─────────────────────────────────────
            if (!IsGeldig()) return;

            // ── Gegevens uitlezen ─────────────────────────────
            string titel = txtTitel.Text.Trim();
            string voornaam = txtVoornaam.Text.Trim();
            string achternaam = txtAchternaam.Text.Trim();
            string melderId = txtMelderId.Text.Trim();
            string toestel = txtToestel.Text.Trim();

            // Prioriteit parsen
            string prioriteitTekst = (cmbPrioriteit.SelectedItem as ComboBoxItem)?.Content.ToString();
            TicketPrioriteit prioriteit = TicketPrioriteit.Normaal;
            if (prioriteitTekst == "Hoog") prioriteit = TicketPrioriteit.Hoog;
            if (prioriteitTekst == "Laag") prioriteit = TicketPrioriteit.Laag;

            // Type uitlezen
            string typeTekst = (cmbType.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Medewerker zoeken of aanmaken
            Medewerker melder = ZoekOfMaakMedewerker(melderId, voornaam, achternaam);

            // Nieuw ID genereren
            int nieuwId = _beheer.GeefNieuwId();

            // ── Juist object aanmaken ─────────────────────────
            Ticket nieuwTicket;
            if (typeTekst == "Hardware")
            {
                nieuwTicket = new HardwareTicket(nieuwId, titel, melder, prioriteit, DateTime.Now, toestel);
            }
            else
            {
                nieuwTicket = new SoftwareTicket(nieuwId, titel, melder, prioriteit, DateTime.Now, toestel);
            }

            // ── Opslaan via de class library ──────────────────
            _beheer.VoegTicketToe(nieuwTicket);

            MessageBox.Show("Ticket #" + nieuwId + " werd succesvol opgeslagen!",
                            "Opgeslagen", MessageBoxButton.OK, MessageBoxImage.Information);

            // Signaleer aan MainWindow dat het gelukt is
            DialogResult = true;
            Close();
        }

        // ════════════════════════════════════════════════════════
        // HULPFUNCTIES
        // ════════════════════════════════════════════════════════

        // Controleert alle verplichte velden en toont foutmelding
        private bool IsGeldig()
        {
            // Verberg vorige fout
            txtFout.Visibility = Visibility.Collapsed;

            if (txtTitel.Text.Trim() == "")
            {
                ToonFout("Vul een titel in.");
                return false;
            }
            if (txtVoornaam.Text.Trim() == "")
            {
                ToonFout("Vul de voornaam van de melder in.");
                return false;
            }
            if (txtAchternaam.Text.Trim() == "")
            {
                ToonFout("Vul de achternaam van de melder in.");
                return false;
            }
            if (txtMelderId.Text.Trim() == "")
            {
                ToonFout("Vul het ID van de melder in.");
                return false;
            }
            if (txtToestel.Text.Trim() == "")
            {
                string label = (cmbType.SelectedItem as ComboBoxItem)?.Content.ToString() == "Software"
                    ? "applicatie" : "toestel";
                ToonFout("Vul het " + label + " in.");
                return false;
            }
            if (cmbPrioriteit.SelectedItem == null)
            {
                ToonFout("Selecteer een prioriteit.");
                return false;
            }
            if (cmbType.SelectedItem == null)
            {
                ToonFout("Selecteer een type.");
                return false;
            }

            return true;
        }

        // Toont een foutmelding in het rood
        private void ToonFout(string bericht)
        {
            txtFout.Text = "⚠ " + bericht;
            txtFout.Visibility = Visibility.Visible;
        }

        // Zoekt een bestaande medewerker op ID of maakt een nieuwe aan
        private Medewerker ZoekOfMaakMedewerker(string id, string voornaam, string achternaam)
        {
            foreach (Medewerker m in _beheer.Medewerkers)
            {
                if (m.Id == id) return m;
            }

            // Nieuwe medewerker aanmaken en toevoegen
            Medewerker nieuw = new Medewerker(id, voornaam, achternaam);
            _beheer.Medewerkers.Add(nieuw);
            return nieuw;
        }
    }
}

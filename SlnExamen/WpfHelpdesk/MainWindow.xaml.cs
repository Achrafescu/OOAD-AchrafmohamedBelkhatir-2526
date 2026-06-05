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
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using CLHelpdesk;

namespace WpfHelpdesk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // ── Velden ───────────────────────────────────────────────
        private TicketBeheer _beheer;
        private string _csvPad;

        // ── Constructor ──────────────────────────────────────────
        public MainWindow()
        {
            InitializeComponent();

            // Pad naar de CSV naast de .exe
            _csvPad = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tickets.csv");

            // TicketBeheer aanmaken; leest CSV automatisch in
            _beheer = new TicketBeheer(_csvPad);

            // Melders in de filter-combobox laden
            VulMeldersCombobox();

            // Ticketlijst tonen
            LaadTicketLijst();
        }

        // ════════════════════════════════════════════════════════
        // LADEN
        // ════════════════════════════════════════════════════════

        // Vult de combobox "Melder" met alle unieke melders uit de CSV
        private void VulMeldersCombobox()
        {
            cmbFilterMelder.Items.Clear();
            cmbFilterMelder.Items.Add("Alle");

            foreach (Medewerker m in _beheer.Medewerkers)
            {
                cmbFilterMelder.Items.Add(m.Voornaam + " " + m.Achternaam);
            }

            cmbFilterMelder.SelectedIndex = 0;
        }

        // Laadt de gefilterde ticketlijst in de ListBox
        private void LaadTicketLijst()
        {
            // Huidig geselecteerd ticket onthouden (om na reload te herselecteren)
            int geselecteerdId = -1;
            if (lstTickets.SelectedItem is Ticket huidig)
            {
                geselecteerdId = huidig.Id;
            }

            lstTickets.Items.Clear();

            // Filters uitlezen
            string filterStatus = (cmbFilterStatus.SelectedItem as ComboBoxItem)?.Content.ToString();
            string filterPrioriteit = (cmbFilterPrioriteit.SelectedItem as ComboBoxItem)?.Content.ToString();
            string filterType = (cmbFilterType.SelectedItem as ComboBoxItem)?.Content.ToString();
            string filterMelder = cmbFilterMelder.SelectedItem?.ToString();

            foreach (Ticket t in _beheer.Tickets)
            {
                // ── Statusfilter ──
                if (filterStatus == "Open" && t.IsAfgesloten) continue;
                if (filterStatus == "Afgesloten" && !t.IsAfgesloten) continue;

                // ── Prioriteitfilter ──
                if (filterPrioriteit != "Alle" && t.Prioriteit.ToString() != filterPrioriteit) continue;

                // ── Typefilter ──
                if (filterType == "Hardware" && !(t is HardwareTicket)) continue;
                if (filterType == "Software" && !(t is SoftwareTicket)) continue;

                // ── Melderfilter ──
                if (filterMelder != "Alle")
                {
                    string volleNaam = t.Melder.Voornaam + " " + t.Melder.Achternaam;
                    if (volleNaam != filterMelder) continue;
                }

                lstTickets.Items.Add(t);
            }

            // Probeer hetzelfde ticket opnieuw te selecteren
            if (geselecteerdId != -1)
            {
                foreach (object item in lstTickets.Items)
                {
                    if (item is Ticket t && t.Id == geselecteerdId)
                    {
                        lstTickets.SelectedItem = item;
                        break;
                    }
                }
            }

            // Detailpaneel wissen als niets geselecteerd
            if (lstTickets.SelectedItem == null)
            {
                txtDetail.Text = "";
                btnAfsluiten.IsEnabled = false;
            }
        }

        // ════════════════════════════════════════════════════════
        // EVENTS — FILTERS
        // ════════════════════════════════════════════════════════

        // Wordt opgeroepen als één van de filters verandert
        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
            // Controleer of de venster al volledig geladen is
            if (lstTickets == null) return;
            LaadTicketLijst();
        }

        // ════════════════════════════════════════════════════════
        // EVENTS — LIJSTSELECTIE
        // ════════════════════════════════════════════════════════

        // Toont de details van het geselecteerde ticket
        private void lstTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstTickets.SelectedItem is Ticket geselecteerd)
            {
                // Details tonen via de polymorfische GeefInfo()
                txtDetail.Text = geselecteerd.GeefInfo();

                // "Afsluiten"-knop alleen actief als ticket nog open is
                btnAfsluiten.IsEnabled = !geselecteerd.IsAfgesloten;
            }
            else
            {
                txtDetail.Text = "";
                btnAfsluiten.IsEnabled = false;
            }
        }

        // ════════════════════════════════════════════════════════
        // EVENTS — KNOPPEN
        // ════════════════════════════════════════════════════════

        // Opent het venster om een nieuw ticket toe te voegen
        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            TicketToevoegenWindow venster = new TicketToevoegenWindow(_beheer);
            bool? resultaat = venster.ShowDialog();

            // Als het ticket succesvol werd toegevoegd, alles herladen
            if (resultaat == true)
            {
                VulMeldersCombobox();
                LaadTicketLijst();
            }
        }

        // Sluit het geselecteerde ticket af
        private void btnAfsluiten_Click(object sender, RoutedEventArgs e)
        {
            if (!(lstTickets.SelectedItem is Ticket geselecteerd)) return;

            // Bevestigingsdialoog
            MessageBoxResult bevestiging = MessageBox.Show(
                "Ben je zeker dat je ticket #" + geselecteerd.Id + " wil afsluiten?",
                "Ticket afsluiten",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (bevestiging != MessageBoxResult.Yes) return;

            // Afsluiten via de class library (schrijft ook naar CSV)
            _beheer.SluitTicketAf(geselecteerd.Id);

            MessageBox.Show("Ticket #" + geselecteerd.Id + " is afgesloten.",
                            "Afgesloten", MessageBoxButton.OK, MessageBoxImage.Information);

            // Overzicht herladen
            LaadTicketLijst();
        }
    }
}
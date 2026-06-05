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

namespace WpfTaken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private Run _foutUitvoerder = new Run("gelieve een uitvoerder te kiezen\n");

        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            string rdbWie = string.Empty;

            if (rdbAdam.IsChecked == true)
            {
                rdbWie = rdbAdam.Content.ToString();
            }
            else if (rdbBilal.IsChecked == true)
            {
                rdbWie = rdbBilal.Content.ToString();
            }
            else if (rdbChelsey.IsChecked == true)
            {
                rdbWie = rdbChelsey.Content.ToString();
            }
            else
            { rdbWie = "Onbekend"; }


            string taak = $"{tbxTaak.Text} (deadline: {dpDeadline.SelectedDate.Value.ToString("dd/MM/yyyy")}; door: {rdbWie})";

            ListBoxItem item = new ListBoxItem();
            item.Content = taak;

            switch (lbxPrioriteit.SelectedIndex)
            {
                case 1:
                    item.Background = new SolidColorBrush(Colors.LightSeaGreen); // Handle low priority
                    break;
                case 2:
                    item.Background = new SolidColorBrush(Colors.LightYellow); // Handle medium priority
                    break;
                case 3:
                    item.Background = new SolidColorBrush(Colors.LightCoral); // Handle high priority
                    break;
            }

            if (CheckForm() == true)
            {
                lbxList.FontWeight = FontWeights.Bold;
                lbxList.Items.Add(item);
                tblFoutmelding.Text = string.Empty;
                btnVerwijder.IsEnabled = true;
            }

            if (string.IsNullOrWhiteSpace(tbxTaak.Text))
            {
                tblFoutmelding.Inlines.Add(_foutTaak);
                tblFoutmelding.Foreground = new SolidColorBrush(Colors.Red);
                tblFoutmelding.FontStyle = FontStyles.Italic;
                tbxTaak.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (lbxPrioriteit.SelectedIndex == 0)
            {
                tblFoutmelding.Inlines.Add(_foutPrioriteit);
                tblFoutmelding.Foreground = new SolidColorBrush(Colors.Red);
                tblFoutmelding.FontStyle = FontStyles.Italic;
                lbxPrioriteit.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (!dpDeadline.SelectedDate.HasValue)
            {
                tblFoutmelding.Inlines.Add(_foutDeadline);
                tblFoutmelding.Foreground = new SolidColorBrush(Colors.Red);
                tblFoutmelding.FontStyle = FontStyles.Italic;
                dpDeadline.BorderBrush = new SolidColorBrush(Colors.Red);
            }

            if (rdbAdam.IsChecked != true && rdbBilal.IsChecked != true && rdbChelsey.IsChecked != true)
            {
                tblFoutmelding.Inlines.Add(_foutUitvoerder);
                tblFoutmelding.Foreground = new SolidColorBrush(Colors.Red);
                tblFoutmelding.FontStyle = FontStyles.Italic;
                rdbAdam.BorderBrush = new SolidColorBrush(Colors.Red);
                rdbBilal.BorderBrush = new SolidColorBrush(Colors.Red);
                rdbChelsey.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tblFoutmelding.Inlines.Remove(_foutUitvoerder);
            }








        }

        private bool CheckForm()
        {
            if (string.IsNullOrWhiteSpace(tbxTaak.Text) || !dpDeadline.SelectedDate.HasValue || lbxPrioriteit.SelectedIndex == 0 || (rdbAdam.IsChecked != true && rdbBilal.IsChecked != true && rdbChelsey.IsChecked != true))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private Run _foutTaak = new Run("gelieve de taak in te vullen\n");

        private void tbxTaak_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (tblFoutmelding == null || tbxTaak == null) return;

            if (string.IsNullOrWhiteSpace(tbxTaak.Text))
            {
                tblFoutmelding.Inlines.Add(_foutTaak);
                tblFoutmelding.Foreground = new SolidColorBrush(Colors.Red);
                tblFoutmelding.FontStyle = FontStyles.Italic;
                tbxTaak.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tblFoutmelding.Inlines.Remove(_foutTaak);
                tbxTaak.BorderBrush = new SolidColorBrush(Colors.Black);
            }
        }

        private Run _foutPrioriteit = new Run("gelieve een prioriteit te kiezen\n");

        private void lbxPrioriteit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (tblFoutmelding == null || lbxPrioriteit == null) return;

            if (lbxPrioriteit.SelectedIndex == 0)
            {
                tblFoutmelding.Inlines.Add(_foutPrioriteit);
                tblFoutmelding.Foreground = new SolidColorBrush(Colors.Red);
                tblFoutmelding.FontStyle = FontStyles.Italic;
                lbxPrioriteit.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tblFoutmelding.Inlines.Remove(_foutPrioriteit);
                lbxPrioriteit.BorderBrush = new SolidColorBrush(Colors.Black);
            }
        }

        private Run _foutDeadline = new Run("gelieve een deadline te kiezen\n");

        private void dpDeadline_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            if (tblFoutmelding == null || dpDeadline == null) return;

            if (!dpDeadline.SelectedDate.HasValue)
            {
                tblFoutmelding.Inlines.Add(_foutDeadline);
                tblFoutmelding.Foreground = new SolidColorBrush(Colors.Red);
                tblFoutmelding.FontStyle = FontStyles.Italic;
                dpDeadline.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tblFoutmelding.Inlines.Remove(_foutDeadline);
                dpDeadline.BorderBrush = new SolidColorBrush(Colors.Black);
            }
        }

        Stack<ListBoxItem> verwijderdItems = new Stack<ListBoxItem>();
        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            /*verwijderdItems.Push((ListBoxItem)lbxList.SelectedItem);*/
            verwijderdItems.Push(lbxList.SelectedItem as ListBoxItem);
            lbxList.Items.Remove(lbxList.SelectedItem);
            btnTerug.IsEnabled = true;

            if (lbxList.Items.Count == 0)
            {
                btnVerwijder.IsEnabled = false;
            }
        }

        private void btnTerug_Click(object sender, RoutedEventArgs e)
        {
            /*verwijderdItems.TryPop(out ListBoxItem item);*/
            
            lbxList.Items.Add(verwijderdItems.Pop());

            if (verwijderdItems.Count == 0)
            {
                btnTerug.IsEnabled = false;
            }

            if (lbxList.Items.Count > 0)
            {
                btnVerwijder.IsEnabled = true;
            }
        }
    }
}
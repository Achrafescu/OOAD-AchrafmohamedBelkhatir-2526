using System.Windows;

namespace WpfLayoutUIOefenblad.Exercises
{
    /// <summary>
    /// Interaction logic for NotepadAppAbout.xaml
    /// </summary>
    public partial class NotepadAppAbout : Window
    {
        public NotepadAppAbout()
        {
            InitializeComponent();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

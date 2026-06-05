using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfPlaylist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] song1 =
        {
            "belvedere-assiege-1m.mp3",
            "La Belvedere Assiege",
            "1991",
            "04:14",
            "anouar-brahem.jpg",
            "Anouar Brahem"
        };

        string[] song2 =
        {
            "ronda-1m.mp3",
            "Ronda",
            "1991",
            "03:07",
            "anouar-brahem.jpg",
            "Anouar Brahem"
        };

        string[] song3 =
        {
            "raf-raf-1m.mp3",
            "Raf Raf",
            "1991",
            "03:33",
            "anouar-brahem.jpg",
            "Anouar Brahem"
        };

        string[] song4 =
        {
            "great-indian-desert-1m.mp3",
            "The great indian desert",
            "2007",
            "06:54",
            "zakir-houssein.jpg",
            "Zakir Houssein"
        };

        string[] song5 =
        {
            "yero-mama-1m.mp3",
            "Yero Mama",
            "1993",
            "04:40",
            "baaba-maal.jpg",
            "Baaba Maal"
        };

        string[] song6 =
        {
            "samba-1m.mp3",
            "Samba",
            "1993",
            "05:43",
            "baaba-maal.jpg",
            "Baaba Maal"
        };

        string[] song7 =
        {
            "habibi-1m.mp3",
            "Habibi",
            "2001",
            "11:42",
            "mohammed-abdu.jpg",
            "Mohammed Abdu"
        };

        // Alle songs samen in één List
        List<string[]> playlist = new ();
        MediaPlayer mediaPlayer = new ();

        public MainWindow()
        {
            InitializeComponent();

            // add songs to list
            playlist.Add(song1);
            playlist.Add(song2);
            playlist.Add(song3);
            playlist.Add(song4);
            playlist.Add(song5);
            playlist.Add(song6);
            playlist.Add(song7);

            // add to listbox
            foreach (string[] s in playlist)
            {
                string text = $"{s[1]} - {s[5]} ({s[2]}, {s[3]})";
                ListBoxItem newItem = new ();
                newItem.Content = text;
                lbxSongs.Items.Add(newItem);
            }

            // player
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        }

        private void MediaPlayer_MediaEnded(object? sender, EventArgs e)
        {
            btnStop.IsEnabled = false;
            btnPlay.IsEnabled = lbxSongs.SelectedItem != null;
            txtMessage.Text = "";
        }

        private void LbxSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            imgArtist.Source = null;
            ListBoxItem selectedItem = lbxSongs.SelectedItem as ListBoxItem;
            btnPlay.IsEnabled = selectedItem != null;
            if (selectedItem == null)
            {
                return;
            }

            string[] song = playlist[lbxSongs.SelectedIndex];
            imgArtist.Source = new BitmapImage(new Uri($"Photos/{song[4]}", UriKind.Relative));
            txtArtist.Text = song[5];
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
            string[] song = playlist[lbxSongs.SelectedIndex];
            mediaPlayer.Open(new Uri($"Mp3/{song[0]}", UriKind.Relative));
            mediaPlayer.Play();
            txtMessage.Text = $"Now playing: “{song[1]}” by {song[4]}";
            btnPlay.IsEnabled = false;
            btnStop.IsEnabled = true;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
            txtMessage.Text = "";
            btnStop.IsEnabled = false;
            btnPlay.IsEnabled = lbxSongs.SelectedItem != null;
        }

        private void SldVolume_ValueChanged(object sender, EventArgs e)
        {
            if (txtVolume == null) return;
            double newVolume = (double)sldVolume.Value / 100.0;
            mediaPlayer.Volume = newVolume;
            txtVolume.Text = $"{sldVolume.Value:F0}";
        }
    }
}
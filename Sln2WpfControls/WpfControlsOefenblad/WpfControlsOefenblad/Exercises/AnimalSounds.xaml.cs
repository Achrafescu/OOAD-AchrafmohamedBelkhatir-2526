using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfControlsOefenblad.Helpers;

namespace WpfControlsOefenblad.Exercises
{
    [NavPage(Title = "Animal sounds", Description = "Afbeeldingen van dieren en hun geluiden", Order = 10, IsVisible = true)]
    public partial class AnimalSounds : Page
    {

        public AnimalSounds()
        {
            InitializeComponent();
        }

        private void Image_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                Image theImage = sender as Image;

                // Récupérer le chemin du son depuis le Tag
                string soundPath = theImage.Tag.ToString();

                // Vérifier si le fichier existe
                if (File.Exists(soundPath))
                {
                    SoundPlayer player = new SoundPlayer(soundPath);
                    player.Play(); // Play() au lieu de PlaySync() pour ne pas bloquer l'interface
                }
                else
                {
                    // Optionnel : afficher un message de débogage
                    System.Diagnostics.Debug.WriteLine($"Fichier non trouvé : {soundPath}");
                }
            }
            catch (Exception ex)
            {
                // Éviter que l'application plante
                System.Diagnostics.Debug.WriteLine($"Erreur : {ex.Message}");
            }
        }

    }
}

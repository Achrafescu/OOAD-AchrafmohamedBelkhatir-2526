using System.Windows;
using System.Windows.Controls;
using WpfLayoutUIOefenblad.Helpers;

namespace WpfLayoutUIOefenblad.Exercises;

[NavPage(title: "Notepad App", description: "Menu en Statusbar in een \ngeïntegreerde oefening", order: 11)]
public partial class NotepadApp : Page
{
    public NotepadApp()
    {
        InitializeComponent();
    }
	
	private void ExitItem_Click(object sender, RoutedEventArgs e)
	{
		// 0 wilt zeggen dat er niets is fout gelopen
		Environment.Exit(0);
	}	
	
	private void _About_Click(object sender, RoutedEventArgs e)
	{
	   // 0 wilt zeggen dat er niets is fout gelopen
		new AboutWindow().Show();
	}

	
}

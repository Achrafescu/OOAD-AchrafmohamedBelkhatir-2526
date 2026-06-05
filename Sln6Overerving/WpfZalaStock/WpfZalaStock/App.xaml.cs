using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfZalaStock
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += (sender, e) =>
            {
                System.IO.File.WriteAllText(@"C:\Users\molef\crash.txt",
                    e.Exception.GetType().Name + "\n" +
                    e.Exception.Message + "\n" +
                    e.Exception.StackTrace);
                e.Handled = true;
            };
        }
    }

}

using Sudoku.Model;
using Sudoku.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Sudoku
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly BoardControl _boardControl;

        public App()
        {
            _boardControl = new BoardControl();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_boardControl)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}

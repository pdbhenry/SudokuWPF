using Sudoku;
using Sudoku.MVVM;
using System.Collections.ObjectModel;
using Sudoku.ViewModel;
using System.Windows;

namespace Sudoku
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel vm = new MainWindowViewModel(lst);
            DataContext = vm;
        }
    }
}
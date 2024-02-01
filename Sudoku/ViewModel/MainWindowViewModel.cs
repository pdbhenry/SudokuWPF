using Sudoku.Model;
using Sudoku.MVVM;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Sudoku;
using System.Windows.Controls;
using System.Windows.Input;
using Sudoku.Commands;

namespace Sudoku.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private BoardControl boardControl;
        public ICommand generateCommand { get; }

        private ObservableCollection<ObservableCollection<int>> boardSource;
        public ObservableCollection<ObservableCollection<int>> BoardSource
        {
            get
            {
                return boardSource;
            }
            set
            {
                boardSource = value;
                OnPropertyChanged(nameof(this.BoardSource));
            }
        }

        public MainWindowViewModel(BoardControl boardControl) 
        {
            generateCommand = new GenerateCommand(this, boardControl);
            BoardSource = boardControl.GenerateBoard();
        }

        private Cell selectedCell;  

        public Cell SelectedCell
        {
            get { return selectedCell; }
            set 
            {
                Debug.Print(value.value.ToString());
                selectedCell = value;
                OnPropertyChanged();
            }
        }

  

    }
}

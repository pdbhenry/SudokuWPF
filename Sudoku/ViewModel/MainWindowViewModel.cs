using Sudoku.Model;
using Sudoku.MVVM;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Sudoku;
using System.Windows.Controls;

namespace Sudoku.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Cell> Cells { get; set; }
        private BoardControl boardControl;
        //public List<List<Cell>> numsList;
        //public RelayCommand GenerateCommand => new RelayCommand(execute => GenerateBoard());

        public MainWindowViewModel(ItemsControl lst) 
        {
            boardControl = new BoardControl();
            //List<List<int>> lsts = board.GenerateBoard();
            List<List<int>> lsts = new List<List<int>>(); 

            for (int i = 0; i < 9; i++)
            {
                lsts.Add(new List<int>());

                for (int j = 0; j < 9; j++)
                {
                    lsts[i].Add(i * 10 + j);
                }
            }

            lst.ItemsSource = boardControl.GenerateBoard();
        }

        private Cell selectedCell;  

        public Cell SelectedCell
        {
            get { return selectedCell; }
            set 
            {
                Debug.Print(value.Value.ToString());
                selectedCell = value;
                OnPropertyChanged();
            }
        }
    }
}

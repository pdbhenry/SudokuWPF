using Sudoku.Model;
using Sudoku.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Documents.DocumentStructures;


namespace Sudoku.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        //public ObservableCollection<Cell> Cells { get; set; }
        public List<List<Cell>> numsList;

        public RelayCommand GenerateCommand => new RelayCommand(execute => GenerateBoard());

        public MainWindowViewModel() 
        {
            numsList = new List<List<Cell>>();

            for (int i = 0; i < 9; i++)
            {
                numsList.Add(new List<Cell>());

                for (int j = 0; j < 9; j++)
                {
                    numsList[i].Add(new Cell(i * 10 + j, true));
                }
            }

            //lst.ItemsSource = numsList; 

            
        }

        private Cell selectedCell;
        public Cell SelectedCell
        {
            get { return SelectedCell; }
            set 
            { 
                SelectedCell = value;
                OnPropertyChanged();
            }
        }

        private void GenerateBoard()
        {
            //Temporary code
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    numsList[i][j] = new Cell(1, true);
                }
            }
        }
    }
}

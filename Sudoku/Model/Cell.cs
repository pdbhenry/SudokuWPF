
namespace Sudoku.Model
{
    internal class Cell
    {
        //public Button Button { get; set; }
        public int Value { get; set; }
        public bool Fixed { get; set; }  
        public int rowInd { get; set; }
        public int colInd { get; set; }

        public Cell(int row, int col) { 
            rowInd = row;
            colInd = col;
        }
    }
}

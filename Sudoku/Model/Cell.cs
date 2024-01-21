
namespace Sudoku.Model
{
    internal class Cell
    {
        //public Button Button { get; set; }
        public int value { get; set; }
        public bool preset { get; set; }  
        public int rowInd { get; set; }
        public int colInd { get; set; }

        public Cell(int row, int col) { 
            rowInd = row;
            colInd = col;
        }

        public Cell(int row, int col, int val)
        {
            rowInd = row;
            colInd = col;
            value = val;
        }
    }
}


namespace Sudoku.Model
{
    class BoardControl
    {
        public List<int> numLst = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public List<List<int>> blankBoard = new List<List<int>>();
        //public Cell emptyCell;
        private int counter;


        public BoardControl() 
        {
            //Make a 9x9 board of 0s
            for (int i = 0; i < 9; i++)
            {
                blankBoard.Add(new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            }

        }

        public List<List<int>> GenerateBoard()
        {
            List<List<int>> board = blankBoard;
            counter = 0;
            FillBoard(board);

            return board;
        }

        public List<int> ShuffleList(Random rng)
        {
            List<int> lst = numLst;
            int n = lst.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                int temp = lst[n];
                lst[n] = lst[k];
                lst[k] = temp;
            }

            return lst;
        }

        public Boolean Safe(List<List<int>> currBoard, Cell emptyCell, int num)
        {
            return RowSafe(currBoard, emptyCell, num) && ColSafe(currBoard, emptyCell, num) && 
                QuadSafe(currBoard, emptyCell, num);
        }

        public Boolean RowSafe(List<List<int>> currBoard, Cell emptyCell, int num)
        {
            if (!currBoard[emptyCell.rowInd].Contains(num))
            {
                return true;
            }

            return false;
        }

        public Boolean ColSafe(List<List<int>> currBoard, Cell emptyCell, int num)
        {
            int col = emptyCell.colInd;
            for (int i = 0; i < 9; i++)
            {
                if (currBoard[i][col] == num)
                    return false;
            }
            
            return true;
        }

        public Boolean QuadSafe(List<List<int>> currBoard, Cell emptyCell, int num)
        {
            int tlRow = emptyCell.rowInd - (emptyCell.rowInd % 3);
            int tlCol = emptyCell.colInd - (emptyCell.colInd % 3);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (currBoard[tlRow + i][tlCol+ j] == num)
                        return false;
                }
            }

            return true;
        }

        public Cell FindEmptyCell(List<List<int>> currBoard)
        {
            Cell emptyCell = new Cell(-1, -1);
            int currRow = 0;

            foreach (List<int> row in currBoard)
            {
                int ind = row.FindIndex(a => a == 0);
                if (ind != -1)
                {
                    emptyCell.rowInd = currRow;
                    emptyCell.colInd = ind;
                    return emptyCell;
                }

                currRow++;
            }

            return null;
        }

        public Boolean FillBoard(List<List<int>> currBoard)
        {
            Cell emptyCell = FindEmptyCell(currBoard);
            if (emptyCell == null)
            {
                return true;
            }

            var rng = new Random();
            List<int> shuffleNums = ShuffleList(rng);

            foreach (int num in shuffleNums.ToList())
            {
                counter++;
                if (counter > 20000000) throw new Exception ("Recursion Timeout");

                if (Safe(currBoard, emptyCell, num))
                {
                    currBoard[emptyCell.rowInd][emptyCell.colInd] = num;

                    if (FillBoard(currBoard))
                    {
                        return true;
                    }

                    currBoard[emptyCell.rowInd][emptyCell.colInd] = 0;
                }
            }

            return false;
        }
    }
}

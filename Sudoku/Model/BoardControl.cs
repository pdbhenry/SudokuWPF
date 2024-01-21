
using System;
using System.Collections;
using System.Windows.Controls;

namespace Sudoku.Model
{
    class BoardControl
    {
        public List<int> numLst = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public List<List<int>> blankBoard = new List<List<int>>();
        //public Cell emptyCell;
        private int counter;
        private int deleteCounter;

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
            deleteCounter = 0;
            FillBoard(board);

            try
            {
                FillBoard(board);
                RemoveFromBoard(board);
                return board;
            } catch (Exception)
            {
                return GenerateBoard();
            }
        }

        public List<int> ShuffleList(List<int> lst, Random rng)
        {
            List<int> lstCopy = lst;
            int n = lstCopy.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                int temp = lstCopy[n];
                lstCopy[n] = lstCopy[k];
                lstCopy[k] = temp;
            }

            return lstCopy;
        }

        //Checks the current Sudoku board if num value exists in the same row,
        //column, and quadrant that emptyCell lies in.
        public Boolean Safe(List<List<int>> currBoard, Cell emptyCell, int num)
        {
            return RowSafe(currBoard, emptyCell, num) && ColSafe(currBoard, emptyCell, num) && 
                QuadSafe(currBoard, emptyCell, num);
        }

        //Checks the current Sudoku board if num value exists in the same row
        //that emptyCell lies in.
        public Boolean RowSafe(List<List<int>> currBoard, Cell emptyCell, int num)
        {
            if (!currBoard[emptyCell.rowInd].Contains(num))
            {
                return true;
            }

            return false;
        }

        //Checks the current Sudoku board if num value exists in the same column
        //that emptyCell lies in.
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

        //Checks the current Sudoku board if num value exists in the same quadrant
        //that emptyCell lies in.
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


        //Returns the first empty cell within the current Sudoku board.
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

        //Uses a backtracking algorithm and randomly selected values to completely fill 
        //a Sudoku board. Each value is checked to be safe amongst its row, column and 
        //quadrant. If it isn't, the backtracking algorithm steps backward and tries 
        //different values in previous cells.
        public Boolean FillBoard(List<List<int>> currBoard)
        {
            Cell emptyCell = FindEmptyCell(currBoard);
            if (emptyCell == null)
            {
                return true;
            }

            var rng = new Random();
            List<int> shuffleNums = ShuffleList(numLst, rng);

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

        //Removes random cells' values until a certain amount of 'holes' have been made.
        //Checks that only one solution is valid, or else backtracks on removed cells.
        public Boolean RemoveFromBoard(List<List<int>> currBoard)
        {
            int holes = 20;
            Random rng = new Random();
            Stack<Cell> removedVals = new Stack<Cell>();
            List<int> vals = ShuffleList(Enumerable.Range(0, 81).ToList(), rng);
            int valsInd = vals.Count - 1;
            while (removedVals.Count < holes)
            {
                if (valsInd < 0) throw new Exception("Impossible Game");
                int nextVal = vals[valsInd--];
                int nextValRow = (int) Math.Floor((float) nextVal / 9);
                int nextValCol = nextVal % 9;

                if (currBoard[nextValRow][nextValCol] == 0) { continue; }

                Cell removed = new Cell(nextValRow, nextValCol, currBoard[nextValRow][nextValCol]);
                removedVals.Push(removed);

                currBoard[nextValRow][nextValCol] = 0;

                List<List<int>> potentialBoard = CreateBoardCopy(currBoard);

                if (MultipleSolutions(potentialBoard))//!FillBoard(potentialBoard))
                {
                    potentialBoard[nextValRow][nextValCol] = removedVals.Pop().value;
                }
            }

            return true;
        }


        public Boolean MultipleSolutions(List<List<int>> currBoard)
        {
            String oneSolution = "";
            List<Cell> emptyCells = EmptyCells(currBoard);

            for (int i = 0; i < emptyCells.Count; i++)
            {
                List<Cell> emptyCells2 = new List<Cell>(emptyCells);
                Cell startingPoint = emptyCells2[i];
                emptyCells2.RemoveAt(i);
                emptyCells2.Insert(0, startingPoint);
                List<List<int>> currBoardCopy = CreateBoardCopy(currBoard);
                String solutionStr = "";

                foreach (List<int> row in TestSolution(currBoardCopy, emptyCells2))
                {
                    solutionStr += String.Join(", ", row.ToArray());
                }

                if (String.Equals(oneSolution, ""))
                {
                    oneSolution = solutionStr;
                }
                else if (!String.Equals(oneSolution, solutionStr))
                {
                    return true;
                }
            }
            return false;
        }

        //
        public List<List<int>> TestSolution(List<List<int>> currBoard, List<Cell> emptyCells)
        {
            Cell emptyCell = StillEmptyCell(currBoard, emptyCells);
            if (emptyCell == null) return currBoard;
            var rng = new Random();
            List<int> shuffleNums = new List<int>(ShuffleList(numLst, rng));

            foreach (int i in shuffleNums)
            {
                deleteCounter++;
                if (deleteCounter > 60000000) throw new Exception("Delete Timeout");

                if (Safe(currBoard, emptyCell, i))
                {
                    currBoard[emptyCell.rowInd][emptyCell.colInd] = i;
                    if (TestSolution(currBoard, emptyCells) != null) return currBoard;
                    currBoard[emptyCell.rowInd][emptyCell.colInd] = 0;
                }
            }
            return null;
        }



        //Returns the first, still empty Cell amongst the previously collected list of
        //empty cells (emptyCells).
        public Cell StillEmptyCell(List<List<int>> currBoard, List<Cell> emptyCells)
        {
            foreach (Cell emptyCell in emptyCells)
            {
                if (currBoard[emptyCell.rowInd][emptyCell.colInd] == 0) return emptyCell;
            }

            return null;
        }

        //Returns a list of all empty cells in the current Sudoku board.
        public List<Cell> EmptyCells(List<List<int>> currBoard)
        {
            List<Cell> emptyCells = new List<Cell>();

            for (int i = 0; i < currBoard.Count; i++)
            {
                for (int j = 0; j < currBoard[i].Count; j++)
                {
                    if (currBoard[i][j] == 0)
                    {
                        emptyCells.Add(new Cell(i, j));
                    }
                }
            }

            return emptyCells;
        }

        public List<List<int>> CreateBoardCopy(List<List<int>> currBoard)
        {
            List<List<int>> boardCopy = new List<List<int>>();
            for (int i = 0; i < currBoard.Count; i++)
            {
                boardCopy.Add(new List<int>());
                for (int j = 0; j < currBoard[i].Count; j++)
                {
                    boardCopy[i].Add(currBoard[i][j]);
                }
            }

            return boardCopy;
        }

    }
}

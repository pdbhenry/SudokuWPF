using Sudoku.Model;
using Sudoku.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Commands
{
    class GenerateCommand : CommandBase
    {
		private readonly MainWindowViewModel _mainWindowViewModel;
		private readonly BoardControl _boardControl;
        
        public GenerateCommand(MainWindowViewModel mainWindowViewModel, BoardControl boardControl) {
            _mainWindowViewModel = mainWindowViewModel;
			_boardControl = boardControl;
        }

        public override void Execute(object parameter)
        {
			_mainWindowViewModel.BoardSource = _boardControl.GenerateBoard();

             
        }
    }
}

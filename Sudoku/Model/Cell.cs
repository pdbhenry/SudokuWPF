using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sudoku.Model
{
    internal class Cell
    {
        //public Button Button { get; set; }
        public int Value { get; set; }
        public bool Fixed { get; set; }  
        
        public Cell(int val, bool fixd) { 
            Value = val;
            Fixed = fixd;            
        }
    }
}

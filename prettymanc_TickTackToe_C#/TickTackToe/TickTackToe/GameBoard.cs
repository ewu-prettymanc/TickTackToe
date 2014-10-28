using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TickTackToe
{
    class GameBoard
    {
        private int _size;
        private Cell[,] _board;
        private Random _rand;

        public GameBoard(int size, Canvas mycanvas )
        {
            _size = size;
            _board = new Cell[_size, _size];
            _rand = new Random();

            for (int r = 0; r < _size; r++)
                for( int c=0; c<_size; c++)
                    _board[c,r] = new Cell(_size, c,r,mycanvas); 
        }

        // Places a gamepiece on the board...
        // Only if the point Causes a collision
        public string PlacePiece(char p, double x, double y)
        {
            foreach (Cell c in _board)
            {
                if( ! c.Used)
                {
                   if (c.CheckPosition(x, y))
                    {
                        c.PlacePiece(p);

                        if (CheckWin(c.Player, c.Col, c.Row))
                            return "win";
                        return "placed";
                   }
                }
            }
            return "notplaced";
        }


        public bool MakeAIMove( char aipiece)
        {
            char enemy;
            if( aipiece == 'X' )
                enemy = 'O';
            else
                enemy = 'X';

            bool win = false;

            if (this.PlaceAINextMoveWin(aipiece))
                win = true;
            else if (this.BlockEnemyNextMoveWin(aipiece, enemy))
                ;
            else if (this.PlaceOnEdge(aipiece))
                ;
            else if (this.PlaceAnyWhere(aipiece)) 
                ;

            return win;
        }

        // Make any moves which will make the AI player win
        private bool PlaceAINextMoveWin(char aipiece )
        {
            foreach (Cell c in _board)
            {
                // Checked used
                if (!c.Used)
                {
                    c.PlacePiece(aipiece, true);
                    // Check if win
                    if (this.CheckWin(aipiece, c.Col, c.Row))
                    {// If a winning move...make it
                        c.ClearTestMove();
                        c.PlacePiece(aipiece);
                        return true;
                    }
                    c.ClearTestMove();
                }
            }

            return false;
        }

        // Place a blocking move on enemy next move win positions
        private bool BlockEnemyNextMoveWin( char aipiece, char enemy)
        {
            foreach (Cell c in _board)
            {
                // Checked used
                if (!c.Used)
                {
                    c.PlacePiece(enemy, true);
                    // Check if win
                    if (this.CheckWin(enemy, c.Col, c.Row))
                    {// If a winning move...make it
                        c.ClearTestMove();
                        c.PlacePiece(aipiece);
                        return true;
                    }
                    c.ClearTestMove();
                }
            }

            return false;
        }

        // Loop through the edges...placing if possible
        private bool PlaceOnEdge(char aipiece)
        {
            int c,r;

            int which = _rand.Next() % 2 + 1;

            // Create a Random choice as to how we loop....to make it
            // Less predictable
            if (which == 2)
            {
                // Loop through the columns in the 0th row
                for (c = r = 0; c < _size; c++)
                    if (!_board[c, r].Used)
                    {
                        _board[c, r].PlacePiece(aipiece);
                        return true;
                    }

                // Loop through the rows in the 0th column
                for (c = r = 0; r < _size; r++)
                    if (!_board[c, r].Used)
                    {
                        _board[c, r].PlacePiece(aipiece);
                        return true;
                    }
            }
            else
            {
                // Loop through the rows in the 0th column
                for (c = r = _size-1; r >=0 ; r--)
                    if (!_board[c, r].Used)
                    {
                        _board[c, r].PlacePiece(aipiece);
                        return true;
                    }

                // Loop through the columns in the 0th row
                for (c = r = _size-1; c >=0; c--)
                    if (!_board[c, r].Used)
                    {
                        _board[c, r].PlacePiece(aipiece);
                        return true;
                    }
            }
               
            return false;
        }

        private bool PlaceAnyWhere(char aipiece)
        {
            foreach( Cell c in _board )
                if( ! c.Used )
                {
                     c.PlacePiece( aipiece);
                     return true;
                }
            return false;
        }
  
        private bool CheckWin(char player, int col, int row)
        {
            //Check the positive diagonal possibility
            if (CountCheck(player, col, row, +1, -1) >= _size)
                return true;
            //Check the horizontal possibilty
            if (CountCheck(player, col, row, +1, 0) >= _size)
                return true;
            //Check the negative diagonal possibility
            if (CountCheck(player, col, row, +1, +1) >= _size)
                return true;
            //Check the vertical possibility
            if (CountCheck(player, col, row, 0, +1) >= _size)
                return true;
            
            return false;
        }


       private int CountCheck(char player,int col, int row, int dc, int dr)/* delta column and delta row*/
        {
           // dr and dc mean delta row and delta column respectively
           // Checking our own spot is pointless so add 1
           // Multiply be -1 to signify the opposing side of our (column/row)
            return 1 + Count(player, col, row, dc, dr)
              + Count(player, col, row, dc * -1, dr * -1);
        }


        private int Count(char player, int col, int row, int dc, int dr)
        {
            int i;
            int total = 0;

            // Loop through checking the direction of change in row and column element
            for (i = 1; OnBoard( col + i * dc , row + i * dr) == true &&
              _board[col + i * dc,row + i * dr].Player == player; total++, i++)
                ;

            return total;
        }



        private bool OnBoard( int col, int row)
        {
            if (col < 0 || row < 0 || row >= _size || col >= _size)
                return false;

            return true;
        }

        public void Reset()
        {
            foreach( Cell c in _board )
                    c.Reset();
        }

        public void ScaleToSize()
        {
            foreach (Cell c in _board)
                c.ScaleToSize();
        }

        /*
 * AI Related Methods
 */

        /* public bool MakeAIMove(char p)
         {
             foreach (Cell c in _board)
             {
                 if (!c.Used)
                 {
                     c.PlacePiece(p);

                     return CheckWin(c.Player, c.Col, c.Row);
                 }
             }

             return false;
         }*/

        /*   public bool MakeAIMove( char aipiece)
           {
               char enemy;
               if( aipiece == 'X' )
                   enemy = 'O';
               else
                   enemy = 'X';

               // Store the board.
               Cell[,] realboard = _board;

               _board = new Cell[_size, _size];

               // Make a clone of the realboard for testing
               for (int r = 0; r < _size; r++)
                   for (int c = 0; c < _size; c++)
                       _board[c, r] = realboard[c, r].Clone();

               bool placed = false;
               Point p = new Point();
               if (this.PlaceAINextMoveWin(aipiece, enemy, realboard))
                   placed = true;
               else if (this.BlockEnemyNextMoveWin(aipiece, enemy, realboard))
                   placed = true;
               else if (this.PlaceInCorner(aipiece, realboard))
                   placed = true;
               else if (this.PlaceOnEdge(aipiece, realboard))
                   placed = true;
               else if (this.PlaceAnyWhere(aipiece, realboard))
                   placed = true;

               _board = realboard;
               return placed;
           }*/

        /*  private bool PlaceAnyWhere(char aipiece)
    {
        int which = _rand.Next() % 2 + 1;
        int collast, rowlast;
        int[] colarray = new int[_size];
        int[] rowarray = new int[_size];
        int num, col, row;

        // Fill the arrays
        for (int i = 0; i < _size; i++)
            colarray[i] = rowarray[i] = i;

        collast = rowlast = _size-1;

        // Generate unduplicated col and row
        // Using an algorithm I invented :):)
        for (; collast >= 0; collast--)
        {
            if (!(collast <= 0))
                num = _rand.Next() % (collast+1);
            else
                num = 0;

            col = colarray[num];
            colarray[num] = colarray[collast];
            colarray[collast] = col;

            for (; rowlast >= 0; rowlast--)
            {
                if( ! (rowlast <= 0 ) )
                  num = _rand.Next() % (rowlast+1);
                else
                    num = 0;

                row = rowarray[num];
                rowarray[num] = rowarray[rowlast];
                rowarray[rowlast] = row;

                if (!_board[col, row].Used)
                {
                    _board[col, row].PlacePiece(aipiece);
                    return true;
                }
            }
        }

        return false;
    }*/

      /*  public bool CheckForWin( )
        {
            bool win = false;

           for (int r = 0; r < _size; r++)
              for (int c = 0; c < _size; c++)
                {
                   win =  CheckWin('X', c, r);
                   if( win )
                        return win;
                }

          for (int r = 0; r < _size; r++)
           for (int c = 0; c < _size; c++)
             {
                win =  CheckWin('O', c, r);
                if( win )
                     return win;
             }

            return win;
        }*/

    }
}

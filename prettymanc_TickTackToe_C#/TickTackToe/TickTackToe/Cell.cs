using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TickTackToe
{
    class Cell
    {
        private char _player;
        private bool _used;
        private int _size;
        private int _col;
        private int _row;
        private double X1;
        private double X2;
        private double Y1;
        private double Y2;
        private double _centerx;
        private double _centery;
        private double _radius;

        //Shape related members
        private Ellipse myEllipse;
        private Line mylneg;
        private Line mylpos;
        private Canvas myCanvas;


       public Cell(int size, int c, int r, Canvas mycanvas )
        {
            _col = c;
            _row = r;
            _player = '-';
            _size = size;
            _used = false;
            myCanvas = mycanvas;

            myEllipse = null;
            mylneg = null;
            mylpos = null;

            UpdatePosition();
        }

        // Takes the input coordinates, the given cell column and row
        // And then calculates the cell extreme coordinates
        public void UpdatePosition()
        {
          //  myUpdatePosition(  myCanvas.MinWidth, myCanvas.MinHeight, myCanvas.MaxWidth, myCanvas.MaxHeight );
            myUpdatePosition(0, 0, myCanvas.Width, myCanvas.Height);
        }

        // This Method finds its pixel margins against the Canvas and stores these values
        private void myUpdatePosition( double x1, double y1, double x2, double y2)
        {
            X1 = Y1 = X2 = Y2 = 0;
            int i;
            double dx,dy;

            dx = (x2-x1)/_size;
            dy = (y2-y1)/_size;

            for (i = 0; i < _col; i++)
                X1 += dx;

            for (i = 0; i < _row; i++)
                Y1 += dy;

            X2 = X1 + dx;
            Y2 = Y1 + dy;

            _centerx = X1 + ((X2 - X1)/2);
            _centery = Y1 + ((Y2 - Y1)/2);
            
            //Set an arbitrary radius within this cell
            if( (X2-X1) < (Y2-Y1) )
                _radius = dx/(3*Math.Sin(45));
            else
                _radius = dy/(3*Math.Sin(45));

            //if (_used)
              //  UpdateShape();
        }

        // Checks to see if this point is an intersection with our cell
        public bool CheckPosition( double x, double y)
        {
            if (x > X1 && x < X2)
                if (y > Y1 && y < Y2)
                    return true;
            return false;
        }

        // Creates and places a shape on the Canvas
        public void PlacePiece(char p, bool testmove = false )
        {
            _used = true;
            _player = p;

            if (!testmove)
            {
                if (_player == 'O')
                {
                    CreateEllipse();
                    return;
                }

                CreateX();
            }
        }

       /* public void UpdateShape( )
        {
            if (_player == 'O')
            {
                CreateEllipse();
                return;
            }

            CreateX();
        }*/


        // Scales the current shape...if any... to the updated size
        public void ScaleToSize(bool resize = true)
        {
            this.UpdatePosition();

            if (!_used)
                return;

            if (_player == 'O')
            {
                CreateEllipse(resize);
                return;
            }

            CreateX(resize);
        }

        private void CreateEllipse( bool resize = false )
        {
            if ( ! resize)
                myEllipse = new Ellipse();

            myEllipse.Width = _radius*2;
            myEllipse.Height = _radius*2;
            myEllipse.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFBE451C")); //System.Windows.Media.Brushes.Red;
            myEllipse.StrokeThickness = 4;
            double left = _centerx - _radius;
            double top = _centery - _radius;
            myEllipse.Margin = new Thickness(left, top, 0, 0);

            if (! resize)
                myCanvas.Children.Add(myEllipse);     
        }

        private void CreateX( bool resize = false )
        {
            // Allocate the negative slope line
            if( ! resize )
            {
                 mylneg = new Line();
                 mylneg.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF912F0D")); 
                 mylneg.StrokeThickness = 4;
                 mylpos = new Line();
                 mylpos.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF912F0D"));
                 mylpos.StrokeThickness = 4;
            }

            mylneg.X1 = _centerx - _radius;
            mylneg.Y1 = _centery - _radius;
            mylneg.X2 = _centerx + _radius;
            mylneg.Y2 = _centery + _radius;

            // Allocate the positive slope line
            mylpos.X1 = _centerx - _radius;
            mylpos.Y1 = _centery + _radius;
            mylpos.X2 = _centerx + _radius;
            mylpos.Y2 = _centery - _radius;

            if (! resize)
            {
                myCanvas.Children.Add(mylneg);
                myCanvas.Children.Add(mylpos);
            }
        }

        public void Reset()
        {
            _used = false;
            _player = '-';

            if (myEllipse != null)
            {
                myCanvas.Children.Remove(myEllipse);
                myEllipse = null;
                return;
            }

            myCanvas.Children.Remove(mylpos);
            myCanvas.Children.Remove(mylneg);
            mylpos = null;
            mylneg = null;
        }

        public bool Used { get { return _used; } }

        public char Player { get { return _player; } }

        public int Col { get { return _col; } }

        public int Row { get { return _row; } }

        public void ClearTestMove()
        {
            _player = '-';
            _used = false;
        }
    }
}

using System;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Task01
{
    public class Matrix
    {
        private int[, ] _matrixBody;
        private readonly int _rows;
        private readonly int _cols;

        public Matrix(int rows, int cols)
        {
            _matrixBody = new int[rows, cols];
            _rows = rows;
            _cols = cols;
        }

        public Matrix(ref Matrix other)
        {
            this._rows = other._rows;
            this._cols = other._cols;

            this._matrixBody = new int[_rows, _cols];

            for (int row = 0; row < _rows; ++row)
            for (int col = 0; col < _cols; ++col)
                _matrixBody[row, col] = other._matrixBody[row, col];
        }

        public void Set(int val, int row, int col)
        {
            Debug.Assert(row >= 0 && col >= 0 && row < _rows && col < _cols);
            _matrixBody[row, col] = val;
        }

        public int Get(int row, int col)
        {
            Debug.Assert(row >= 0 && col >= 0 && row < _rows && col < _cols);
            return _matrixBody[row, col];
        }

        public int SumOfRow(int rowIndex)
        {
            int sum = 0;
            for (int colInd = 0; colInd < _cols; ++colInd)
                sum += _matrixBody[rowIndex, colInd];
            return sum;
        }

        public int[] this[int rowInd]
        {
            get
            {
                int[] row = new int[_cols];
                for (int colInd = 0; colInd < _cols; ++colInd)
                    row[colInd] = _matrixBody[rowInd, colInd];
                return row;
            }
        }

        public void SetRow(int rowIndex, int[] values)
        {
            Debug.Assert(values.GetUpperBound(0).Equals(_cols));

            for (int colInd = 0; colInd < _cols; ++colInd)
                _matrixBody[rowIndex, colInd] = values[colInd];
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();

            for (int rowInd = 0; rowInd < _rows; ++rowInd)
            {
                for (int colInd = 0; colInd < _cols; colInd++)
                {
                    b.Append("\t").Append(_matrixBody[rowInd, colInd]);
                }

                b.Append("\n");
            }

            return b.ToString();
        }

        public int Rows => _rows;

        public int Cols => _cols;
    }

    public class MatrixProcess
    {
        public static ref Matrix SortByNonDecreasingSum(ref Matrix m)
        {
            Matrix matrixCopy = new Matrix(ref m);
            
            (int, int)[] indexSumArray = new (int, int)[m.Rows];

            for (int rowIndex = 0; rowIndex < m.Rows; ++rowIndex)
                indexSumArray[rowIndex] = (rowIndex, m.SumOfRow(rowIndex));

            indexSumArray = indexSumArray.OrderBy(i => i.Item2).ToArray();

            for (int rowIndex = 0; rowIndex < m.Rows; ++rowIndex)
            {
                var rowDesc = indexSumArray[rowIndex];
                m.SetRow(rowIndex, matrixCopy[rowDesc.Item1]);
            }
            return ref m;
        }
    }
}
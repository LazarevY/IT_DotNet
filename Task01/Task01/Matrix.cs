using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Task01
{
    public class Matrix
    {
        private readonly int[,] _matrixBody;

        public Matrix(int rows, int cols)
        {
            _matrixBody = new int[rows, cols];
            Rows = rows;
            Cols = cols;
        }

        public Matrix(ref Matrix other)
        {
            Rows = other.Rows;
            Cols = other.Cols;

            _matrixBody = new int[Rows, Cols];

            for (var row = 0; row < Rows; ++row)
            for (var col = 0; col < Cols; ++col)
                _matrixBody[row, col] = other._matrixBody[row, col];
        }

        public int[] this[int rowInd]
        {
            get
            {
                var row = new int[Cols];
                for (var colInd = 0; colInd < Cols; ++colInd)
                    row[colInd] = _matrixBody[rowInd, colInd];
                return row;
            }
        }

        public int Rows { get; }

        public int Cols { get; }

        public void Set(int val, int row, int col)
        {
            Debug.Assert(row >= 0 && col >= 0 && row < Rows && col < Cols);
            _matrixBody[row, col] = val;
        }

        public int Get(int row, int col)
        {
            Debug.Assert(row >= 0 && col >= 0 && row < Rows && col < Cols);
            return _matrixBody[row, col];
        }

        public int SumOfRow(int rowIndex)
        {
            var sum = 0;
            for (var colInd = 0; colInd < Cols; ++colInd)
                sum += _matrixBody[rowIndex, colInd];
            return sum;
        }

        public void SetRow(int rowIndex, int[] values)
        {
            Debug.Assert(values.GetUpperBound(0).Equals(Cols));

            for (var colInd = 0; colInd < Cols; ++colInd)
                _matrixBody[rowIndex, colInd] = values[colInd];
        }

        public override string ToString()
        {
            var b = new StringBuilder();

            for (var rowInd = 0; rowInd < Rows; ++rowInd)
            {
                for (var colInd = 0; colInd < Cols; colInd++) b.Append("\t").Append(_matrixBody[rowInd, colInd]);

                b.Append("\n");
            }

            return b.ToString();
        }
    }

    public class MatrixProcess
    {
        public static ref Matrix SortByNonDecreasingSum(ref Matrix m)
        {
            var matrixCopy = new Matrix(ref m);

            var indexSumArray = new (int, int)[m.Rows];

            for (var rowIndex = 0; rowIndex < m.Rows; ++rowIndex)
                indexSumArray[rowIndex] = (rowIndex, m.SumOfRow(rowIndex));

            indexSumArray = indexSumArray.OrderBy(i => i.Item2).ToArray();

            for (var rowIndex = 0; rowIndex < m.Rows; ++rowIndex)
            {
                var rowDesc = indexSumArray[rowIndex];
                m.SetRow(rowIndex, matrixCopy[rowDesc.Item1]);
            }

            return ref m;
        }
    }
}
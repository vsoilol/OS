using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9.Client
{
    internal class Matrix<T>
    {
        private readonly T[,] data;

        public int Rows { get; }
        public int Columns { get; }

        public Matrix(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            data = new T[rows, columns];
        }

        public T this[int i, int j]
        {
            get => data[i, j];
            set => data[i, j] = value;
        }
    }
}

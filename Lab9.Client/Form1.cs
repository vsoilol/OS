using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab9.Client
{
    public partial class Form1 : Form
    {
        private Matrix<NumericUpDown> _numericUpDownMatrix;

        private const int StartXMatrixPosition = 3;
        private const int StartYMatrixPosition = 3;

        private const int MatrixNumericUpDownWidth = 60;
        private const int MatrixNumericUpDownHeight = 22;

        private const int DistanceBetweenMatrixNumericUpDown = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void createMatrixButton_Click(object sender, EventArgs e)
        {
            matrixPanel.Controls.Clear();

            var rows = (int)rowAmountNumericUpDown.Value;
            var columns = (int)columnAmountNumericUpDown.Value;

            _numericUpDownMatrix = new Matrix<NumericUpDown>(rows, columns);

            var xPosition = StartXMatrixPosition;
            var yPosition = StartYMatrixPosition;

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    _numericUpDownMatrix[row, column] = new NumericUpDown();

                    matrixPanel.Controls.Add(_numericUpDownMatrix[row, column]);

                    var numericUpDownName = $"Cell{row}{column}";

                    _numericUpDownMatrix[row, column].Name = numericUpDownName;

                    _numericUpDownMatrix[row, column].Width = MatrixNumericUpDownWidth;
                    _numericUpDownMatrix[row, column].Height = MatrixNumericUpDownHeight;
                    _numericUpDownMatrix[row, column].Location = new Point(xPosition, yPosition);
                    _numericUpDownMatrix[row, column].TabIndex = 0;
                    _numericUpDownMatrix[row, column].Minimum = -99;
                    _numericUpDownMatrix[row, column].Maximum = 99;
                    _numericUpDownMatrix[row, column].Controls.RemoveAt(0);

                    xPosition += (MatrixNumericUpDownWidth + DistanceBetweenMatrixNumericUpDown);
                }

                yPosition += (MatrixNumericUpDownHeight + DistanceBetweenMatrixNumericUpDown);
                xPosition = StartXMatrixPosition;
            }
        }

        private void randomValuesButton_Click(object sender, EventArgs e)
        {
            var random = new Random();

            if (_numericUpDownMatrix is null)
            {
                MessageBox.Show("Matrix is empty");
                return;
            }

            for (var row = 0; row < _numericUpDownMatrix.Rows; row++)
            {
                for (var column = 0; column < _numericUpDownMatrix.Columns; column++)
                {
                    var randomNumber = random.Next(-99, 100); // Generates a random number between 1 and 100 (inclusive)

                    _numericUpDownMatrix[row, column].Value = randomNumber;
                }
            }
        }

        private void clearMatrixButton_Click(object sender, EventArgs e)
        {
            if (_numericUpDownMatrix is null)
            {
                MessageBox.Show("Matrix is empty");
                return;
            }

            for (var row = 0; row < _numericUpDownMatrix.Rows; row++)
            {
                for (var column = 0; column < _numericUpDownMatrix.Columns; column++)
                {
                    _numericUpDownMatrix[row, column].Value = 0;
                }
            }
        }

        private void makeTaskButton_Click(object sender, EventArgs e)
        {
            if (_numericUpDownMatrix is null)
            {
                MessageBox.Show("Matrix is empty");
                return;
            }

            var client = new ServiceReference1.Service1Client();

            var minimumPositiveRow = 0;
            var minimumPositiveCount = 0;

            for (var row = 0; row < _numericUpDownMatrix.Rows; row++)
            {
                var rowValues = GetArrayFromMatrixNumericUpDownByRow(_numericUpDownMatrix, row);
                var positiveElementsCount = client.GetAmountOfPositiveValues(rowValues);

                if (minimumPositiveCount > positiveElementsCount || minimumPositiveCount == 0)
                {
                    minimumPositiveRow = row;
                    minimumPositiveCount = positiveElementsCount;
                }
            }

            MessageBox.Show(
                $"Строка матрицы содержащей минимальное количество положительных элементов: {minimumPositiveRow + 1}. Количество положительных элементов: {minimumPositiveCount}");
        }

        private int[] GetArrayFromMatrixNumericUpDownByRow(Matrix<NumericUpDown> matrixNumericUpDown, int row)
        {
            var array1D = new int[matrixNumericUpDown.Columns];
            for (var i = 0; i < matrixNumericUpDown.Columns; i++)
            {
                array1D[i] = (int)matrixNumericUpDown[row, i].Value;
            }

            return array1D;
        }
    }
}
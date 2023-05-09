using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab9.Client
{
    public partial class Form1 : Form
    {
        private Matrix<int> matrix;
        private Matrix<NumericUpDown> numericUpDownMatrix;

        private const int StartXMatrixPosition = 3;
        private const int StartYMatrixPosition = 3;

        private const int MatrixNumericUpDownWidth = 60;
        private const int MatrixNumericUpDownHeight = 22;

        private const int DistanceBetweenMatrixNumericUpDown = 3;

        public Form1()
        {
            InitializeComponent();

           

            //client.Get
            //client.Get
            //client.Get
            
        }

        private void createMatrixButton_Click(object sender, EventArgs e)
        {
            matrixPanel.Controls.Clear();

            var rows = (int)rowAmountNumericUpDown.Value;
            var columns = (int)columnAmountNumericUpDown.Value;

            matrix = new Matrix<int>(rows, columns);
            numericUpDownMatrix = new Matrix<NumericUpDown>(rows, columns);

            var xPosition = StartXMatrixPosition;
            var yPosition = StartYMatrixPosition;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    numericUpDownMatrix[row, column] = new NumericUpDown();

                    matrixPanel.Controls.Add(numericUpDownMatrix[row, column]);

                    var numericUpDownName = $"Cell{row}{column}";

                    numericUpDownMatrix[row, column].Name = numericUpDownName;

                    numericUpDownMatrix[row, column].Width = MatrixNumericUpDownWidth;
                    numericUpDownMatrix[row, column].Height = MatrixNumericUpDownHeight;
                    numericUpDownMatrix[row, column].Location = new Point(xPosition, yPosition);
                    numericUpDownMatrix[row, column].TabIndex = 0;
                    numericUpDownMatrix[row, column].Minimum = -99;
                    numericUpDownMatrix[row, column].Maximum = 99;
                    numericUpDownMatrix[row, column].Controls.RemoveAt(0);

                    xPosition += (MatrixNumericUpDownWidth + DistanceBetweenMatrixNumericUpDown);
                }

                yPosition += (MatrixNumericUpDownHeight + DistanceBetweenMatrixNumericUpDown);
                xPosition = StartXMatrixPosition;
            }
        }

        private void randomValuesButton_Click(object sender, EventArgs e)
        {
            var random = new Random();

            if(numericUpDownMatrix is null)
            {
                MessageBox.Show("Matrix is empty");
                return;
            }

            for (int row = 0; row < numericUpDownMatrix.Rows; row++)
            {
                for (int column = 0; column < numericUpDownMatrix.Columns; column++)
                {
                    var randomNumber = random.Next(-99, 100); // Generates a random number between 1 and 100 (inclusive)

                    numericUpDownMatrix[row, column].Value = randomNumber;
                }
            }
        }

        private void clearMatrixButton_Click(object sender, EventArgs e)
        {
            if (numericUpDownMatrix is null)
            {
                MessageBox.Show("Matrix is empty");
                return;
            }

            for (int row = 0; row < numericUpDownMatrix.Rows; row++)
            {
                for (int column = 0; column < numericUpDownMatrix.Columns; column++)
                {
                    numericUpDownMatrix[row, column].Value = 0;
                }
            }
        }

        private void makeTaskButton_Click(object sender, EventArgs e)
        {
            if (numericUpDownMatrix is null)
            {
                MessageBox.Show("Matrix is empty");
                return;
            }

            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

            var minimumPositiveRow = 0;
            var minimumPositiveCount = 0;

            for (int row = 0; row < numericUpDownMatrix.Rows; row++)
            {
                var rowValues = GetArrayFromMatrixNumericUpDownByRow(numericUpDownMatrix, row);
                int positiveElementsCount = client.GetAmountOfPositiveValues(rowValues);

                if (minimumPositiveCount > positiveElementsCount || minimumPositiveCount == 0)
                {
                    minimumPositiveRow = row;
                    minimumPositiveCount = positiveElementsCount;
                }
            }

            MessageBox.Show($"Строка матрицы содержащей минимальное количество положительных элементов: {minimumPositiveRow + 1}. Количество положительных элементов: {minimumPositiveCount}");
        }

        private int[] GetArrayFromMatrixNumericUpDownByRow(Matrix<NumericUpDown> matrixNumericUpDown, int row)
        {
            int[] array1D = new int[matrixNumericUpDown.Columns];
            for (int i = 0; i < matrixNumericUpDown.Columns; i++)
            {
                array1D[i] = (int)matrixNumericUpDown[row, i].Value;
            }

            return array1D;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Patterns1_2
{
    public class Decorator : IMatrix
    {
        private IMatrix matrix;
        private IVector rowMapping;
        private IVector colMapping;

        public Decorator(IMatrix matrix)
        {
            this.matrix = matrix;
            Init();
        }

        private void Init()
        {
            rowMapping = new BaseVector(matrix.RowsSize);
            colMapping = new BaseVector(matrix.ColsSize);

            for (int i = 0; i < matrix.RowsSize; i++)
                rowMapping[i] = i;

            for (int j = 0; j < matrix.ColsSize; j++)
                colMapping[j] = j;
        }

        public int RowsSize => matrix.RowsSize;
        public int ColsSize => matrix.ColsSize;

        public double GetElem(int indexX, int indexY)
        {
            CheckIndices(indexX, indexY);
            int originalRow = (int)rowMapping[indexX];
            int originalCol = (int)colMapping[indexY];
            return matrix.GetElem(originalRow, originalCol);
        }

        public void SetElem(int indexX, int indexY, double value)
        {
            CheckIndices(indexX, indexY);
            int originalRow = (int)rowMapping[indexX];
            int originalCol = (int)colMapping[indexY];
            matrix.SetElem(originalRow, originalCol, value);
        }

        public IVector this[int row]
        {
            get
            {
                CheckRowIndex(row);
                BaseVector result = new BaseVector(ColsSize);
                for (int col = 0; col < ColsSize; col++)
                {
                    result[col] = this[row, col];
                }
                return result;
            }
        }

        public double this[int row, int col]
        {
            get
            {
                CheckIndices(row, col);
                int originalRow = (int)rowMapping[row];
                int originalCol = (int)colMapping[col];
                return matrix[originalRow, originalCol];
            }
            set
            {
                CheckIndices(row, col);
                int originalRow = (int)rowMapping[row];
                int originalCol = (int)colMapping[col];
                matrix[originalRow, originalCol] = value;
            }
        }
        public void SwapRows(int row1, int row2)
        {
            CheckRowIndex(row1);
            CheckRowIndex(row2);
            double temp = rowMapping[row1];
            rowMapping[row1] = rowMapping[row2];
            rowMapping[row2] = temp;
        }

        public void SwapColumns(int col1, int col2)
        {
            CheckColumnIndex(col1);
            CheckColumnIndex(col2);

            double temp = colMapping[col1];
            colMapping[col1] = colMapping[col2];
            colMapping[col2] = temp;
        }

        public void Change()
        {
            Random rand = new Random();
            int row1 = rand.Next(RowsSize);
            int row2 = rand.Next(RowsSize);
            while (row2 == row1)
                row2 = rand.Next(RowsSize);

            int col1 = rand.Next(ColsSize);
            int col2 = rand.Next(ColsSize);
            while (col2 == col1)
                col2 = rand.Next(ColsSize);

            SwapRows(row1, row2);
            SwapColumns(col1, col2);
            Console.Write($"Переставлены строки: {row1}  - {row2}, столбцы: {col1}  - {col2}");
        }

        public void RestoreOriginalOrder()
        {
            Init();
        }

        private void CheckRowIndex(int row)
        {
            if (row < 0 || row >= RowsSize)
                throw new IndexOutOfRangeException($"Строка {row} вне диапазона");
        }

        private void CheckColumnIndex(int col)
        {
            if (col < 0 || col >= ColsSize)
                throw new IndexOutOfRangeException($"Столбец {col} вне диапазона");
        }

        private void CheckIndices(int row, int col)
        {
            CheckRowIndex(row);
            CheckColumnIndex(col);
        }
        public void Visualize(IVisualizator visualizator, bool showBorder, int shiftRow, int shiftCol)
        {
            visualizator.SetShowBorder(showBorder);
            if (showBorder)
            {
                visualizator.DrawBorder(RowsSize, ColsSize, shiftCol, shiftRow);
            }
            for (int i = 0; i < RowsSize; i++)
            {
                for (int j = 0; j < ColsSize; j++)
                {
                    double value = this[i, j];
                    string text = value.ToString("F3");

                    if (matrix is SpecialMatrix && value == 0)
                    {
                        text = " ";
                    }

                    visualizator.DrawCell(RowsSize, ColsSize, shiftRow + i, shiftCol + j, text);
                }
            }
            if (showBorder)
            {
                visualizator.DrawBorder(RowsSize, ColsSize, shiftCol, shiftRow);
            }
        }

    }
    // декоратор для вертикального отображения одной матрицы
    public class VerticalMatrixDecorator : IMatrix
    {
        private IMatrix origMatrix;

        
        public VerticalMatrixDecorator(IMatrix matrix)
        {
            origMatrix = matrix;
        }

       
        public int RowsSize => origMatrix.ColsSize;
        public int ColsSize => origMatrix.RowsSize;

        
        public double GetElem(int row, int col) => origMatrix.GetElem(col, row);
        public void SetElem(int row, int col, double value) => origMatrix.SetElem(col, row, value);

       
        public IVector this[int row]
        {
            get
            {
                if (row < 0 || row >= RowsSize)
                    throw new IndexOutOfRangeException($"Строка {row} вне диапазона");

                BaseVector result = new BaseVector(ColsSize);
                for (int col = 0; col < ColsSize; col++)
                {
                    result[col] = this[row, col];
                }
                return result;
            }
        }

        public double this[int row, int col]
        {
            get => GetElem(row, col);
            set => SetElem(row, col, value);
        }

        
        public void Visualize(IVisualizator visualizator, bool showBorder, int shiftRow, int shiftCol)
        {
            
            visualizator.SetShowBorder(showBorder);

            
            if (showBorder)
            {
                visualizator.DrawBorder(RowsSize, ColsSize, shiftCol, shiftRow);
            }

            
            for (int i = 0; i < RowsSize; i++)
            {
                for (int j = 0; j < ColsSize; j++)
                {
                    
                    double value = this[i, j];

                    string text;
                    if (origMatrix is SpecialMatrix && value == 0)
                    {
                        text = " ";
                    }
                    else
                    {
                        text = value.ToString("F3");
                    }

                   
                    visualizator.DrawCell(RowsSize, ColsSize, shiftRow + i, shiftCol + j, text);
                }
            }

            if (showBorder)
            {
                visualizator.DrawBorder(RowsSize, ColsSize, shiftCol, shiftRow);
            }
        }
    }

    
    public class VerticalGroupDecorator : IMatrix
    {
        private IMatrix origGroup;
        private Brush[] colors = new Brush[]
        {
            Brushes.LightPink,
            Brushes.LightBlue,
            Brushes.LightYellow,
            Brushes.Purple,
            Brushes.Pink,
            Brushes.LightGreen
        };
        private int colorIndex = 0;
        

        public VerticalGroupDecorator(IMatrix group)
        {
            origGroup = group;
        }

        public int RowsSize => origGroup.ColsSize;
        public int ColsSize => origGroup.RowsSize;

        
        public double GetElem(int row, int col) => origGroup.GetElem(col, row);

        public void SetElem(int row, int col, double value) => origGroup.SetElem(col, row, value);

        
        public IVector this[int row]
        {
            get
            {
                if (row < 0 || row >= RowsSize)
                    throw new IndexOutOfRangeException($"Строка {row} вне диапазона");

                BaseVector result = new BaseVector(ColsSize);
                for (int col = 0; col < ColsSize; col++)
                {
                    result[col] = this[row, col];
                }
                return result;
            }
        }

        public double this[int row, int col]
        {
            get => GetElem(row, col);
            set => SetElem(row, col, value);
        }

        public void Visualize(IVisualizator visualizator, bool showBorder, int shiftRow, int shiftCol)
        {
            
            if (!(origGroup is GorizontalGroup group))
            {
                var matrixDecorator = new VerticalMatrixDecorator(origGroup);
                matrixDecorator.Visualize(visualizator, showBorder, shiftRow, shiftCol);
                return;
            }
            
            Req(visualizator, group, showBorder, shiftRow, shiftCol);
        }

        private void Req(IVisualizator visualizator, GorizontalGroup group, bool showBorder, int shiftRow, int shiftCol)
        {
            
            int currentRow = shiftRow;
            for (int i = 0; i < group.GetGroupSize(); i++)
            {
                var element = group.GetMatrix(i);
                if (element is GorizontalGroup subGroup)
                {
                    Req(visualizator, subGroup, showBorder, currentRow, shiftCol);
                    visualizator.SetShowBorder(showBorder);
                    visualizator.DrawBorder(subGroup.ColsSize, subGroup.RowsSize, shiftCol,currentRow);

                    currentRow += subGroup.ColsSize;
                }
                else
                {
                    visualizator.SetCellBrush(colors[colorIndex]);
                    colorIndex++;

                    var matrixDecorator = new VerticalMatrixDecorator(element);
                    matrixDecorator.Visualize(visualizator, false, currentRow, shiftCol);
                    visualizator.ResetCellBrush();
                    currentRow += matrixDecorator.RowsSize;
                }
            }
            
            visualizator.SetShowBorder(showBorder);
            int actualHeight = currentRow - shiftRow;
            int groupWidth = group.RowsSize;
            visualizator.DrawBorder(actualHeight, groupWidth, shiftCol, shiftRow);
            
        }

    }

}
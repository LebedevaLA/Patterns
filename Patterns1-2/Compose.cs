using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace Patterns1_2
{
    
    class GorizontalGroup:IMatrix
    {
        private IMatrix[] arrMatrix;
        private int index = 0;

        private Brush[] colors = new Brush[]
        {
            Brushes.LightPink, 
            Brushes.LightBlue, 
            Brushes.LightYellow,
            Brushes.Purple, 
            Brushes.Pink, 
            Brushes.LightGreen
        };
        int colorIndex = 0;
        public GorizontalGroup(int count)
        {
            arrMatrix = new IMatrix[count];
        }
        public int RowsSize
        {
            get
            {
                if (arrMatrix.Length == 0)
                    return 0;

                int max = 0;
                for (int i = 0; i < arrMatrix.Length; i++)
                {
                    if (arrMatrix[i] != null)
                        max = Math.Max(max, arrMatrix[i].RowsSize);
                }
                return max;
            }
        }

        public int ColsSize
        {
            get
            {
                if (arrMatrix.Length == 0)
                    return 0;

                int sum = 0;
                for (int i = 0; i < arrMatrix.Length; i++)
                {
                    if (arrMatrix[i] != null)
                        sum += arrMatrix[i].ColsSize;
                }
                return sum;
            }
        }
        public void AddMatrix(IMatrix matrix) {
            if (index > arrMatrix.Length - 1)
            {
                throw new ArgumentException("Out of range");
            }
            arrMatrix[index] = matrix;
            index++;
        }

        public double GetElem(int indexX, int indexY)
        {
            CheckIndices(indexX, indexY);

            int currentCol = 0;
            for (int i = 0; i < arrMatrix.Length; i++)
            {
                if (arrMatrix[i] == null) continue;

                if (indexY < currentCol + arrMatrix[i].ColsSize)
                {
                    if (indexX < arrMatrix[i].RowsSize)
                    {
                        int localCol = indexY - currentCol;
                        return arrMatrix[i].GetElem(indexX, localCol);
                    }
                    else
                    {
                        
                        return 0;
                    }
                }
                currentCol += arrMatrix[i].ColsSize;
            }
            return 0;
        }

        public void SetElem(int indexX, int indexY, double value)
        {
            CheckIndices(indexX, indexY);

            int currentCol = 0;
            for (int i = 0; i < arrMatrix.Length; i++)
            {
                if (arrMatrix[i] == null) continue;

                if (indexY < currentCol + arrMatrix[i].ColsSize)
                {
                    
                    if (indexX < arrMatrix[i].RowsSize)
                    {
                        int localCol = indexY - currentCol;
                        arrMatrix[i].SetElem(indexX, localCol, value);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                currentCol += arrMatrix[i].ColsSize;
            }
        }

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

        private void CheckIndices(int row, int col)
        {
            if (row < 0 || row >= RowsSize)
                throw new IndexOutOfRangeException($"Строка {row} вне диапазона");
            if (col < 0 || col >= ColsSize)
                throw new IndexOutOfRangeException($"Столбец {col} вне диапазона");
        }
       

        
        public int GetGroupSize()
        {
            return arrMatrix.Length;
        }

        public IMatrix GetMatrix(int index)
        {
            if (index < 0 || index >= arrMatrix.Length || arrMatrix[index] == null)
                throw new IndexOutOfRangeException();
            return arrMatrix[index];
        }
        public void Visualize(IVisualizator visualizator, bool showBorder, int shiftRow, int shiftCol)
        {
            int currentCol = shiftCol;
            int maxHeight = 0;

            for (int i = 0; i < arrMatrix.Length; i++)
            {
                if (arrMatrix[i] != null)
                {
                    
                    if (arrMatrix[i] is GorizontalGroup subGroup)
                    {
                        
                        int groupHeight = 0;
                        int groupWidth = 0;

                        int tempCol = currentCol;
                        for (int j = 0; j < subGroup.GetGroupSize(); j++)
                        {
                            var subMatrix = subGroup.GetMatrix(j);
                            if (subMatrix != null)
                            {
                                visualizator.SetCellBrush(colors[colorIndex % colors.Length]);
                                colorIndex++;
                                subMatrix.Visualize(visualizator, false, shiftRow, tempCol);
                                visualizator.ResetCellBrush();
                                tempCol += subMatrix.ColsSize;
                                groupWidth += subMatrix.ColsSize;
                                groupHeight = Math.Max(groupHeight, subMatrix.RowsSize);
                            }
                        }
                        if (showBorder)
                        {
                            visualizator.SetShowBorder(showBorder);
                            visualizator.DrawBorder(groupHeight, groupWidth, currentCol, shiftRow);
                        }

                        currentCol = tempCol;
                        maxHeight = Math.Max(maxHeight, groupHeight);
                    }
                    else 
                    {
                        visualizator.SetCellBrush(colors[colorIndex % colors.Length]);
                        colorIndex++;

                        arrMatrix[i].Visualize(visualizator, false, shiftRow, currentCol);

                        visualizator.ResetCellBrush();

                        currentCol += arrMatrix[i].ColsSize;
                        maxHeight = Math.Max(maxHeight, arrMatrix[i].RowsSize);
                    }
                }
            }
            
                visualizator.SetShowBorder(showBorder);
                visualizator.DrawBorder(maxHeight, currentCol - shiftCol, shiftCol, shiftRow);
            
        }


    }


}


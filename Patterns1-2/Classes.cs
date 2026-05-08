using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns1_2
{
    public interface IVector
    {
        double GetComponent(int index);
        void SetComponent(int index, double value);
        int GetSize();
        double this[int index] { get; set; }
    }

    public interface IMatrix
    {
        double GetElem(int indexX, int indexY);
        void SetElem(int indexX, int indexY, double value);
        int RowsSize { get; }
        int ColsSize { get; }
        IVector this[int row] { get; }
        double this[int row, int col] { get; set; }
        void Visualize(IVisualizator visualizator, bool showBorder, int shiftRow, int shiftCol);
    }

    class BaseVector : IVector
    {
        private int size;
        private double[] components;

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException("index");
            }
        }

        public BaseVector(int size)
        {
            this.size = size;
            this.components = new double[size];
        }

        public int GetSize() => size;

        public void SetComponent(int index, double value)
        {
            CheckIndex(index);
            components[index] = value;
        }

        public double GetComponent(int index)
        {
            CheckIndex(index);
            return components[index];
        }

        public void SetAllComponents(double[] allcomponents)
        {
            if (allcomponents.Length != this.size)
            {
                throw new ArgumentException("size");
            }
            else
            {
                for (int i = 0; i < this.size; i++)
                {
                    components[i] = allcomponents[i];
                }
            }
        }
        public double this[int index]
        {
            get
            {
                CheckIndex(index);
                return components[index];
            }
            set
            {
                CheckIndex(index);
                components[index] = value;
            }
        }
    }

    class SpecialVector : IVector
    {
        private int size;
        private Dictionary<int, double> notZeros = new Dictionary<int, double>();

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException("index");
            }
        }

        public SpecialVector(int size)
        {
            this.size = size;
        }

        public int GetSize() => size;

        public void SetComponent(int index, double value)
        {
            CheckIndex(index);
            if (value != 0)
            {
                notZeros[index] = value;
            }
            else
            {
                notZeros.Remove(index);
            }
        }

        public double GetComponent(int index)
        {
            CheckIndex(index);
            return notZeros.ContainsKey(index) ? notZeros[index] : 0;
        }

        public void SetNotZeroComponents(Dictionary<int, double> id_component)
        {
            foreach (var kvp in id_component)
            {
                CheckIndex(kvp.Key);
                SetComponent(kvp.Key, kvp.Value);
            }
        }

        public Dictionary<int, double> GetNotZeroComponents()
        {
            return new Dictionary<int, double>(notZeros);
        }
        public double this[int index]
        {
            get
            {
                CheckIndex(index);
                return GetComponent(index);
            }
            set
            {
                CheckIndex(index);
                SetComponent(index, value);
            }
        }
    }
    public abstract class SomeMatrix : IMatrix
    {
        protected int rows_size;
        protected int cols_size;
        protected IVector[] vectors;
        protected SomeMatrix(int rows_size, int columns_size)
        {
            this.rows_size = rows_size;
            this.cols_size = columns_size;
        }

        public int RowsSize => rows_size;
        public int ColsSize => cols_size;

        public abstract double GetElem(int indexX, int indexY);
        public abstract void SetElem(int indexX, int indexY, double value);

        public abstract void Visualize(IVisualizator visualizator, bool showBorder, int shiftRow, int shiftCol);

        public virtual IVector this[int row]
        {
            get
            {
                CheckRowIndex(row);
                return vectors[row];
            }
        }

        public virtual double this[int row, int col]
        {
            get
            {
                CheckIndices(row, col);
                return vectors[row][col];
            }
            set
            {
                CheckIndices(row, col);
                vectors[row][col] = value;
            }
        }

        protected void CheckRowIndex(int row)
        {
            if (row < 0 || row >= rows_size)
                throw new IndexOutOfRangeException($"Строка {row} вне диапазона");
        }

        protected void CheckIndices(int row, int col)
        {
            CheckRowIndex(row);
            if (col < 0 || col >= cols_size)
                throw new IndexOutOfRangeException($"Столбец {col} вне диапазона");
        }
    }

    public class BaseMatrix : SomeMatrix
    {
        public BaseMatrix(int rows, int cols) : base(rows, cols)
        {
            vectors = new BaseVector[rows];
            for (int i = 0; i < rows; i++)
            {
                vectors[i] = new BaseVector(cols);
            }
        }

        public override double GetElem(int indexX, int indexY)
        {
            CheckIndices(indexX, indexY);
            return vectors[indexX][indexY];
        }

        public override void SetElem(int indexX, int indexY, double value)
        {
            CheckIndices(indexX, indexY);
            vectors[indexX][indexY] = value;
        }

        public override void Visualize(IVisualizator visualizator, bool showBorder, int shiftRow, int shiftCol)
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
                    string value = this[i, j].ToString("F3");
                    visualizator.DrawCell(RowsSize, ColsSize, shiftRow + i, shiftCol + j, value);
                }
            }

            if (showBorder)
            {
                visualizator.DrawBorder(RowsSize, ColsSize, shiftCol, shiftRow);
            }
        }
    }

    public class SpecialMatrix : SomeMatrix
    {
        public SpecialMatrix(int rows, int cols) : base(rows, cols)
        {
            vectors = new SpecialVector[rows];
            for (int i = 0; i < rows; i++)
            {
                vectors[i] = new SpecialVector(cols);
            }
        }

        public override double GetElem(int indexX, int indexY)
        {
            CheckIndices(indexX, indexY);
            return vectors[indexX][indexY];
        }

        public override void SetElem(int indexX, int indexY, double value)
        {
            CheckIndices(indexX, indexY);
            vectors[indexX][indexY] = value;
        }
        public override void Visualize(IVisualizator visualizator, bool showBorder, int shiftRow, int shiftCol)
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
                    if (value != 0)
                    {
                        visualizator.DrawCell(RowsSize, ColsSize, shiftRow + i, shiftCol + j,
                            value.ToString("F3"));
                    }
                    else
                    {
                        visualizator.DrawCell(RowsSize, ColsSize, shiftRow + i, shiftCol + j, " ");
                    }
                }
            }

            if (showBorder)
            {
                visualizator.DrawBorder(RowsSize, ColsSize, shiftCol, shiftRow);
            }
        }
    }

    public class MatrixInit
    {
        private static Random rand = new Random();
        public static void Init(IMatrix matrix, int notNull, double maxNum)
        {
            
            int n = matrix.RowsSize;
            int m = matrix.ColsSize;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (notNull > 0)
                    {
                        double randomValue = rand.NextDouble() * maxNum;
                        notNull--;
                        matrix.SetElem(i, j, randomValue);
                    }
                    else
                    {
                        matrix.SetElem(i, j, 0);
                    }
                }
            }
        }
    }
    public static class MatrixStatistic
    {
        public static double CalculateSum(IMatrix matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix.RowsSize; i++)
            {
                for (int j = 0; j < matrix.ColsSize; j++)
                {
                    sum += matrix.GetElem(i, j);
                }
            }
            return sum;
        }

        public static int CountNonZero(IMatrix matrix)
        {
            int count = 0;
            for (int i = 0; i < matrix.RowsSize; i++)
            {
                for (int j = 0; j < matrix.ColsSize; j++)
                {
                    if (matrix.GetElem(i, j) != 0) count++;
                }
            }
            return count;
        }

        public static double FindMax(IMatrix matrix)
        {
            double max = double.MinValue;
            for (int i = 0; i < matrix.RowsSize; i++)
            {
                for (int j = 0; j < matrix.ColsSize; j++)
                {
                    double value = matrix.GetElem(i, j);
                    if (value > max) max = value;
                }
            }
            return max == double.MinValue ? 0 : max;
        }

        public static double CalculateAverage(IMatrix matrix)
        {
            return CalculateSum(matrix) / (matrix.RowsSize * matrix.ColsSize);
        }

        public static double[] CalculateAll(IMatrix matrix)
        {
            double sum = 0;
            int nonZeroCount = 0;
            double max = double.MinValue;

            for (int i = 0; i < matrix.RowsSize; i++)
            {
                for (int j = 0; j < matrix.ColsSize; j++)
                {
                    double value = matrix.GetElem(i, j);
                    sum += value;
                    if (value != 0) nonZeroCount++;
                    if (value > max) max = value;
                }
            }

            double average = sum / (matrix.RowsSize * matrix.ColsSize);
            if (max == double.MinValue) max = 0;
            return new double[] { nonZeroCount, sum, average, max };
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Patterns1_2
{
    public interface IVisualizator
    {
        void SetShowBorder(bool showBorder);
        void DrawBorder(int rows, int cols, int shiftcols, int shiftrows);
        void DrawCell(int rows, int cols, int row, int col, string value);
        void SetGraphics(Graphics graphics);
        void SetCellBrush(Brush brush);

        void DrawHighlightedCell(int rows, int cols, int row, int col, string value, Color highlightColor);
        void ResetCellBrush();
    }

    public class ConsoleVisualizator : IVisualizator
    {
        private bool showBorder;

        public ConsoleVisualizator() { }

        public void SetShowBorder(bool showBorder)
        {
            this.showBorder = showBorder;
        }

        public void SetGraphics(Graphics graphics) { }
        public void SetCellBrush(Brush brush) { }

        public void ResetCellBrush() { }

        public void DrawHighlightedCell(int rows, int cols, int row, int col, string value, Color highlightColor) { }
        public void DrawBorder(int rows, int cols, int shiftcols, int shiftrows)
        {
            if (!showBorder) return;
            string border = new string('─', cols * 8 - 1);
            Console.WriteLine(border);
        }

        public void DrawCell(int rows, int cols, int row, int col, string value)
        {
            if (value == " " || string.IsNullOrEmpty(value))
            {
                value = "    ";
            }
            if (showBorder)
            {
                if (col == 0)
                {
                    Console.Write("|");
                }

                Console.Write($" {value} |");
            }
            else
            {
                Console.Write($"{value,6} ");
            }
            if (col == cols - 1)
            {
                Console.WriteLine();
            }

        }
    }
    public class GraphicVisualizator : IVisualizator
    {
        private Graphics graphics;
        private bool showBorder;
        private Font cellFont;
        private Brush textBrush;
        private Pen borderPen;
        private Pen cellPen;
        private Brush cellBrush;

        public const int CELL_WIDTH = 60;
        public const int CELL_HEIGHT = 30;

        public GraphicVisualizator()
        {
            InitializeResources();
        }

        private void InitializeResources()
        {
            this.cellFont = new Font("Arial", 10);
            this.textBrush = Brushes.Black;
            this.borderPen = new Pen(Color.Red, 3);
            this.cellPen = new Pen(Color.Gray, 1);
            this.cellBrush = Brushes.White;
        }



        // Метод для установки цвета заливки ячеек
        public void SetCellBrush(Brush brush)
        {
            this.cellBrush = brush;
        }

        // Метод для сброса цвета к белому
        public void ResetCellBrush()
        {
            this.cellBrush = Brushes.White;
        }

        public void SetShowBorder(bool showBorder)
        {
            this.showBorder = showBorder;
        }

        public void SetGraphics(Graphics graphics)
        {
            this.graphics = graphics;
        }

        public void DrawBorder(int rows, int cols, int shiftcols, int shiftrows)
        {
            if (!showBorder || graphics == null) return;
            int shift_r = shiftcols * CELL_WIDTH;
            int shift_t = shiftrows * CELL_HEIGHT;
            int totalWidth = cols * CELL_WIDTH;
            int totalHeight = rows * CELL_HEIGHT;
            graphics.DrawRectangle(borderPen, shift_r, shift_t, totalWidth, totalHeight);
        }

        public void DrawCell(int rows, int cols, int row, int col, string value)
        {
            if (graphics == null) return;

            int x = col * CELL_WIDTH;
            int y = row * CELL_HEIGHT;

            // Заливаем ячейку цветом
            graphics.FillRectangle(cellBrush, x, y, CELL_WIDTH, CELL_HEIGHT);

            if (showBorder)
            {
                graphics.DrawRectangle(cellPen, x, y, CELL_WIDTH, CELL_HEIGHT);
            }

            if (value != " " && !string.IsNullOrEmpty(value))
            {
                string text = value;
                SizeF textSize = graphics.MeasureString(text, cellFont);
                float textX = x + (CELL_WIDTH - textSize.Width) / 2;
                float textY = y + (CELL_HEIGHT - textSize.Height) / 2;
                graphics.DrawString(text, cellFont, textBrush, textX, textY);
            }
        }
        public void DrawHighlightedCell(int rows, int cols, int row, int col, string value, Color highlightColor)
        {
            if (graphics == null) return;

            int x = col * CELL_WIDTH;
            int y = row * CELL_HEIGHT;

            using (var highlightBrush = new SolidBrush(highlightColor))
            {
                graphics.FillRectangle(highlightBrush, x, y, CELL_WIDTH, CELL_HEIGHT);
            }

            if (showBorder)
            {
                graphics.DrawRectangle(cellPen, x, y, CELL_WIDTH, CELL_HEIGHT);
            }

            if (value != " " && !string.IsNullOrEmpty(value))
            {
                string text = value;
                SizeF textSize = graphics.MeasureString(text, cellFont);
                float textX = x + (CELL_WIDTH - textSize.Width) / 2;
                float textY = y + (CELL_HEIGHT - textSize.Height) / 2;
                graphics.DrawString(text, cellFont, textBrush, textX, textY);
            }
        }
    }

}
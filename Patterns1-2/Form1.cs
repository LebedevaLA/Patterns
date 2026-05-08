using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Windows.Forms;

namespace Patterns1_2
{
    public partial class Form1 : Form
    {
        private IMatrix currentMatrix;
        private GorizontalGroup group;
        private IVisualizator consoleVisualizator;
        private IVisualizator graphicVisualizator;
        private Decorator decorator;
        private bool firstCommand  = false;
        private CommandManager cmdManager = CommandManager.Instance();
        private static Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            consoleVisualizator = new ConsoleVisualizator();
            graphicVisualizator = new GraphicVisualizator();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetAllCheckboxes();
        }

        private void ResetAllCheckboxes()
        {
            BaseMartrix.Checked = false;
            SpecialMatrix.Checked = false;
            PaintBorder.Checked = false;
            ConsoePaint.Checked = false;
            PanelPaint.Checked = false;
            Group.Checked = false;
            GroupGroup.Checked = false;
        }

        private void PrintMatrix_Click(object sender, EventArgs e)
        {
            if (!BaseMartrix.Checked && !SpecialMatrix.Checked && !Group.Checked && !GroupGroup.Checked)
            {
                MessageBox.Show("Выберите тип матрицы или группу!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ConsoePaint.Checked && !PanelPaint.Checked)
            {
                MessageBox.Show("Выберите способ визуализации!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Group.Checked || GroupGroup.Checked)
            {
                CreateAndVisualizeGroup();
            }
            else
            {
                CreateAndVisualizeMatrix();
            }
        }
        private void VisualizeCurrentMatrix()
        {
            if (currentMatrix == null) return;

            if (ConsoePaint.Checked)
            {
                VisualizeToConsole();
            }
            else if (PanelPaint.Checked)
            {
                VisualizeToPanel();
            }
        }
        private void VisualizeToConsole()
        {
            Console.WriteLine($"=== {(BaseMartrix.Checked ? "ОБЫЧНАЯ" : SpecialMatrix.Checked ? "РАЗРЕЖЕННАЯ" : "ГРУППА")} ===");
            Console.WriteLine($"Граница: {(PaintBorder.Checked ? "ВКЛ" : "ВЫКЛ")}");

            currentMatrix.Visualize(consoleVisualizator, PaintBorder.Checked, 0, 0);
            Console.WriteLine();
        }

        private void VisualizeToPanel()
        {
            using (Graphics g = panel1.CreateGraphics())
            {
                g.Clear(Color.LemonChiffon);
                graphicVisualizator.SetGraphics(g);
                currentMatrix.Visualize(graphicVisualizator, PaintBorder.Checked, 0, 0);
            }
        }
        private void CreateAndVisualizeMatrix()
        {
            if (currentMatrix == null)
            {
                int nonZeroCount;
                int rows = 5;
                int cols = 5;
                if (BaseMartrix.Checked)
                {
                    currentMatrix = new BaseMatrix(rows, cols);
                    nonZeroCount = rows * cols - 2;
                }
                else
                {
                    currentMatrix = new SpecialMatrix(rows, cols);
                    nonZeroCount = (rows * cols) / 2;
                }
                MatrixInit.Init(currentMatrix, nonZeroCount, 10.0);
            }
            VisualizeCurrentMatrix();
        }
        private void CreateAndVisualizeGroup()
        {
            if (Group.Checked)
            {
                if (currentMatrix == null)
                {
                    CreateGroup();
                    currentMatrix = group;
                }
            }
            else if (GroupGroup.Checked)
            {
                if (currentMatrix == null)
                {
                    CreateGroupGroup();
                    currentMatrix = group;
                }
                
            }

            VisualizeCurrentMatrix();
        }
        private void CreateGroup()
        {
            group = new GorizontalGroup(3);
            SomeMatrix m1 = new SpecialMatrix(5, 5);
            SomeMatrix m2 = new BaseMatrix(2, 3);
            SomeMatrix m3 = new BaseMatrix(4, 2);
            MatrixInit.Init(m1, 5, 10.0);
            MatrixInit.Init(m2, 6, 10.0);
            MatrixInit.Init(m3, 8, 10.0);
            group.AddMatrix(m1);
            group.AddMatrix(m2);
            group.AddMatrix(m3);
        }
        private void CreateGroupGroup() {
            group = new GorizontalGroup(3);
            GorizontalGroup g1 = new GorizontalGroup(1);
            SomeMatrix m1 = new BaseMatrix(2, 2);
            MatrixInit.Init(m1, 8, 10.0);
            g1.AddMatrix(m1);

            GorizontalGroup g2 = new GorizontalGroup(2);
            SomeMatrix m11 = new BaseMatrix(2, 2);
            SomeMatrix m12 = new SpecialMatrix(4, 2);
            MatrixInit.Init(m11, 3, 10.0);
            MatrixInit.Init(m12, 3, 10.0);
            g2.AddMatrix(m11);
            g2.AddMatrix (m12);

            GorizontalGroup g3 = new GorizontalGroup(1);
            SomeMatrix m21 = new SpecialMatrix(3, 3);
            MatrixInit.Init(m21, 7, 10.0);
            g3.AddMatrix(m21);
           
            group.AddMatrix(g1);
            group.AddMatrix(g2);
            group.AddMatrix(g3);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void BaseMartrix_CheckedChanged(object sender, EventArgs e)
        {
            if (BaseMartrix.Checked)
            {
                SpecialMatrix.Checked = false;
                Group.Checked = false;
                GroupGroup.Checked = false;
                ConsoePaint.Enabled = true;
               
            }
        }

        private void SpecialMatrix_CheckedChanged(object sender, EventArgs e)
        {
           
            if (SpecialMatrix.Checked)
            {
                BaseMartrix.Checked = false;
                Group.Checked = false;
                Group.Checked = false;
                
                ConsoePaint.Enabled = true;
            }
        }

        private void ConsoePaint_CheckedChanged(object sender, EventArgs e)
        {
            
            if (ConsoePaint.Checked)
            {
                PanelPaint.Checked = false;
            }
        }

        private void PanelPaint_CheckedChanged(object sender, EventArgs e)
        {
            
            if (PanelPaint.Checked)
            {
                ConsoePaint.Checked = false;
            }
        }

        private void PaintBorder_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentMatrix == null && group == null)
            {
                MessageBox.Show("Сначала создайте матрицу!", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (currentMatrix != null && !(currentMatrix is GorizontalGroup))
            {
                if (decorator == null || decorator != currentMatrix)
                {
                    decorator = new Decorator(currentMatrix);
                }
                decorator.Change();
                VisualizeDecorator();
            }
            
            else if (group != null)
            {
                MessageBox.Show("Декоратор перенумерации не работает с группами!", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void VisualizeDecorator()
        {
            if (decorator == null) return;

            if (ConsoePaint.Checked)
            {
                Console.WriteLine($"=== МАТРИЦА С ПЕРЕСТАВЛЕННЫМИ СТРОКАМИ/СТОЛБЦАМИ ===");
                Console.WriteLine($"Граница: {(PaintBorder.Checked ? "ВКЛ" : "ВЫКЛ")}");
                decorator.Visualize(consoleVisualizator, PaintBorder.Checked, 0, 0);
                Console.WriteLine();
            }
            else if (PanelPaint.Checked)
            {
                using (Graphics g = panel1.CreateGraphics())
                {
                    g.Clear(Color.LemonChiffon);
                    graphicVisualizator.SetGraphics(g);
                    decorator.Visualize(graphicVisualizator, PaintBorder.Checked, 0, 0);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (decorator == null)
            {
                MessageBox.Show("Сначала перенумеруйте матрицу!", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decorator.RestoreOriginalOrder();

            Console.WriteLine("=== ИСХОДНАЯ МАТРИЦА (через декоратор) ===");
            VisualizeDecorator();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (group == null)
            {
                MessageBox.Show("Сначала создайте группу!", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var verticalGroup = new VerticalGroupDecorator(group);

            using (Graphics g = panel1.CreateGraphics())
            {
                g.Clear(Color.LemonChiffon);
                graphicVisualizator.SetGraphics(g);
                verticalGroup.Visualize(graphicVisualizator, PaintBorder.Checked, 0, 0);
            }

        }

        private void Group_CheckedChanged(object sender, EventArgs e)
        {
            if (Group.Checked)
            {
                BaseMartrix.Checked = false;
                SpecialMatrix.Checked = false;
                ConsoePaint.Enabled = false;
                PanelPaint.Checked = true;
                GroupGroup.Checked = false;
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            group = null;
            currentMatrix = null;
            using (Graphics g = panel1.CreateGraphics())
            {
                g.Clear(Color.LemonChiffon);
            }
            ResetAllCheckboxes();
        }

        private void GroupGroup_CheckedChanged(object sender, EventArgs e)
        {
            if (GroupGroup.Checked)
            {
                BaseMartrix.Checked = false;
                SpecialMatrix.Checked = false;
                ConsoePaint.Enabled = false;
                PanelPaint.Checked = true;
                Group.Checked = false;
                decorator = null;
            }
        }

        private void Do_Click(object sender, EventArgs e)
        {
            if (!firstCommand)
            {
                group = null;
                currentMatrix = null;
                using (Graphics g = panel1.CreateGraphics())
                {
                    g.Clear(Color.LemonChiffon);
                }
                ResetAllCheckboxes();
                currentMatrix = new BaseMatrix(5, 5);
                
                InitApplicationCommand first = new InitApplicationCommand(currentMatrix);
                first.Execute();
                cmdManager.RegisterCommand(first);
                
                using (Graphics g = panel1.CreateGraphics())
                {
                    graphicVisualizator.SetGraphics(g);
                    currentMatrix.Visualize(graphicVisualizator, true, 0, 0);
                }
                firstCommand = true;
            }
            else {

                int row = random.Next(currentMatrix.RowsSize);
                int col = random.Next(currentMatrix.ColsSize);
                double newValue = random.NextDouble() * 100;

                SetMatrixValueCommand cmd = new SetMatrixValueCommand(currentMatrix, row, col, newValue);
                cmd.Execute();

                using (Graphics g = panel1.CreateGraphics())
                {
                    graphicVisualizator.SetGraphics(g);
                    currentMatrix.Visualize(graphicVisualizator, true, 0, 0);
                }

                cmdManager.RegisterCommand(cmd);

            }

        }

        private void Undo_Click(object sender, EventArgs e)
        {
            cmdManager.Undo();
            
            using (Graphics g = panel1.CreateGraphics())
            {
                graphicVisualizator.SetGraphics(g);
                if (currentMatrix != null)
                {
                    currentMatrix.Visualize(graphicVisualizator, true, 0, 0);
                }
            }

        }

        private void Redo_Click(object sender, EventArgs e)
        {
            cmdManager.Redo();
            using (Graphics g = panel1.CreateGraphics())
            {
                graphicVisualizator.SetGraphics(g);
                if (currentMatrix != null)
                {
                    currentMatrix.Visualize(graphicVisualizator, true, 0, 0);
                }
            }
        }
    }
}
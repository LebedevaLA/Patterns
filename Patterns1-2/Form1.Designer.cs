namespace Patterns1_2
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.PrintMatrix = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BaseMartrix = new System.Windows.Forms.CheckBox();
            this.SpecialMatrix = new System.Windows.Forms.CheckBox();
            this.PaintBorder = new System.Windows.Forms.CheckBox();
            this.ConsoePaint = new System.Windows.Forms.CheckBox();
            this.PanelPaint = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.Group = new System.Windows.Forms.CheckBox();
            this.GroupGroup = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            this.Do = new System.Windows.Forms.Button();
            this.Undo = new System.Windows.Forms.Button();
            this.Redo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PrintMatrix
            // 
            this.PrintMatrix.BackColor = System.Drawing.Color.YellowGreen;
            this.PrintMatrix.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.PrintMatrix.Location = new System.Drawing.Point(1628, 486);
            this.PrintMatrix.Name = "PrintMatrix";
            this.PrintMatrix.Size = new System.Drawing.Size(141, 64);
            this.PrintMatrix.TabIndex = 1;
            this.PrintMatrix.Text = "Print";
            this.PrintMatrix.UseVisualStyleBackColor = false;
            this.PrintMatrix.Click += new System.EventHandler(this.PrintMatrix_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LemonChiffon;
            this.panel1.Location = new System.Drawing.Point(65, 73);
            this.panel1.MinimumSize = new System.Drawing.Size(1494, 1001);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1494, 1001);
            this.panel1.TabIndex = 2;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // BaseMartrix
            // 
            this.BaseMartrix.AutoSize = true;
            this.BaseMartrix.Location = new System.Drawing.Point(1628, 189);
            this.BaseMartrix.Name = "BaseMartrix";
            this.BaseMartrix.Size = new System.Drawing.Size(224, 29);
            this.BaseMartrix.TabIndex = 3;
            this.BaseMartrix.Text = "Обычная матрица";
            this.BaseMartrix.UseVisualStyleBackColor = true;
            this.BaseMartrix.CheckedChanged += new System.EventHandler(this.BaseMartrix_CheckedChanged);
            // 
            // SpecialMatrix
            // 
            this.SpecialMatrix.AutoSize = true;
            this.SpecialMatrix.Location = new System.Drawing.Point(1628, 245);
            this.SpecialMatrix.Name = "SpecialMatrix";
            this.SpecialMatrix.Size = new System.Drawing.Size(270, 29);
            this.SpecialMatrix.TabIndex = 4;
            this.SpecialMatrix.Text = "Разреженная матрица";
            this.SpecialMatrix.UseVisualStyleBackColor = true;
            this.SpecialMatrix.CheckedChanged += new System.EventHandler(this.SpecialMatrix_CheckedChanged);
            // 
            // PaintBorder
            // 
            this.PaintBorder.AutoSize = true;
            this.PaintBorder.Location = new System.Drawing.Point(1628, 309);
            this.PaintBorder.Name = "PaintBorder";
            this.PaintBorder.Size = new System.Drawing.Size(251, 29);
            this.PaintBorder.TabIndex = 5;
            this.PaintBorder.Text = "Отобразить границы";
            this.PaintBorder.UseVisualStyleBackColor = true;
            // 
            // ConsoePaint
            // 
            this.ConsoePaint.AutoSize = true;
            this.ConsoePaint.Location = new System.Drawing.Point(1628, 362);
            this.ConsoePaint.Name = "ConsoePaint";
            this.ConsoePaint.Size = new System.Drawing.Size(266, 29);
            this.ConsoePaint.TabIndex = 6;
            this.ConsoePaint.Text = "Отобразить в консоль";
            this.ConsoePaint.UseVisualStyleBackColor = true;
            this.ConsoePaint.CheckedChanged += new System.EventHandler(this.ConsoePaint_CheckedChanged);
            // 
            // PanelPaint
            // 
            this.PanelPaint.AutoSize = true;
            this.PanelPaint.Location = new System.Drawing.Point(1628, 417);
            this.PanelPaint.Name = "PanelPaint";
            this.PanelPaint.Size = new System.Drawing.Size(269, 29);
            this.PanelPaint.TabIndex = 7;
            this.PanelPaint.Text = "Отобразить на панель";
            this.PanelPaint.UseVisualStyleBackColor = true;
            this.PanelPaint.CheckedChanged += new System.EventHandler(this.PanelPaint_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Location = new System.Drawing.Point(1628, 580);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 64);
            this.button1.TabIndex = 8;
            this.button1.Text = "Перенумеровать";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.Location = new System.Drawing.Point(1628, 787);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(206, 64);
            this.button2.TabIndex = 9;
            this.button2.Text = "Восстановить";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.MediumPurple;
            this.button3.ForeColor = System.Drawing.Color.DarkRed;
            this.button3.Location = new System.Drawing.Point(1628, 677);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(235, 78);
            this.button3.TabIndex = 10;
            this.button3.Text = "Транспонировать";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Group
            // 
            this.Group.AutoSize = true;
            this.Group.Location = new System.Drawing.Point(1628, 73);
            this.Group.Name = "Group";
            this.Group.Size = new System.Drawing.Size(194, 29);
            this.Group.TabIndex = 4;
            this.Group.Text = "Группа матриц";
            this.Group.UseVisualStyleBackColor = true;
            this.Group.CheckedChanged += new System.EventHandler(this.Group_CheckedChanged);
            // 
            // GroupGroup
            // 
            this.GroupGroup.AutoSize = true;
            this.GroupGroup.Location = new System.Drawing.Point(1628, 131);
            this.GroupGroup.Name = "GroupGroup";
            this.GroupGroup.Size = new System.Drawing.Size(180, 29);
            this.GroupGroup.TabIndex = 11;
            this.GroupGroup.Text = "Группа Групп";
            this.GroupGroup.UseVisualStyleBackColor = true;
            this.GroupGroup.CheckedChanged += new System.EventHandler(this.GroupGroup_CheckedChanged);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Orange;
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button4.Location = new System.Drawing.Point(1628, 1009);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(140, 65);
            this.button4.TabIndex = 2;
            this.button4.Text = "End";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Do
            // 
            this.Do.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Do.ForeColor = System.Drawing.Color.PeachPuff;
            this.Do.Location = new System.Drawing.Point(1628, 895);
            this.Do.Name = "Do";
            this.Do.Size = new System.Drawing.Size(91, 78);
            this.Do.TabIndex = 12;
            this.Do.Text = "Do";
            this.Do.UseVisualStyleBackColor = false;
            this.Do.Click += new System.EventHandler(this.Do_Click);
            // 
            // Undo
            // 
            this.Undo.BackColor = System.Drawing.Color.MistyRose;
            this.Undo.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.Undo.Location = new System.Drawing.Point(1735, 895);
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(144, 78);
            this.Undo.TabIndex = 11;
            this.Undo.Text = "Undo";
            this.Undo.UseVisualStyleBackColor = false;
            this.Undo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // Redo
            // 
            this.Redo.BackColor = System.Drawing.Color.Khaki;
            this.Redo.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.Redo.Location = new System.Drawing.Point(1797, 1009);
            this.Redo.Name = "Redo";
            this.Redo.Size = new System.Drawing.Size(140, 65);
            this.Redo.TabIndex = 13;
            this.Redo.Text = "Redo";
            this.Redo.UseVisualStyleBackColor = false;
            this.Redo.Click += new System.EventHandler(this.Redo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1993, 1150);
            this.Controls.Add(this.Redo);
            this.Controls.Add(this.Undo);
            this.Controls.Add(this.Do);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.GroupGroup);
            this.Controls.Add(this.Group);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.PanelPaint);
            this.Controls.Add(this.ConsoePaint);
            this.Controls.Add(this.PaintBorder);
            this.Controls.Add(this.SpecialMatrix);
            this.Controls.Add(this.BaseMartrix);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PrintMatrix);
            this.MinimumSize = new System.Drawing.Size(1970, 1160);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button PrintMatrix;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox BaseMartrix;
        private System.Windows.Forms.CheckBox SpecialMatrix;
        private System.Windows.Forms.CheckBox PaintBorder;
        private System.Windows.Forms.CheckBox ConsoePaint;
        private System.Windows.Forms.CheckBox PanelPaint;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox Group;
        private System.Windows.Forms.CheckBox GroupGroup;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button Do;
        private System.Windows.Forms.Button Undo;
        private System.Windows.Forms.Button Redo;
    }
}


namespace QuadTree
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button1 = new Button();
            richTextBox1 = new RichTextBox();
            numericUpDown1 = new NumericUpDown();
            numericUpDown2 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
            numericUpDown4 = new NumericUpDown();
            label9 = new Label();
            QuadPanel = new Panel();
            heigth_tree = new NumericUpDown();
            width_tree = new NumericUpDown();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            numberOfObjects = new NumericUpDown();
            maxDepth = new NumericUpDown();
            removeCount = new NumericUpDown();
            label15 = new Label();
            radioButtonPoints = new RadioButton();
            radioButtonPolygons = new RadioButton();
            radioButtonBoth = new RadioButton();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heigth_tree).BeginInit();
            ((System.ComponentModel.ISupportInitialize)width_tree).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numberOfObjects).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxDepth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)removeCount).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(35, 72);
            label1.Name = "label1";
            label1.Size = new Size(103, 20);
            label1.TabIndex = 4;
            label1.Text = "Počet operácií";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(35, 109);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 5;
            label2.Text = "Počet Insert";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(35, 137);
            label3.Name = "label3";
            label3.Size = new Size(126, 20);
            label3.TabIndex = 6;
            label3.Text = "Počet PointSearch";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(35, 244);
            label4.Name = "label4";
            label4.Size = new Size(120, 20);
            label4.TabIndex = 7;
            label4.Text = "Seed QUADTREE";
            // 
            // button1
            // 
            button1.Location = new Point(576, 233);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 8;
            button1.Text = "GO";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(35, 278);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(635, 354);
            richTextBox1.TabIndex = 9;
            richTextBox1.Text = "";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(191, 65);
            numericUpDown1.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(150, 27);
            numericUpDown1.TabIndex = 100;
            numericUpDown1.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(191, 102);
            numericUpDown2.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(150, 27);
            numericUpDown2.TabIndex = 11;
            numericUpDown2.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(191, 135);
            numericUpDown3.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            numericUpDown3.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(150, 27);
            numericUpDown3.TabIndex = 12;
            numericUpDown3.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // numericUpDown4
            // 
            numericUpDown4.Location = new Point(191, 235);
            numericUpDown4.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDown4.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDown4.Name = "numericUpDown4";
            numericUpDown4.Size = new Size(150, 27);
            numericUpDown4.TabIndex = 13;
            numericUpDown4.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(35, 24);
            label9.Name = "label9";
            label9.Size = new Size(122, 20);
            label9.TabIndex = 109;
            label9.Text = "INSERT and FIND";
            // 
            // QuadPanel
            // 
            QuadPanel.Location = new Point(711, 24);
            QuadPanel.Name = "QuadPanel";
            QuadPanel.Size = new Size(535, 516);
            QuadPanel.TabIndex = 111;
            QuadPanel.Paint += QuadPanel_Paint;
            // 
            // heigth_tree
            // 
            heigth_tree.Location = new Point(763, 605);
            heigth_tree.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            heigth_tree.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            heigth_tree.Name = "heigth_tree";
            heigth_tree.Size = new Size(150, 27);
            heigth_tree.TabIndex = 112;
            heigth_tree.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // width_tree
            // 
            width_tree.Location = new Point(763, 552);
            width_tree.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            width_tree.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            width_tree.Name = "width_tree";
            width_tree.Size = new Size(150, 27);
            width_tree.TabIndex = 113;
            width_tree.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(698, 612);
            label11.Name = "label11";
            label11.Size = new Size(54, 20);
            label11.TabIndex = 115;
            label11.Text = "Heigth";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(698, 559);
            label12.Name = "label12";
            label12.Size = new Size(49, 20);
            label12.TabIndex = 114;
            label12.Text = "Width";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(926, 612);
            label13.Name = "label13";
            label13.Size = new Size(106, 20);
            label13.TabIndex = 119;
            label13.Text = "No.  of objects";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(926, 559);
            label14.Name = "label14";
            label14.Size = new Size(78, 20);
            label14.TabIndex = 118;
            label14.Text = "MaxDepth";
            // 
            // numberOfObjects
            // 
            numberOfObjects.Location = new Point(1050, 605);
            numberOfObjects.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numberOfObjects.Name = "numberOfObjects";
            numberOfObjects.Size = new Size(150, 27);
            numberOfObjects.TabIndex = 116;
            numberOfObjects.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // maxDepth
            // 
            maxDepth.Location = new Point(1050, 552);
            maxDepth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            maxDepth.Name = "maxDepth";
            maxDepth.Size = new Size(150, 27);
            maxDepth.TabIndex = 117;
            maxDepth.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // removeCount
            // 
            removeCount.Location = new Point(191, 174);
            removeCount.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            removeCount.Name = "removeCount";
            removeCount.Size = new Size(150, 27);
            removeCount.TabIndex = 121;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(35, 176);
            label15.Name = "label15";
            label15.Size = new Size(103, 20);
            label15.TabIndex = 120;
            label15.Text = "Počet Remove";
            // 
            // radioButtonPoints
            // 
            radioButtonPoints.AutoSize = true;
            radioButtonPoints.Location = new Point(500, 69);
            radioButtonPoints.Name = "radioButtonPoints";
            radioButtonPoints.Size = new Size(71, 24);
            radioButtonPoints.TabIndex = 122;
            radioButtonPoints.TabStop = true;
            radioButtonPoints.Text = "points";
            radioButtonPoints.UseVisualStyleBackColor = true;
            // 
            // radioButtonPolygons
            // 
            radioButtonPolygons.AutoSize = true;
            radioButtonPolygons.Location = new Point(500, 102);
            radioButtonPolygons.Name = "radioButtonPolygons";
            radioButtonPolygons.Size = new Size(173, 24);
            radioButtonPolygons.TabIndex = 123;
            radioButtonPolygons.TabStop = true;
            radioButtonPolygons.Text = "polygons (rectangles)";
            radioButtonPolygons.UseVisualStyleBackColor = true;
            // 
            // radioButtonBoth
            // 
            radioButtonBoth.AutoSize = true;
            radioButtonBoth.Location = new Point(500, 135);
            radioButtonBoth.Name = "radioButtonBoth";
            radioButtonBoth.Size = new Size(61, 24);
            radioButtonBoth.TabIndex = 124;
            radioButtonBoth.TabStop = true;
            radioButtonBoth.Text = "both";
            radioButtonBoth.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(500, 37);
            label5.Name = "label5";
            label5.Size = new Size(113, 20);
            label5.TabIndex = 125;
            label5.Text = "Objects in quad";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 679);
            Controls.Add(label5);
            Controls.Add(radioButtonBoth);
            Controls.Add(radioButtonPolygons);
            Controls.Add(radioButtonPoints);
            Controls.Add(removeCount);
            Controls.Add(label15);
            Controls.Add(label13);
            Controls.Add(label14);
            Controls.Add(numberOfObjects);
            Controls.Add(maxDepth);
            Controls.Add(label11);
            Controls.Add(label12);
            Controls.Add(heigth_tree);
            Controls.Add(width_tree);
            Controls.Add(QuadPanel);
            Controls.Add(label9);
            Controls.Add(numericUpDown4);
            Controls.Add(numericUpDown3);
            Controls.Add(numericUpDown2);
            Controls.Add(numericUpDown1);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown4).EndInit();
            ((System.ComponentModel.ISupportInitialize)heigth_tree).EndInit();
            ((System.ComponentModel.ISupportInitialize)width_tree).EndInit();
            ((System.ComponentModel.ISupportInitialize)numberOfObjects).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxDepth).EndInit();
            ((System.ComponentModel.ISupportInitialize)removeCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button1;
        private RichTextBox richTextBox1;
        private NumericUpDown numericUpDown1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown3;
        private NumericUpDown numericUpDown4;
        private Label label9;
        private Panel QuadPanel;
        private NumericUpDown heigth_tree;
        private NumericUpDown width_tree;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private NumericUpDown numberOfObjects;
        private NumericUpDown maxDepth;
        private NumericUpDown removeCount;
        private Label label15;
        private RadioButton radioButtonPoints;
        private RadioButton radioButtonPolygons;
        private RadioButton radioButtonBoth;
        private Label label5;
    }
}
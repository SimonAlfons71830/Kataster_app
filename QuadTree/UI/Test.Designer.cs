namespace QuadTree
{
    partial class Test
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
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            richTextBox1 = new RichTextBox();
            numericUpDown2 = new NumericUpDown();
            numericUpDown3 = new NumericUpDown();
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
            checkBoxDrawing = new CheckBox();
            checkBoxInterference = new CheckBox();
            label1 = new Label();
            checkBoxSeed = new CheckBox();
            groupBox1 = new GroupBox();
            radioButtonQtree = new RadioButton();
            radioButtonMyQtree = new RadioButton();
            groupBox2 = new GroupBox();
            setNewDepthButton = new Button();
            appbutton = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)heigth_tree).BeginInit();
            ((System.ComponentModel.ISupportInitialize)width_tree).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numberOfObjects).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxDepth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)removeCount).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 92);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 5;
            label2.Text = "Počet Insert";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 120);
            label3.Name = "label3";
            label3.Size = new Size(126, 20);
            label3.TabIndex = 6;
            label3.Text = "Počet PointSearch";
            // 
            // button1
            // 
            button1.Location = new Point(493, 243);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 8;
            button1.Text = "GO";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(25, 292);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(635, 353);
            richTextBox1.TabIndex = 9;
            richTextBox1.Text = "";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(186, 86);
            numericUpDown2.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(150, 27);
            numericUpDown2.TabIndex = 11;
            numericUpDown2.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // numericUpDown3
            // 
            numericUpDown3.Location = new Point(186, 119);
            numericUpDown3.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDown3.Name = "numericUpDown3";
            numericUpDown3.Size = new Size(150, 27);
            numericUpDown3.TabIndex = 12;
            numericUpDown3.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(26, 50);
            label9.Name = "label9";
            label9.Size = new Size(187, 20);
            label9.TabIndex = 109;
            label9.Text = "INSERT, FIND and REMOVE";
            // 
            // QuadPanel
            // 
            QuadPanel.Location = new Point(685, 39);
            QuadPanel.Name = "QuadPanel";
            QuadPanel.Size = new Size(549, 516);
            QuadPanel.TabIndex = 111;
            QuadPanel.Paint += QuadPanel_Paint;
            // 
            // heigth_tree
            // 
            heigth_tree.Location = new Point(750, 616);
            heigth_tree.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            heigth_tree.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            heigth_tree.Name = "heigth_tree";
            heigth_tree.Size = new Size(95, 27);
            heigth_tree.TabIndex = 112;
            heigth_tree.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // width_tree
            // 
            width_tree.Location = new Point(750, 563);
            width_tree.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            width_tree.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            width_tree.Name = "width_tree";
            width_tree.Size = new Size(95, 27);
            width_tree.TabIndex = 113;
            width_tree.Value = new decimal(new int[] { 500, 0, 0, 0 });
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(685, 623);
            label11.Name = "label11";
            label11.Size = new Size(54, 20);
            label11.TabIndex = 115;
            label11.Text = "Heigth";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(685, 569);
            label12.Name = "label12";
            label12.Size = new Size(49, 20);
            label12.TabIndex = 114;
            label12.Text = "Width";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(871, 623);
            label13.Name = "label13";
            label13.Size = new Size(106, 20);
            label13.TabIndex = 119;
            label13.Text = "No.  of objects";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(871, 569);
            label14.Name = "label14";
            label14.Size = new Size(78, 20);
            label14.TabIndex = 118;
            label14.Text = "MaxDepth";
            // 
            // numberOfObjects
            // 
            numberOfObjects.Location = new Point(994, 616);
            numberOfObjects.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numberOfObjects.Name = "numberOfObjects";
            numberOfObjects.Size = new Size(95, 27);
            numberOfObjects.TabIndex = 116;
            numberOfObjects.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // maxDepth
            // 
            maxDepth.Location = new Point(994, 563);
            maxDepth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            maxDepth.Name = "maxDepth";
            maxDepth.Size = new Size(95, 27);
            maxDepth.TabIndex = 117;
            maxDepth.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // removeCount
            // 
            removeCount.Location = new Point(186, 158);
            removeCount.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            removeCount.Name = "removeCount";
            removeCount.Size = new Size(150, 27);
            removeCount.TabIndex = 121;
            removeCount.Value = new decimal(new int[] { 1000, 0, 0, 0 });
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(31, 159);
            label15.Name = "label15";
            label15.Size = new Size(103, 20);
            label15.TabIndex = 120;
            label15.Text = "Počet Remove";
            // 
            // radioButtonPoints
            // 
            radioButtonPoints.AutoSize = true;
            radioButtonPoints.Checked = true;
            radioButtonPoints.Location = new Point(6, 36);
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
            radioButtonPolygons.Location = new Point(6, 72);
            radioButtonPolygons.Name = "radioButtonPolygons";
            radioButtonPolygons.Size = new Size(173, 24);
            radioButtonPolygons.TabIndex = 123;
            radioButtonPolygons.Text = "polygons (rectangles)";
            radioButtonPolygons.UseVisualStyleBackColor = true;
            // 
            // radioButtonBoth
            // 
            radioButtonBoth.AutoSize = true;
            radioButtonBoth.Location = new Point(6, 107);
            radioButtonBoth.Name = "radioButtonBoth";
            radioButtonBoth.Size = new Size(61, 24);
            radioButtonBoth.TabIndex = 124;
            radioButtonBoth.Text = "both";
            radioButtonBoth.UseVisualStyleBackColor = true;
            // 
            // checkBoxDrawing
            // 
            checkBoxDrawing.AutoSize = true;
            checkBoxDrawing.Checked = true;
            checkBoxDrawing.CheckState = CheckState.Checked;
            checkBoxDrawing.Location = new Point(493, 212);
            checkBoxDrawing.Name = "checkBoxDrawing";
            checkBoxDrawing.Size = new Size(74, 24);
            checkBoxDrawing.TabIndex = 126;
            checkBoxDrawing.Text = "DRAW";
            checkBoxDrawing.UseVisualStyleBackColor = true;
            // 
            // checkBoxInterference
            // 
            checkBoxInterference.AutoSize = true;
            checkBoxInterference.Location = new Point(27, 251);
            checkBoxInterference.Name = "checkBoxInterference";
            checkBoxInterference.Size = new Size(157, 24);
            checkBoxInterference.TabIndex = 127;
            checkBoxInterference.Text = "partial interference";
            checkBoxInterference.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 217);
            label1.Name = "label1";
            label1.Size = new Size(133, 20);
            label1.TabIndex = 128;
            label1.Text = "INTERVAL SEARCH";
            // 
            // checkBoxSeed
            // 
            checkBoxSeed.AutoSize = true;
            checkBoxSeed.Location = new Point(234, 248);
            checkBoxSeed.Name = "checkBoxSeed";
            checkBoxSeed.Size = new Size(99, 24);
            checkBoxSeed.TabIndex = 129;
            checkBoxSeed.Text = "fixed seed";
            checkBoxSeed.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButtonBoth);
            groupBox1.Controls.Add(radioButtonPolygons);
            groupBox1.Controls.Add(radioButtonPoints);
            groupBox1.Location = new Point(486, 50);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(182, 148);
            groupBox1.TabIndex = 133;
            groupBox1.TabStop = false;
            groupBox1.Text = "Objects in box";
            // 
            // radioButtonQtree
            // 
            radioButtonQtree.AutoSize = true;
            radioButtonQtree.Location = new Point(6, 36);
            radioButtonQtree.Name = "radioButtonQtree";
            radioButtonQtree.Size = new Size(68, 24);
            radioButtonQtree.TabIndex = 134;
            radioButtonQtree.TabStop = true;
            radioButtonQtree.Text = "QTree";
            radioButtonQtree.UseVisualStyleBackColor = true;
            // 
            // radioButtonMyQtree
            // 
            radioButtonMyQtree.AutoSize = true;
            radioButtonMyQtree.Location = new Point(6, 69);
            radioButtonMyQtree.Name = "radioButtonMyQtree";
            radioButtonMyQtree.Size = new Size(87, 24);
            radioButtonMyQtree.TabIndex = 135;
            radioButtonMyQtree.TabStop = true;
            radioButtonMyQtree.Text = "MyQtree";
            radioButtonMyQtree.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(radioButtonMyQtree);
            groupBox2.Controls.Add(radioButtonQtree);
            groupBox2.Location = new Point(351, 50);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(121, 148);
            groupBox2.TabIndex = 136;
            groupBox2.TabStop = false;
            groupBox2.Text = "Struct";
            // 
            // setNewDepthButton
            // 
            setNewDepthButton.Location = new Point(1119, 565);
            setNewDepthButton.Margin = new Padding(3, 4, 3, 4);
            setNewDepthButton.Name = "setNewDepthButton";
            setNewDepthButton.Size = new Size(114, 28);
            setNewDepthButton.TabIndex = 137;
            setNewDepthButton.Text = "Set";
            setNewDepthButton.UseVisualStyleBackColor = true;
            setNewDepthButton.Click += setNewDepthButton_Click;
            // 
            // appbutton
            // 
            appbutton.Location = new Point(24, 12);
            appbutton.Name = "appbutton";
            appbutton.Size = new Size(85, 34);
            appbutton.TabIndex = 138;
            appbutton.Text = "app";
            appbutton.UseVisualStyleBackColor = true;
            appbutton.Click += appbutton_Click;
            // 
            // Test
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(1247, 679);
            Controls.Add(appbutton);
            Controls.Add(setNewDepthButton);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(checkBoxSeed);
            Controls.Add(label1);
            Controls.Add(checkBoxInterference);
            Controls.Add(checkBoxDrawing);
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
            Controls.Add(numericUpDown3);
            Controls.Add(numericUpDown2);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(label2);
            Name = "Test";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown3).EndInit();
            ((System.ComponentModel.ISupportInitialize)heigth_tree).EndInit();
            ((System.ComponentModel.ISupportInitialize)width_tree).EndInit();
            ((System.ComponentModel.ISupportInitialize)numberOfObjects).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxDepth).EndInit();
            ((System.ComponentModel.ISupportInitialize)removeCount).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label3;
        private Button button1;
        private RichTextBox richTextBox1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown3;
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
        private CheckBox checkBoxDrawing;
        private CheckBox checkBoxInterference;
        private Label label1;
        private CheckBox checkBoxSeed;
        private GroupBox groupBox1;
        private RadioButton radioButtonQtree;
        private RadioButton radioButtonMyQtree;
        private GroupBox groupBox2;
        private Button setNewDepthButton;
        private Button appbutton;
    }
}
namespace QuadTree.UI
{
    partial class App
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            QuadPanel = new Panel();
            testbutton = new Button();
            menupanel = new Panel();
            editPropButton = new Button();
            addPlotButton = new Button();
            addPropButton = new Button();
            deletePropButton = new Button();
            giveRangeButton = new Button();
            searchPropButton = new Button();
            editPlotButton = new Button();
            panelSearchForProp = new Panel();
            rbPlot = new RadioButton();
            rbProp = new RadioButton();
            propInfo = new RichTextBox();
            button2 = new Button();
            label5 = new Label();
            latitudeNum = new NumericUpDown();
            label4 = new Label();
            longitudeNum = new NumericUpDown();
            label3 = new Label();
            panelGiveRange = new Panel();
            groupBox2 = new GroupBox();
            label2 = new Label();
            endPosLat = new NumericUpDown();
            label6 = new Label();
            endPosLong = new NumericUpDown();
            groupBox1 = new GroupBox();
            label9 = new Label();
            startPosLat = new NumericUpDown();
            label10 = new Label();
            startPosLong = new NumericUpDown();
            objInfo = new RichTextBox();
            searchRangeButton = new Button();
            label8 = new Label();
            panelAddProp = new Panel();
            label7 = new Label();
            label1 = new Label();
            description = new RichTextBox();
            registrationNumber = new TextBox();
            groupBox4 = new GroupBox();
            label11 = new Label();
            posLat = new NumericUpDown();
            label12 = new Label();
            posLong = new NumericUpDown();
            addBTN = new Button();
            label13 = new Label();
            panelAddPlot = new Panel();
            groupBox5 = new GroupBox();
            label19 = new Label();
            endPosPlotLat = new NumericUpDown();
            label20 = new Label();
            endPosPlotLong = new NumericUpDown();
            label14 = new Label();
            label15 = new Label();
            PlotDesc = new RichTextBox();
            regPlotNumber = new TextBox();
            groupBox3 = new GroupBox();
            label16 = new Label();
            startPosPlotLat = new NumericUpDown();
            label17 = new Label();
            startPosPlotLong = new NumericUpDown();
            PlotAddBtn = new Button();
            label18 = new Label();
            panelDelete = new Panel();
            datagridOBJ = new DataGridView();
            deleteBtn = new Button();
            label27 = new Label();
            menupanel.SuspendLayout();
            panelSearchForProp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)latitudeNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)longitudeNum).BeginInit();
            panelGiveRange.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)endPosLat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)endPosLong).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)startPosLat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)startPosLong).BeginInit();
            panelAddProp.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)posLat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)posLong).BeginInit();
            panelAddPlot.SuspendLayout();
            groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)endPosPlotLat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)endPosPlotLong).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)startPosPlotLat).BeginInit();
            ((System.ComponentModel.ISupportInitialize)startPosPlotLong).BeginInit();
            panelDelete.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)datagridOBJ).BeginInit();
            SuspendLayout();
            // 
            // QuadPanel
            // 
            QuadPanel.Location = new Point(521, 12);
            QuadPanel.Name = "QuadPanel";
            QuadPanel.Size = new Size(712, 565);
            QuadPanel.TabIndex = 112;
            QuadPanel.Paint += QuadPanel_Paint;
            // 
            // testbutton
            // 
            testbutton.Location = new Point(0, 12);
            testbutton.Name = "testbutton";
            testbutton.Size = new Size(191, 26);
            testbutton.TabIndex = 139;
            testbutton.Text = "test";
            testbutton.UseVisualStyleBackColor = true;
            testbutton.Click += appbutton_Click;
            // 
            // menupanel
            // 
            menupanel.BackColor = Color.PapayaWhip;
            menupanel.Controls.Add(editPropButton);
            menupanel.Controls.Add(addPlotButton);
            menupanel.Controls.Add(addPropButton);
            menupanel.Controls.Add(deletePropButton);
            menupanel.Controls.Add(giveRangeButton);
            menupanel.Controls.Add(searchPropButton);
            menupanel.Controls.Add(editPlotButton);
            menupanel.Controls.Add(testbutton);
            menupanel.Dock = DockStyle.Left;
            menupanel.Location = new Point(0, 0);
            menupanel.Name = "menupanel";
            menupanel.Size = new Size(191, 679);
            menupanel.TabIndex = 140;
            // 
            // editPropButton
            // 
            editPropButton.FlatStyle = FlatStyle.Flat;
            editPropButton.Location = new Point(0, 399);
            editPropButton.Name = "editPropButton";
            editPropButton.Size = new Size(191, 59);
            editPropButton.TabIndex = 148;
            editPropButton.Text = "EDIT PROP";
            editPropButton.UseVisualStyleBackColor = true;
            // 
            // addPlotButton
            // 
            addPlotButton.FlatStyle = FlatStyle.Flat;
            addPlotButton.Location = new Point(0, 280);
            addPlotButton.Name = "addPlotButton";
            addPlotButton.Size = new Size(191, 59);
            addPlotButton.TabIndex = 143;
            addPlotButton.Text = "ADD PLOT";
            addPlotButton.UseVisualStyleBackColor = true;
            addPlotButton.Click += addPlotButton_Click;
            // 
            // addPropButton
            // 
            addPropButton.FlatStyle = FlatStyle.Flat;
            addPropButton.Location = new Point(0, 210);
            addPropButton.Name = "addPropButton";
            addPropButton.Size = new Size(191, 59);
            addPropButton.TabIndex = 142;
            addPropButton.Text = "ADD PROP";
            addPropButton.UseVisualStyleBackColor = true;
            addPropButton.Click += addPropButton_Click;
            // 
            // deletePropButton
            // 
            deletePropButton.FlatStyle = FlatStyle.Flat;
            deletePropButton.Location = new Point(0, 539);
            deletePropButton.Name = "deletePropButton";
            deletePropButton.Size = new Size(191, 59);
            deletePropButton.TabIndex = 146;
            deletePropButton.Text = "DELETE";
            deletePropButton.UseVisualStyleBackColor = true;
            deletePropButton.Click += deletePropButton_Click;
            // 
            // giveRangeButton
            // 
            giveRangeButton.FlatStyle = FlatStyle.Flat;
            giveRangeButton.Location = new Point(0, 140);
            giveRangeButton.Name = "giveRangeButton";
            giveRangeButton.Size = new Size(191, 59);
            giveRangeButton.TabIndex = 141;
            giveRangeButton.Text = "GIVE RANGE";
            giveRangeButton.UseVisualStyleBackColor = true;
            giveRangeButton.Click += giveRangeButton_Click;
            // 
            // searchPropButton
            // 
            searchPropButton.FlatStyle = FlatStyle.Flat;
            searchPropButton.Location = new Point(0, 70);
            searchPropButton.Name = "searchPropButton";
            searchPropButton.Size = new Size(191, 59);
            searchPropButton.TabIndex = 140;
            searchPropButton.Text = "SEARCH";
            searchPropButton.UseVisualStyleBackColor = true;
            searchPropButton.Click += searchPropButton_Click;
            // 
            // editPlotButton
            // 
            editPlotButton.FlatStyle = FlatStyle.Flat;
            editPlotButton.Location = new Point(0, 469);
            editPlotButton.Name = "editPlotButton";
            editPlotButton.Size = new Size(191, 59);
            editPlotButton.TabIndex = 145;
            editPlotButton.Text = "EDIT PLOT";
            editPlotButton.UseVisualStyleBackColor = true;
            // 
            // panelSearchForProp
            // 
            panelSearchForProp.BackColor = Color.DarkSalmon;
            panelSearchForProp.Controls.Add(rbPlot);
            panelSearchForProp.Controls.Add(rbProp);
            panelSearchForProp.Controls.Add(propInfo);
            panelSearchForProp.Controls.Add(button2);
            panelSearchForProp.Controls.Add(label5);
            panelSearchForProp.Controls.Add(latitudeNum);
            panelSearchForProp.Controls.Add(label4);
            panelSearchForProp.Controls.Add(longitudeNum);
            panelSearchForProp.Controls.Add(label3);
            panelSearchForProp.Location = new Point(191, 0);
            panelSearchForProp.Name = "panelSearchForProp";
            panelSearchForProp.Size = new Size(310, 679);
            panelSearchForProp.TabIndex = 141;
            // 
            // rbPlot
            // 
            rbPlot.AutoSize = true;
            rbPlot.Location = new Point(95, 108);
            rbPlot.Name = "rbPlot";
            rbPlot.Size = new Size(62, 24);
            rbPlot.TabIndex = 8;
            rbPlot.Text = "PLOT";
            rbPlot.UseVisualStyleBackColor = true;
            // 
            // rbProp
            // 
            rbProp.AutoSize = true;
            rbProp.Checked = true;
            rbProp.Location = new Point(95, 73);
            rbProp.Name = "rbProp";
            rbProp.Size = new Size(98, 24);
            rbProp.TabIndex = 7;
            rbProp.TabStop = true;
            rbProp.Text = "PROPERTY";
            rbProp.UseVisualStyleBackColor = true;
            // 
            // propInfo
            // 
            propInfo.Location = new Point(36, 436);
            propInfo.Name = "propInfo";
            propInfo.Size = new Size(222, 197);
            propInfo.TabIndex = 6;
            propInfo.Text = "";
            // 
            // button2
            // 
            button2.Location = new Point(36, 331);
            button2.Name = "button2";
            button2.Size = new Size(222, 58);
            button2.TabIndex = 5;
            button2.Text = "SEARCH";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(77, 238);
            label5.Name = "label5";
            label5.Size = new Size(60, 20);
            label5.TabIndex = 4;
            label5.Text = "latitude";
            // 
            // latitudeNum
            // 
            latitudeNum.Location = new Point(77, 261);
            latitudeNum.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            latitudeNum.Name = "latitudeNum";
            latitudeNum.Size = new Size(135, 27);
            latitudeNum.TabIndex = 3;
            latitudeNum.TextAlign = HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(77, 170);
            label4.Name = "label4";
            label4.Size = new Size(73, 20);
            label4.TabIndex = 2;
            label4.Text = "longitude";
            // 
            // longitudeNum
            // 
            longitudeNum.Location = new Point(77, 193);
            longitudeNum.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            longitudeNum.Name = "longitudeNum";
            longitudeNum.Size = new Size(135, 27);
            longitudeNum.TabIndex = 1;
            longitudeNum.TextAlign = HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(53, 18);
            label3.Name = "label3";
            label3.Size = new Size(205, 20);
            label3.TabIndex = 0;
            label3.Text = "SEARCH FOR PROPERTY/PLOT";
            // 
            // panelGiveRange
            // 
            panelGiveRange.BackColor = Color.DarkSalmon;
            panelGiveRange.Controls.Add(groupBox2);
            panelGiveRange.Controls.Add(groupBox1);
            panelGiveRange.Controls.Add(objInfo);
            panelGiveRange.Controls.Add(searchRangeButton);
            panelGiveRange.Controls.Add(label8);
            panelGiveRange.Location = new Point(191, 0);
            panelGiveRange.Name = "panelGiveRange";
            panelGiveRange.Size = new Size(310, 679);
            panelGiveRange.TabIndex = 142;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(endPosLat);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(endPosLong);
            groupBox2.Location = new Point(156, 138);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(141, 166);
            groupBox2.TabIndex = 12;
            groupBox2.TabStop = false;
            groupBox2.Text = "Ending position";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 96);
            label2.Name = "label2";
            label2.Size = new Size(60, 20);
            label2.TabIndex = 10;
            label2.Text = "latitude";
            // 
            // endPosLat
            // 
            endPosLat.Location = new Point(12, 121);
            endPosLat.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            endPosLat.Name = "endPosLat";
            endPosLat.Size = new Size(123, 27);
            endPosLat.TabIndex = 9;
            endPosLat.TextAlign = HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 31);
            label6.Name = "label6";
            label6.Size = new Size(73, 20);
            label6.TabIndex = 8;
            label6.Text = "longitude";
            // 
            // endPosLong
            // 
            endPosLong.Location = new Point(12, 53);
            endPosLong.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            endPosLong.Name = "endPosLong";
            endPosLong.Size = new Size(123, 27);
            endPosLong.TabIndex = 7;
            endPosLong.TextAlign = HorizontalAlignment.Right;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label9);
            groupBox1.Controls.Add(startPosLat);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(startPosLong);
            groupBox1.Location = new Point(9, 138);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(141, 166);
            groupBox1.TabIndex = 11;
            groupBox1.TabStop = false;
            groupBox1.Text = "Starting position";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 96);
            label9.Name = "label9";
            label9.Size = new Size(60, 20);
            label9.TabIndex = 10;
            label9.Text = "latitude";
            // 
            // startPosLat
            // 
            startPosLat.Location = new Point(12, 121);
            startPosLat.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            startPosLat.Name = "startPosLat";
            startPosLat.Size = new Size(123, 27);
            startPosLat.TabIndex = 9;
            startPosLat.TextAlign = HorizontalAlignment.Right;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 31);
            label10.Name = "label10";
            label10.Size = new Size(73, 20);
            label10.TabIndex = 8;
            label10.Text = "longitude";
            // 
            // startPosLong
            // 
            startPosLong.Location = new Point(12, 53);
            startPosLong.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            startPosLong.Name = "startPosLong";
            startPosLong.Size = new Size(123, 27);
            startPosLong.TabIndex = 7;
            startPosLong.TextAlign = HorizontalAlignment.Right;
            // 
            // objInfo
            // 
            objInfo.Location = new Point(36, 436);
            objInfo.Name = "objInfo";
            objInfo.Size = new Size(222, 197);
            objInfo.TabIndex = 6;
            objInfo.Text = "";
            // 
            // searchRangeButton
            // 
            searchRangeButton.Location = new Point(36, 332);
            searchRangeButton.Name = "searchRangeButton";
            searchRangeButton.Size = new Size(222, 58);
            searchRangeButton.TabIndex = 5;
            searchRangeButton.Text = "SEARCH";
            searchRangeButton.UseVisualStyleBackColor = true;
            searchRangeButton.Click += searchRangeButton_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(68, 38);
            label8.Name = "label8";
            label8.Size = new Size(173, 20);
            label8.TabIndex = 0;
            label8.Text = "GIVE RANGE TO SEARCH";
            // 
            // panelAddProp
            // 
            panelAddProp.BackColor = Color.DarkSalmon;
            panelAddProp.Controls.Add(label7);
            panelAddProp.Controls.Add(label1);
            panelAddProp.Controls.Add(description);
            panelAddProp.Controls.Add(registrationNumber);
            panelAddProp.Controls.Add(groupBox4);
            panelAddProp.Controls.Add(addBTN);
            panelAddProp.Controls.Add(label13);
            panelAddProp.Location = new Point(191, 0);
            panelAddProp.Name = "panelAddProp";
            panelAddProp.Size = new Size(310, 679);
            panelAddProp.TabIndex = 143;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(113, 373);
            label7.Name = "label7";
            label7.Size = new Size(85, 20);
            label7.TabIndex = 15;
            label7.Text = "Description";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(82, 100);
            label1.Name = "label1";
            label1.Size = new Size(147, 20);
            label1.TabIndex = 14;
            label1.Text = "Registration Number";
            // 
            // description
            // 
            description.Location = new Point(54, 396);
            description.Name = "description";
            description.Size = new Size(202, 135);
            description.TabIndex = 13;
            description.Text = "";
            // 
            // registrationNumber
            // 
            registrationNumber.Location = new Point(65, 123);
            registrationNumber.Name = "registrationNumber";
            registrationNumber.Size = new Size(181, 27);
            registrationNumber.TabIndex = 12;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(posLat);
            groupBox4.Controls.Add(label12);
            groupBox4.Controls.Add(posLong);
            groupBox4.Location = new Point(85, 169);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(141, 166);
            groupBox4.TabIndex = 11;
            groupBox4.TabStop = false;
            groupBox4.Text = "Position";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(12, 96);
            label11.Name = "label11";
            label11.Size = new Size(60, 20);
            label11.TabIndex = 10;
            label11.Text = "latitude";
            // 
            // posLat
            // 
            posLat.Location = new Point(12, 121);
            posLat.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            posLat.Name = "posLat";
            posLat.Size = new Size(123, 27);
            posLat.TabIndex = 9;
            posLat.TextAlign = HorizontalAlignment.Right;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(12, 31);
            label12.Name = "label12";
            label12.Size = new Size(73, 20);
            label12.TabIndex = 8;
            label12.Text = "longitude";
            // 
            // posLong
            // 
            posLong.Location = new Point(12, 53);
            posLong.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            posLong.Name = "posLong";
            posLong.Size = new Size(123, 27);
            posLong.TabIndex = 7;
            posLong.TextAlign = HorizontalAlignment.Right;
            // 
            // addBTN
            // 
            addBTN.Location = new Point(44, 575);
            addBTN.Name = "addBTN";
            addBTN.Size = new Size(222, 58);
            addBTN.TabIndex = 5;
            addBTN.Text = "ADD";
            addBTN.UseVisualStyleBackColor = true;
            addBTN.Click += addBTN_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(99, 38);
            label13.Name = "label13";
            label13.Size = new Size(113, 20);
            label13.TabIndex = 0;
            label13.Text = "ADD PROPERTY";
            // 
            // panelAddPlot
            // 
            panelAddPlot.BackColor = Color.DarkSalmon;
            panelAddPlot.Controls.Add(groupBox5);
            panelAddPlot.Controls.Add(label14);
            panelAddPlot.Controls.Add(label15);
            panelAddPlot.Controls.Add(PlotDesc);
            panelAddPlot.Controls.Add(regPlotNumber);
            panelAddPlot.Controls.Add(groupBox3);
            panelAddPlot.Controls.Add(PlotAddBtn);
            panelAddPlot.Controls.Add(label18);
            panelAddPlot.Location = new Point(191, 0);
            panelAddPlot.Name = "panelAddPlot";
            panelAddPlot.Size = new Size(310, 679);
            panelAddPlot.TabIndex = 144;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label19);
            groupBox5.Controls.Add(endPosPlotLat);
            groupBox5.Controls.Add(label20);
            groupBox5.Controls.Add(endPosPlotLong);
            groupBox5.Location = new Point(158, 174);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(141, 166);
            groupBox5.TabIndex = 16;
            groupBox5.TabStop = false;
            groupBox5.Text = "End Position";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(12, 96);
            label19.Name = "label19";
            label19.Size = new Size(60, 20);
            label19.TabIndex = 10;
            label19.Text = "latitude";
            // 
            // endPosPlotLat
            // 
            endPosPlotLat.Location = new Point(12, 121);
            endPosPlotLat.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            endPosPlotLat.Name = "endPosPlotLat";
            endPosPlotLat.Size = new Size(123, 27);
            endPosPlotLat.TabIndex = 9;
            endPosPlotLat.TextAlign = HorizontalAlignment.Right;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(12, 31);
            label20.Name = "label20";
            label20.Size = new Size(73, 20);
            label20.TabIndex = 8;
            label20.Text = "longitude";
            // 
            // endPosPlotLong
            // 
            endPosPlotLong.Location = new Point(12, 53);
            endPosPlotLong.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            endPosPlotLong.Name = "endPosPlotLong";
            endPosPlotLong.Size = new Size(123, 27);
            endPosPlotLong.TabIndex = 7;
            endPosPlotLong.TextAlign = HorizontalAlignment.Right;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(115, 377);
            label14.Name = "label14";
            label14.Size = new Size(85, 20);
            label14.TabIndex = 15;
            label14.Text = "Description";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(84, 104);
            label15.Name = "label15";
            label15.Size = new Size(147, 20);
            label15.TabIndex = 14;
            label15.Text = "Registration Number";
            // 
            // PlotDesc
            // 
            PlotDesc.Location = new Point(56, 400);
            PlotDesc.Name = "PlotDesc";
            PlotDesc.Size = new Size(202, 135);
            PlotDesc.TabIndex = 13;
            PlotDesc.Text = "";
            // 
            // regPlotNumber
            // 
            regPlotNumber.Location = new Point(67, 127);
            regPlotNumber.Name = "regPlotNumber";
            regPlotNumber.Size = new Size(181, 27);
            regPlotNumber.TabIndex = 12;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label16);
            groupBox3.Controls.Add(startPosPlotLat);
            groupBox3.Controls.Add(label17);
            groupBox3.Controls.Add(startPosPlotLong);
            groupBox3.Location = new Point(11, 174);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(141, 166);
            groupBox3.TabIndex = 11;
            groupBox3.TabStop = false;
            groupBox3.Text = "Start Position";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(12, 96);
            label16.Name = "label16";
            label16.Size = new Size(60, 20);
            label16.TabIndex = 10;
            label16.Text = "latitude";
            // 
            // startPosPlotLat
            // 
            startPosPlotLat.Location = new Point(12, 121);
            startPosPlotLat.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            startPosPlotLat.Name = "startPosPlotLat";
            startPosPlotLat.Size = new Size(123, 27);
            startPosPlotLat.TabIndex = 9;
            startPosPlotLat.TextAlign = HorizontalAlignment.Right;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(12, 31);
            label17.Name = "label17";
            label17.Size = new Size(73, 20);
            label17.TabIndex = 8;
            label17.Text = "longitude";
            // 
            // startPosPlotLong
            // 
            startPosPlotLong.Location = new Point(12, 53);
            startPosPlotLong.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            startPosPlotLong.Name = "startPosPlotLong";
            startPosPlotLong.Size = new Size(123, 27);
            startPosPlotLong.TabIndex = 7;
            startPosPlotLong.TextAlign = HorizontalAlignment.Right;
            // 
            // PlotAddBtn
            // 
            PlotAddBtn.Location = new Point(46, 579);
            PlotAddBtn.Name = "PlotAddBtn";
            PlotAddBtn.Size = new Size(222, 58);
            PlotAddBtn.TabIndex = 5;
            PlotAddBtn.Text = "ADD";
            PlotAddBtn.UseVisualStyleBackColor = true;
            PlotAddBtn.Click += PlotAddBtn_Click;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(113, 38);
            label18.Name = "label18";
            label18.Size = new Size(77, 20);
            label18.TabIndex = 0;
            label18.Text = "ADD PLOT";
            // 
            // panelDelete
            // 
            panelDelete.BackColor = Color.DarkSalmon;
            panelDelete.Controls.Add(datagridOBJ);
            panelDelete.Controls.Add(deleteBtn);
            panelDelete.Controls.Add(label27);
            panelDelete.Location = new Point(191, 0);
            panelDelete.Name = "panelDelete";
            panelDelete.Size = new Size(310, 679);
            panelDelete.TabIndex = 145;
            // 
            // datagridOBJ
            // 
            datagridOBJ.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            datagridOBJ.Location = new Point(17, 70);
            datagridOBJ.Name = "datagridOBJ";
            datagridOBJ.RowHeadersWidth = 51;
            datagridOBJ.RowTemplate.Height = 29;
            datagridOBJ.Size = new Size(268, 494);
            datagridOBJ.TabIndex = 6;
            datagridOBJ.CellContentClick += datagridOBJ_CellContentClick;
            // 
            // deleteBtn
            // 
            deleteBtn.Location = new Point(40, 579);
            deleteBtn.Name = "deleteBtn";
            deleteBtn.Size = new Size(222, 58);
            deleteBtn.TabIndex = 5;
            deleteBtn.Text = "DELETE";
            deleteBtn.UseVisualStyleBackColor = true;
            deleteBtn.Click += deleteBtn_Click;
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Location = new Point(122, 38);
            label27.Name = "label27";
            label27.Size = new Size(59, 20);
            label27.TabIndex = 0;
            label27.Text = "DELETE";
            label27.Click += label27_Click;
            // 
            // App
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1258, 679);
            Controls.Add(panelDelete);
            Controls.Add(panelAddPlot);
            Controls.Add(panelAddProp);
            Controls.Add(panelGiveRange);
            Controls.Add(panelSearchForProp);
            Controls.Add(menupanel);
            Controls.Add(QuadPanel);
            Name = "App";
            StartPosition = FormStartPosition.CenterParent;
            Text = "App";
            Load += App_Load;
            menupanel.ResumeLayout(false);
            panelSearchForProp.ResumeLayout(false);
            panelSearchForProp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)latitudeNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)longitudeNum).EndInit();
            panelGiveRange.ResumeLayout(false);
            panelGiveRange.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)endPosLat).EndInit();
            ((System.ComponentModel.ISupportInitialize)endPosLong).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)startPosLat).EndInit();
            ((System.ComponentModel.ISupportInitialize)startPosLong).EndInit();
            panelAddProp.ResumeLayout(false);
            panelAddProp.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)posLat).EndInit();
            ((System.ComponentModel.ISupportInitialize)posLong).EndInit();
            panelAddPlot.ResumeLayout(false);
            panelAddPlot.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)endPosPlotLat).EndInit();
            ((System.ComponentModel.ISupportInitialize)endPosPlotLong).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)startPosPlotLat).EndInit();
            ((System.ComponentModel.ISupportInitialize)startPosPlotLong).EndInit();
            panelDelete.ResumeLayout(false);
            panelDelete.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)datagridOBJ).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel QuadPanel;
        private Button testbutton;
        private Panel menupanel;
        private Button editPropButton;
        private Button addPlotButton;
        private Button addPropButton;
        private Button deletePropButton;
        private Button giveRangeButton;
        private Button searchPropButton;
        private Button editPlotButton;
        private Panel panelSearchForProp;
        private Label label3;
        private Button button2;
        private Label label5;
        private NumericUpDown latitudeNum;
        private Label label4;
        private NumericUpDown longitudeNum;
        private RichTextBox propInfo;
        private RadioButton rbPlot;
        private RadioButton rbProp;
        private Panel panelGiveRange;
        private RichTextBox objInfo;
        private Button searchRangeButton;
        private Label label8;
        private GroupBox groupBox2;
        private Label label2;
        private NumericUpDown endPosLat;
        private Label label6;
        private NumericUpDown endPosLong;
        private GroupBox groupBox1;
        private Label label9;
        private NumericUpDown startPosLat;
        private Label label10;
        private NumericUpDown startPosLong;
        private Panel panelAddProp;
        private Label label7;
        private Label label1;
        private RichTextBox description;
        private TextBox registrationNumber;
        private GroupBox groupBox4;
        private Label label11;
        private NumericUpDown posLat;
        private Label label12;
        private NumericUpDown posLong;
        private Button addBTN;
        private Label label13;
        private Panel panelAddPlot;
        private GroupBox groupBox5;
        private Label label19;
        private NumericUpDown endPosPlotLat;
        private Label label20;
        private NumericUpDown endPosPlotLong;
        private Label label14;
        private Label label15;
        private RichTextBox PlotDesc;
        private TextBox regPlotNumber;
        private GroupBox groupBox3;
        private Label label16;
        private NumericUpDown startPosPlotLat;
        private Label label17;
        private NumericUpDown startPosPlotLong;
        private Button PlotAddBtn;
        private Label label18;
        private Panel panelDelete;
        private GroupBox groupBox6;
        private Label label21;
        private NumericUpDown numericUpDown1;
        private Label label22;
        private NumericUpDown numericUpDown2;
        private Label label23;
        private Label label24;
        private RichTextBox richTextBox1;
        private TextBox textBox1;
        private GroupBox groupBox7;
        private Label label25;
        private NumericUpDown numericUpDown3;
        private Label label26;
        private NumericUpDown numericUpDown4;
        private Button deleteBtn;
        private Label label27;
        private DataGridView datagridOBJ;
    }
}
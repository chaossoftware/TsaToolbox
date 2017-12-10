using System.Windows.Forms;

namespace TimeSeriesAnalysis {
    partial class mainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.openFileBtn = new System.Windows.Forms.Button();
            this.poincareMapPBox = new System.Windows.Forms.PictureBox();
            this.plotBtn = new System.Windows.Forms.Button();
            this.signalPBox = new System.Windows.Forms.PictureBox();
            this.fileNameLbl = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.sigPlotLbl = new System.Windows.Forms.Label();
            this.phasePortraitLbl = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.waveletPBox = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.fourierPBox = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.waveletGroup = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.CboxColorMap = new System.Windows.Forms.ComboBox();
            this.wav_nameCbox = new System.Windows.Forms.ComboBox();
            this.wav_dtNum = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.wav_endFreq = new System.Windows.Forms.NumericUpDown();
            this.wav_startFreq = new System.Windows.Forms.NumericUpDown();
            this.waveletNameLbl = new System.Windows.Forms.Label();
            this.waveletCheckbox = new System.Windows.Forms.CheckBox();
            this.fourierGroup = new System.Windows.Forms.GroupBox();
            this.fourier_dtNum = new System.Windows.Forms.NumericUpDown();
            this.fourier_logCheckbox = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.fourierEndFreqNum = new System.Windows.Forms.NumericUpDown();
            this.fourierStartFreqNum = new System.Windows.Forms.NumericUpDown();
            this.fourierCheckbox = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lyap_calc_Grp = new System.Windows.Forms.GroupBox();
            this.lyap_calc_Rad_kantz = new System.Windows.Forms.RadioButton();
            this.leInTimeGroup = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.lyapunovRedrawBtn = new System.Windows.Forms.Button();
            this.lyapunovPBox = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lyapStartNum = new System.Windows.Forms.NumericUpDown();
            this.lyapEndNum = new System.Windows.Forms.NumericUpDown();
            this.startBtn = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.resultText = new System.Windows.Forms.RichTextBox();
            this.lyap_calc_Rad_rosenstein = new System.Windows.Forms.RadioButton();
            this.lyap_calc_Rad_wolf = new System.Windows.Forms.RadioButton();
            this.lyap_k_Grp = new System.Windows.Forms.GroupBox();
            this.ComboKantzSlope = new System.Windows.Forms.ComboBox();
            this.lyap_k_Lbl_window = new System.Windows.Forms.Label();
            this.lyap_k_Num_maxiter = new System.Windows.Forms.NumericUpDown();
            this.lyap_k_Lbl_maxiter = new System.Windows.Forms.Label();
            this.lyap_k_Lbl_scales = new System.Windows.Forms.Label();
            this.lyap_k_Num_window = new System.Windows.Forms.NumericUpDown();
            this.lyap_k_Num_scales = new System.Windows.Forms.NumericUpDown();
            this.lyap_r_Grp = new System.Windows.Forms.GroupBox();
            this.rosStepsNum = new System.Windows.Forms.NumericUpDown();
            this.rosStepsLbl = new System.Windows.Forms.Label();
            this.rosDistanceLbl = new System.Windows.Forms.Label();
            this.rosDistanceNum = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.scaleMaxNum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.lyap_w_Grp = new System.Windows.Forms.GroupBox();
            this.stepNum = new System.Windows.Forms.NumericUpDown();
            this.stepLbl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.evolveStepsNum = new System.Windows.Forms.NumericUpDown();
            this.scaleMinNum = new System.Windows.Forms.NumericUpDown();
            this.tauNum = new System.Windows.Forms.NumericUpDown();
            this.dimLbl = new System.Windows.Forms.Label();
            this.tauLbl = new System.Windows.Forms.Label();
            this.dimNum = new System.Windows.Forms.NumericUpDown();
            this.sourcePrefGBox = new System.Windows.Forms.GroupBox();
            this.sourceStepLbl = new System.Windows.Forms.Label();
            this.sourceStepTxt = new System.Windows.Forms.RichTextBox();
            this.useTimeCheckbox = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.endPointNum = new System.Windows.Forms.NumericUpDown();
            this.startPointNum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pointsNum = new System.Windows.Forms.NumericUpDown();
            this.sourceColumnNum = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.poincareMapPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalPBox)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waveletPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fourierPBox)).BeginInit();
            this.waveletGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wav_dtNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wav_endFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wav_startFreq)).BeginInit();
            this.fourierGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fourier_dtNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fourierEndFreqNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fourierStartFreqNum)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.lyap_calc_Grp.SuspendLayout();
            this.leInTimeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lyapunovPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyapStartNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyapEndNum)).BeginInit();
            this.lyap_k_Grp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lyap_k_Num_maxiter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyap_k_Num_window)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyap_k_Num_scales)).BeginInit();
            this.lyap_r_Grp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rosStepsNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rosDistanceNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleMaxNum)).BeginInit();
            this.lyap_w_Grp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.evolveStepsNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleMinNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tauNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dimNum)).BeginInit();
            this.sourcePrefGBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endPointNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPointNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointsNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceColumnNum)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileBtn
            // 
            this.openFileBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.openFileBtn.Location = new System.Drawing.Point(15, 12);
            this.openFileBtn.Name = "openFileBtn";
            this.openFileBtn.Size = new System.Drawing.Size(75, 23);
            this.openFileBtn.TabIndex = 0;
            this.openFileBtn.Text = "Открыть";
            this.openFileBtn.UseVisualStyleBackColor = true;
            this.openFileBtn.Click += new System.EventHandler(this.openFileBtn_Click);
            // 
            // poincareMapPBox
            // 
            this.poincareMapPBox.BackColor = System.Drawing.Color.White;
            this.poincareMapPBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.poincareMapPBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.poincareMapPBox.Location = new System.Drawing.Point(336, 30);
            this.poincareMapPBox.Name = "poincareMapPBox";
            this.poincareMapPBox.Size = new System.Drawing.Size(320, 240);
            this.poincareMapPBox.TabIndex = 12;
            this.poincareMapPBox.TabStop = false;
            this.poincareMapPBox.DoubleClick += new System.EventHandler(this.poincareMapPBox_DoubleClick);
            // 
            // plotBtn
            // 
            this.plotBtn.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.plotBtn.Location = new System.Drawing.Point(822, 30);
            this.plotBtn.Name = "plotBtn";
            this.plotBtn.Size = new System.Drawing.Size(70, 23);
            this.plotBtn.TabIndex = 2;
            this.plotBtn.Text = "Графики";
            this.plotBtn.UseVisualStyleBackColor = true;
            this.plotBtn.Click += new System.EventHandler(this.plotBtn_Click);
            // 
            // signalPBox
            // 
            this.signalPBox.BackColor = System.Drawing.Color.White;
            this.signalPBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.signalPBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.signalPBox.Location = new System.Drawing.Point(6, 30);
            this.signalPBox.Name = "signalPBox";
            this.signalPBox.Size = new System.Drawing.Size(320, 240);
            this.signalPBox.TabIndex = 11;
            this.signalPBox.TabStop = false;
            this.signalPBox.DoubleClick += new System.EventHandler(this.signalPBox_DoubleClick);
            // 
            // fileNameLbl
            // 
            this.fileNameLbl.AutoSize = true;
            this.fileNameLbl.BackColor = System.Drawing.Color.Khaki;
            this.fileNameLbl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fileNameLbl.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.fileNameLbl.Location = new System.Drawing.Point(96, 12);
            this.fileNameLbl.Name = "fileNameLbl";
            this.fileNameLbl.Size = new System.Drawing.Size(114, 15);
            this.fileNameLbl.TabIndex = 14;
            this.fileNameLbl.Text = "ФАЙЛ НЕ ВЫБРАН";
            // 
            // saveBtn
            // 
            this.saveBtn.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveBtn.Location = new System.Drawing.Point(1069, 84);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(79, 23);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "Сохранить";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // sigPlotLbl
            // 
            this.sigPlotLbl.AutoSize = true;
            this.sigPlotLbl.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.sigPlotLbl.Location = new System.Drawing.Point(3, 12);
            this.sigPlotLbl.Name = "sigPlotLbl";
            this.sigPlotLbl.Size = new System.Drawing.Size(54, 13);
            this.sigPlotLbl.TabIndex = 16;
            this.sigPlotLbl.Text = "Сигнал:";
            // 
            // phasePortraitLbl
            // 
            this.phasePortraitLbl.AutoSize = true;
            this.phasePortraitLbl.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.phasePortraitLbl.Location = new System.Drawing.Point(333, 12);
            this.phasePortraitLbl.Name = "phasePortraitLbl";
            this.phasePortraitLbl.Size = new System.Drawing.Size(164, 13);
            this.phasePortraitLbl.TabIndex = 17;
            this.phasePortraitLbl.Text = "Псевдосечение Пуанкаре:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tabControl1.Location = new System.Drawing.Point(0, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(906, 576);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.waveletPBox);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.fourierPBox);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.waveletGroup);
            this.tabPage1.Controls.Add(this.fourierGroup);
            this.tabPage1.Controls.Add(this.sigPlotLbl);
            this.tabPage1.Controls.Add(this.plotBtn);
            this.tabPage1.Controls.Add(this.poincareMapPBox);
            this.tabPage1.Controls.Add(this.signalPBox);
            this.tabPage1.Controls.Add(this.phasePortraitLbl);
            this.tabPage1.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(898, 550);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Графики";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // waveletPBox
            // 
            this.waveletPBox.BackColor = System.Drawing.Color.White;
            this.waveletPBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.waveletPBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.waveletPBox.Location = new System.Drawing.Point(336, 300);
            this.waveletPBox.Name = "waveletPBox";
            this.waveletPBox.Size = new System.Drawing.Size(320, 240);
            this.waveletPBox.TabIndex = 25;
            this.waveletPBox.TabStop = false;
            this.waveletPBox.DoubleClick += new System.EventHandler(this.waveletPBox_DoubleClick);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label13.Location = new System.Drawing.Point(333, 282);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "Вейвлет:";
            // 
            // fourierPBox
            // 
            this.fourierPBox.BackColor = System.Drawing.Color.White;
            this.fourierPBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fourierPBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.fourierPBox.Location = new System.Drawing.Point(6, 300);
            this.fourierPBox.Name = "fourierPBox";
            this.fourierPBox.Size = new System.Drawing.Size(320, 240);
            this.fourierPBox.TabIndex = 23;
            this.fourierPBox.TabStop = false;
            this.fourierPBox.DoubleClick += new System.EventHandler(this.fourierPBox_DoubleClick);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.label12.Location = new System.Drawing.Point(3, 282);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Спектр мощности:";
            // 
            // waveletGroup
            // 
            this.waveletGroup.Controls.Add(this.label10);
            this.waveletGroup.Controls.Add(this.CboxColorMap);
            this.waveletGroup.Controls.Add(this.wav_nameCbox);
            this.waveletGroup.Controls.Add(this.wav_dtNum);
            this.waveletGroup.Controls.Add(this.label11);
            this.waveletGroup.Controls.Add(this.label15);
            this.waveletGroup.Controls.Add(this.label14);
            this.waveletGroup.Controls.Add(this.wav_endFreq);
            this.waveletGroup.Controls.Add(this.wav_startFreq);
            this.waveletGroup.Controls.Add(this.waveletNameLbl);
            this.waveletGroup.Controls.Add(this.waveletCheckbox);
            this.waveletGroup.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.waveletGroup.Location = new System.Drawing.Point(670, 326);
            this.waveletGroup.Name = "waveletGroup";
            this.waveletGroup.Size = new System.Drawing.Size(222, 196);
            this.waveletGroup.TabIndex = 22;
            this.waveletGroup.TabStop = false;
            this.waveletGroup.Text = "Вейвлет";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(86, 163);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Цвет:";
            // 
            // CboxColorMap
            // 
            this.CboxColorMap.DisplayMember = "haar";
            this.CboxColorMap.FormattingEnabled = true;
            this.CboxColorMap.Items.AddRange(new object[] {
            "pink",
            "parula"});
            this.CboxColorMap.Location = new System.Drawing.Point(131, 160);
            this.CboxColorMap.Name = "CboxColorMap";
            this.CboxColorMap.Size = new System.Drawing.Size(80, 21);
            this.CboxColorMap.TabIndex = 30;
            this.CboxColorMap.Text = "pink";
            // 
            // wav_nameCbox
            // 
            this.wav_nameCbox.DisplayMember = "haar";
            this.wav_nameCbox.FormattingEnabled = true;
            this.wav_nameCbox.Items.AddRange(new object[] {
            "haar",
            "sym2",
            "gaus8",
            "cgau8"});
            this.wav_nameCbox.Location = new System.Drawing.Point(131, 46);
            this.wav_nameCbox.Name = "wav_nameCbox";
            this.wav_nameCbox.Size = new System.Drawing.Size(80, 21);
            this.wav_nameCbox.TabIndex = 29;
            this.wav_nameCbox.Text = "gaus8";
            // 
            // wav_dtNum
            // 
            this.wav_dtNum.DecimalPlaces = 5;
            this.wav_dtNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.wav_dtNum.Location = new System.Drawing.Point(132, 133);
            this.wav_dtNum.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.wav_dtNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.wav_dtNum.Name = "wav_dtNum";
            this.wav_dtNum.Size = new System.Drawing.Size(80, 21);
            this.wav_dtNum.TabIndex = 28;
            this.wav_dtNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.wav_dtNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 135);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Шаг по времени:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(9, 106);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(117, 13);
            this.label15.TabIndex = 25;
            this.label15.Text = "Конечная частота:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(2, 77);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(124, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "Начальная частота:";
            // 
            // wav_endFreq
            // 
            this.wav_endFreq.DecimalPlaces = 2;
            this.wav_endFreq.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.wav_endFreq.Location = new System.Drawing.Point(132, 104);
            this.wav_endFreq.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.wav_endFreq.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.wav_endFreq.Name = "wav_endFreq";
            this.wav_endFreq.Size = new System.Drawing.Size(80, 21);
            this.wav_endFreq.TabIndex = 23;
            this.wav_endFreq.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.wav_endFreq.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // wav_startFreq
            // 
            this.wav_startFreq.DecimalPlaces = 2;
            this.wav_startFreq.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.wav_startFreq.Location = new System.Drawing.Point(131, 75);
            this.wav_startFreq.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.wav_startFreq.Name = "wav_startFreq";
            this.wav_startFreq.Size = new System.Drawing.Size(80, 21);
            this.wav_startFreq.TabIndex = 22;
            this.wav_startFreq.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // waveletNameLbl
            // 
            this.waveletNameLbl.AutoSize = true;
            this.waveletNameLbl.Location = new System.Drawing.Point(35, 49);
            this.waveletNameLbl.Name = "waveletNameLbl";
            this.waveletNameLbl.Size = new System.Drawing.Size(91, 13);
            this.waveletNameLbl.TabIndex = 20;
            this.waveletNameLbl.Text = "Тип вейвлета:";
            // 
            // waveletCheckbox
            // 
            this.waveletCheckbox.AutoSize = true;
            this.waveletCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.waveletCheckbox.Location = new System.Drawing.Point(16, 22);
            this.waveletCheckbox.Name = "waveletCheckbox";
            this.waveletCheckbox.Size = new System.Drawing.Size(136, 17);
            this.waveletCheckbox.TabIndex = 14;
            this.waveletCheckbox.Text = "Построить вейвлет";
            this.waveletCheckbox.UseVisualStyleBackColor = true;
            // 
            // fourierGroup
            // 
            this.fourierGroup.Controls.Add(this.fourier_dtNum);
            this.fourierGroup.Controls.Add(this.fourier_logCheckbox);
            this.fourierGroup.Controls.Add(this.label16);
            this.fourierGroup.Controls.Add(this.label9);
            this.fourierGroup.Controls.Add(this.label8);
            this.fourierGroup.Controls.Add(this.fourierEndFreqNum);
            this.fourierGroup.Controls.Add(this.fourierStartFreqNum);
            this.fourierGroup.Controls.Add(this.fourierCheckbox);
            this.fourierGroup.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.fourierGroup.Location = new System.Drawing.Point(670, 155);
            this.fourierGroup.Name = "fourierGroup";
            this.fourierGroup.Size = new System.Drawing.Size(222, 165);
            this.fourierGroup.TabIndex = 20;
            this.fourierGroup.TabStop = false;
            this.fourierGroup.Text = "Спектр мощности Фурье";
            // 
            // fourier_dtNum
            // 
            this.fourier_dtNum.DecimalPlaces = 5;
            this.fourier_dtNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.fourier_dtNum.Location = new System.Drawing.Point(131, 105);
            this.fourier_dtNum.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.fourier_dtNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.fourier_dtNum.Name = "fourier_dtNum";
            this.fourier_dtNum.Size = new System.Drawing.Size(80, 21);
            this.fourier_dtNum.TabIndex = 26;
            this.fourier_dtNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.fourier_dtNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // fourier_logCheckbox
            // 
            this.fourier_logCheckbox.AutoSize = true;
            this.fourier_logCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.fourier_logCheckbox.Location = new System.Drawing.Point(17, 132);
            this.fourier_logCheckbox.Name = "fourier_logCheckbox";
            this.fourier_logCheckbox.Size = new System.Drawing.Size(131, 17);
            this.fourier_logCheckbox.TabIndex = 25;
            this.fourier_logCheckbox.Text = "Логарифмический";
            this.fourier_logCheckbox.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(20, 107);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(106, 13);
            this.label16.TabIndex = 23;
            this.label16.Text = "Шаг по времени:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Конечная частота:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Начальная частота:";
            // 
            // fourierEndFreqNum
            // 
            this.fourierEndFreqNum.DecimalPlaces = 2;
            this.fourierEndFreqNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.fourierEndFreqNum.Location = new System.Drawing.Point(132, 76);
            this.fourierEndFreqNum.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.fourierEndFreqNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.fourierEndFreqNum.Name = "fourierEndFreqNum";
            this.fourierEndFreqNum.Size = new System.Drawing.Size(80, 21);
            this.fourierEndFreqNum.TabIndex = 6;
            this.fourierEndFreqNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.fourierEndFreqNum.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // fourierStartFreqNum
            // 
            this.fourierStartFreqNum.DecimalPlaces = 2;
            this.fourierStartFreqNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.fourierStartFreqNum.Location = new System.Drawing.Point(132, 47);
            this.fourierStartFreqNum.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.fourierStartFreqNum.Name = "fourierStartFreqNum";
            this.fourierStartFreqNum.Size = new System.Drawing.Size(80, 21);
            this.fourierStartFreqNum.TabIndex = 5;
            this.fourierStartFreqNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // fourierCheckbox
            // 
            this.fourierCheckbox.AutoSize = true;
            this.fourierCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.fourierCheckbox.Location = new System.Drawing.Point(20, 20);
            this.fourierCheckbox.Name = "fourierCheckbox";
            this.fourierCheckbox.Size = new System.Drawing.Size(128, 17);
            this.fourierCheckbox.TabIndex = 0;
            this.fourierCheckbox.Text = "Построить спектр";
            this.fourierCheckbox.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lyap_calc_Grp);
            this.tabPage2.Font = new System.Drawing.Font("Calibri", 9.75F);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(898, 550);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Показатели Ляпунова";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lyap_calc_Grp
            // 
            this.lyap_calc_Grp.Controls.Add(this.lyap_calc_Rad_kantz);
            this.lyap_calc_Grp.Controls.Add(this.leInTimeGroup);
            this.lyap_calc_Grp.Controls.Add(this.lyap_calc_Rad_rosenstein);
            this.lyap_calc_Grp.Controls.Add(this.lyap_calc_Rad_wolf);
            this.lyap_calc_Grp.Controls.Add(this.lyap_k_Grp);
            this.lyap_calc_Grp.Controls.Add(this.lyap_r_Grp);
            this.lyap_calc_Grp.Controls.Add(this.label4);
            this.lyap_calc_Grp.Controls.Add(this.scaleMaxNum);
            this.lyap_calc_Grp.Controls.Add(this.label3);
            this.lyap_calc_Grp.Controls.Add(this.lyap_w_Grp);
            this.lyap_calc_Grp.Controls.Add(this.scaleMinNum);
            this.lyap_calc_Grp.Controls.Add(this.tauNum);
            this.lyap_calc_Grp.Controls.Add(this.dimLbl);
            this.lyap_calc_Grp.Controls.Add(this.tauLbl);
            this.lyap_calc_Grp.Controls.Add(this.dimNum);
            this.lyap_calc_Grp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lyap_calc_Grp.Location = new System.Drawing.Point(8, 3);
            this.lyap_calc_Grp.Name = "lyap_calc_Grp";
            this.lyap_calc_Grp.Size = new System.Drawing.Size(884, 541);
            this.lyap_calc_Grp.TabIndex = 21;
            this.lyap_calc_Grp.TabStop = false;
            this.lyap_calc_Grp.Text = "Расчёт";
            // 
            // lyap_calc_Rad_kantz
            // 
            this.lyap_calc_Rad_kantz.AutoSize = true;
            this.lyap_calc_Rad_kantz.Location = new System.Drawing.Point(6, 351);
            this.lyap_calc_Rad_kantz.Name = "lyap_calc_Rad_kantz";
            this.lyap_calc_Rad_kantz.Size = new System.Drawing.Size(57, 17);
            this.lyap_calc_Rad_kantz.TabIndex = 16;
            this.lyap_calc_Rad_kantz.Text = "Kantz";
            this.lyap_calc_Rad_kantz.UseVisualStyleBackColor = true;
            // 
            // leInTimeGroup
            // 
            this.leInTimeGroup.Controls.Add(this.label19);
            this.leInTimeGroup.Controls.Add(this.lyapunovRedrawBtn);
            this.leInTimeGroup.Controls.Add(this.lyapunovPBox);
            this.leInTimeGroup.Controls.Add(this.label17);
            this.leInTimeGroup.Controls.Add(this.lyapStartNum);
            this.leInTimeGroup.Controls.Add(this.lyapEndNum);
            this.leInTimeGroup.Controls.Add(this.startBtn);
            this.leInTimeGroup.Controls.Add(this.label18);
            this.leInTimeGroup.Controls.Add(this.resultText);
            this.leInTimeGroup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leInTimeGroup.Location = new System.Drawing.Point(366, 250);
            this.leInTimeGroup.Name = "leInTimeGroup";
            this.leInTimeGroup.Size = new System.Drawing.Size(518, 285);
            this.leInTimeGroup.TabIndex = 18;
            this.leInTimeGroup.TabStop = false;
            this.leInTimeGroup.Text = "Результаты";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(6, 19);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(244, 13);
            this.label19.TabIndex = 25;
            this.label19.Text = "Кривая старшего показателя Ляпунова:";
            // 
            // lyapunovRedrawBtn
            // 
            this.lyapunovRedrawBtn.Location = new System.Drawing.Point(412, 255);
            this.lyapunovRedrawBtn.Name = "lyapunovRedrawBtn";
            this.lyapunovRedrawBtn.Size = new System.Drawing.Size(100, 23);
            this.lyapunovRedrawBtn.TabIndex = 14;
            this.lyapunovRedrawBtn.Text = "Перерисовать";
            this.lyapunovRedrawBtn.UseVisualStyleBackColor = true;
            this.lyapunovRedrawBtn.Click += new System.EventHandler(this.lyapunovRedrawBtn_Click);
            // 
            // lyapunovPBox
            // 
            this.lyapunovPBox.BackColor = System.Drawing.Color.White;
            this.lyapunovPBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lyapunovPBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.lyapunovPBox.Location = new System.Drawing.Point(7, 38);
            this.lyapunovPBox.Name = "lyapunovPBox";
            this.lyapunovPBox.Size = new System.Drawing.Size(286, 240);
            this.lyapunovPBox.TabIndex = 13;
            this.lyapunovPBox.TabStop = false;
            this.lyapunovPBox.DoubleClick += new System.EventHandler(this.lyapunovPBox_DoubleClick);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(293, 171);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(113, 13);
            this.label17.TabIndex = 6;
            this.label17.Text = "Начальная точка:";
            // 
            // lyapStartNum
            // 
            this.lyapStartNum.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lyapStartNum.Location = new System.Drawing.Point(412, 168);
            this.lyapStartNum.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.lyapStartNum.Name = "lyapStartNum";
            this.lyapStartNum.Size = new System.Drawing.Size(100, 23);
            this.lyapStartNum.TabIndex = 8;
            this.lyapStartNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // lyapEndNum
            // 
            this.lyapEndNum.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lyapEndNum.Location = new System.Drawing.Point(412, 197);
            this.lyapEndNum.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.lyapEndNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lyapEndNum.Name = "lyapEndNum";
            this.lyapEndNum.Size = new System.Drawing.Size(100, 23);
            this.lyapEndNum.TabIndex = 9;
            this.lyapEndNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.lyapEndNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(331, 36);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 13;
            this.startBtn.Text = "Счёт";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(300, 200);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(106, 13);
            this.label18.TabIndex = 7;
            this.label18.Text = "Конечная точка:";
            // 
            // resultText
            // 
            this.resultText.BackColor = System.Drawing.Color.Khaki;
            this.resultText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resultText.Location = new System.Drawing.Point(412, 38);
            this.resultText.MaxLength = 100;
            this.resultText.Name = "resultText";
            this.resultText.ReadOnly = true;
            this.resultText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.resultText.ShowSelectionMargin = true;
            this.resultText.Size = new System.Drawing.Size(100, 21);
            this.resultText.TabIndex = 4;
            this.resultText.Text = "";
            // 
            // lyap_calc_Rad_rosenstein
            // 
            this.lyap_calc_Rad_rosenstein.AutoSize = true;
            this.lyap_calc_Rad_rosenstein.Location = new System.Drawing.Point(6, 250);
            this.lyap_calc_Rad_rosenstein.Name = "lyap_calc_Rad_rosenstein";
            this.lyap_calc_Rad_rosenstein.Size = new System.Drawing.Size(87, 17);
            this.lyap_calc_Rad_rosenstein.TabIndex = 15;
            this.lyap_calc_Rad_rosenstein.Text = "Rosenstein";
            this.lyap_calc_Rad_rosenstein.UseVisualStyleBackColor = true;
            // 
            // lyap_calc_Rad_wolf
            // 
            this.lyap_calc_Rad_wolf.AutoSize = true;
            this.lyap_calc_Rad_wolf.Checked = true;
            this.lyap_calc_Rad_wolf.Location = new System.Drawing.Point(6, 145);
            this.lyap_calc_Rad_wolf.Name = "lyap_calc_Rad_wolf";
            this.lyap_calc_Rad_wolf.Size = new System.Drawing.Size(49, 17);
            this.lyap_calc_Rad_wolf.TabIndex = 14;
            this.lyap_calc_Rad_wolf.TabStop = true;
            this.lyap_calc_Rad_wolf.Text = "Wolf";
            this.lyap_calc_Rad_wolf.UseVisualStyleBackColor = true;
            // 
            // lyap_k_Grp
            // 
            this.lyap_k_Grp.Controls.Add(this.ComboKantzSlope);
            this.lyap_k_Grp.Controls.Add(this.lyap_k_Lbl_window);
            this.lyap_k_Grp.Controls.Add(this.lyap_k_Num_maxiter);
            this.lyap_k_Grp.Controls.Add(this.lyap_k_Lbl_maxiter);
            this.lyap_k_Grp.Controls.Add(this.lyap_k_Lbl_scales);
            this.lyap_k_Grp.Controls.Add(this.lyap_k_Num_window);
            this.lyap_k_Grp.Controls.Add(this.lyap_k_Num_scales);
            this.lyap_k_Grp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lyap_k_Grp.Location = new System.Drawing.Point(6, 365);
            this.lyap_k_Grp.Name = "lyap_k_Grp";
            this.lyap_k_Grp.Size = new System.Drawing.Size(301, 134);
            this.lyap_k_Grp.TabIndex = 20;
            this.lyap_k_Grp.TabStop = false;
            // 
            // ComboKantzSlope
            // 
            this.ComboKantzSlope.FormattingEnabled = true;
            this.ComboKantzSlope.Location = new System.Drawing.Point(118, 101);
            this.ComboKantzSlope.Name = "ComboKantzSlope";
            this.ComboKantzSlope.Size = new System.Drawing.Size(177, 21);
            this.ComboKantzSlope.TabIndex = 30;
            // 
            // lyap_k_Lbl_window
            // 
            this.lyap_k_Lbl_window.AutoSize = true;
            this.lyap_k_Lbl_window.Location = new System.Drawing.Point(104, 52);
            this.lyap_k_Lbl_window.Name = "lyap_k_Lbl_window";
            this.lyap_k_Lbl_window.Size = new System.Drawing.Size(85, 13);
            this.lyap_k_Lbl_window.TabIndex = 21;
            this.lyap_k_Lbl_window.Text = "Размер окна:";
            // 
            // lyap_k_Num_maxiter
            // 
            this.lyap_k_Num_maxiter.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lyap_k_Num_maxiter.Location = new System.Drawing.Point(195, 20);
            this.lyap_k_Num_maxiter.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.lyap_k_Num_maxiter.Name = "lyap_k_Num_maxiter";
            this.lyap_k_Num_maxiter.Size = new System.Drawing.Size(100, 21);
            this.lyap_k_Num_maxiter.TabIndex = 19;
            this.lyap_k_Num_maxiter.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.lyap_k_Num_maxiter.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lyap_k_Lbl_maxiter
            // 
            this.lyap_k_Lbl_maxiter.AutoSize = true;
            this.lyap_k_Lbl_maxiter.Location = new System.Drawing.Point(50, 23);
            this.lyap_k_Lbl_maxiter.Name = "lyap_k_Lbl_maxiter";
            this.lyap_k_Lbl_maxiter.Size = new System.Drawing.Size(139, 13);
            this.lyap_k_Lbl_maxiter.TabIndex = 18;
            this.lyap_k_Lbl_maxiter.Text = "Количество итераций:";
            // 
            // lyap_k_Lbl_scales
            // 
            this.lyap_k_Lbl_scales.AutoSize = true;
            this.lyap_k_Lbl_scales.Location = new System.Drawing.Point(43, 76);
            this.lyap_k_Lbl_scales.Name = "lyap_k_Lbl_scales";
            this.lyap_k_Lbl_scales.Size = new System.Drawing.Size(146, 13);
            this.lyap_k_Lbl_scales.TabIndex = 16;
            this.lyap_k_Lbl_scales.Text = "Количество масштабов:";
            // 
            // lyap_k_Num_window
            // 
            this.lyap_k_Num_window.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lyap_k_Num_window.Location = new System.Drawing.Point(195, 47);
            this.lyap_k_Num_window.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.lyap_k_Num_window.Name = "lyap_k_Num_window";
            this.lyap_k_Num_window.Size = new System.Drawing.Size(100, 21);
            this.lyap_k_Num_window.TabIndex = 20;
            this.lyap_k_Num_window.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // lyap_k_Num_scales
            // 
            this.lyap_k_Num_scales.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.lyap_k_Num_scales.Location = new System.Drawing.Point(195, 74);
            this.lyap_k_Num_scales.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.lyap_k_Num_scales.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lyap_k_Num_scales.Name = "lyap_k_Num_scales";
            this.lyap_k_Num_scales.Size = new System.Drawing.Size(100, 21);
            this.lyap_k_Num_scales.TabIndex = 12;
            this.lyap_k_Num_scales.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.lyap_k_Num_scales.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lyap_r_Grp
            // 
            this.lyap_r_Grp.Controls.Add(this.rosStepsNum);
            this.lyap_r_Grp.Controls.Add(this.rosStepsLbl);
            this.lyap_r_Grp.Controls.Add(this.rosDistanceLbl);
            this.lyap_r_Grp.Controls.Add(this.rosDistanceNum);
            this.lyap_r_Grp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lyap_r_Grp.Location = new System.Drawing.Point(6, 261);
            this.lyap_r_Grp.Name = "lyap_r_Grp";
            this.lyap_r_Grp.Size = new System.Drawing.Size(301, 84);
            this.lyap_r_Grp.TabIndex = 20;
            this.lyap_r_Grp.TabStop = false;
            // 
            // rosStepsNum
            // 
            this.rosStepsNum.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.rosStepsNum.Location = new System.Drawing.Point(192, 24);
            this.rosStepsNum.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.rosStepsNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rosStepsNum.Name = "rosStepsNum";
            this.rosStepsNum.Size = new System.Drawing.Size(100, 21);
            this.rosStepsNum.TabIndex = 19;
            this.rosStepsNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.rosStepsNum.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // rosStepsLbl
            // 
            this.rosStepsLbl.AutoSize = true;
            this.rosStepsLbl.Location = new System.Drawing.Point(47, 30);
            this.rosStepsLbl.Name = "rosStepsLbl";
            this.rosStepsLbl.Size = new System.Drawing.Size(139, 13);
            this.rosStepsLbl.TabIndex = 18;
            this.rosStepsLbl.Text = "Количество итераций:";
            // 
            // rosDistanceLbl
            // 
            this.rosDistanceLbl.AutoSize = true;
            this.rosDistanceLbl.Location = new System.Drawing.Point(101, 53);
            this.rosDistanceLbl.Name = "rosDistanceLbl";
            this.rosDistanceLbl.Size = new System.Drawing.Size(85, 13);
            this.rosDistanceLbl.TabIndex = 16;
            this.rosDistanceLbl.Text = "Размер окна:";
            // 
            // rosDistanceNum
            // 
            this.rosDistanceNum.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.rosDistanceNum.Location = new System.Drawing.Point(192, 51);
            this.rosDistanceNum.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.rosDistanceNum.Name = "rosDistanceNum";
            this.rosDistanceNum.Size = new System.Drawing.Size(100, 21);
            this.rosDistanceNum.TabIndex = 12;
            this.rosDistanceNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Максимальный масштаб:";
            // 
            // scaleMaxNum
            // 
            this.scaleMaxNum.DecimalPlaces = 9;
            this.scaleMaxNum.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scaleMaxNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.scaleMaxNum.Location = new System.Drawing.Point(198, 107);
            this.scaleMaxNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.scaleMaxNum.Name = "scaleMaxNum";
            this.scaleMaxNum.Size = new System.Drawing.Size(100, 21);
            this.scaleMaxNum.TabIndex = 11;
            this.scaleMaxNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.scaleMaxNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(145, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Минимальный масштаб:";
            // 
            // lyap_w_Grp
            // 
            this.lyap_w_Grp.Controls.Add(this.stepNum);
            this.lyap_w_Grp.Controls.Add(this.stepLbl);
            this.lyap_w_Grp.Controls.Add(this.label5);
            this.lyap_w_Grp.Controls.Add(this.evolveStepsNum);
            this.lyap_w_Grp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lyap_w_Grp.Location = new System.Drawing.Point(6, 158);
            this.lyap_w_Grp.Name = "lyap_w_Grp";
            this.lyap_w_Grp.Size = new System.Drawing.Size(301, 86);
            this.lyap_w_Grp.TabIndex = 12;
            this.lyap_w_Grp.TabStop = false;
            // 
            // stepNum
            // 
            this.stepNum.DecimalPlaces = 7;
            this.stepNum.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stepNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.stepNum.Location = new System.Drawing.Point(192, 22);
            this.stepNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.stepNum.Name = "stepNum";
            this.stepNum.Size = new System.Drawing.Size(100, 21);
            this.stepNum.TabIndex = 19;
            this.stepNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.stepNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // stepLbl
            // 
            this.stepLbl.AutoSize = true;
            this.stepLbl.Location = new System.Drawing.Point(80, 24);
            this.stepLbl.Name = "stepLbl";
            this.stepLbl.Size = new System.Drawing.Size(106, 13);
            this.stepLbl.TabIndex = 18;
            this.stepLbl.Text = "Шаг по времени:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Кол-во шагов эволюции:";
            // 
            // evolveStepsNum
            // 
            this.evolveStepsNum.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.evolveStepsNum.Location = new System.Drawing.Point(192, 49);
            this.evolveStepsNum.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.evolveStepsNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.evolveStepsNum.Name = "evolveStepsNum";
            this.evolveStepsNum.Size = new System.Drawing.Size(100, 21);
            this.evolveStepsNum.TabIndex = 12;
            this.evolveStepsNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.evolveStepsNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // scaleMinNum
            // 
            this.scaleMinNum.DecimalPlaces = 9;
            this.scaleMinNum.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scaleMinNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.scaleMinNum.Location = new System.Drawing.Point(198, 80);
            this.scaleMinNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.scaleMinNum.Name = "scaleMinNum";
            this.scaleMinNum.Size = new System.Drawing.Size(100, 21);
            this.scaleMinNum.TabIndex = 10;
            this.scaleMinNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.scaleMinNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            // 
            // tauNum
            // 
            this.tauNum.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.tauNum.Location = new System.Drawing.Point(198, 53);
            this.tauNum.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.tauNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tauNum.Name = "tauNum";
            this.tauNum.Size = new System.Drawing.Size(100, 21);
            this.tauNum.TabIndex = 9;
            this.tauNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.tauNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // dimLbl
            // 
            this.dimLbl.AutoSize = true;
            this.dimLbl.Location = new System.Drawing.Point(45, 28);
            this.dimLbl.Name = "dimLbl";
            this.dimLbl.Size = new System.Drawing.Size(147, 13);
            this.dimLbl.TabIndex = 6;
            this.dimLbl.Text = "Размерность вложения:";
            // 
            // tauLbl
            // 
            this.tauLbl.AutoSize = true;
            this.tauLbl.Location = new System.Drawing.Point(28, 55);
            this.tauLbl.Name = "tauLbl";
            this.tauLbl.Size = new System.Drawing.Size(164, 13);
            this.tauLbl.TabIndex = 7;
            this.tauLbl.Text = "Задержка реконструкции:";
            // 
            // dimNum
            // 
            this.dimNum.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dimNum.Location = new System.Drawing.Point(198, 26);
            this.dimNum.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.dimNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dimNum.Name = "dimNum";
            this.dimNum.Size = new System.Drawing.Size(100, 21);
            this.dimNum.TabIndex = 8;
            this.dimNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.dimNum.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // sourcePrefGBox
            // 
            this.sourcePrefGBox.Controls.Add(this.sourceStepLbl);
            this.sourcePrefGBox.Controls.Add(this.sourceStepTxt);
            this.sourcePrefGBox.Controls.Add(this.useTimeCheckbox);
            this.sourcePrefGBox.Controls.Add(this.label7);
            this.sourcePrefGBox.Controls.Add(this.label6);
            this.sourcePrefGBox.Controls.Add(this.endPointNum);
            this.sourcePrefGBox.Controls.Add(this.startPointNum);
            this.sourcePrefGBox.Controls.Add(this.label2);
            this.sourcePrefGBox.Controls.Add(this.label1);
            this.sourcePrefGBox.Controls.Add(this.pointsNum);
            this.sourcePrefGBox.Controls.Add(this.sourceColumnNum);
            this.sourcePrefGBox.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sourcePrefGBox.Location = new System.Drawing.Point(912, 129);
            this.sourcePrefGBox.Name = "sourcePrefGBox";
            this.sourcePrefGBox.Size = new System.Drawing.Size(236, 202);
            this.sourcePrefGBox.TabIndex = 19;
            this.sourcePrefGBox.TabStop = false;
            this.sourcePrefGBox.Text = "Настройки сигнала";
            // 
            // sourceStepLbl
            // 
            this.sourceStepLbl.AutoSize = true;
            this.sourceStepLbl.Location = new System.Drawing.Point(45, 51);
            this.sourceStepLbl.Name = "sourceStepLbl";
            this.sourceStepLbl.Size = new System.Drawing.Size(84, 13);
            this.sourceStepLbl.TabIndex = 24;
            this.sourceStepLbl.Text = "Шаг данных:";
            // 
            // sourceStepTxt
            // 
            this.sourceStepTxt.BackColor = System.Drawing.Color.Khaki;
            this.sourceStepTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sourceStepTxt.Location = new System.Drawing.Point(135, 49);
            this.sourceStepTxt.Name = "sourceStepTxt";
            this.sourceStepTxt.ReadOnly = true;
            this.sourceStepTxt.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.sourceStepTxt.ShowSelectionMargin = true;
            this.sourceStepTxt.Size = new System.Drawing.Size(80, 21);
            this.sourceStepTxt.TabIndex = 23;
            this.sourceStepTxt.Text = "";
            // 
            // useTimeCheckbox
            // 
            this.useTimeCheckbox.AutoSize = true;
            this.useTimeCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.useTimeCheckbox.Location = new System.Drawing.Point(6, 26);
            this.useTimeCheckbox.Name = "useTimeCheckbox";
            this.useTimeCheckbox.Size = new System.Drawing.Size(144, 17);
            this.useTimeCheckbox.TabIndex = 3;
            this.useTimeCheckbox.Text = "1-й столбец - время";
            this.useTimeCheckbox.UseVisualStyleBackColor = true;
            this.useTimeCheckbox.CheckedChanged += new System.EventHandler(this.useTimeCheckbox_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Конечная точка:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Начальная точка:";
            // 
            // endPointNum
            // 
            this.endPointNum.Location = new System.Drawing.Point(135, 134);
            this.endPointNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.endPointNum.Name = "endPointNum";
            this.endPointNum.Size = new System.Drawing.Size(80, 21);
            this.endPointNum.TabIndex = 6;
            this.endPointNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.endPointNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // startPointNum
            // 
            this.startPointNum.Location = new System.Drawing.Point(135, 105);
            this.startPointNum.Name = "startPointNum";
            this.startPointNum.Size = new System.Drawing.Size(80, 21);
            this.startPointNum.TabIndex = 5;
            this.startPointNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Каждые N точек:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Колонка с данными:";
            // 
            // pointsNum
            // 
            this.pointsNum.Location = new System.Drawing.Point(135, 163);
            this.pointsNum.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.pointsNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pointsNum.Name = "pointsNum";
            this.pointsNum.Size = new System.Drawing.Size(80, 21);
            this.pointsNum.TabIndex = 7;
            this.pointsNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.pointsNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pointsNum.ValueChanged += new System.EventHandler(this.pointsNum_ValueChanged);
            // 
            // sourceColumnNum
            // 
            this.sourceColumnNum.Location = new System.Drawing.Point(135, 76);
            this.sourceColumnNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sourceColumnNum.Name = "sourceColumnNum";
            this.sourceColumnNum.Size = new System.Drawing.Size(80, 21);
            this.sourceColumnNum.TabIndex = 4;
            this.sourceColumnNum.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.sourceColumnNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // mainForm
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1160, 635);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.fileNameLbl);
            this.Controls.Add(this.sourcePrefGBox);
            this.Controls.Add(this.openFileBtn);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.Text = "Signal Analyser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.poincareMapPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalPBox)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.waveletPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fourierPBox)).EndInit();
            this.waveletGroup.ResumeLayout(false);
            this.waveletGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wav_dtNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wav_endFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wav_startFreq)).EndInit();
            this.fourierGroup.ResumeLayout(false);
            this.fourierGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fourier_dtNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fourierEndFreqNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fourierStartFreqNum)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.lyap_calc_Grp.ResumeLayout(false);
            this.lyap_calc_Grp.PerformLayout();
            this.leInTimeGroup.ResumeLayout(false);
            this.leInTimeGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lyapunovPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyapStartNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyapEndNum)).EndInit();
            this.lyap_k_Grp.ResumeLayout(false);
            this.lyap_k_Grp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lyap_k_Num_maxiter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyap_k_Num_window)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lyap_k_Num_scales)).EndInit();
            this.lyap_r_Grp.ResumeLayout(false);
            this.lyap_r_Grp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rosStepsNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rosDistanceNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleMaxNum)).EndInit();
            this.lyap_w_Grp.ResumeLayout(false);
            this.lyap_w_Grp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.evolveStepsNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleMinNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tauNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dimNum)).EndInit();
            this.sourcePrefGBox.ResumeLayout(false);
            this.sourcePrefGBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endPointNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startPointNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pointsNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceColumnNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button openFileBtn;
        private PictureBox poincareMapPBox;
        private Button plotBtn;
        private PictureBox signalPBox;
        private Label fileNameLbl;
        private Button saveBtn;
        private Label sigPlotLbl;
        private Label phasePortraitLbl;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private GroupBox fourierGroup;
        private NumericUpDown fourierEndFreqNum;
        private NumericUpDown fourierStartFreqNum;
        private CheckBox fourierCheckbox;
        private GroupBox sourcePrefGBox;
        private CheckBox useTimeCheckbox;
        private Label label7;
        private Label label6;
        private NumericUpDown endPointNum;
        private NumericUpDown startPointNum;
        private Label label2;
        private Label label1;
        private NumericUpDown pointsNum;
        private NumericUpDown sourceColumnNum;
        private TabPage tabPage2;
        private GroupBox lyap_w_Grp;
        private Label label5;
        private NumericUpDown evolveStepsNum;
        private Label label4;
        private NumericUpDown scaleMaxNum;
        private Label label3;
        private NumericUpDown scaleMinNum;
        private RichTextBox resultText;
        private Button startBtn;
        private Label dimLbl;
        private NumericUpDown dimNum;
        private NumericUpDown tauNum;
        private Label tauLbl;
        private Label label9;
        private Label label8;
        private PictureBox lyapunovPBox;
        private GroupBox waveletGroup;
        private Label waveletNameLbl;
        private CheckBox waveletCheckbox;
        private PictureBox fourierPBox;
        private Label label12;
        private PictureBox waveletPBox;
        private Label label13;
        private GroupBox leInTimeGroup;
        private Button lyapunovRedrawBtn;
        private Label label17;
        private NumericUpDown lyapStartNum;
        private NumericUpDown lyapEndNum;
        private Label label18;
        private NumericUpDown wav_endFreq;
        private NumericUpDown wav_startFreq;
        private Label label15;
        private Label label14;
        private Label label16;
        private NumericUpDown stepNum;
        private Label stepLbl;
        private GroupBox lyap_r_Grp;
        private NumericUpDown rosStepsNum;
        private Label rosStepsLbl;
        private Label rosDistanceLbl;
        private NumericUpDown rosDistanceNum;
        private GroupBox lyap_calc_Grp;
        private RadioButton lyap_calc_Rad_rosenstein;
        private RadioButton lyap_calc_Rad_wolf;
        private Label sourceStepLbl;
        private RichTextBox sourceStepTxt;
        private GroupBox lyap_k_Grp;
        private NumericUpDown lyap_k_Num_maxiter;
        private Label lyap_k_Lbl_maxiter;
        private Label lyap_k_Lbl_scales;
        private NumericUpDown lyap_k_Num_scales;
        private Label lyap_k_Lbl_window;
        private NumericUpDown lyap_k_Num_window;
        private RadioButton lyap_calc_Rad_kantz;
        private NumericUpDown fourier_dtNum;
        private CheckBox fourier_logCheckbox;
        private NumericUpDown wav_dtNum;
        private Label label11;
        private ComboBox wav_nameCbox;
        private ComboBox ComboKantzSlope;
        private Label label19;
        private Label label10;
        private ComboBox CboxColorMap;
    }
}


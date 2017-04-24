using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MathLib.DrawEngine.Charts;
using MathLib.IO;
using MathLib.MathMethods.Lyapunov;
using System.Globalization;

namespace TimeSeriesAnalysis {
    public partial class mainForm : Form {

        private const string emptyFileMsg = "Select file first";

        private Routines routines = new Routines();

        public mainForm() {
            InitializeComponent();
        }

        //File open and read
        private void openFileBtn_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files|*.*|Time series data|*.dat *.txt *.csv";
            openFileDialog.ShowDialog();
            string fName = openFileDialog.FileName;
            if (fName.Equals("")) {
                return;
            }

            cleanUp();

            try {
                routines.sourceData = DataReader.readTimeSeries(fName);
            }
            catch (ArgumentException ex) {
                MessageBox.Show("Unable to read file file:" + ex.Message);
                return;
            }

            fillUiWithData();
        }

        //Perform calculation in a separate thread
        private void startBtn_Click(object sender, EventArgs e) {
            if (!refreshTimeSeries()) {
                return;
            }

            resultText.Text = "CALCULATING...";
            resultText.BackColor = Color.Red;

            Thread newThread = new Thread(calculateLyapunovExponent);
            newThread.Start();
        }

        private bool refreshTimeSeries() {
            if (routines.sourceData == null) {
                MessageBox.Show(emptyFileMsg);
                return false;
            }
            routines.sourceData.SetTimeSeries(
                (int)sourceColumnNum.Value - 1,
                (int)startPointNum.Value,
                (int)endPointNum.Value,
                (int)pointsNum.Value,
                useTimeCheckbox.Checked
            );
            return true;
        }

        private void saveBtn_Click(object sender, EventArgs e) {
            if (routines.sourceData == null) {
                MessageBox.Show(emptyFileMsg);
                return;
            }

            string fName = routines.sourceData.folder + "\\" + routines.sourceData.fileName + "_rez" + "\\" + routines.sourceData.fileName;

            if (!Directory.Exists(routines.sourceData.folder + "\\" + routines.sourceData.fileName + "_rez")) {
                Directory.CreateDirectory(routines.sourceData.folder + "\\" + routines.sourceData.fileName + "_rez");
            }

            if (signalPBox.Image != null && poincareMapPBox.Image != null) {
                signalPBox.Image.Save(fName + "_plot.png", ImageFormat.Png);
                poincareMapPBox.Image.Save(fName + "_poincare.png", ImageFormat.Png);
                DataWriter.CreateDataFile(fName + "_signal", routines.sourceData.GetTimeSeriesString(false));
            }

            if (fourierPBox.Image != null) {
                fourierPBox.Image.Save(fName + "_fourier.png", ImageFormat.Png);
            }

            if (waveletPBox.Image != null) {
                waveletPBox.Image.Save(fName + "_wavelet.png", ImageFormat.Png);
            }

            if (routines.lyapunov != null) {
                DataWriter.CreateDataFile(fName + "_lyapunov.txt", routines.lyapunov.GetInfoFull());
            }

            if (lyapunovPBox.Image != null) {
                lyapunovPBox.Image.Save(fName + "_lyapunovInTime.png", ImageFormat.Png);
            }
        }

        private void fillUiWithData() {
            fileNameLbl.Text = routines.sourceData.GetInfo();

            sourceColumnNum.Maximum = routines.sourceData.columnsCount;
            sourceColumnNum.Minimum = 1;

            startPointNum.Maximum = routines.sourceData.timeSeriesLength - 1;

            endPointNum.Maximum = routines.sourceData.timeSeriesLength;
            endPointNum.Value = routines.sourceData.timeSeriesLength;
        }


        #region "CHARTS"

        private void plotBtn_Click(object sender, EventArgs e) {
            if (!refreshTimeSeries()) {
                return;
            }

            routines.deleteTempFiles();

            signalPBox.Image = routines
                .GetSignalPlot(signalPBox.Size, 1, useTimeCheckbox.Checked, (int)startPointNum.Value, (int)endPointNum.Value)
                .Plot();

            poincareMapPBox.Image = routines
                .GetPoincarePlot(poincareMapPBox.Size, 1)
                .Plot();

            if (fourierCheckbox.Checked == true) {
                fourierPBox.Image = null;
                try{
                    fourierPBox.Image = routines.GetFourierPlot(signalPBox.Size, 1, GetDoubleFromUI(fourierStartFreqNum), GetDoubleFromUI(fourierEndFreqNum), GetDoubleFromUI(fourier_dtNum), Convert.ToInt32(fourier_logCheckbox.Checked))
                        .Plot();

                } catch (Exception ex) {
                    MessageBox.Show("Не удалось построить спект мощности Фурье:\n" + ex.Message);
                }
            }

            if (waveletCheckbox.Checked == true) {
                waveletPBox.Image = null;
                double tStart = routines.sourceData.TimeSeries.PointMin.X;
                double tEnd = routines.sourceData.TimeSeries.PointMax.X;

                try {
                    routines.BuildWavelet(wav_nameCbox.Text, tStart, tEnd, GetDoubleFromUI(wav_startFreq), GetDoubleFromUI(wav_endFreq), GetDoubleFromUI(wav_dtNum), CboxColorMap.Text);
                    GetImageFromFile(waveletPBox, "wavelet.tmp");
                }
                catch (Exception ex) {
                    MessageBox.Show("Не удалось построить вейвлет:\n" + ex.Message);
                }
            }
        }

        private void lyapunovRedrawBtn_Click(object sender, EventArgs e) {
            try {

                string res;
                if (routines.lyapunov is KantzMethod)
                    ((KantzMethod)routines.lyapunov).SetSlope(this.ComboKantzSlope.Text);

                lyapunovPBox.Image = routines.GetLyapunovPlot(lyapunovPBox.Size, 1, (int)lyapStartNum.Value, (int)lyapEndNum.Value, lyap_calc_Rad_wolf.Checked, out res).Plot();

                if (routines.lyapunov is KantzMethod || routines.lyapunov is RosensteinMethod)
                    resultText.Text = res;
            }
            catch (Exception ex) {
                MessageBox.Show("Error plotting Lyapunov slope: " + ex.Message); 
            }
        }

        private void poincareMapPBox_DoubleClick(object sender, EventArgs e) {
            if (poincareMapPBox.Image != null) {
                PreviewForm pf = new PreviewForm("Псевдосечение Пуанкаре");
                pf.Show();

                MapPlot pp = routines.GetPoincarePlot(pf.previewPBox.Size, 2);
                pf.previewPBox.Image = pp.Plot();
                pf.plotObject = pp;
            }
        }

        private void signalPBox_DoubleClick(object sender, EventArgs e) {
            if (signalPBox.Image != null) {
                PreviewForm pf = new PreviewForm("Сигнал");
                pf.Show();

                SignalPlot sp = routines.GetSignalPlot(pf.previewPBox.Size, 1, useTimeCheckbox.Checked, (int)startPointNum.Value, (int)endPointNum.Value);
                pf.previewPBox.Image = sp.Plot();
                pf.plotObject = sp;
            }
        }

        private void lyapunovPBox_DoubleClick(object sender, EventArgs e) {
            if (lyapunovPBox.Image != null) {
                PreviewForm pf = new PreviewForm("Показатель Ляпунова во времени");
                pf.Show();
                string tmp;

                PlotObject sp = routines.GetLyapunovPlot(pf.previewPBox.Size, 1, (int)lyapStartNum.Value, (int)lyapEndNum.Value, lyap_calc_Rad_wolf.Checked, out tmp);
                pf.previewPBox.Image = sp.Plot();
                pf.plotObject = sp;
            }
        }

        private void fourierPBox_DoubleClick(object sender, EventArgs e) {
            PreviewForm pf = new PreviewForm("Спектр мощности Фурье");
            pf.Show();

            SignalPlot sp = routines.GetFourierPlot(pf.previewPBox.Size, 1, GetDoubleFromUI(fourierStartFreqNum), GetDoubleFromUI(fourierEndFreqNum), GetDoubleFromUI(fourier_dtNum), Convert.ToInt32(fourier_logCheckbox.Checked));
            pf.previewPBox.Image = sp.Plot();
            pf.plotObject = sp;
        }

        private void waveletPBox_DoubleClick(object sender, EventArgs e) {
            PreviewForm pf = new PreviewForm("Вейвлет");
            pf.Show();
            GetImageFromFile(pf.previewPBox, "wavelet.tmp");
        }

        private void GetImageFromFile(PictureBox pb, string fileName) {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            pb.Image = Image.FromStream(fs);
            fs.Close();

            pb.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        #endregion


        #region lyapunovRelated

        private void calculateLyapunovExponent() {

            int dim = (int)dimNum.Value;
            int tau = (int)tauNum.Value;
            double scaleMin = GetDoubleFromUI(scaleMinNum);

            if (lyap_calc_Rad_wolf.Checked)
                routines.lyapunov = new WolfMethod(
                    routines.sourceData.TimeSeries.ValY,
                    dim,
                    tau,
                    GetDoubleFromUI(stepNum),
                    scaleMin,
                    GetDoubleFromUI(scaleMaxNum), 
                    (int)evolveStepsNum.Value
                );

            if (lyap_calc_Rad_rosenstein.Checked)
                routines.lyapunov = new RosensteinMethod(
                    routines.sourceData.TimeSeries.ValY,
                    dim,
                    tau, 
                    (int)rosStepsNum.Value, 
                    (int)rosDistanceNum.Value,
                    scaleMin
                );

            if (lyap_calc_Rad_kantz.Checked)
                routines.lyapunov = new KantzMethod(
                    routines.sourceData.TimeSeries.ValY,
                    dim,
                    (int)lyap_k_Num_dmax.Value,
                    tau,
                    (int)lyap_k_Num_maxiter.Value,
                    (int)lyap_k_Num_window.Value,
                    scaleMin,
                    GetDoubleFromUI(scaleMaxNum),
                    (int)lyap_k_Num_scales.Value
                );

            try {
                routines.lyapunov.Calculate();
                MethodInvoker mi = new MethodInvoker(this.setLyapunovResult);
                this.BeginInvoke(mi);
            }
            catch (Exception ex) {
                MessageBox.Show("Unable to calculate LE:\n" + ex.Message);
                resultText.Text = "Error";
            }
        }


        private void setLyapunovResult() {
            resultText.BackColor = Color.Khaki;
            string result = "";

            if (routines.lyapunov is WolfMethod) {
                result = string.Format("{0:F5}", ((WolfMethod)routines.lyapunov).rezult);
                resultText.Text = result;
            }

            if (routines.lyapunov is KantzMethod) {
                this.ComboKantzSlope.Items.Clear();
                string[] items = new string[((KantzMethod)routines.lyapunov).SlopesList.Count];
                ((KantzMethod)routines.lyapunov).SlopesList.Keys.CopyTo(items, 0);
                this.ComboKantzSlope.Items.AddRange(items);
                this.ComboKantzSlope.SelectedIndex = 0;
                ((KantzMethod)routines.lyapunov).SetSlope(this.ComboKantzSlope.Text);
            }

            if (routines.lyapunov.slope.Length > 1) {
                resultText.Text = routines.lyapunov.GetInfoShort();
                lyapEndNum.Value = routines.lyapunov.slope.Length - 1;
                try {
                    lyapunovPBox.Image = routines.GetLyapunovPlot(lyapunovPBox.Size, 1, (int)lyapStartNum.Value, (int)lyapEndNum.Value, lyap_calc_Rad_wolf.Checked, out result).Plot();
                }
                catch (Exception ex) {
                    MessageBox.Show("Error plotting Lyapunov slope: " + ex.Message);
                    result = "No Value";
                }
            }
            else {
                result = "No Value";
            }
               

            if (routines.lyapunov is KantzMethod || routines.lyapunov is RosensteinMethod)
                resultText.Text = result;
        }

        #endregion


        private void useTimeCheckbox_CheckedChanged(object sender, EventArgs e) {
            if (this.useTimeCheckbox.Checked) {
                refreshTimeSeries();
                sourceStepTxt.Text = string.Format(CultureInfo.InvariantCulture, "{0:F8}", routines.sourceData.Step);
            }
            else {
                sourceStepTxt.Text = "";
            }
        }


        private void cleanUp() {
            routines.sourceData = null;
            signalPBox.Image = null;
            poincareMapPBox.Image = null;
            waveletPBox.Image = null;
            fourierPBox.Image = null;
            lyapunovPBox.Image = null;
            routines.lyapunov = null;
            routines.deleteTempFiles();
        }


        private void mainForm_FormClosing(object sender, FormClosingEventArgs e) {
            cleanUp();
        }


        private void pointsNum_ValueChanged(object sender, EventArgs e) {
            if (this.useTimeCheckbox.Checked) {
                refreshTimeSeries();
                sourceStepTxt.Text = string.Format(CultureInfo.InvariantCulture, "{0:F8}", routines.sourceData.Step);
            }
        }


        private double GetDoubleFromUI(NumericUpDown field)
        {
            return Convert.ToDouble(field.Value, CultureInfo.InvariantCulture);
        }
    }
}

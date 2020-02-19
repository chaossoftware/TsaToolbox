using MathLib.NumericalMethods.Lyapunov;
using System;
using System.Windows;
using System.Windows.Media;

namespace TimeSeriesToolbox
{
    internal class LyapunovExponents
    {
        private readonly MainWindow _window;

        private LyapunovMethod method;

        public LyapunovExponents(MainWindow window)
        {
            _window = window;
        }


        public void Calculate(double[] series)
        {
            SetLyapunovMethod(series);

            try
            {
                _window.Dispatcher.Invoke(() => _window.le_resultTbox.Background = Brushes.OrangeRed);
                _window.Dispatcher.Invoke(() => _window.le_resultTbox.Text = StringData.Calculating);

                method.Calculate();
                //_window.Dispatcher.Invoke(() => { SetLyapunovResult(); });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to calculate LE:\n" + ex);
                _window.le_resultTbox.Text = "Error";
            }
        }

        private void SetLyapunovMethod(double[] series)
        {
            var dim = _window.le_eDimTbox.ReadInt();
            var tau = _window.le_tauTbox.ReadInt();
            var scaleMin = _window.le_epsMinTbox.ReadDouble();

            if (_window.le_wolfRad.IsChecked.Value)
            {
                var dt = _window.le_w_dtTbox.ReadDouble();
                var scaleMax = _window.le_w_epsMaxTbox.ReadDouble();
                var evolSteps = _window.le_w_evolStepsTbox.ReadInt();

                method = new WolfMethod(series, dim, tau, dt, scaleMin, scaleMax, evolSteps);
            }
            else if (_window.le_rosRad.IsChecked.Value)
            {
                var iter = _window.le_r_iterTbox.ReadInt();
                var window = _window.le_r_windowTbox.ReadInt();

                method = new RosensteinMethod(series, dim, tau, iter, window, scaleMin);
            }
            else if (_window.le_kantzRad.IsChecked.Value)
            {
                var iter = _window.le_k_iterTbox.ReadInt();
                var window = _window.le_k_windowTbox.ReadInt();
                var scaleMax = _window.le_k_epsMaxTbox.ReadDouble();
                var scales = _window.le_k_scalesTbox.ReadInt();

                method = new KantzMethod(series, dim, tau, iter, window, scaleMin, scaleMax, scales);
            }
            else if (_window.le_ssRad.IsChecked.Value)
            {
                var inverse = _window.le_ss_inverseCbox.IsChecked.Value;
                var scaleFactor = _window.le_ss_scaleFactorTbox.ReadDouble();
                var minNeigh = _window.le_ss_minNeighbTbox.ReadInt();

                method = new SanoSawadaMethod(series, dim, tau, series.Length, scaleMin, scaleFactor, minNeigh, inverse);
            }
        }

        private void SetLyapunovResult()
        {
            le_resultText.BackColor = Color.Khaki;
            string result = string.Empty;

            le_resultText.Text = routines.Lyapunov.GetResult();
            lyap_log_text.Text = routines.Lyapunov.ToString() + "\n\n" + routines.Lyapunov.Log.ToString();

            if (routines.Lyapunov is KantzMethod)
            {
                this.le_kantz_slopeCombo.Items.Clear();
                string[] items = new string[((KantzMethod)routines.Lyapunov).SlopesList.Count];
                ((KantzMethod)routines.Lyapunov).SlopesList.Keys.CopyTo(items, 0);
                this.le_kantz_slopeCombo.Items.AddRange(items);
                this.le_kantz_slopeCombo.SelectedIndex = 0;
                ((KantzMethod)routines.Lyapunov).SetSlope(this.le_kantz_slopeCombo.Text);
            }

            if (routines.Lyapunov.Slope.Length > 1)
            {
                le_pEndNum.Value = routines.Lyapunov.Slope.Length - 1;

                try
                {
                    if (!le_wolf_radio.Checked)
                    {
                        var leSectorEnd = Ext.SlopeChangePointIndex(routines.Lyapunov.Slope, 2, routines.Lyapunov.Slope.Amplitude.Y / 30);

                        if (leSectorEnd > 0)
                        {
                            le_pEndNum.Value = leSectorEnd;
                        }
                    }

                    result = routines.FillLyapunovChart(chartLyapunov, le_pStartNum.ToInt(), le_pEndNum.ToInt(), le_wolf_radio.Checked);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error plotting Lyapunov slope: " + ex.Message);
                    result = StringData.NoValue;
                }
            }
            else
            {
                result = StringData.NoValue;
            }

            if (routines.Lyapunov is KantzMethod || routines.Lyapunov is RosensteinMethod)
            {
                le_resultText.Text = result;
            }
        }
    }
}

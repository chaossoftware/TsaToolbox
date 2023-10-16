using ChaosSoft.Core.Data;
using MathWorks.MATLAB.NET.Arrays;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows;
using TsaToolbox.Models.Setups;

namespace TsaToolbox.Helpers;

internal class FreqAnalysis
{
    public static Bitmap GetWavelet(double[] yValues, double[] xValues, string tmpFileName, 
        WaveletSetup setup, double width, double height)
    {
        try
        {
            BuildWavelet(yValues, xValues, tmpFileName, setup, width, height);

            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var data = new BitmapImage();
            var stream = File.OpenRead(Path.Combine(dir, tmpFileName));

            data.BeginInit();
            data.CacheOption = BitmapCacheOption.OnLoad;
            data.StreamSource = stream;
            data.EndInit();
            stream.Close();
            stream.Dispose();
            data.Freeze();

            using MemoryStream outStream = new ();
            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create(data));
            enc.Save(outStream);
            return new Bitmap(outStream);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Unable to build {Properties.Resources.Wavelet}:\n" + ex.Message);
            return null;
        }
    }

    public static DataSeries GetFourier(double[] timeSeries, FftSetup setup)
    {
        int powOfTwo = (int)Math.Log(timeSeries.Length, 2);
        int newLength = (int)Math.Pow(2, powOfTwo);
        int skip = (timeSeries.Length - newLength) / 2;

        double[] signal = timeSeries.Skip(skip).Take(newLength).ToArray();

        // Shape the signal using a Hanning window
        var window = new FftSharp.Windows.Hanning();
        window.ApplyInPlace(signal);

        // Calculate the FFT as an array of complex numbers
        System.Numerics.Complex[] spectrum = FftSharp.FFT.Forward(signal);

        double[] power = FftSharp.FFT.Power(spectrum);

        double multiplier = setup.UseRadians ? 2d * Math.PI : 1d;

        double fs = 1 / (2 * setup.Dt);
        double freqCoeff = multiplier * fs / power.Length;
        double[] freq = new double[power.Length];

        for (int i = 0; i < power.Length; i++)
        {
            freq[i] = freqCoeff * i;
        }

        var fourier = new DataSeries();

        for (int i = 0; i < power.Length; i++)
        {
            double x = freq[i];

            if (x >= setup.OmegaFrom && x <= setup.OmegaTo)
            {
                fourier.AddDataPoint(x, power[i]);
            }
        }

        return fourier;
    }

    private static void BuildWavelet(double[] yValues, double[] xValues, string tmpFileName, 
        WaveletSetup setup, double width, double height)
    {
        MatlabEngine.MatlabBridge matlabBridge = new();

        MWNumericArray mwSignalArray = yValues;
        MWNumericArray mwTimeArray = xValues;
        MWCharArray mwWname = setup.Family.ToString().ToLowerInvariant();
        MWCharArray mwFolder = string.Empty;
        MWCharArray mwfileName = tmpFileName;
        MWCharArray mwColMap = setup.ColorMap.ToString().ToLowerInvariant();
        MWNumericArray omegaRange = new double[] { setup.OmegaFrom, setup.OmegaTo };
        MWNumericArray picSize = new double[] { width, height };
        int rad = setup.UseRadians ? 1 : 0;

        matlabBridge.Build2DWavelet(
            mwSignalArray, mwTimeArray, mwWname, omegaRange, 10, rad, mwColMap, 1, mwFolder, mwfileName, picSize);
    }
}

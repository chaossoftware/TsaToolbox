using MathWorks.MATLAB.NET.Arrays;

namespace ChaosSoft.MatlabIntegration
{
    public class Wavelet
    {
        public static void BuildWavelet(double[] yValues, double[] xValues, string tmpFileName, string wName, 
            double fStart, double fEnd, string colMap, double width, double height)
        {
            var matlabBridge = new MatlabEngine.MatlabBridge();

            var mwSignalArray = (MWNumericArray)yValues;
            var mwTimeArray = (MWNumericArray)xValues;
            var mwWname = (MWCharArray)wName;
            var mwFolder = (MWCharArray)string.Empty;
            var mwfileName = (MWCharArray)tmpFileName;
            var mwColMap = (MWCharArray)colMap;

            matlabBridge.Build2DWavelet(
                mwSignalArray, mwTimeArray, mwWname, fStart, fEnd, 10, mwFolder, mwfileName, mwColMap, width, height, 1);
        }
    }
}

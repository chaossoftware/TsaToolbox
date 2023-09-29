using MathWorks.MATLAB.NET.Arrays;

namespace ChaosSoft.MatlabIntegration
{
    public class Wavelet
    {
        public static void BuildWavelet(double[] yValues, double[] xValues, string tmpFileName, string wName, 
            double fStart, double fEnd, string colMap, bool inRadians, double width, double height)
        {
            var matlabBridge = new MatlabEngine.MatlabBridge();

            var mwSignalArray = (MWNumericArray)yValues;
            var mwTimeArray = (MWNumericArray)xValues;
            var mwWname = (MWCharArray)wName;
            var mwFolder = (MWCharArray)string.Empty;
            var mwfileName = (MWCharArray)tmpFileName;
            var mwColMap = (MWCharArray)colMap;
            var omegaRange = (MWNumericArray)new double[] { fStart, fEnd };
            var picSize = (MWNumericArray)new double[] { width, height };
            int rad = inRadians ? 1 : 0;

            matlabBridge.Build2DWavelet(
                mwSignalArray, mwTimeArray, mwWname, omegaRange, 10, rad, mwColMap, 1, mwFolder, mwfileName, picSize);
        }
    }
}

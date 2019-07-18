﻿using System;
using System.Globalization;
using System.Text;
using MathLib.IO;

namespace MathLib.MathMethods.Lyapunov
{
    public class WolfMethod : LyapunovMethod
    {
        private readonly int eDim;
        private readonly int tau;
        private readonly int evolv;
        private readonly double stepSize;
        private readonly double scaleMin;
        private readonly double scaleMax;

        // Pre-calculated variables
        private readonly double evMulStMulLog2;
        private readonly int tsLen;

        private double[] pt1;
        private double[] pt2; 

        private double zlyap;
        private double rezult;

        private int step;

        public WolfMethod(double[] timeSeries, int eDim, int tau, double stepSize, double scaleMin, double scaleMax, int evolv)
            : base(timeSeries)
        {
            this.eDim = eDim;
            this.tau = tau;
            this.stepSize = stepSize;
            this.scaleMin = scaleMin;
            this.scaleMax = scaleMax;
            this.evolv = evolv;

            pt1 = new double[this.eDim];
            pt2 = new double[this.eDim];

            evMulStMulLog2 = (double)this.evolv * this.stepSize * Math.Log(2d);
            tsLen = timeSeries.Length;
        }

        public override string GetInfo() =>
            new StringBuilder()
                .AppendLine($"m = {eDim}")
                .AppendLine($"τ = {tau}")
                .AppendLine($"Δt = {stepSize.ToString(NumFormat.Short, CultureInfo.InvariantCulture)}")
                .AppendLine($"min ε = {scaleMin.ToString(NumFormat.Short, CultureInfo.InvariantCulture)}")
                .AppendLine($"max ε = {scaleMax.ToString(NumFormat.Short, CultureInfo.InvariantCulture)}")
                .AppendLine($"Evolution steps: {evolv}")
                .ToString();

        public override string GetInfoFull() =>
            new StringBuilder()
                .AppendLine($"Embedding dimension: {eDim}")
                .AppendLine($"Reconstruction delay: {tau}")
                .AppendLine($"Step size: {stepSize.ToString(NumFormat.Short, CultureInfo.InvariantCulture)}")
                .AppendLine($"Min scale: {scaleMin.ToString(NumFormat.Short, CultureInfo.InvariantCulture)}")
                .AppendLine($"Max scale: {scaleMax.ToString(NumFormat.Short, CultureInfo.InvariantCulture)}")
                .AppendLine($"Evolution steps: {evolv}")
                .ToString();

        public override string GetResult() => rezult.ToString(NumFormat.Short, CultureInfo.InvariantCulture);

        public override void Calculate()
        {
            //ind points to fiducial trajectory
            //ind2 points to second trajectory
            //sum holds running exponent estimate sans i/time
            //its is total number of propagation steps

            //int ind = 0; //in fortran was 1
            int ind2 = 0;//in fortran was empty and defined later
            double sum = 0d;
            int its = 0;

            double dii = 0;//initialization absent in fortran

            //calculate useful size of datafile
            int dataPointsCount = TimeSeries.Length - eDim * tau - evolv;

            //find nearest neighbor to first data point
            double di = 1e38;

            //dont take point too close to fiducial point
            for (int i = 10; i < dataPointsCount; i++)
            {
                //compute separation between fiducial point and candidate
                double d = 0d;

                for (int j = 0; j < eDim; j++)
                {
                    d += Math.Pow(GetAttractorPoint(0, j) - GetAttractorPoint(i, j), 2);
                }

                d = Math.Sqrt(d);

                //store the best point so far but no closer than noise scale
                if (d >= scaleMin && d <= di)
                {
                    di = d;
                    ind2 = i;
                }
            }

            //Log.Append("Lyapunov exponent\tTotal Propagation Time\tDI\tInformation dimention\n");

            for(int ind = evolv; ind < dataPointsCount; ind += evolv)
            { 
                //get coordinates of evolved points
                for (int j = 0; j < eDim; j++)
                {
                    pt1[j] = GetAttractorPoint(ind, j);
                    pt2[j] = GetAttractorPoint(ind2 + evolv, j);
                }

                //compute final separation between pair, update exponent
                double df = 0d;

                for (int j = 0; j < eDim; j++)
                    df += Math.Pow(pt1[j] - pt2[j], 2);

                df = Math.Sqrt(df);

                its++;
                sum += Math.Log(df / di) / evMulStMulLog2;
                zlyap = sum / (double)its;

                //Log.AppendFormat("{0:F5}\t{1}\t{2:F5}\t{3:F5}\n", zlyap, Evolv * its, di, df);
                Slope.AddDataPoint(step++, zlyap);

                //look for replacement point
                //zmult is multiplier of scalMax when go to longer distances
                int indold = ind2;
                double zmult = 1d;
                double anglmx = 0.3;

                metka:

                double thmin = Math.PI;//3.14;

                //search over all points
                for (int i = 0; i < dataPointsCount; i++)
                {
                    //dont take points too close in time to fiducial point
                    int iii = Math.Abs(i - ind);

                    if (iii < 9)
                    {
                        continue;
                    }

                    //compute distance between fiducial point and candidate
                    double dnew = 0d;

                    for (int j = 0; j < eDim; j++)
                    {
                        dnew += Math.Pow(pt1[j] - GetAttractorPoint(i, j), 2);
                    }

                    dnew = Math.Sqrt(dnew);

                    //look further away than noise scale, closer than zmult*scalMax
                    if (dnew > zmult * scaleMax || dnew < scaleMin)
                    {
                        continue;
                    }

                    //find angular change old to new vector
                    double dot = 0d;

                    for (int j = 0; j < eDim; j++)
                    {
                        dot += (pt1[j] - GetAttractorPoint(i, j)) * (pt1[j] - pt2[j]);
                    }

                    double cth = Math.Abs(dot / (dnew * df));

                    cth = Math.Min(cth, 1d);

                    double th = Math.Acos(cth);

                    //save point with smallest angular change so far
                    if (th < thmin)
                    { 
                        thmin = th;
                        dii = dnew;
                        ind2 = i;
                    }
                }

                if (thmin > anglmx)
                {
                    //cant find a replacement - look at longer distances
                    zmult++;

                    if (zmult <= 5d)
                    {
                        goto metka;
                    }

                    //no replacement at 5*scale, double search angle, reset distance
                    zmult = 1d;
                    anglmx *= 2d;

                    if (anglmx < Math.PI /*3.14*/)
                    {
                        goto metka;
                    }

                    ind2 = indold + evolv;
                    dii = df;
                }

                di = dii;
            }

            rezult = zlyap;
        }

        ///<summary>Define delay coordinates with a statement function</summary>
        ///<returns>j-th component of i-th reconstructed attractor point</returns>
        private double GetAttractorPoint(int i, int j)
        {
            int point = i + j * tau;
            return point < tsLen ? TimeSeries[point] : 0;
        }
    }
}

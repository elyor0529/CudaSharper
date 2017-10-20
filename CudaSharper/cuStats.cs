﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CudaSharper
{
    public sealed class CuStats : IDisposable
    {
        private ICudaDevice CudaDeviceComponent { get; }
        private IntPtr PtrToUnmanagedClass { get; set; }

        public int DeviceId => CudaDeviceComponent.DeviceId;

        static CuStats()
        {
            CudaSettings.Load();
        }

        public CuStats(CudaDevice device)
        {
            CudaDeviceComponent = new CudaDevice(device.DeviceId, device.AllocationSize);
            PtrToUnmanagedClass = SafeNativeMethods.CreateStatClass(CudaDeviceComponent.DeviceId, CudaDeviceComponent.AllocationSize);
        }
        
        public ICudaResult<double> SampleStandardDeviation(float[] sample, float mean)
        {
            double result = 0;
            var error_code = SafeNativeMethods.SampleStandardDeviationFloat(
                PtrToUnmanagedClass,
                ref result,
                sample, sample.LongLength, mean);
            return new CudaResult<double>(error_code, result);
        }

        public ICudaResult<double> SampleStandardDeviation(float[] sample)
        {
            return SampleStandardDeviation(sample, sample.Average());
        }

        public ICudaResult<double> SampleStandardDeviation(double[] sample, double mean)
        {
            double result = 0;
            var error_code = SafeNativeMethods.SampleStandardDeviationDouble(
                PtrToUnmanagedClass,
                ref result,
                sample, sample.LongLength, mean);
            return new CudaResult<double>(error_code, result);
        }

        public ICudaResult<double> SampleStandardDeviation(double[] sample)
        {
            return SampleStandardDeviation(sample, sample.Average());
        }

        public ICudaResult<double> StandardDeviation(float[] sample, float mean)
        {
            double result = 0;
            var error_code = SafeNativeMethods.StandardDeviationFloat(
                PtrToUnmanagedClass,
                ref result,
                sample, sample.LongLength, mean);
            return new CudaResult<double>(error_code, result);
        }

        public ICudaResult<double> StandardDeviation(float[] sample)
        {
            return StandardDeviation(sample, sample.Average());
        }

        public ICudaResult<double> StandardDeviation(double[] sample, double mean)
        {
            double result = 0;
            var error_code = SafeNativeMethods.StandardDeviationDouble(
                PtrToUnmanagedClass,
                ref result,
                sample, sample.LongLength, mean);
            return new CudaResult<double>(error_code, result);
        }

        public ICudaResult<double> StandardDeviation(double[] sample)
        {
            return StandardDeviation(sample, sample.Average());
        }

        public ICudaResult<double> SampleCovariance(float[] x_array, float x_mean, float[] y_array, float y_mean)
        {
            double result = 0;
            var error_code = SafeNativeMethods.SampleCovarianceFloat(
                PtrToUnmanagedClass,
                ref result,
                x_array, x_mean,
                y_array, y_mean,
                x_array.LongLength);
            return new CudaResult<double>(error_code, result);
        }

        public ICudaResult<double> SampleCovariance(float[] x_array, float[] y_array)
        {
            return SampleCovariance(x_array, x_array.Average(), y_array, y_array.Average());
        }

        public ICudaResult<double> SampleCovariance(double[] x_array, double x_mean, double[] y_array, double y_mean)
        {
            double result = 0;
            var error_code = SafeNativeMethods.SampleCovarianceDouble(
                PtrToUnmanagedClass,
                ref result,
                x_array, x_mean,
                y_array, y_mean,
                x_array.LongLength);
            return new CudaResult<double>(error_code, result);
        }

        public ICudaResult<double> SampleCovariance(double[] x_array, double[] y_array)
        {
            return SampleCovariance(x_array, x_array.Average(), y_array, y_array.Average());
        }

        public ICudaResult<double> Covariance(double[] x_array, double x_mean, double[] y_array, double y_mean)
        {
            double result = 0;
            var error_code = SafeNativeMethods.CovarianceDouble(
                PtrToUnmanagedClass,
                ref result,
                x_array, x_mean,
                y_array, y_mean,
                x_array.LongLength);
            return new CudaResult<double>(error_code, result);
        }

        public ICudaResult<double> Covariance(float[] x_array, float x_mean, float[] y_array, float y_mean)
        {
            double result = 0;
            var error_code = SafeNativeMethods.CovarianceFloat(
                PtrToUnmanagedClass,
                ref result,
                x_array, x_mean,
                y_array, y_mean,
                x_array.LongLength);
            return new CudaResult<double>(error_code, result);
        }

        public ICudaResult<double> Covariance(double[] x_array, double[] y_array)
        {
            return Covariance(x_array, x_array.Average(), y_array, y_array.Average());
        }

        public ICudaResult<double> Covariance(float[] x_array, float[] y_array)
        {
            return Covariance(x_array, x_array.Average(), y_array, y_array.Average());
        }

        public ICudaResult<double> Correlation(float[] x_array, float x_mean, float[] y_array, float y_mean)
        {
            double result = 0;
            var error_code = SafeNativeMethods.PearsonCorrelationFloat(
                PtrToUnmanagedClass,
                ref result,
                x_array, x_mean,
                y_array, y_mean,
                x_array.LongLength);
            return new CudaResult<double>(error_code, result);
        }

        public ICudaResult<double> Correlation(float[] x_array, float[] y_array)
        {
            return Correlation(x_array, x_array.Average(), y_array, y_array.Average());
        }

        public ICudaResult<double> Correlation(double[] x_array, double x_mean, double[] y_array, double y_mean)
        {
            double result = 0;
            var error_code = SafeNativeMethods.PearsonCorrelationDouble(
                PtrToUnmanagedClass,
                ref result,
                x_array, x_mean,
                y_array, y_mean,
                x_array.LongLength);
            return new CudaResult<double>(error_code, result);
        }

        public ICudaResult<double> Correlation(double[] x_array, double[] y_array)
        {
            return Correlation(x_array, x_array.Average(), y_array, y_array.Average());
        }

        public ICudaResult<float[][]> CorrelationMatrix(float[][] sets_of_scalars)
        {
            var set_length = sets_of_scalars.LongLength;
            var C = new float[set_length][];
            var error_code = CudaError.Success;

            for (long i = 0; i < set_length; i++)
            {
                C[i] = new float[set_length];

                for (long j = 0; j < set_length; j++)
                {
                    var cov = Correlation(sets_of_scalars[i], sets_of_scalars[j]);
                    error_code = cov.Error;
                    C[i][j] = (float)cov.Result;
                }
            }

            return new CudaResult<float[][]>(error_code, C);
        }
        
        public ICudaResult<double[][]> CorrelationMatrix(double[][] sets_of_scalars)
        {
            var set_length = sets_of_scalars.LongLength;
            var C = new double[set_length][];
            var error_code = CudaError.Success;

            for (long i = 0; i < set_length; i++)
            {
                C[i] = new double[set_length];

                for (long j = 0; j < set_length; j++)
                {
                    var cov = Correlation(sets_of_scalars[i], sets_of_scalars[j]);
                    error_code = cov.Error;
                    C[i][j] = cov.Result;
                }
            }

            return new CudaResult<double[][]>(error_code, C);
        }

        public ICudaResult<float[][]> CovarianceMatrix(float[][] sets_of_scalars)
        {
            var set_length = sets_of_scalars.LongLength;
            var C = new float[set_length][];
            var error_code = CudaError.Success;

            for (long i = 0; i < set_length; i++)
            {
                C[i] = new float[set_length];

                for (long j = 0; j < set_length; j++)
                {
                    var cov = Covariance(sets_of_scalars[i], sets_of_scalars[j]);
                    error_code = cov.Error;
                    C[i][j] = (float)cov.Result;
                }
            }

            return new CudaResult<float[][]>(error_code, C);
        }

        public ICudaResult<double[][]> CovarianceMatrix(double[][] sets_of_scalars)
        {
            var set_length = sets_of_scalars.LongLength;
            var C = new double[set_length][];
            var error_code = CudaError.Success;

            for (long i = 0; i < set_length; i++)
            {
                C[i] = new double[set_length];

                for (long j = 0; j < set_length; j++)
                {
                    var cov = Covariance(sets_of_scalars[i], sets_of_scalars[j]);
                    error_code = cov.Error;
                    C[i][j] = cov.Result;
                }
            }

            return new CudaResult<double[][]>(error_code, C);
        }

        /// <summary>
        /// Calculates the Value-at-Risk for a portfolio.
        /// </summary>
        /// <param name="invested_amounts">An array (1xN matrix) of the amounts invested in each portfolio.</param>
        /// <param name="covariance_matrix">A covariance matrix (NxN matrix) of the portfolio for the given time period.</param>
        /// <param name="confidence_level">The confidence level. This should be in units of standard deviation of a normal distribution (e.g., 0.90 = 1.645).</param>
        /// <param name="time_period">The time period for measuring risk.</param>
        /// <returns>The Value-at-Risk. No units involved.</returns>
        public double VaR(float[] invested_amounts, float[][] covariance_matrix, double confidence_level, int time_period)
        {
            var cuArray = new CuArray(new CudaDevice(CudaDeviceComponent.DeviceId, CudaDeviceComponent.AllocationSize));

            var invested_amounts_horizontal = new float[][] { invested_amounts };
            var covariance_times_beta_horizontal = cuArray.Multiply(
                CUBLAS_OP.DO_NOT_TRANSPOSE, CUBLAS_OP.DO_NOT_TRANSPOSE,
                1,
                invested_amounts_horizontal,
                covariance_matrix,
                0);

            var covariance_times_beta_vertical = cuArray.Multiply(
                CUBLAS_OP.TRANSPOSE, CUBLAS_OP.DO_NOT_TRANSPOSE,
                1,
                covariance_times_beta_horizontal.Result,
                invested_amounts_horizontal,
                0);

            if(covariance_times_beta_vertical.Result.Length > 1 || covariance_times_beta_vertical.Result[0].Length > 1)
            {
                throw new ArgumentOutOfRangeException("The matrix given for Beta * CovMatrix * Beta^T was bigger than one.");
            }

            return Math.Sqrt(covariance_times_beta_vertical.Result[0][0]) * confidence_level * Math.Sqrt(time_period);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                if (PtrToUnmanagedClass != IntPtr.Zero)
                {
                    SafeNativeMethods.DisposeStatClass(PtrToUnmanagedClass);
                    PtrToUnmanagedClass = IntPtr.Zero;
                }

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~CuStats()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

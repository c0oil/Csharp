using System;
using System.Diagnostics;
using System.Linq;

namespace DIP6
{
    public sealed class ConcurrentNetwork
    {
        private const double StudySpeed = 0.5;

        private readonly int clusterSize; // 144
        private readonly int clusterCount;  // was 3, changed to 2
        private readonly double[,] weightMatrix;
        private readonly double[] winList;
        public int[] Output { get; set; }

        public ConcurrentNetwork(int clusterSize, int clusterCount)
        {
            this.clusterSize = clusterSize;
            this.clusterCount = clusterCount;
            this.weightMatrix = new double[clusterCount, clusterSize];
            this.Output = new int[clusterCount];
            this.winList = new double[clusterCount];

            InitializeWeightMatrix();
            InitializeWinList();
        }

        private void InitializeWeightMatrix()
        {
            var r = new Random();
            for (int i = 0; i < clusterCount; i++)
            {
                for (int j = 0; j < clusterSize; j++)
                {
                    weightMatrix[i, j] = r.NextDouble() * 2 - 1;
                }
            }
        }

        private void InitializeWinList()
        {
            for (int i = 0; i < clusterCount; ++i)
            {
                winList[i] = 1;
            }
        }

        private double Dv(int c, double[] x)
        {
            int i = 0;
            return x.Sum(d => Math.Abs(weightMatrix[c, i] - x[i++])) * winList[c]; 
        }

        private double GaussBell(int t, int j, int m)
        {
            double[] tarr = new double[clusterSize];
            for (int i = 0; i < clusterSize; i++)
            {
                tarr[i] = weightMatrix[m, i];
            }
            double d = Dv(j, tarr) / winList[j];
            double res = Math.Exp(-(d * d) / Math.Exp(-t));
            return res;
        }

        private void CorrectWeightMatrix(int winnerNum, double[] x, int t)
        {
            for (int j = 0; j < clusterCount; j++)
            {
                for (int i = 0; i < clusterSize; i++)
                {
                    weightMatrix[j, i] = 
                        weightMatrix[j, i] + StudySpeed * GaussBell(t, j, winnerNum) * (x[i] - weightMatrix[j, i]); // (4.6)
                }
            }
        }

        private int GetWinnerNeuron(double[] x, ref double winnerValue)
        {
            double minNeuronValue = double.PositiveInfinity;
            int minNeuronNum = -1;
            for (int i = 0; i < clusterCount; i++)
            {
                double d = Dv(i, x);  // (4.5)
                if (d < minNeuronValue)
                {
                    minNeuronValue = d;
                    minNeuronNum = i;
                }
            }
            winnerValue = minNeuronValue;
            return minNeuronNum;
        }

        public int StudyNeuron(double[] x, int t, ref double winnerValue)
        {
            int winnerNum = GetWinnerNeuron(x, ref winnerValue);
            winList[winnerNum] += 1;
            CorrectWeightMatrix(winnerNum, x, t);
            return winnerNum;
        }

        public void Teach(double[][] x)
        {
            int t = 0;
            double minValue = double.PositiveInfinity;
            while (minValue > 1) // min distance condition of loop exit
            {
                for (int i = 0; i < clusterCount; i++)
                {
                    double winnerNeuronValue = 0;
                    int winnerNeuronNum = StudyNeuron(x[i], t++, ref winnerNeuronValue);
                    if (winnerNeuronValue < minValue)
                    {
                        minValue = winnerNeuronValue;
                        Debug.WriteLine("minValue: " + minValue);
                    }
                    Output[i] = winnerNeuronNum;
                }
            }
            Debug.WriteLine("Output: " + Output.Select(i => i.ToString()).Aggregate((acc, str) => acc + ", " + str));
        }

        public int Reproduct(double[] x)
        {
            double dv = 0;
            int winner = GetWinnerNeuron(x, ref dv);
            return winner;
        }
    }
}

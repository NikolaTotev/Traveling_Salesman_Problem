using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Traveling_Salesman_CLI
{
    enum MutationType
    {
        swap,
        insert,
        reverse
    }
    class Specimen
    {
        public List<int> Path;
        public double FitnessLevel = 0;
        public bool FitnessLevelSet = false;
        public List<PointF> m_Points;
        private int m_popSize;

        public Specimen(int numberOfPoints, List<PointF> points, List<int> path, int popSize)
        {
            Path = new List<int>();
            foreach (var i in path)
            {
                Path.Add(i);
            }

            m_popSize = popSize;

            m_Points = points;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void FitnessFunction()
        {
            for (int i = 0; i < Path.Count - 1; i++)
            {
                FitnessLevel += EucDistance(m_Points[Path[i]], m_Points[Path[i + 1]]);
            }
        }

        double EucDistance(PointF firstPoint, PointF secondPoint)
        {
            float xDiff = firstPoint.X - secondPoint.X;
            float yDiff = firstPoint.Y - secondPoint.Y;
            return Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
        }

        void Mutate(MutationType mutation, Specimen child)
        {
            switch (mutation)
            {
                case MutationType.swap:
                    break;
                case MutationType.insert:
                    break;
                case MutationType.reverse:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mutation), mutation, null);
            }
        }

        public void SwapMutation(int index1, int index2)
        {
            (Path[index1], Path[index2]) = (Path[index2], Path[index1]);
        }

        void InsertMutation(int index, int value)
        {
            Path.Insert(index, value);
        }

        public void ReverseMutation(int firstIndex, int secondIndex)
        {
            List<int> revPart = Path.GetRange(firstIndex, secondIndex - firstIndex);
            revPart.Reverse();
            int counter = 0;
            for (int i = firstIndex; i < firstIndex+(secondIndex-firstIndex); i++)
            {
                Path[i] = revPart[counter];
                counter++;
            }

        }

        public void InitializationRandomSwap()
        {
            Random rand = new System.Random();

            for (int i = 0; i < Path.Count; i++)
            {
                int firstRandIndex = rand.Next(0, Path.Count);
                int secondRandIndex = rand.Next(0, Path.Count);
                SwapMutation(firstRandIndex, secondRandIndex);
            }
        }


        void Reproduce()
        {

        }
    }
}

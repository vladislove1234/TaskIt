using System;
namespace Taskit_server.Model.Entities
{
    public class Points
    {
        public int SumDone { get; set; }
        public int Sum { get; set; }
        public Points(int sumDone, int sum)
        {
            SumDone = sumDone;
            Sum = sum;
        }
    }
}

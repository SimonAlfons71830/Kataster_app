using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuadTree.QTree;

namespace QuadTree.Optimalization
{
    public class Health
    {
        public double Value { get; set; } = 100.0;
        public int HEALTH_KOEF = 2;

        public void CalculateNewHealthObjCountInQuad(int actualObjCount, int maxObjCount) 
        { 
            
            Value -= (actualObjCount - maxObjCount) * HEALTH_KOEF;
            if (Value < 0)
            {
                Value = 0;
            }
        }

        public void Reset()
        {
            Value = 100;
        }

        public void ReverseHealth(int actualObjCount, int maxObjCount) 
        {
            // Calculate the change in actualObjCount

            if (actualObjCount <= maxObjCount)
            {
                int change = 1;
                Value += change * HEALTH_KOEF;
            }
            else
            {
                //v opacnom pripade sa strom nezlepsil
                int change = Math.Abs(actualObjCount - maxObjCount);
            }
        }
    }
}

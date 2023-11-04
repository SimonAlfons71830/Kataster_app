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
        }
    }
}

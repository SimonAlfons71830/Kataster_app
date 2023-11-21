using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuadTree.Hashing
{
    public class Block<T> : IRecord<T> where T : IData<T>
    {
        private List<T> _records;
        private int _validRecordsCount;

        public Block() { }


        public void fromByteArray(byte[] byteArray)
        {
            throw new NotImplementedException();
        }

        public int getSize()
        {
            throw new NotImplementedException();
        }

        public byte[] toByteArray()
        {
            throw new NotImplementedException();
        }
    }
}

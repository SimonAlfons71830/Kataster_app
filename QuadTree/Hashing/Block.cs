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
        //records 
        private List<T> _records;
        //valid records count (non valid are fillind the address)
        private int _validRecordsCount;
        //information of class type ?? Plot/Property
        private Type _type;
        //number of blocks for the AL
        private int _bf;


        public Block(int blockFactor) 
        {
            _bf = blockFactor;
            this._type = typeof(T);
            this._records = new List<T>(_bf);

            for (int i = 0; i < _bf; i++)
            {
                //??
                var record = (T)Activator.CreateInstance(_type);
                //(T)Activator.CreateInstance<T>().createInstanceOfClass()
                this._records.Add(record) ;
            }

            _validRecordsCount = 0;
        }

        public bool Insert(T record) 
        {
            if (_validRecordsCount < _bf)
            {
                _records.Add(record);
                _validRecordsCount++;
                return true;
            }
            return false; //full
        }



        public void fromByteArray(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            using (BinaryReader reader = new BinaryReader(stream, Encoding.Default, true))
            {
                _validRecordsCount = reader.ReadInt32();
                _records.Clear();

                //?? test
                for (int i = 0; i < _bf; i++)
                {
                    T record = Activator.CreateInstance<T>();
                    record.fromByteArray(reader.ReadBytes(record.getSize()));
                    _records.Add(record);
                }
            }
        }

        public int getSize()
        {
            //type size * _blockFactor + _validRecordsCount
            //return (T)Activator.CreateInstance(_type).getSize() * _bf + sizeof(int);
            return Activator.CreateInstance<T>().getSize() * _bf + sizeof(int);
            //throw new NotImplementedException();
        }

        public byte[] toByteArray()
        {
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(_validRecordsCount);

                foreach (T record in _records) 
                { 
                    //records has own method implemented
                    writer.Write(record.toByteArray());
                }

                // Get the byte array from the stream
                return stream.ToArray();
            }
        }
    }
}

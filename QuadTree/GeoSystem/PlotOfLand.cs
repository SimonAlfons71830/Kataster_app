using QuadTree.Hashing;
using QuadTree.Structures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree.GeoSystem
{
    public class PlotOfLand : Polygon, IData<Property>
    {
        public string _description;
        public (Coordinates startPos, Coordinates endPos) _coordinates;
        public List<Property> _properties;
        public List<int> _properties_ids;

        private const int MAX_DESC_LENGTH = 11;
        private const int MAX_RECORDS_OF_PROP = 5;
        
        /// <summary>
        /// constructor for SEM1
        /// </summary>
        /// <param name="registerNumber"></param>
        /// <param name="description"></param>
        /// <param name="coordinates"></param>
        /// <param name="properties"></param>
        public PlotOfLand(int registerNumber, string description, (Coordinates startPos, Coordinates endPos) coordinates, List<Property> properties) 
            : base(registerNumber, coordinates)
        {
            RegisterNumber = registerNumber;
            Description = description;
            Coordinates = coordinates;
            Properties = properties;
        }

        /// <summary>
        /// constructor for SEM2
        /// </summary>
        /// <param name="registerNumber"></param>
        /// <param name="description"></param>
        /// <param name="coordinates"></param>
        /// <param name="properties"></param>
        public PlotOfLand(int registerNumber, string description, (Coordinates startPos, Coordinates endPos) coordinates, List<int> properties_ids)
           : base(registerNumber, coordinates)
        {
            RegisterNumber = registerNumber;
            Description = ShortenDesc(description);
            Coordinates = coordinates;
            Properties_ids = properties_ids.Take(MAX_RECORDS_OF_PROP).ToList();
        }

        private string ShortenDesc(string desc)
        {
            if (desc.Length <= MAX_DESC_LENGTH)
            {
                return desc;
            }
            else
            {
                return desc.Substring(0, MAX_DESC_LENGTH);
            }
        }

        public int RegisterNumber { get => _registerNumber; set => _registerNumber = value; }
        public string Description { get => _description; set => _description = ShortenDesc(value); }
        internal (Coordinates startPos, Coordinates endPos) Coordinates { get => _coordinates; set => _coordinates = value; }
        internal List<Property> Properties { get => _properties; set => _properties = value; }
        internal List<int> Properties_ids { get => _properties_ids; set => _properties_ids = value.Take(MAX_RECORDS_OF_PROP).ToList(); }

        public bool MyEquals(Property other)
        {
            return this.RegisterNumber.Equals(other.RegisterNumber);
        }

        public void fromByteArray(byte[] byteArray)
        {
            throw new NotImplementedException();
        }

        public BitArray getHash()
        {
            return new BitArray(Encoding.Default.GetBytes(_registerNumber.ToString()));
        }

        public int getSize()
        {
            int size = 0;
            //size of the bytes in this class
            //registerNumber - int
            size += sizeof(int);
            //description - length of string + 1 for null terminator
            size += Encoding.Default.GetByteCount(_description) + 1;
            //coordinates - 2 doubles
            size += 2 * sizeof(double);
            //size of properties in list (int)
            size += sizeof(int) + _properties_ids.Count * sizeof(int);
            return size;
        }

        public byte[] toByteArray()
        {
            throw new NotImplementedException();
        }

        public Property createInstanceOfClass()
        {
            throw new NotImplementedException();
        }
    }
}

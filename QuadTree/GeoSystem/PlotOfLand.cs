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

        public PlotOfLand() :
            this(0, "", (new Coordinates(0, 0, -1), new Coordinates(0, 0, -1)), new List<int>())
        { 
                //new instance
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
            //size += Encoding.Default.GetByteCount(_description) + 1;
            size += MAX_DESC_LENGTH + 1;
            //coordinates - 4 doubles
            size += 4 * sizeof(double);
            //size of properties in list (int)
            size += sizeof(int) + MAX_RECORDS_OF_PROP * sizeof(int);
            return size;
        }

        public byte[] toByteArray()
        {
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(_registerNumber);

                byte[] descriptionBytes = Encoding.Default.GetBytes(_description);
                writer.Write((byte)descriptionBytes.Length);  // Store the length of the description
                writer.Write(descriptionBytes);

                writer.Write(_coordinates.startPos.Longitude);
                writer.Write(_coordinates.startPos.Latitude);
                writer.Write(_coordinates.endPos.Longitude);
                writer.Write(_coordinates.endPos.Latitude);

                // Write the number of properties_ids and then each element
                writer.Write(_properties_ids.Count);
                foreach (int propertyId in _properties_ids)
                {
                    writer.Write(propertyId);
                }

                // Get the byte array from the stream
                return stream.ToArray();
            }
        }

        public void fromByteArray(byte[] byteArray)
        {
            using (MemoryStream stream = new MemoryStream(byteArray))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                _registerNumber = reader.ReadInt32();

                byte descriptionLength = reader.ReadByte();
                byte[] descriptionBytes = reader.ReadBytes(descriptionLength);
                _description = Encoding.Default.GetString(descriptionBytes);

                double startLongitude = reader.ReadDouble();
                double startLatitude = reader.ReadDouble();
                double endLongitude = reader.ReadDouble();
                double endLatitude = reader.ReadDouble();
                _coordinates = (new Coordinates(startLongitude, startLatitude,-1), new Coordinates(endLongitude, endLatitude,-1));

                int propertiesCount = reader.ReadInt32();
                _properties_ids = new List<int>(propertiesCount);
                for (int i = 0; i < propertiesCount; i++)
                {
                    int propertyId = reader.ReadInt32();
                    _properties_ids.Add(propertyId);
                }
            }
        }

        public Property createInstanceOfClass()
        {
            return new Property();
        }
    }
}

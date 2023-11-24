using QuadTree.Hashing;
using QuadTree.Structures;
using System.Collections;
using System.Text;

namespace QuadTree.GeoSystem
{
    public class Property : Polygon, IData<Property>
    {
        //public int _registerNumber { get; set; }
        public (Coordinates x, Coordinates y) suradnice { get; set; }
        public string _description;
        public List<PlotOfLand> _lands;
        public List<int> _lands_ids;

        private const int MAX_DESC_LENGTH = 15;
        private const int MAX_RECORDS_OF_PLOTS = 6;

        /// <summary>
        /// old constructor for SEM1
        /// </summary>
        /// <param name="registerNumber"></param>
        /// <param name="description"></param>
        /// <param name="coordinates"></param>
        /// <param name="lands"></param>
        public Property(int registerNumber, string description, (Coordinates x, Coordinates y) coordinates, List<PlotOfLand> lands) 
            : base(registerNumber, coordinates)
        {
            RegisterNumber = registerNumber;
            Description = description;
            Coordinates = coordinates;
            Lands = lands;
        }

        /// <summary>
        /// new constructor for SEM2
        /// </summary>
        /// <param name="registerNumber"></param>
        /// <param name="description"></param>
        /// <param name="coordinates"></param>
        /// <param name="lands"></param>
        public Property(int registerNumber, string description, (Coordinates x, Coordinates y) coordinates, List<int> lands_ids)
           : base(registerNumber, coordinates)
        {
            RegisterNumber = registerNumber;
            Description = ShortenDesc(description);
            Coordinates = coordinates;
            Lands_ids = lands_ids.Take(MAX_RECORDS_OF_PLOTS).ToList();
        }

        public Property()
            : this(0, "", (new Coordinates(0, 0, -1), new Coordinates(0, 0, -1)), new List<PlotOfLand>())
        {
            //empty inicialization constructor
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
        
        internal (Coordinates x, Coordinates y) Coordinates { get => suradnice; set => suradnice = value; }
        internal List<PlotOfLand> Lands { get => _lands; set => _lands = value; }
        internal List<int> Lands_ids { get => _lands_ids; set => _lands_ids = value.Take(MAX_RECORDS_OF_PLOTS).ToList(); }
        
        public bool MyEquals(Property other)
        {
            return this.RegisterNumber.Equals(other.RegisterNumber);
        }

        public BitArray getHash()
        {
            //default, UTF8, ...
            byte[] regnumberChars = Encoding.Default.GetBytes(this._registerNumber.ToString());
            var bitarr = new BitArray(regnumberChars);
            return bitarr;

            //return new BitArray(Encoding.Default.GetBytes(_registerNumber.ToString()));
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
            size += sizeof(int) + _lands_ids.Count * sizeof(int);
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

                writer.Write(suradnice.x.Longitude);
                writer.Write(suradnice.x.Latitude);
                writer.Write(suradnice.y.Longitude);
                writer.Write(suradnice.y.Latitude);

                // Write the number of properties_ids and then each element
                writer.Write(_lands_ids.Count);
                foreach (int propertyId in _lands_ids)
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
                suradnice = (new Coordinates(startLongitude, startLatitude, -1), new Coordinates(endLongitude, endLatitude, -1));

                int landsCount = reader.ReadInt32();
                _lands_ids = new List<int>(landsCount);
                for (int i = 0; i < landsCount; i++)
                {
                    int plotId = reader.ReadInt32();
                    _lands_ids.Add(plotId);
                }
            }
        }

        public Property createInstanceOfClass()
        {
            return new Property();
        }
    }
}

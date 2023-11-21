using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree.Trie
{
    internal class ExternalNode : Node
    {
        private int _countOfRecords;
        private int _index;

        public ExternalNode()
        {
            IsLeaf = true;
            this._countOfRecords = 0;
            this._index = -1; //?
        }

        public int CountOfRecords
        {
            get { return _countOfRecords; }
            set { _countOfRecords = value; }
        }

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
    }
}

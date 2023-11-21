using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTree.Trie
{
    internal class Node
    {
        private bool _isLeaf = false;
        private InternalNode _parent;

        public Node()
        { 
           
        }

        public bool IsLeaf
        {
            get { return _isLeaf; }
            set { _isLeaf = value; }
        }

        public InternalNode Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }
    }
}

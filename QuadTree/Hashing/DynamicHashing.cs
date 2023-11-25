using QuadTree.Trie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuadTree.Trie;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Reflection.Metadata;
using System.Collections;
using System.Security.Policy;

namespace QuadTree.Hashing
{
    internal class DynamicHashing<T> where T : IData<T>
    {
        //private Node _root;
        private Trie.Trie _trie;
        private int _blockFactor;
        private FileStream _file;

        public DynamicHashing(String fileName,int blockFactor)
        {
            _blockFactor = blockFactor;
            _trie = new Trie.Trie();


            try
            {
                // Open or create the file with Read-Write access
                _file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            }
            catch (FileNotFoundException e)
            {
                throw new InvalidOperationException("Error in Hashing: File not found.", e);
            }
            catch (IOException e)
            {
                throw new InvalidOperationException("Error in Hashing: IO exception.", e);
            }
        }


        //insert
        public void Insert(T data) 
        {
            /*Operácia vlož:
                1.Aplikuj H(K)…. výsledok bitový reťazec B; //done
                2.Ak koreň interný, traverzuj podľa B k externému vrcholu E;
                3.Ak externý vrchol E neukazuje na žiadny blok, alokuj blok, ulož
                    doňho záznam a nastav naňho adresu “Blok”
                    inak
                    ak blok s adresou “Blok” nie je plný, vlož záznam,
                    inak opakuj až do vyriešenia situácie preplnenia:
                            A.externý vrchol E transformuj na interný a vytvor
                            mu dvoch synov – externé vrcholy; káždému z
                            týchto externých vrcholov alokuj blok(zatiaľ iba
                            v operačnej pamäti).
                            B.presuň záznamy z preplneného bloku a nový
                            záznam do ľavého alebo pravého bloku použijúc
                            ďalší bit bitového reťazca(zväčšenie hĺbky D
                            súboru). Ak do jedného z blokov nepadnú
                            žiadne záznamy, dealokuj ho – jeho dvojča
                            ostáva preplnené(zapíše sa do súboru), situáciu
                            rieš opakovaním od bodu A.
                V prípade, že dôjde k využitiu všetkých bitov z výsledku
                hešovacej funkcie a záznam nebolo možné vložiť dôjde ku kolízií.
                Na jej riešenie je možné použiť oblasť preplňujúcich blokov, alebo
                preplňujúci súbor.V prípade, že sa nevyužívajú bity z výsledku
                hešovacej funkcie(tento výsledok nemá zaručenú unikátnosť), 
                ale z unikátneho kľúča, nedochádza k vzniku kolízií*/


            //get hash value from data
            var hashData = data.getHash();
            //seek corresonding file position using trie
            var node = _trie.getExternalNode(hashData);
            if (node != null)
            {
                //exists

                if (((ExternalNode)node).Index == -1) //nema este ziadny zaznam(neexistuje pre nu adresa)
                {
                    //alokacia bloku
                    var block = new Block<T>(this._blockFactor);
                    //ulozenie zaznamu
                    block.Insert(data);
                    //nastavenie adresy na koniec suboru?
                    var newIndex = _file.Length;
                    _file.SetLength(_file.Length + block.getSize());
                    node.Index = (int)newIndex;
                    WriteBlockBackToFile(node.Index, block);
                    node.CountOfRecords++;

                }
                else if (((ExternalNode)node).Index != -1 && ((ExternalNode)node).CountOfRecords < this._blockFactor) 
                {
                    //vloz k existujucemu bloku zaznam
                    var existingBlock = FindBlockByHash(hashData);
                    if (existingBlock != null) 
                    {
                        if (existingBlock.Insert(data))
                        {
                            //return from FindBlockByHash alongside with Block? not to search again for it in trie
                            var index = _trie.getExternalNode(hashData).Index;
                            //write back to file on the same address
                            WriteBlockBackToFile(index, existingBlock);
                            node.CountOfRecords++;
                        }
                        else
                        {
                            Console.WriteLine("Did not insert into block. Err.");
                        }
                    }
                    else
                    {
                        Console.Write("Did not find block in File. Err.");
                    }
                }

                //records in node exceeds blockfactor

                if (((ExternalNode)node).CountOfRecords >= this._blockFactor)
                {
                    //preplnenie
                    //externý vrchol transformuj na interný a vytvor mu dvoch synov
                    //káždému z týchto externých vrcholov alokuj blok
                    //(zatiaľ iba v operačnej pamäti).
                    
                    //data
                    //node Parent : y
                    //node CountOfRecords
                    //node Index (adress)
                    //node isLeaf : y

                    InternalNode newNode = new InternalNode();
                    //reset parent
                    newNode.Parent = node.Parent;
                    node.Parent = null;
                    //Parents reference to his son changed (left/right)?
                    if (node.Parent!.LeftNode == node) //is left (TODO: check when parent is null!!!)
                    {
                        newNode.Parent.LeftNode = newNode;
                    }
                    else
                    {
                        newNode.Parent.RightNode = newNode;
                    }
                    newNode.IsLeaf = false;

                    //create sons
                    var Left = new ExternalNode(0,-1);
                    var Right = new ExternalNode(0, -1);
                    newNode.LeftNode = Left;
                    newNode.RightNode = Right;
                    Left.Parent = newNode;
                    Right.Parent = newNode;

                    //alocate new blocks for each sons
                    var blockL = new Block<T>(this._blockFactor);
                    var blockR = new Block<T>(this._blockFactor);


                    
                    







                    var block = ReadBlockFromFile(node.Index);



                    //presuň záznamy z preplneného bloku a nový
                    //záznam do ľavého alebo pravého bloku použijúc
                    //ďalší bit bitového reťazca(zväčšenie hĺbky D súboru).
                    //Ak do jedného z blokov nepadnú žiadne záznamy,
                    //dealokuj ho – jeho dvojča ostáva preplnené(zapíše sa do súboru),
                    //situáciu rieš opakovaním od bodu A - znovu preplnenie.
                }
                
            }

            //seek to the file position

        }

        public Block<T> FindBlockByHash(BitArray hash) 
        {
            var node = _trie.getExternalNode(hash);
            if (node != null)
            {
                //nacitat block z file a returnut ho
                var block = ReadBlockFromFile(node.Index);
                return block;
            }
            return null;

        }

        public void WriteBlockBackToFile(int index, Block<T> block) 
        {
            _file.Seek(index, SeekOrigin.Begin);
            _file.Write(block.toByteArray());
        }

        public Block<T> ReadBlockFromFile(int index) 
        {
            Block<T> block = new Block<T>(this._blockFactor);
            byte[] bytes = new byte[block.getSize()]; //nove pole bytev o velkosti block.getSize

            _file.Seek(index, SeekOrigin.Begin); //from the start of the file
            _file.Read(bytes); //how much bytes should it read
            //reads the array of selected size from the actual position of stream 

            block.fromByteArray(bytes);
            return block;
        }


        //find
        //delete

    }
}

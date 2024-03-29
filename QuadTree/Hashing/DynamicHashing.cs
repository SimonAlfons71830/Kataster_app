﻿using QuadTree.Trie;
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
using static System.Reflection.Metadata.BlobBuilder;

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
            var level = -1;
            var node = _trie.getExternalNode(hashData, out level);
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
                            var index = _trie.getExternalNode(hashData, out level).Index;
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




                //=============================preplnenie=============================

                if (((ExternalNode)node).CountOfRecords >= this._blockFactor)
                {
                    //need a level of current hash
                    //level;

                    //preplnenie
                    //externý vrchol transformuj na interný a vytvor mu dvoch synov
                    //káždému z týchto externých vrcholov alokuj blok
                    //(zatiaľ iba v operačnej pamäti).

                    //data
                    //node Parent : y
                    //node CountOfRecords
                    //node Index (adress)
                    //node isLeaf : y

                    //get a full block
                    var block = this.FindBlockByHash(hashData);
                    // level of the current node < data hash count 
                    bool end = false;
                    while (level < hashData.Count)
                    {
                        if (end) 
                        {
                            //end the cycle
                            return;
                        }
                        //=====A
                        InternalNode newNode = new InternalNode();
                        //reset parent
                        newNode.Parent = node.Parent;
                        
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
                        node.Parent = null;
                        //create sons
                        var Left = new ExternalNode(0, -1);
                        var Right = new ExternalNode(0, -1);
                        newNode.LeftNode = Left;
                        newNode.RightNode = Right;
                        Left.Parent = newNode;
                        Right.Parent = newNode;

                        //alocate new blocks for each sons
                        var blockL = new Block<T>(this._blockFactor);
                        var blockR = new Block<T>(this._blockFactor);



                        //=====B
                        //B.presuň záznamy z preplneného bloku a nový
                        //záznam do ľavého alebo pravého bloku použijúc
                        //ďalší bit bitového reťazca(zväčšenie hĺbky D
                        //súboru).
                        //Ak do jedného z blokov nepadnú
                        //žiadne záznamy, dealokuj ho – jeho dvojča
                        //ostáva preplnené(zapíše sa do súboru), situáciu
                        //rieš opakovaním od bodu A.
                        bool rightContinue = false;
                        bool leftContinue = false;

                        //for each record from the block records
                        foreach (var record in block.Records)
                        {
                            var hash = record.getHash();
                            if (hash.Count > level)
                            {
                                if (hash[level]) // ak je to 1 idem doprava podla hashu a levelu na ktorom sa ma nachadzat novy syn
                                {
                                    if (!blockR.Insert(record))
                                    {
                                        rightContinue = true;
                                    }//else podaril sa insert
                                    //Right.CountOfRecords++;
                                }
                                else
                                {
                                    if (!blockL.Insert(record))
                                    {
                                        leftContinue = true;
                                    }
                                    //((ExternalNode)newNode.LeftNode).CountOfRecords++;
                                    //Left.CountOfRecords++;
                                }
                            }
                        }

                        //TODO: while cyclus
                        // kontrola ci right a left node maju dodrzany BF
                        //ak nie tak opakuj a nastav ako existing block lavy a pravy

                        if (!rightContinue && !leftContinue)
                        {
                            end = true;
                            Left.CountOfRecords = blockL.ValidRecordsCount;
                            Right.CountOfRecords = blockR.ValidRecordsCount;

                            if (Right.CountOfRecords > 0)
                            {
                                //find free index (now just add at the end of the file)
                                var newIndex = _file.Length;
                                _file.SetLength(_file.Length + blockR.getSize());
                                Right.Index = (int)newIndex;

                                this.WriteBlockBackToFile(Right.Index, blockR);
                                //write to file
                            }
                            else
                            {
                                Right.Index = -1;
                            }

                            if (Left.CountOfRecords > 0)
                            {
                                var newIndex = _file.Length;
                                _file.SetLength(_file.Length + blockL.getSize());
                                Left.Index = (int)newIndex;

                                this.WriteBlockBackToFile(Left.Index, blockL);
                                //writeToFile
                            }
                            else
                            {
                                Left.Index = -1;
                            }
                        }
                        else
                        {
                            //continue cycle with node that has full block
                            if (rightContinue)
                            {
                                node = Right;
                            }
                            else if (leftContinue) 
                            {
                                node = Left;
                            }

                            level++;
                        }

                        

                        //presuň záznamy z preplneného bloku a nový
                        //záznam do ľavého alebo pravého bloku použijúc
                        //ďalší bit bitového reťazca(zväčšenie hĺbky D súboru).
                        //Ak do jedného z blokov nepadnú žiadne záznamy,
                        //dealokuj ho – jeho dvojča ostáva preplnené(zapíše sa do súboru),
                        //situáciu rieš opakovaním od bodu A - znovu preplnenie.
                    }
                }
                
            }

            //seek to the file position

        }

        public Block<T> FindBlockByHash(BitArray hash) 
        {
            var levelnot = -1;
            var node = _trie.getExternalNode(hash,out levelnot);
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
        
        public T Find (T data)
        {
            //Vypočítaj I = hd(K)(prvých D bitov hodnoty hešovacejfunkcie),
            //Pomocou adresára(trie) sprístupni blok P[i],
            //3.V bloku P[i] nájdi záznam s kľúčom K.
            var levelPom = -1;
            var pom = _trie.getExternalNode(data.getHash(), out levelPom);

            if (pom.CountOfRecords == 0)
            {
                //not any records in block
                return default(T);
            }
            else
            {
                //vieme ze existuje zaznam, mozeme hladat zo suboru
                var block = this.FindBlockByHash(data.getHash());
                for (int i = 0; i < block.Records.Count; i++)
                {
                    if (i < block.ValidRecordsCount)
                    {
                        if (data.MyEquals(block.Records.ElementAt(i)))
                        {
                            return block.Records.ElementAt(i);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Item is not in file");
                    }
                }
                //not found
                return default(T);
            }

        }
        //delete

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Lucene.Net.Store;
using System.IO;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.PanGu;

namespace WebApplication1.Controllers.Lucene
{
    public class IndexManager
    {
        public static readonly IndexManager bookIndex = new IndexManager();

        private IndexManager()
        {

        }


        //请求队列 ,处理并发
        private Queue<IndexViewMode> indexQueue = new Queue<IndexViewMode>();

        public void Add(SearchUnit su, string type)
        {
            IndexViewMode ivm = new IndexViewMode(su);
            ivm.IT = IndexType.Modify;
            ivm.type = type;
            indexQueue.Enqueue(ivm);
        }
        public void Del(int id, string type)
        {
            IndexViewMode ivm = new IndexViewMode();
            ivm.id = id.ToString();
            ivm.IT = IndexType.Delete;
            ivm.type = type;
            indexQueue.Enqueue(ivm);
        }

        public void Mod(SearchUnit su, string type)
        {
            IndexViewMode ivm = new IndexViewMode();
            ivm.IT = IndexType.Modify;
            ivm.type = type;
            indexQueue.Enqueue(ivm);
        }

        public void Clear()
        {
            indexQueue.Clear();
        }

        public void StartNewThread()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(QueueToIndex));
        }

        //定义一个线程 将队列中的数据取出来 插入索引库中
        private void QueueToIndex(object para)
        {
            while (true)
            {
                if (indexQueue.Count > 0)
                {
                    CRUDIndex();
                }
                else
                {
                    //3小时更新一次
                    Thread.Sleep(1000 * 60 * 60 * 3);
                }
                 
            }
        }

        private void CRUDIndex()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["IndexPath"].ToString();

            FSDirectory directory_Test1 = FSDirectory.Open(new DirectoryInfo(path + "\\Test1"), new NoLockFactory());
            FSDirectory directory_Test2 = FSDirectory.Open(new DirectoryInfo(path + "\\Test2"), new NoLockFactory());
            FSDirectory directory_Test3 = FSDirectory.Open(new DirectoryInfo(path + "\\Test3"), new NoLockFactory());
            bool isExistT1 = IndexReader.IndexExists(directory_Test1);
            if (isExistT1)
            {
                if (IndexWriter.IsLocked(directory_Test1))
                {
                    IndexWriter.Unlock(directory_Test1);
                }
            }
            bool isExistT2 = IndexReader.IndexExists(directory_Test2);
            if (isExistT2)
            {
                if (IndexWriter.IsLocked(directory_Test2))
                {
                    IndexWriter.Unlock(directory_Test2);
                }
            }
            bool isExistT3 = IndexReader.IndexExists(directory_Test3);
            if (isExistT3)
            {
                if (IndexWriter.IsLocked(directory_Test3))
                {
                    IndexWriter.Unlock(directory_Test3);
                }
            }

            IndexWriter writerD = new IndexWriter(directory_Test1, new PanGuAnalyzer(), !isExistT1, IndexWriter.MaxFieldLength.UNLIMITED);
            IndexWriter writerA = new IndexWriter(directory_Test2, new PanGuAnalyzer(), !isExistT2, IndexWriter.MaxFieldLength.UNLIMITED);
            IndexWriter writerV = new IndexWriter(directory_Test3, new PanGuAnalyzer(), !isExistT3, IndexWriter.MaxFieldLength.UNLIMITED);
            while (indexQueue.Count > 0)
            {
                Document document = new Document();
                IndexViewMode index = indexQueue.Dequeue();
                if (index.IT == IndexType.Insert)
                {
                    document.Add(new Field("id", index.id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("updatetime", index.updatetime.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    Field namef = new Field("name", index.name, Field.Store.YES, Field.Index.ANALYZED,Field.TermVector.WITH_POSITIONS_OFFSETS);
                    namef.Boost = 100f;
                    document.Add(namef);
                    document.Add(new Field("model", index.model, Field.Store.YES, Field.Index.ANALYZED,
                                           Field.TermVector.WITH_POSITIONS_OFFSETS));
                    document.Add(new Field("detail", index.detail, Field.Store.YES, Field.Index.ANALYZED,
                                           Field.TermVector.WITH_POSITIONS_OFFSETS));
                    document.Add(new Field("hits", index.hits.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    
                    switch (index.type)
                    {
                        case "Test1":
                            writerD.AddDocument(document); break;
                        case "Test2":
                            writerA.AddDocument(document); break;
                        case "Test3":
                            writerV.AddDocument(document); break;
                    }
                }
                else if (index.IT == IndexType.Delete)
                {
                    switch (index.type)
                    {
                        case "Test1":
                            writerD.DeleteDocuments(new Term("id", index.id.ToString())); break;
                        case "Test2":
                            writerA.DeleteDocuments(new Term("id", index.id.ToString())); break;
                        case "Test3":
                            writerV.DeleteDocuments(new Term("id", index.id.ToString())); break;
                    }
                }
                else if (index.IT == IndexType.Modify)
                {
                    //先删除 再新增
                    document.Add(new Field("id", index.id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    document.Add(new Field("updatetime", index.updatetime.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    Field namef = new Field("name", index.name, Field.Store.YES, Field.Index.ANALYZED, Field.TermVector.WITH_POSITIONS_OFFSETS);
                    namef.Boost = 100f;
                    document.Add(namef);
                    document.Add(new Field("model", index.model, Field.Store.YES, Field.Index.ANALYZED,
                                           Field.TermVector.WITH_POSITIONS_OFFSETS));
                    document.Add(new Field("detail", index.detail, Field.Store.YES, Field.Index.ANALYZED,
                                           Field.TermVector.WITH_POSITIONS_OFFSETS));
                    document.Add(new Field("hits", index.hits.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                    
                    switch (index.type)
                    {
                        case "Test1":
                            writerD.DeleteDocuments(new Term("id", index.id.ToString()));
                            writerD.AddDocument(document); break;
                        case "Test2":
                            writerA.DeleteDocuments(new Term("id", index.id.ToString()));
                            writerA.AddDocument(document); break;
                        case "Test3":
                            writerV.DeleteDocuments(new Term("id", index.id.ToString()));
                            writerV.AddDocument(document); break;
                    }
                }
            }
            writerD.Dispose();
            directory_Test1.Dispose();
            writerA.Dispose();
            directory_Test2.Dispose();
            writerV.Dispose();
            directory_Test3.Dispose();

        }

    }
    
    public class IndexViewMode
    {
        public IndexViewMode() { }
        public IndexViewMode(SearchUnit su)
        {
            this.id = su.id;
            this.name = su.name;
            this.model = su.model;
            this.detail = su.detail;
            this.updatetime = su.updatetime;
        }

        public IndexType IT { get; set; }

        public string type { get; set; }
        
        public string id { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public string detail { get; set; }
        public DateTime updatetime { get; set; }
        public string hits { get; set; }
    }


    public enum IndexType
    {
        Insert,
        Modify,
        Delete
    }
}

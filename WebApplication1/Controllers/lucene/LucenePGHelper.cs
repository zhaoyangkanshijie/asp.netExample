using System.Collections.Generic;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Store;
using PanGu;

namespace WebApplication1.Controllers.Lucene
{
    public class LucenePGHelper
    {
        private LucenePGHelper() { }

        #region 单一实例 instance
        private static LucenePGHelper _instance = null;
        public static LucenePGHelper instance
        {
            get
            {
                if (_instance == null) _instance = new LucenePGHelper();
                return _instance;
            }
        }
        #endregion

        #region 分词 Token
        //public List<string> Token(string content)
        //{
        //    List<string> strList = new List<string>();
        //    Analyzer analyzer = new PanGuAnalyzer();//指定使用盘古 PanGuAnalyzer 分词算法

        //    System.IO.StringReader reader = new System.IO.StringReader(content);

        //    TokenStream ts = analyzer.TokenStream("", reader);

        //    bool hasNext = ts.IncrementToken();

        //    Lucene.Net.Analysis.Tokenattributes.ITermAttribute ita;
        //    //Console.WriteLine("start----------");
        //    while (hasNext)
        //    {
        //        ita = ts.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();
        //        strList.Add(ita.Term);
        //        //Console.WriteLine(ita.Term);
        //        hasNext = ts.IncrementToken();
        //    }
        //    //Console.WriteLine("end----------");
        //    ts.CloneAttributes();
        //    reader.Close();
        //    analyzer.Close();
        //    return strList;
        //}
        #endregion

        #region 创建索引 CreateIndex
        //public bool CreateIndex(List<SearchUnit> list, string type)
        //{
        //    FSDirectory dir = GetDirectory(type);
        //    bool isExit = IndexReader.IndexExists(dir);
        //    if (isExit)
        //    {
        //        //如果索引目录被锁定（比如索引过程中程序异常退出或另一进程在操作索引库），则解锁
        //        if (IndexWriter.IsLocked(dir))
        //        {
        //            IndexWriter.Unlock(dir);
        //        }
        //    }

        //    IndexWriter writer = null;
        //    try
        //    {
        //        writer = new IndexWriter(dir, analyzer, !isExit, IndexWriter.MaxFieldLength.LIMITED);//false表示追加（true表示删除之前的重新写入）
        //    }
        //    catch
        //    {
        //        writer = new IndexWriter(dir, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);//false表示追加（true表示删除之前的重新写入）
        //    }
        //    foreach (SearchUnit data in list)
        //    {
        //        CreateIndex(writer, data);
        //    }
        //    writer.Optimize();
        //    writer.Dispose();
        //    return true;
        //}

        //public bool CreateIndex(IndexWriter writer, SearchUnit data)
        //{
        //    try
        //    {
        //        if (data == null) return false;
        //        Document doc = new Document();
        //        doc.Add(new Field("id", data.id, Field.Store.YES, Field.Index.NOT_ANALYZED));
        //        doc.Add(new Field("updatetime", data.updatetime.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
        //        Field namef = new Field("name", data.name, Field.Store.YES, Field.Index.ANALYZED);
        //        namef.Boost = 100f;
        //        doc.Add(namef);
        //        doc.Add(new Field("model", data.model, Field.Store.YES, Field.Index.ANALYZED));//id不分词
        //        doc.Add(new Field("detail", data.detail, Field.Store.YES, Field.Index.ANALYZED));
        //        //ScoreDoc
        //        writer.AddDocument(doc);
        //    }
        //    catch (System.IO.FileNotFoundException e)
        //    {
        //        throw e;
        //    }
        //    return true;
        //}

        #endregion

        #region 查询数据  Search
        //public List<SearchUnit> Search(string kw, string type)
        //{

        //    List<SearchUnit> result = new List<SearchUnit>();
        //    //try
        //    //{
        //        IndexSearcher searcher = new IndexSearcher(GetDirectory(type), true);//true-表示只读
        //        BooleanQuery query = new BooleanQuery();

        //        if (kw != "")
        //        {
        //            ICollection<WordInfo> iw = new PanGuTokenizer().SegmentToWordInfos(kw.ToLower());
        //            foreach (WordInfo info in iw)
        //            {
        //                //if~else执行的搜索相同,原因是需求的变更,为避免以后需求再次更改,暂时保留
        //                if (isNum(info.Word.ToLower()))
        //                {
        //                    BooleanQuery queryB = new BooleanQuery();
        //                    Query query1 = new WildcardQuery(new Term("name", "*" + info.Word.ToLower() + "*"));
        //                    Query query2 = new WildcardQuery(new Term("model", info.Word.ToLower()));
        //                    //Query query3 = new WildcardQuery(new Term("detail", "*" + info.Word.ToLower() + "*"));
        //                    queryB.Add(query1, Occur.SHOULD);
        //                    queryB.Add(query2, Occur.SHOULD);
        //                    //queryB.Add(query3, Occur.SHOULD);
        //                    //query.Add(queryB, Occur.MUST);
        //                    query.Add(queryB, Occur.SHOULD);
        //                }
        //                else
        //                {
        //                    Query query1 = new WildcardQuery(new Term("name", "*" + info.Word.ToLower() + "*"));
        //                    Query query2 = new WildcardQuery(new Term("model", info.Word.ToLower()));
        //                    //Query query3 = new WildcardQuery(new Term("detail", "*" + info.Word.ToLower() + "*"));
        //                    query.Add(query1, Occur.SHOULD);
        //                    query.Add(query2, Occur.SHOULD);
        //                    //query.Add(query3, Occur.SHOULD);
        //                }
        //            }

        //        }
        //        TopDocs docs = searcher.Search(query, (Lucene.Net.Search.Filter)null, 1000);
        //        if (docs == null || docs.TotalHits == 0)
        //        {
        //            return result;
        //        }
        //        else
        //        {
        //            List<SearchUnit> resultTmp1 = new List<SearchUnit>();  //名称含kw且通用
        //            List<SearchUnit> resultTmp2 = new List<SearchUnit>();   //名称含kw
        //            List<SearchUnit> resultTmp3 = new List<SearchUnit>();   //通用

        //            foreach (ScoreDoc sd in docs.ScoreDocs)//遍历搜索到的结果
        //            {
        //                Document doc = searcher.Doc(sd.Doc);

        //                string id = doc.Get("id");
        //                string name = GetHighLighter(kw, doc.Get("name"));
        //                string model = doc.Get("model");
        //                string detail = doc.Get("detail");
        //                string classid = doc.Get("classid");
        //                DateTime updatetime = Convert.ToDateTime(doc.Get("updatetime"));
        //                string hits = doc.Get("hits");
        //                bool iscommon = Convert.ToBoolean(doc.Get("iscommond"));
        //                SearchUnit su = new SearchUnit(id, name, model, detail, classid, updatetime, hits,iscommon);
        //                if (su.name.Contains(kw) && su.isCommon)
        //                {
        //                    resultTmp1.Add(su); //名称包含kw的提取且为通用文档,放于列表头
        //                }
        //                else if (su.name.Contains(kw))
        //                {
        //                    resultTmp2.Add(su); //名称包含kw,但非通用文档,优先级提高,置于resultTmp2
        //                }
        //                else if (su.isCommon)
        //                {
        //                    resultTmp3.Add(su); //没有关联内部产品分类类型,可能为通用文档,优先级提高,置于resultTmp3
        //                }
        //                else
        //                {
        //                    result.Add(su);
        //                }
        //            }

        //            if (resultTmp3.Count != 0)
        //            {
        //                foreach (SearchUnit sutmp in resultTmp3)
        //                {
        //                    result.Insert(0, sutmp);
        //                }
        //            }
        //            if (resultTmp2.Count != 0)
        //            {
        //                foreach (SearchUnit sutmp in resultTmp2)
        //                {
        //                    result.Insert(0, sutmp);
        //                }
        //            }
        //            if (resultTmp1.Count != 0)
        //            {
        //                foreach (SearchUnit sutmp in resultTmp1)
        //                {
        //                    result.Insert(0, sutmp);
        //                }
        //            }


        //        }

        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    throw e;
        //    //}
        //    return result;
        //}
        #endregion

        #region 删除全部索引数据 DeleteIndex
        //public bool DeleteIndex(string type)
        //{
        //    bool IsSuccess = true;
        //    try
        //    {
        //        IndexWriter writer = new IndexWriter(GetDirectory(type), analyzer, false, IndexWriter.MaxFieldLength.LIMITED);
        //        writer.DeleteAll();
        //        writer.Commit();
        //        //writer.Optimize();//
        //        IsSuccess = writer.HasDeletions();
        //        writer.Dispose();
        //    }
        //    catch
        //    {
        //        IsSuccess = false;
        //    }
        //    return IsSuccess;
        //}
        #endregion

        #region 索引目录 GetDirectory
        //public Lucene.Net.Store.FSDirectory GetDirectory(string type)
        //{
        //    DirectoryInfo dir = null;

        //    //string dirPath = System.Web.HttpContext.Current.Server.MapPath("/IndexData/" + type);
        //    string dirPath = System.Configuration.ConfigurationManager.AppSettings["IndexPath"].ToString() + "\\" + type;
            
        //    if (System.IO.Directory.Exists(dirPath) == false)
        //    {
        //        dir = System.IO.Directory.CreateDirectory(dirPath);
        //    }
        //    else
        //    {
        //        dir = new System.IO.DirectoryInfo(dirPath);
        //    }

        //    return Lucene.Net.Store.FSDirectory.Open(dir);
        //}
        #endregion

        #region 分析器 analyzer
        //private Analyzer _analyzer = null;
        //public Analyzer analyzer
        //{
        //    get
        //    {
        //        if (_analyzer == null)
        //        {
        //            _analyzer = new Lucene.Net.Analysis.PanGu.PanGuAnalyzer();
        //        }
        //        return _analyzer;
        //    }
        //}
        #endregion

        #region 版本号  version
        //private static Lucene.Net.Util.Version _version = Lucene.Net.Util.Version.LUCENE_30;
        //public Lucene.Net.Util.Version version
        //{
        //    get
        //    {
        //        return _version;
        //    }
        //}
        #endregion

        #region isNum
        public bool isNum(string kw)
        {
            try
            {
                int.Parse(kw);
                return true;
            }
            catch
            {
                return false;
            }

        }
        #endregion

        #region 高亮
        public string GetHighLighter(string kw, string content)
        {
            if (content == null || content == "")
                return content;

            if (content.Contains(kw))
            {
                content = content.Replace(kw, "<font style=\"color:red;\">" + kw + "</font>");
                return content;
            }
            string lighter = string.Empty;

            //创建HTMLFormatter,参数为高亮单词的前后缀
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter =
                new PanGu.HighLight.SimpleHTMLFormatter("<font style=\"color:red;\">", "</font>");
            //创建 Highlighter ，输入HTMLFormatter 和 盘古分词对象Semgent
            PanGu.HighLight.Highlighter highlighter =
                            new PanGu.HighLight.Highlighter(simpleHTMLFormatter,
                            new Segment());
            //设置每个摘要段的字符数
            highlighter.FragmentSize = 1000;
            //获取最匹配的摘要段
            lighter = highlighter.GetBestFragment(kw, content);
            if (lighter == "")
            {
                return content;
            }
            else
            {
                return lighter;
            }

        }
        #endregion

    }


}

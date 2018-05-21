using System.Collections.Generic;
using System.Text;
using Lucene.Net.Search;
using System.Text.RegularExpressions;

namespace WebApplication1.Controllers.Lucene
{
    public class SearchByLucene
    {
        //获得搜索结果数
        //public List<int> Search(string kw,string type)
        //{
        //    List<SearchUnit> list= LucenePGHelper.instance.Search(kw,type);
        //    List<int> result = new List<int>();
        //    foreach(SearchUnit su in list)
        //    {
        //        result.Add(int.Parse(su.id));
        //    }
        //    return result;
        //}

        //public List<SearchUnit> SearchByKw(string kw, string type)
        //{
        //    return LucenePGHelper.instance.Search(kw.ToUpper(), type);
        //}

        //public void CreateIndex(List<Test1> al,List<Test2> dl,List<Test3> vl)
        //{
        //    List<SearchUnit> listd = new List<SearchUnit>();
        //    foreach (Test1 d in dl)
        //    {
        //        bool iscommon = false;
                
        //        SearchUnit su = new SearchUnit(d.id.ToString(), d.showName, d.suitableModels, this.filters(d.description), d.classId.ToString(), d.publishDate, d.hits.ToString(),iscommon);
        //        listd.Add(su);    
        //    }
        //    LucenePGHelper.instance.CreateIndex(listd, "Test1");
        //    List<SearchUnit> lista = new List<SearchUnit>();
        //    foreach (Test2 a in al)
        //    {
        //        bool iscommon = false;
        //        if (a.suitableClasses != null && a.suitableClasses.Trim() != "")
        //        {
        //            iscommon = true;
        //        }
        //        SearchUnit su = new SearchUnit(a.id.ToString(), a.title, a.suitableModels, this.filters(a.detail), a.classId.ToString(), a.publishDate, a.hits.ToString(), iscommon);
        //        lista.Add(su);
        //    }
        //    LucenePGHelper.instance.CreateIndex(lista, "Test2");
        //    List<SearchUnit> listv = new List<SearchUnit>();
        //    foreach (Test3 v in vl)
        //    {
        //        bool iscommon = false;
                 
        //        SearchUnit su = new SearchUnit(v.id.ToString(), v.showName, v.keywords, this.filters(v.description), v.classId.ToString(), v.publishDate, v.hits.ToString(), iscommon);
        //        listv.Add(su);
        //    }
        //    LucenePGHelper.instance.CreateIndex(listv, "Test3");
        //}

        //删除所有索引
        //public void DeleteAllIndex()
        //{
        //    LucenePGHelper.instance.DeleteIndex("Test1");
        //    LucenePGHelper.instance.DeleteIndex("Test2");
        //    LucenePGHelper.instance.DeleteIndex("Test3");
        //}

        //过滤HTML标签
        public string filters(string source)
        {
            try
            {
                string result;
                result = source.Replace("\r", " ");
                result = result.Replace("\n", " ");
                result = result.Replace("'", " ");
                result = result.Replace("\t", string.Empty);
                result = Regex.Replace(result, @"( )+", " ");
                result = Regex.Replace(result, @"<( )*head([^>])*>", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"(<( )*(/)( )*head( )*>)", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(<head>).*(</head>)", string.Empty, RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*script([^>])*>", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"(<( )*(/)( )*script( )*>)", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"(<script>).*(</script>)", string.Empty, RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*style([^>])*>", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"(<( )*(/)( )*style( )*>)", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(<style>).*(</style>)", string.Empty, RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*td([^>])*>", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*br( )*>", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*li( )*>", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*div([^>])*>", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*tr([^>])*>", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<( )*p([^>])*>", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&nbsp;", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&bull;", " * ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&lsaquo;", "<", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&rsaquo;", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&trade;", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&frasl;", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"<", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @">", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&copy;", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&reg;", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, @"&(.{2,6});", string.Empty, RegexOptions.IgnoreCase);
                result = result.Replace("\n", " ");
                result = Regex.Replace(result, "(\r)( )+(\r)", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(\t)( )+(\t)", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(\t)( )+(\r)", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(\r)( )+(\t)", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(\r)(\t)+(\r)", " ", RegexOptions.IgnoreCase);
                result = Regex.Replace(result, "(\r)(\t)+", " ", RegexOptions.IgnoreCase);
                return result;
            }
            catch
            {
                return source;
            }

        }

        public string GetIdRange(List<int> idList)
        {
            if (idList.Count > 0)
            {
                StringBuilder idRange = new StringBuilder(@"(");
                for (int a = 0; a < idList.Count; a++)
                {
                    idRange.Append(idList[a]);
                    if (a != idList.Count - 1)
                    {
                        idRange.Append(@",");
                    }
                }
                idRange.Append(@")");

                return idRange.ToString();
            }
            else
            {
                return null;
            }
        }

    }
}

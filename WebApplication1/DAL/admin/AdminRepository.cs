using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using WebApplication1.Dal;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class AdminRepository : EntityRepository<r_admin>
    {
        public AdminRepository(context context)
            : base(context)
        { }

        public List<r_admin> Search(string[] keywords)
        {
            StringBuilder su = new StringBuilder(@"select id,title,publishDate,hits,displayOrder from Article where visible = 1 and _typeId<>2 ");
            if (keywords.Length > 0)
            {
                su.Append("and (");
                for (int i = 0; i < keywords.Length; i++)
                {
                    //su.Append(" (title like '%" + keywords[i] + "%' or suitableModels like '%" + keywords[i] + "%' or dbo.regexReplace(detail,'<[^<^>]*>','',1,1) like '%" + keywords[i] + "%') " + AND_OR + " ");
                    su.Append(" (title like '%" + keywords[i] + "%' or suitableModels like '%" + keywords[i] + "%') " + "or" + " ");
                    if (i == keywords.Length - 1) { su.Append("1=2)"); }
                }
            }
            su.Append(" order by publishDate desc");
            return context.Database.SqlQuery<r_admin>(su.ToString()).ToList<r_admin>();
        }

        //注意一下两者的区别：list对应contain，class对应equals
        //public List<PPTItem> getPPTItemByParentClassId(int parentClassId)
        //{
        //    List<PPTItem> result;
        //    //StringBuilder su = new StringBuilder(string.Format(@"select pi.* from PPTItem pi inner join PPTClass pc on pi.classId = pc.id where pi.visible = 1 and pc.parentClassId = {0}", parentClassId.ToString()));
        //    //result = context.Database.SqlQuery<PPTItem>(su.ToString()).ToList<PPTItem>();
        //    List<int> classIds = context.PPTClasses.Where(t => t.parentClassId == parentClassId).Select(t => t.id).ToList();
        //    result = context.PPTItems.Where(d => classIds.Contains(d.classId)).ToList<PPTItem>();

        //    return result;
        //}
        
        //public List<PPTItem> getPPTItemInIndexOther()
        //{
        //    List<PPTItem> result;
        //    //StringBuilder su = new StringBuilder(@"select pi.* from PPTItem pi inner join PPTClass pc on pi.classId = pc.id where pi.visible = 1 and pc.classTip = 'other' and pc.parentClassId = 0");
        //    //result = context.Database.SqlQuery<PPTItem>(su.ToString()).ToList<PPTItem>();
        //    PPTClass PPTClass = context.PPTClasses.Where(t => t.classTip.Equals("other") && t.parentClassId == 0).First();
        //    result = context.PPTItems.Where(d => d.classId.Equals(PPTClass.id) && d.visible).ToList<PPTItem>();
        //    return result;
        //}

        //public List<ArticlePart> GetArticleListByClassTip(int pageIndex, int pageSize, string classTip, int orderType, ref int pageCount)
        //{
        //    List<ArticlePart> result;
        //    ArticleClass articleClass = context.ArticleClasses.Where(t => t.classTip.Equals(classTip)).First();

        //    //context.Configuration.LazyLoadingEnabled = true;
        //    //int recordCount = context.ArticleClasses.Include(t => t.Articles).Where(t => t.classTip == classTip).First().Articles.Where(d => d.visible && d.C_typeId != 2).Count();
        //    int recordCount = context.Articles.Where(d => d.classId.Equals(articleClass.id) && d.visible == 1 && d.C_typeId != 2).Count();
        //    pageCount = recordCount % pageSize == 0 ? recordCount / pageSize : recordCount / pageSize + 1;
        //    if (pageIndex > pageCount)
        //    {
        //        pageIndex = pageCount;
        //    }
        //    if (orderType == 0)
        //    {
        //        result = context.Articles.Where(d => d.classId.Equals(articleClass.id) && d.visible == 1 && d.C_typeId != 2)
        //            .OrderByDescending(d => d.publishDate).Skip((pageIndex - 1) * pageSize).Take(pageSize)
        //            .Select(d => new ArticlePart
        //            {
        //                id = d.id,
        //                title = d.title,
        //                displayOrder = d.displayOrder,
        //                hits = d.hits,
        //                publishDate = d.publishDate
        //            }).ToList<ArticlePart>();
        //    }
        //    else
        //    {
        //        result = context.Articles.Where(d => d.classId.Equals(articleClass.id) && d.visible == 1 && d.C_typeId != 2)
        //            .OrderByDescending(d => d.hits).Skip((pageIndex - 1) * pageSize).Take(pageSize)
        //            .Select(d => new ArticlePart
        //            {
        //                id = d.id,
        //                title = d.title,
        //                displayOrder = d.displayOrder,
        //                hits = d.hits,
        //                publishDate = d.publishDate
        //            }).ToList<ArticlePart>();
        //    }
        //    //context.Configuration.LazyLoadingEnabled = false;
        //    return result;
        //}
    }
}
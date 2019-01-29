using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Dal;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class UEditorRepository : EntityRepository<ueditor>
    {
        public UEditorRepository(context context)
            : base(context)
        { }
    }
}
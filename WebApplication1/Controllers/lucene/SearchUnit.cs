using System;

namespace WebApplication1.Controllers.Lucene
{
    public class SearchUnit
    {
        public SearchUnit() { }
        public SearchUnit(string _id, string _name, string _model, string _detail,DateTime _time)
        {
            this.id = _id;
            this.name = _name;
            this.model = _model;
            this.detail = _detail;
            this.updatetime = _time;

        }
        public string id { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public string detail { get; set; }
        public DateTime updatetime { get; set; }
    }
}
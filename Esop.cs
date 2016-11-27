using System.Collections.Generic;

namespace WebApplication1
{
    public class Esop
    {
        public string Ver
        { get; set; }

        public string Stage
        { get; set; }

        public string Pn
        { get; set; }

        public List<EsopDetail> EsopDetailList;
    }

    public class EsopDetail
    {
        public string Pn
        { get; set; }

        public string Stage
        { get; set; }
        
        public string FileName
        { get; set; }

        public string Folder
        { get; set;    }

        public string Type
        { get; set; }
    }
}
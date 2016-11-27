namespace WebApplication1
{
    public class PlmSop
    {
        public PlmSop(string filePath)
        {
            this.FileName = System.IO.Path.GetFileName(filePath);
            this.Path = System.IO.Path.GetDirectoryName(filePath);

            int verIdx = this.FileName.IndexOf("Ver");
            int endIdx = this.FileName.LastIndexOf(".");
            
            if (verIdx > -1)
            {
                verIdx += 3;
                int getLength = endIdx - verIdx;
                this.Ver = this.FileName.Substring(verIdx, getLength);

                if (this.Ver.IndexOf("_") > -1)
                {
                    this.Ver = this.Ver.Substring(0, this.Ver.IndexOf("_"));
                }

                //從 Ver 之後找"_" 為 stage name
                int stgIdx = FileName.IndexOf("_", verIdx);
                if (stgIdx > -1)
                {
                    stgIdx += 1;
                    getLength = endIdx - stgIdx;
                    this.Stage = FileName.Substring(stgIdx, getLength);

                }
            }

            
            

            
                


        }

        
        public string FileName
        { get; set; }

        public string Path
        { get; set; }

        public string Ver
        { get; set; }

        public string Stage
        { get; set; }
    }
}
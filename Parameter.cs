using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPU_OTOMASYON
{
    class Parameter
    {

     
        // Excel den okunana verileri globalde tutmak icin bu Class olusturuldu
        public string variableName { get; set; }
        public string name { get; set; }
        public string variableType { get; set; }
        public string iotype { get; set; }  // read-write


        public Parameter(string varName, string parName, string type, string iotype)
        {



            this.variableName = varName;
            this.name = parName;
            this.variableType = type;
            this.iotype = iotype;

        }



    }
}

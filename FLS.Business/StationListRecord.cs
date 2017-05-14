using FLS.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FLS.Business
{
    public class StationListRecord 
    {                
        
        public long StationId { get; set; }
        public string StationName { get; set; }
        public string StationDesc { get; set; }
        public string StationAddress { get; set; }
        public string TaxCode { get; set; }
        public StationListRecord()
        {
            StationId = 0;
            StationName = string.Empty;
            StationDesc = string.Empty;
            StationAddress = string.Empty;
            TaxCode = string.Empty;
        }       
        
    }
}

using FuelLevelSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelLevelSystem.ViewModel
{
    class ViewModelImportReport : ViewModelBase
    {
        private ImportReport importReportData;
        public ImportReport ImportReportData
        {
            get { return importReportData; }
            set { SetProperty(ref importReportData, value); }
        }
        public ViewModelImportReport()
        {
            importReportData = new ImportReport();
            importReportData.StationName = @"Tín Nghĩa";
            importReportData.ReportDate = DateTime.Now;
            importReportData.StaffName = @"Nguyễn Văn A";
            importReportData.StaffPos = @"Trạm trưởng";
            importReportData.TemplateBottle = @"DSHFKJSHFKJ";
            importReportData.DriverName = "dhfkshfkjs";
            importReportData.CarPlate = "dfsjlfjs";
            importReportData.BeginCapacity = 100;
            importReportData.BeginLevel = 1.05;
            importReportData.BeginTotalPump = 748274947;
            importReportData.EndCapacity = 10000;
            importReportData.EndLevel = 100.05;
            importReportData.EndTotalPump = 748274947;
            importReportData.ImportCapacity = 10000;
            importReportData.RealCapacity = 9900;
            importReportData.Fuel = @"Xăng A92";
        }

        public ViewModelImportReport(ImportReport importReportData)
        {
            this.importReportData = importReportData;
        }
    }
}

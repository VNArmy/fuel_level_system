using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace FLS.Business
{
    public interface ICalibrationTabDAL
    {
        /// <summary>
        /// Return all the persistent customers
        /// </summary>
        List<CalibrationRecord> GetCalibrationTab(long tankid);


        void ImportCalibrationTab(String fileName, long tankid);

        /// <summary>
        /// Updates or adds the given customer
        /// </summary>
        void UpdateCalibrationTab(CalibrationRecord record);

        /// <summary>
        /// Delete the given customer
        /// </summary>
        void DeleteCalibrationTab(CalibrationRecord record);
    }

    public class CalibrationTabDAL: ICalibrationTabDAL
    {
        public CalibrationTabDAL()
        {
            BusinessHelper.InitConnection();
        }
        /// <summary>
        /// Return all the persistent customers
        /// </summary>
        public List<CalibrationRecord> GetCalibrationTab(long tankid)
        {
            List<CalibrationRecord> collec = new List<CalibrationRecord>();
            DataTable dt = BusinessHelper.LoadCalibration((long)tankid);
            foreach (DataRow _row in dt.Rows)
            {
                CalibrationRecord cr = new CalibrationRecord();
                cr.PK = Convert.ToUInt64(_row["pk_id"]);
                cr.TankId = Convert.ToInt64(_row["tankid"]);
                cr.Raw = Convert.ToInt32(_row["raw"]);
                cr.Level = Convert.ToInt32(_row["level"]);                               
               
                collec.Add(cr);
            }

            return collec;

        }
        public void ImportCalibrationTab(String fileName,long tankid)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                return;
            }
            try
            {
                Excel.Application excelApp = new Excel.Application();
                Excel.Workbook workbook;
                Excel.Worksheet worksheet;
                Excel.Range range;
                workbook = excelApp.Workbooks.Open(fileName);
                worksheet = (Excel.Worksheet)workbook.Sheets[1];

                int column = 0;
                int row = 0;

                range = worksheet.UsedRange;
                DataTable dt = new DataTable();
                dt.Columns.Add("Raw");
                dt.Columns.Add("Level");
                

                for (row = 2; row <= range.Rows.Count; row++)
                {
                    DataRow dr = dt.NewRow();
                    for (column = 1; column <= range.Columns.Count; column++)
                    {
                        dr[column - 1] = (range.Cells[row, column] as Excel.Range).Value2.ToString();
                    }
                    dt.Rows.Add(dr);
                    dt.AcceptChanges();
                }
                workbook.Close(true, Missing.Value, Missing.Value);
                excelApp.Quit();


                foreach (DataRow _row in dt.Rows)
                {
                    CalibrationRecord cr = new CalibrationRecord();
                    //ir.PK = Convert.ToUInt64(_row["pk_id"]);
                    cr.TankId = tankid;//Convert.ToInt64(_row["pumpid"]);
                    cr.Raw = Convert.ToInt32(_row["raw"]);
                    cr.Level = Convert.ToInt32(_row["Level"]);
                    
                    UpdateCalibrationTab(cr);
                }

            }
            catch (Exception)
            {
                
                throw;
            }
            
            
            
        }
        /// <summary>
        /// Updates or adds the given customer
        /// </summary>
        public void UpdateCalibrationTab(CalibrationRecord record)
        {
            long pkid = (long)record.PK;
            try
            {
                BusinessHelper.InserUpdateCalibrationTab(ref pkid, (long)record.TankId, record.Raw, record.Level);
                if (pkid != -1)
                {
                    record.PK = (UInt64)pkid;
                }
                
            }
            catch (Exception)
            {
                
                throw;
            }
            
        }

        /// <summary>
        /// Delete the given customer
        /// </summary>
        public void DeleteCalibrationTab(CalibrationRecord record)
        {
            long pkid = (long)record.PK;
            try
            {
                BusinessHelper.DeleteCalibrationTab(pkid);

            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
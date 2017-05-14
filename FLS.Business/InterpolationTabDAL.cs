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
    public interface IInterpolationTabDAL
    {
        /// <summary>
        /// Return all the persistent customers
        /// </summary>
        List<InterpolationRecord> GetInterpolationTab(long tankid);


        void ImportInterpolationTab(String fileName, long tankid);

        /// <summary>
        /// Updates or adds the given customer
        /// </summary>
        void UpdateInterpolationTab(InterpolationRecord record);

        /// <summary>
        /// Delete the given customer
        /// </summary>
        void DeleteInterpolationTab(InterpolationRecord record);
    }

    public class InterpolationTabDAL: IInterpolationTabDAL
    {
        public InterpolationTabDAL()
        {
            BusinessHelper.InitConnection();
        }
        /// <summary>
        /// Return all the persistent customers
        /// </summary>
        public List<InterpolationRecord> GetInterpolationTab(long tankid)
        {
            List<InterpolationRecord> collec = new List<InterpolationRecord>();
            DataTable dt = BusinessHelper.LoadInterpolation((long)tankid);
            foreach (DataRow _row in dt.Rows)
            {
                InterpolationRecord ir = new InterpolationRecord();
                ir.PK = Convert.ToUInt64(_row["pk_id"]);
                ir.TankId = Convert.ToInt64(_row["pumpid"]);
                ir.Level = Convert.ToDouble(_row["in_level"]);
                ir.Capacity = Convert.ToDouble(_row["out_cap"]);

                collec.Add(ir);
            }

            return collec;

        }
        public void ImportInterpolationTab(String fileName,long tankid)
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

                dt.Columns.Add("Level");
                dt.Columns.Add("Capacity");

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
                    InterpolationRecord ir = new InterpolationRecord();
                    //ir.PK = Convert.ToUInt64(_row["pk_id"]);
                    ir.TankId = tankid;//Convert.ToInt64(_row["pumpid"]);
                    ir.Level = Convert.ToDouble(_row["Level"]);
                    ir.Capacity = Convert.ToDouble(_row["Capacity"]);

                    UpdateInterpolationTab(ir);
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
        public void UpdateInterpolationTab(InterpolationRecord record)
        {
            long pkid = (long)record.PK;
            try
            {
                BusinessHelper.InserUpdateInterpolationTab(ref pkid, (long)record.TankId, record.Level, record.Capacity);
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
        public void DeleteInterpolationTab(InterpolationRecord record)
        {
            long pkid = (long)record.PK;
            try
            {
                BusinessHelper.DeleteInterpolationTab(pkid);

            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
using DALC4NET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS.Business
{
    public class BusinessHelper
    {
        static private DBHelper _dbHelper = null;
        //public BusinessHelper()
        //{
            
        //}
        static public void InitConnection()
        {
            _dbHelper = new DBHelper();
            
        }
        static public void CloseConnection()
        {
            _dbHelper.GetConnObject().Close();
            
        }
        static public DataTable AssignedRoles(string username)
        {
            DataTable table = new DataTable();
            string sqlCommand = String.Format(@"SELECT DISTINCT role.name AS Role FROM user INNER JOIN user_role USING (user_id) INNER JOIN role USING (role_id) WHERE user.username = '{0}'",username);
            try
            {
                table = _dbHelper.ExecuteDataTable(sqlCommand);
            }
            catch (Exception)
            {
                //MessageBox.Show(err.Message);
                throw;
            }

            return table;
        }
        static public DataTable ListAllUsers()
        {
            DataTable table = new DataTable();
            string sqlCommand = String.Format(@"SELECT * FROM `user`");
            try
            {
                table = _dbHelper.ExecuteDataTable(sqlCommand);
            }
            catch (Exception)
            {
                //MessageBox.Show(err.Message);
                throw;
            }

            return table;

        }
        static public DataTable UserPermissions(string user)
        {
            DataTable table = new DataTable();
            string sqlCommand = String.Format("SELECT DISTINCT role.name AS Role, permission.name AS Permission FROM permission INNER JOIN role_permission USING (permission_id) INNER JOIN role USING (role_id) INNER JOIN user_role USING (role_id) INNER JOIN user USING (user_id) WHERE user.username = '{0}'",user);
            try
            {
                table = _dbHelper.ExecuteDataTable(sqlCommand);
            }
            catch (Exception)
            {
                //MessageBox.Show(err.Message);
                throw;
            }

            return table;
        }
        static public DataTable ListAllRolls(long userid)
        {
            DataTable table = new DataTable();
            string sqlCommand = String.Format(@"SELECT * FROM `user`");
            try
            {
                table = _dbHelper.ExecuteDataTable(sqlCommand);
            }
            catch (Exception)
            {
                //MessageBox.Show(err.Message);
                throw;
            }

            return table;

        }
        static public DataTable ListAllTank(long stationid)
        {
            DataTable table = new DataTable();
            string sqlCommand = String.Format(@"select stationlist.stationname, tanklist.* from stationlist, tanklist where stationlist.stationid = {0} and tanklist.stationid = {0}", stationid);

            try
            {
                table = _dbHelper.ExecuteDataTable(sqlCommand);
            }
            catch (Exception )
            {
                throw;
                //MessageBox.Show(err.Message);
            }

            return table;
        }

        //static public DataTable ListAllPump(long stationid)
        //{
        //    DataTable table = new DataTable();
        //    string sqlCommand = String.Format(@"SELECT * FROM `pumplist` INNER JOIN (SELECT tankid FROM `tanklist` WHERE `tanklist`.stationid = {0} ) AS tl ON `pumplist`.tankid = `tl`.tankid LEFT JOIN (SELECT pickuptimes,pumpid FROM `pumpdata` where id in (select max(id) FROM `pumpdata` GROUP BY pumpid) )AS last_pump_data ON `pumplist`.pumpid = `last_pump_data`.pumpid", stationid);

        //    try
        //    {
        //        table = _dbHelper.ExecuteDataTable(sqlCommand);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //        //MessageBox.Show(err.Message);
        //    }

        //    return table;
        //}

        static public DataTable ListAllPump(long stationid)
        {
            DataTable dt = new DataTable();
            DBParameter param1 = new DBParameter("@stId", stationid);


            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(param1);

            IDbTransaction transaction = _dbHelper.BeginTransaction();
            string message = string.Empty;
            try
            {
                dt = _dbHelper.ExecuteDataTable(@"ListAllPump",
                    paramCollection, CommandType.StoredProcedure);


                message = dt.Rows.Count > 0 ? "Record listed successfully." :
                    "Error in listing record.";
            }
            catch (Exception err)
            {
                message = err.Message;
                return null;
            }
            Console.WriteLine(message);
            return dt;
        }

        static public DataTable AddMqttServer(string hostname,int hostport,string username,string securepassword)
        {
            DataTable table = new DataTable();
            string sqlCommand = String.Format(@"INSERT INTO `mqttserver`( `hostname`, `hostport`, `username`, `password`) VALUES ('{0}',{1},'{2}','{3}')",
                hostname,hostport,username,securepassword);

            try
            {
                table = _dbHelper.ExecuteDataTable(sqlCommand);
            }
            catch (Exception)
            {

                //MessageBox.Show(err.Message);
                throw;
            }

            return table;
        }

        static public DataTable ListAllFuel()
        {
            DataTable table = new DataTable();
            string sqlCommand = String.Format(@"select * from fuellist");

            try
            {
                table = _dbHelper.ExecuteDataTable(sqlCommand);
            }
            catch (Exception)
            {
                //MessageBox.Show(err.Message);
                throw;
            }

            return table;
        }
        static public DataTable GetServerInfo()
        {
            DataTable table = new DataTable();
            string sqlCommand = String.Format(@"SELECT * FROM `mqttserver`");

            try
            {
                table = _dbHelper.ExecuteDataTable(sqlCommand);
            }
            catch (Exception )
            {
                //MessageBox.Show(err.Message);
                throw;
            }

            return table;
        }
        static public DataTable ListAllStation(long cityid)
        {
            DataTable table = new DataTable();
            string sqlCommand;
            if (cityid != -1)
            {
                sqlCommand = String.Format(@"SELECT * FROM stationlist WHERE stationid = {0}", cityid);
            }
            else
            {
                sqlCommand = String.Format(@"SELECT * FROM stationlist");
            }

            try
            {
                table = _dbHelper.ExecuteDataTable(sqlCommand);
            }
            catch (Exception )
            {
                //MessageBox.Show(err.Message);
                throw;
            }

            return table;
        }
        static public DataTable GetTankList(long tankid)
        {
            DataTable table = new DataTable();
            string sqlCommand = String.Format(@"select * from tanklist where tankid = {0}", tankid);

            try
            {
                table = _dbHelper.ExecuteDataTable(sqlCommand);
            }
            catch (Exception)
            {
                throw;
                //MessageBox.Show(err.Message);
            }

            return table;
        }
        static public void UpdateTankStatus(long pumpid)
        {
            String sqlCmd = @"UPDATE `tankdata` SET Status = 1 WHERE TankId = @pId ORDER BY Id DESC LIMIT 1";
            string message = string.Empty;
            DBParameter param1 = new DBParameter("@pId", pumpid);
            DBParameterCollection paramCollection = new DBParameterCollection();          
            paramCollection.Add(param1);

            
            IDbTransaction transaction = _dbHelper.BeginTransaction();
            try
            {
                IDataReader objScalar =_dbHelper.ExecuteDataReader(sqlCmd, paramCollection,transaction, CommandType.Text);
                if (objScalar!=null)
                {
                    objScalar.Close();
                    objScalar.Dispose();

                }
                _dbHelper.CommitTransaction(transaction);
               
            }
            catch (Exception err)
            {
                message = err.Message;
                _dbHelper.RollbackTransaction(transaction);
            }
        }
        static public void SaveTankData(long tankid,Double capacity,Double thermo,Double water,Byte status,DateTime datetime)
        {
            int rowsAffected = 0;
            DBParameter param1 = new DBParameter("@tankid", tankid);
            DBParameter param2 = new DBParameter("@capacity", capacity);
            DBParameter param3 = new DBParameter("@thermo", thermo);
            DBParameter waterparam = new DBParameter("@water", water);
            string sDatetime = datetime.ToString(@"yyyy-MM-dd HH:mm");
            DBParameter param4 = new DBParameter("@savedtime", sDatetime);
            DBParameter param5 = new DBParameter("@st", status);

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(param1);
            paramCollection.Add(param2);
            paramCollection.Add(param3);
            paramCollection.Add(waterparam);
            paramCollection.Add(param4);
            paramCollection.Add(param5);
            IDbTransaction transaction = _dbHelper.BeginTransaction();
            string message = string.Empty;
            try
            {
                rowsAffected = _dbHelper.ExecuteNonQuery(@"InserNewData",
                    paramCollection, transaction, CommandType.StoredProcedure);
                message = rowsAffected > 0 ? "Record inserted successfully." :
                    "Error in inserting record.";
                _dbHelper.CommitTransaction(transaction);
            }
            catch (Exception )
            {
                _dbHelper.RollbackTransaction(transaction);
                throw;
            }
            Console.WriteLine(message);
        }
        static public DataTable LoadInterpolation(long tankId)
        {
            String sqlCmd = String.Format(@"SELECT * FROM `interpolation` WHERE `tankid`={0} ORDER BY `in_level`;", tankId);
            DataTable dt = new DataTable();
            try
            {
                dt = _dbHelper.ExecuteDataTable(sqlCmd);
                return dt;
            }
            catch (Exception)
            {

                //throw;
                return null;
            }
        }
        static public DataTable ListTankDetails(Int64 tankid,DateTime dtFrom,DateTime dtTo)
        {
            DataTable dt = new DataTable();
            DBParameter param1 = new DBParameter("@tid", tankid);
            string sFrom = dtFrom.ToString(@"yyyy-MM-dd HH:mm");
            DBParameter param2 = new DBParameter("@fromdate", sFrom);
            string sTo = dtTo.ToString(@"yyyy-MM-dd HH:mm");
            DBParameter param3 = new DBParameter("@todate", sTo);

            DBParameterCollection paramCollection = new DBParameterCollection();
            paramCollection.Add(param1);
            paramCollection.Add(param2);
            paramCollection.Add(param3);
            
            string message = string.Empty;
            try
            {
                dt = _dbHelper.ExecuteDataTable(@"ListTankDetail",
                    paramCollection, CommandType.StoredProcedure) ;

                
                message = dt.Rows.Count > 0 ? "Record listed successfully." :
                    "Error in listing record.";
            }
            catch (Exception err)
            {
                message = err.Message;
                return null;
            }
            Console.WriteLine(message);
            return dt;
        }
        static public void InserUpdateStation(ref long stationid, string name,string desc, string address, string taxcode)
        {
            String sqlCmd = string.Format(@"INSERT INTO `stationlist` ( `stationname`,`stationdesc`,`stationaddress`, `stationtaxcode`) VALUES('{0}', '{1}', '{2}','{3}');select LAST_INSERT_ID();",
                name,desc,address,taxcode);
            String sqlUpdateCmd =string.Format( @"INSERT INTO `stationlist` (`stationid`, `stationname`,`stationdesc`,`stationaddress`, `stationtaxcode`) VALUES({0}, '{1}', '{2}','{3}','{4}') ON DUPLICATE KEY UPDATE `stationname`= '{1}', `stationdesc`= '{2}', `stationaddress`= '{3}',`stationtaxcode` = '{4}';",stationid,name,desc,address,taxcode);
            int rowsAffected = 0;
            object o = null;
            IDbTransaction transaction = _dbHelper.BeginTransaction();
            string message = string.Empty;
            try
            {
                if (stationid > 0)
                {
                    rowsAffected = _dbHelper.ExecuteNonQuery(sqlUpdateCmd, transaction);
                }
                else
                {
                    o = _dbHelper.ExecuteScalar(sqlCmd, transaction);
                    if (o != null)
                    {
                        stationid = Convert.ToInt64(o);
                        rowsAffected = 1;
                    }
                    else
                    {
                        rowsAffected = 0;
                        stationid = -1;
                    }
                }
               
                
                
                message = rowsAffected > 0 ? "Record inserted successfully." :
                    "Error in inserting record.";
                _dbHelper.CommitTransaction(transaction);
            }
            catch (Exception)
            {
                _dbHelper.RollbackTransaction(transaction);
                throw;
            }
            Console.WriteLine(message);
        }

        static public void DeleteStation(long stationid)
        {
            String sqlCmd = string.Format(@"DELETE FROM `stationlist` WHERE `stationid`={0};", stationid);

            int rowsAffected = 0;

            IDbTransaction transaction = _dbHelper.BeginTransaction();
            string message = string.Empty;
            try
            {
                if (stationid > 0)
                {
                    rowsAffected = _dbHelper.ExecuteNonQuery(sqlCmd, transaction);
                }
                message = rowsAffected > 0 ? "Record deleted successfully." :
                    "Error in deleting record.";
                _dbHelper.CommitTransaction(transaction);

            }
            catch (Exception)
            {
                _dbHelper.RollbackTransaction(transaction);
                throw;
            }
            Console.WriteLine(message);
        }

        static public void InserUpdateTank(ref long tankid, long stationid, long fuelid, string name, string desc, Double max, Double offset)
        {
            String sqlCmd = string.Format(@"INSERT INTO `tanklist` ( `stationid`,`fuelid`,`tankname`, `tankdesc`,`capacity`,`offset`) VALUES({0}, {1}, '{2}','{3}',{4},{5});select LAST_INSERT_ID();", 
                stationid,fuelid,name, desc, max,offset);
            String sqlUpdateCmd = string.Format(@"INSERT INTO `tanklist` (`tankid`, `stationid`,`fuelid`,`tankname`, `tankdesc`,`capacity`,`offset`) VALUES({0}, {1}, {2},'{3}','{4}',{5},{6}) ON DUPLICATE KEY UPDATE `stationid`= {1}, `fuelid`= {2},`tankname` = '{3}',`tankdesc`='{4}',`capacity`={5},`offset`={6};",
                tankid, stationid,fuelid, name, desc, max,offset);
            int rowsAffected = 0;

            IDbTransaction transaction = _dbHelper.BeginTransaction();
            string message = string.Empty;
            object o = null;
            try
            {
                if (tankid > 0)
                {
                    rowsAffected = _dbHelper.ExecuteNonQuery(sqlUpdateCmd, transaction);
                }
                else
                {
                    o = _dbHelper.ExecuteScalar(sqlCmd, transaction);
                    if (o != null)
                    {
                        tankid = Convert.ToInt64(o);
                        rowsAffected = 1;
                    }
                    else
                    {
                        rowsAffected = 0;
                        tankid = -1;
                    }
                }
                

                message = rowsAffected > 0 ? "Record inserted successfully." :
                    "Error in inserting record.";
                _dbHelper.CommitTransaction(transaction);
            }
            catch (Exception err)
            {
                _dbHelper.RollbackTransaction(transaction);
                message = err.Message;
                throw;
            }
            Console.WriteLine(message);
        }

        static public void InserUpdatePump(ref long pumpid,  long tankid, string name,string ipadress,int port,int delaytime, string desc,int status)
        {
            String sqlCmd = string.Format(@"INSERT INTO `pumplist` (`pumpid`, `name`,`ipaddress`,`port`,`delaytime`, `description`,`status`) VALUES({0}, '{1}', '{2}',{3},{4},'{5}',{6});select LAST_INSERT_ID();",
                                                                    tankid ,name,ipadress,port,delaytime, desc,status);
            String sqlUpdateCmd = string.Format(@"INSERT INTO `pumplist` (`pumpid`,`tankid`, `name`,`ipaddress`,`port`,`delaytime`, `description`,`status`) VALUES({0}, {1}, '{2}','{3}',{4},{5},'{6}',{7}) ON DUPLICATE KEY UPDATE `tankid`={1}, `name`='{2}',`ipaddress`='{3}',`port`={4},`delaytime`={5}, `description`='{6}',`status`={7}",
                                                                    pumpid,tankid, name, ipadress, port, delaytime, desc, status);
                
            int rowsAffected = 0;

            IDbTransaction transaction = _dbHelper.BeginTransaction();
            string message = string.Empty;
            object o = null;
            try
            {
                if (tankid > 0)
                {
                    rowsAffected = _dbHelper.ExecuteNonQuery(sqlUpdateCmd, transaction);
                }
                else
                {
                    o = _dbHelper.ExecuteScalar(sqlCmd, transaction);
                    if (o != null)
                    {
                        tankid = Convert.ToInt64(o);
                        rowsAffected = 1;
                    }
                    else
                    {
                        rowsAffected = 0;
                        tankid = -1;
                    }
                }


                message = rowsAffected > 0 ? "Record inserted successfully." :
                    "Error in inserting record.";
                _dbHelper.CommitTransaction(transaction);
            }
            catch (Exception err)
            {
                _dbHelper.RollbackTransaction(transaction);
                message = err.Message;
                throw;
            }
            Console.WriteLine(message);
        }
        
        static public void DeleteTank(long tankid)
        {
            String sqlCmd = string.Format(@"DELETE FROM `tanklist` WHERE `tankid`={0};", tankid);

            int rowsAffected = 0;

            IDbTransaction transaction = _dbHelper.BeginTransaction();
            string message = string.Empty;
            try
            {
                if (tankid > 0)
                {
                    rowsAffected = _dbHelper.ExecuteNonQuery(sqlCmd, transaction);
                }
                message = rowsAffected > 0 ? "Record deleted successfully." :
                    "Error in deleting record.";
                _dbHelper.CommitTransaction(transaction);

            }
            catch (Exception err)
            {
                _dbHelper.RollbackTransaction(transaction);
                message = err.Message;
                throw;
            }
            Console.WriteLine(message);
        }
        static public void DeletePump(long pumpid)
        {
            String sqlCmd = string.Format(@"DELETE FROM `pumplist` WHERE `pumpid`={0};", pumpid);

            int rowsAffected = 0;

            IDbTransaction transaction = _dbHelper.BeginTransaction();
            string message = string.Empty;
            try
            {
                if (pumpid > 0)
                {
                    rowsAffected = _dbHelper.ExecuteNonQuery(sqlCmd, transaction);
                }
                message = rowsAffected > 0 ? "Record deleted successfully." :
                    "Error in deleting record.";
                _dbHelper.CommitTransaction(transaction);

            }
            catch (Exception err)
            {
                _dbHelper.RollbackTransaction(transaction);
                message = err.Message;
                throw;
            }
            Console.WriteLine(message);
        }
        static public void InserUpdateInterpolationTab(ref long pk_id, long tankid, Double level,Double cap)
        {
            String sqlCmd = string.Format(@"INSERT INTO `interpolation` ( `tankid`,`in_level`,`out_cap`) VALUES({0}, {1}, {2});select LAST_INSERT_ID();",
                tankid, level, cap);
            String sqlUpdateCmd = string.Format(@"INSERT INTO `interpolation` (`pk_id`, `tankid`,`in_level`,`out_cap`) VALUES({0}, {1}, {2},{3}) ON DUPLICATE KEY UPDATE `tankid`= {1}, `in_level`= {2},`out_cap` = {3};",
                pk_id,tankid, level,cap);
            int rowsAffected = 0;
            object o = null;
            IDbTransaction transaction = _dbHelper.BeginTransaction();
            string message = string.Empty;
            try
            {
                if (pk_id > 0)
                {
                    rowsAffected = _dbHelper.ExecuteNonQuery(sqlUpdateCmd, transaction);
                    
                }
                else
                {
                    //rowsAffected = _dbHelper.ExecuteNonQuery(sqlCmd, transaction);
                    o = _dbHelper.ExecuteScalar(sqlCmd,transaction);
                    if (o != null)
                    {
                        pk_id = Convert.ToInt64(o);
                        rowsAffected = 1;
                    }
                    else
                    {
                        rowsAffected = 0;
                        pk_id = -1;
                    }
                    
                }
                
                message = rowsAffected > 0 ? "Record inserted successfully." :
                    "Error in inserting record.";
                _dbHelper.CommitTransaction(transaction);
                Console.WriteLine(o);
                //if ((pk_id==0)&&(rowsAffected > 0))
                //{
                //    object o = _dbHelper.ExecuteScalar("select LAST_INSERT_ID();");
                //    pk_id = Convert.ToInt64(o);

                //}
            }
            catch (Exception err)
            {
                _dbHelper.RollbackTransaction(transaction);
                message = err.Message;
                throw;
            }
            Console.WriteLine(message);
        }
        static public void DeleteInterpolationTab(long pk_id)
        {
            String sqlCmd = string.Format(@"DELETE FROM `interpolation` WHERE `pk_id`={0};", pk_id);
            
            int rowsAffected = 0;
            
            IDbTransaction transaction = _dbHelper.BeginTransaction();
            string message = string.Empty;
            try
            {
                if (pk_id > 0)
                {
                    rowsAffected = _dbHelper.ExecuteNonQuery(sqlCmd, transaction);
                }               

                message = rowsAffected > 0 ? "Record deleted successfully." :
                    "Error in deleting record.";
                _dbHelper.CommitTransaction(transaction);
                
            }
            catch (Exception)
            {
                _dbHelper.RollbackTransaction(transaction);
                throw;
            }
            Console.WriteLine(message);
        }
    }
}

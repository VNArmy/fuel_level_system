using FLS.Business;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelSupervisorSetting.ViewModel
{
    public delegate void CalibrationPersistenceErrorHandler(CalibrationTabDataProvider dataProvider, Exception e);

    public class CalibrationTabDataProvider
    {
        ICalibrationTabDAL dataAccessLayer;
        long currTankId;
        public CalibrationTabDataProvider()
        {
            dataAccessLayer = new CalibrationTabDAL();
        }

        public CalibrationTabUIObjects GetCalibrationTab(long tankid)
        {
            //if (!(String.IsNullOrEmpty(fileName)))
            //{
            //     dataAccessLayer.ImportCalibrationTab(fileName, pumpid);
            //}
            currTankId = tankid;
            // populate our list of customers from the data access layer
            CalibrationTabUIObjects iTabObjs = new CalibrationTabUIObjects();

            List<CalibrationRecord> iTabDataObjects = dataAccessLayer.GetCalibrationTab(tankid);
            foreach (CalibrationRecord iTabDataObject in iTabDataObjects)
            {
                // create a business object from each data object
                iTabObjs.Add(new CalibrationTabUIObject(iTabDataObject));
            }

            iTabObjs.ItemEndEdit += new CalibrationItemEndEditEventHandler(CalibrationTabItemEndEdit);
            iTabObjs.CollectionChanged += new NotifyCollectionChangedEventHandler(CalibrationTabCollectionChanged);

            return iTabObjs;
        }
        public CalibrationTabUIObjects ImportCalibrationTab(long tankid, String fileName)
        {
            if ((String.IsNullOrEmpty(fileName))) return null;
            try
            {

                dataAccessLayer.ImportCalibrationTab(fileName, tankid);

                return GetCalibrationTab(tankid);
            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);
                throw;
            }

        }

        void CalibrationTabCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                try
                {

                    foreach (object item in e.OldItems)
                    {
                        CalibrationTabUIObject iTabObject = item as CalibrationTabUIObject;

                        // use the data access layer to delete the wrapped data object
                        dataAccessLayer.DeleteCalibrationTab(iTabObject.GetDataObject());
                    }
                }
                catch (Exception ex)
                {
                    App.Log.LogException(ex);

                    if (PersistenceError != null)
                    {
                        PersistenceError(this, ex);
                    }
                }
            }
        }

        void CalibrationTabItemEndEdit(IEditableObject sender)
        {
            CalibrationTabUIObject iTabObject = sender as CalibrationTabUIObject;

            try
            {
                if (iTabObject.TankId == 0)
                {
                    iTabObject.TankId = currTankId;
                }
                // use the data access layer to update the wrapped data object
                dataAccessLayer.UpdateCalibrationTab(iTabObject.GetDataObject());
            }
            catch (Exception ex)
            {
                App.Log.LogException(ex);

                if (PersistenceError != null)
                {
                    PersistenceError(this, ex);
                }
            }
        }

        public static event CalibrationPersistenceErrorHandler PersistenceError;
    }

}

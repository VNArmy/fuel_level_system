using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelLevelSystem.Model
{
    class FuelStat : GenericRecord
    {
        private String _FuelName;
        private UInt64 _NotedCapacity;
        private UInt64 _MeasuredCapacity;
        private UInt64 _NotedTotal;
        private UInt64 _MeasuredTotal;
        private Int64 _SelledTotal;
        private Int64 _DiffCapacity;

        public String FuelName 
        {
            get { return _FuelName; }
            set
            {
                _FuelName = value; RaisePropertyChanged("FuelName");
            } 
        }
        public UInt64 NotedCapacity
        {
            get { return _NotedCapacity; }
            set
            {
                _NotedCapacity = value; RaisePropertyChanged("NotedCapacity"); DiffCapacity = (Int64)_MeasuredCapacity - (Int64)_NotedCapacity;
            }
        }
        public UInt64 MeasuredCapacity
        {
            get { return _MeasuredCapacity; }
            set
            {
                _MeasuredCapacity = value; RaisePropertyChanged("MeasuredCapacity");
            }
        }
        public UInt64 NotedTotal
        {
            get { return _NotedTotal; }
            set
            {
                _NotedTotal = value; RaisePropertyChanged("NotedTotal"); SelledTotal = (Int64)_MeasuredTotal - (Int64)_NotedTotal;
            }
        }
        public UInt64 MeasuredTotal
        {
            get { return _MeasuredTotal; }
            set
            {
                _MeasuredTotal = value; RaisePropertyChanged("MeasuredTotal");
            }
        }
        public Int64 SelledTotal
        {
            get { return _SelledTotal; }
            set
            {
                _SelledTotal = value; RaisePropertyChanged("SelledTotal");
            }
        }
        public Int64 DiffCapacity
        {
            get { return _DiffCapacity; }
            set
            {
                _DiffCapacity = value; RaisePropertyChanged("DiffCapacity");
            }
        }
    }
}

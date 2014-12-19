//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using LinqConnect template.
// Git
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using Devart.Data.Linq;
using Devart.Data.Linq.Mapping;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace MainContext
{

    /// <summary>
    /// There are no comments for MainContext.RoomRateW in the schema.
    /// </summary>
    [Table(Name = @"""main"".RoomRateWS")]
    public partial class RoomRateW : INotifyPropertyChanging, INotifyPropertyChanged, ICloneable
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);
        #pragma warning disable 0649

        private int _HotelId;

        private string _RmTypeEName;

        private string _BreakfastEDesc;

        private System.Nullable<int> _MinVacRooms;

        private string _RateCodeCName;

        private string _RateCodeEName;

        private string _RmTypeEDesc;

        private string _RatePrice;

        private string _RateCode;

        private System.Nullable<System.DateTime> _RateDate;

        private string _RmType;

        private string _RmTypeCDesc;

        private string _RmTypeCName;

        private System.Nullable<int> _VacRooms;

        private string _BreakfastDesc;

        private System.Nullable<int> _NeedPay;

        private System.Nullable<int> _NeedGuarant;

        private int _Id;
        #pragma warning restore 0649
    
        #region Extensibility Method Definitions

        partial void OnLoaded();
        partial void OnValidate(ChangeAction action);
        partial void OnCreated();
        partial void OnHotelIdChanging(int value);
        partial void OnHotelIdChanged();
        partial void OnRmTypeENameChanging(string value);
        partial void OnRmTypeENameChanged();
        partial void OnBreakfastEDescChanging(string value);
        partial void OnBreakfastEDescChanged();
        partial void OnMinVacRoomsChanging(System.Nullable<int> value);
        partial void OnMinVacRoomsChanged();
        partial void OnRateCodeCNameChanging(string value);
        partial void OnRateCodeCNameChanged();
        partial void OnRateCodeENameChanging(string value);
        partial void OnRateCodeENameChanged();
        partial void OnRmTypeEDescChanging(string value);
        partial void OnRmTypeEDescChanged();
        partial void OnRatePriceChanging(string value);
        partial void OnRatePriceChanged();
        partial void OnRateCodeChanging(string value);
        partial void OnRateCodeChanged();
        partial void OnRateDateChanging(System.Nullable<System.DateTime> value);
        partial void OnRateDateChanged();
        partial void OnRmTypeChanging(string value);
        partial void OnRmTypeChanged();
        partial void OnRmTypeCDescChanging(string value);
        partial void OnRmTypeCDescChanged();
        partial void OnRmTypeCNameChanging(string value);
        partial void OnRmTypeCNameChanged();
        partial void OnVacRoomsChanging(System.Nullable<int> value);
        partial void OnVacRoomsChanged();
        partial void OnBreakfastDescChanging(string value);
        partial void OnBreakfastDescChanged();
        partial void OnNeedPayChanging(System.Nullable<int> value);
        partial void OnNeedPayChanged();
        partial void OnNeedGuarantChanging(System.Nullable<int> value);
        partial void OnNeedGuarantChanged();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        #endregion

        public RoomRateW()
        {
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for HotelId in the schema.
        /// </summary>
        [Column(Name = @"hotelId", Storage = "_HotelId", CanBeNull = false, DbType = "INT NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public int HotelId
        {
            get
            {
                return this._HotelId;
            }
            set
            {
                if (this._HotelId != value)
                {
                    this.OnHotelIdChanging(value);
                    this.SendPropertyChanging();
                    this._HotelId = value;
                    this.SendPropertyChanged("HotelId");
                    this.OnHotelIdChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RmTypeEName in the schema.
        /// </summary>
        [Column(Name = @"rmTypeEName", Storage = "_RmTypeEName", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string RmTypeEName
        {
            get
            {
                return this._RmTypeEName;
            }
            set
            {
                if (this._RmTypeEName != value)
                {
                    this.OnRmTypeENameChanging(value);
                    this.SendPropertyChanging();
                    this._RmTypeEName = value;
                    this.SendPropertyChanged("RmTypeEName");
                    this.OnRmTypeENameChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for BreakfastEDesc in the schema.
        /// </summary>
        [Column(Name = @"breakfastEDesc", Storage = "_BreakfastEDesc", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string BreakfastEDesc
        {
            get
            {
                return this._BreakfastEDesc;
            }
            set
            {
                if (this._BreakfastEDesc != value)
                {
                    this.OnBreakfastEDescChanging(value);
                    this.SendPropertyChanging();
                    this._BreakfastEDesc = value;
                    this.SendPropertyChanged("BreakfastEDesc");
                    this.OnBreakfastEDescChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for MinVacRooms in the schema.
        /// </summary>
        [Column(Name = @"minVacRooms", Storage = "_MinVacRooms", DbType = "INT", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> MinVacRooms
        {
            get
            {
                return this._MinVacRooms;
            }
            set
            {
                if (this._MinVacRooms != value)
                {
                    this.OnMinVacRoomsChanging(value);
                    this.SendPropertyChanging();
                    this._MinVacRooms = value;
                    this.SendPropertyChanged("MinVacRooms");
                    this.OnMinVacRoomsChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RateCodeCName in the schema.
        /// </summary>
        [Column(Name = @"rateCodeCName", Storage = "_RateCodeCName", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string RateCodeCName
        {
            get
            {
                return this._RateCodeCName;
            }
            set
            {
                if (this._RateCodeCName != value)
                {
                    this.OnRateCodeCNameChanging(value);
                    this.SendPropertyChanging();
                    this._RateCodeCName = value;
                    this.SendPropertyChanged("RateCodeCName");
                    this.OnRateCodeCNameChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RateCodeEName in the schema.
        /// </summary>
        [Column(Name = @"rateCodeEName", Storage = "_RateCodeEName", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string RateCodeEName
        {
            get
            {
                return this._RateCodeEName;
            }
            set
            {
                if (this._RateCodeEName != value)
                {
                    this.OnRateCodeENameChanging(value);
                    this.SendPropertyChanging();
                    this._RateCodeEName = value;
                    this.SendPropertyChanged("RateCodeEName");
                    this.OnRateCodeENameChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RmTypeEDesc in the schema.
        /// </summary>
        [Column(Name = @"rmTypeEDesc", Storage = "_RmTypeEDesc", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string RmTypeEDesc
        {
            get
            {
                return this._RmTypeEDesc;
            }
            set
            {
                if (this._RmTypeEDesc != value)
                {
                    this.OnRmTypeEDescChanging(value);
                    this.SendPropertyChanging();
                    this._RmTypeEDesc = value;
                    this.SendPropertyChanged("RmTypeEDesc");
                    this.OnRmTypeEDescChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RatePrice in the schema.
        /// </summary>
        [Column(Name = @"ratePrice", Storage = "_RatePrice", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string RatePrice
        {
            get
            {
                return this._RatePrice;
            }
            set
            {
                if (this._RatePrice != value)
                {
                    this.OnRatePriceChanging(value);
                    this.SendPropertyChanging();
                    this._RatePrice = value;
                    this.SendPropertyChanged("RatePrice");
                    this.OnRatePriceChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RateCode in the schema.
        /// </summary>
        [Column(Name = @"rateCode", Storage = "_RateCode", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string RateCode
        {
            get
            {
                return this._RateCode;
            }
            set
            {
                if (this._RateCode != value)
                {
                    this.OnRateCodeChanging(value);
                    this.SendPropertyChanging();
                    this._RateCode = value;
                    this.SendPropertyChanged("RateCode");
                    this.OnRateCodeChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RateDate in the schema.
        /// </summary>
        [Column(Name = @"rateDate", Storage = "_RateDate", DbType = "DATE", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<System.DateTime> RateDate
        {
            get
            {
                return this._RateDate;
            }
            set
            {
                if (this._RateDate != value)
                {
                    this.OnRateDateChanging(value);
                    this.SendPropertyChanging();
                    this._RateDate = value;
                    this.SendPropertyChanged("RateDate");
                    this.OnRateDateChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RmType in the schema.
        /// </summary>
        [Column(Name = @"rmType", Storage = "_RmType", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string RmType
        {
            get
            {
                return this._RmType;
            }
            set
            {
                if (this._RmType != value)
                {
                    this.OnRmTypeChanging(value);
                    this.SendPropertyChanging();
                    this._RmType = value;
                    this.SendPropertyChanged("RmType");
                    this.OnRmTypeChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RmTypeCDesc in the schema.
        /// </summary>
        [Column(Name = @"rmTypeCDesc", Storage = "_RmTypeCDesc", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string RmTypeCDesc
        {
            get
            {
                return this._RmTypeCDesc;
            }
            set
            {
                if (this._RmTypeCDesc != value)
                {
                    this.OnRmTypeCDescChanging(value);
                    this.SendPropertyChanging();
                    this._RmTypeCDesc = value;
                    this.SendPropertyChanged("RmTypeCDesc");
                    this.OnRmTypeCDescChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RmTypeCName in the schema.
        /// </summary>
        [Column(Name = @"rmTypeCName", Storage = "_RmTypeCName", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string RmTypeCName
        {
            get
            {
                return this._RmTypeCName;
            }
            set
            {
                if (this._RmTypeCName != value)
                {
                    this.OnRmTypeCNameChanging(value);
                    this.SendPropertyChanging();
                    this._RmTypeCName = value;
                    this.SendPropertyChanged("RmTypeCName");
                    this.OnRmTypeCNameChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for VacRooms in the schema.
        /// </summary>
        [Column(Name = @"vacRooms", Storage = "_VacRooms", DbType = "INT", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> VacRooms
        {
            get
            {
                return this._VacRooms;
            }
            set
            {
                if (this._VacRooms != value)
                {
                    this.OnVacRoomsChanging(value);
                    this.SendPropertyChanging();
                    this._VacRooms = value;
                    this.SendPropertyChanged("VacRooms");
                    this.OnVacRoomsChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for BreakfastDesc in the schema.
        /// </summary>
        [Column(Name = @"breakfastDesc", Storage = "_BreakfastDesc", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string BreakfastDesc
        {
            get
            {
                return this._BreakfastDesc;
            }
            set
            {
                if (this._BreakfastDesc != value)
                {
                    this.OnBreakfastDescChanging(value);
                    this.SendPropertyChanging();
                    this._BreakfastDesc = value;
                    this.SendPropertyChanged("BreakfastDesc");
                    this.OnBreakfastDescChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for NeedPay in the schema.
        /// </summary>
        [Column(Name = @"needPay", Storage = "_NeedPay", DbType = "INT", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> NeedPay
        {
            get
            {
                return this._NeedPay;
            }
            set
            {
                if (this._NeedPay != value)
                {
                    this.OnNeedPayChanging(value);
                    this.SendPropertyChanging();
                    this._NeedPay = value;
                    this.SendPropertyChanged("NeedPay");
                    this.OnNeedPayChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for NeedGuarant in the schema.
        /// </summary>
        [Column(Name = @"needGuarant", Storage = "_NeedGuarant", DbType = "INT", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> NeedGuarant
        {
            get
            {
                return this._NeedGuarant;
            }
            set
            {
                if (this._NeedGuarant != value)
                {
                    this.OnNeedGuarantChanging(value);
                    this.SendPropertyChanging();
                    this._NeedGuarant = value;
                    this.SendPropertyChanged("NeedGuarant");
                    this.OnNeedGuarantChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Id in the schema.
        /// </summary>
        [Column(Name = @"id", Storage = "_Id", AutoSync = AutoSync.OnInsert, CanBeNull = false, DbType = "INTEGER NOT NULL", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id
        {
            get
            {
                return this._Id;
            }
        }
    
        #region ICloneable Members

        public virtual object Clone()
        {
            RoomRateW obj = new RoomRateW();
            obj.HotelId = HotelId;
            obj.RmTypeEName = RmTypeEName;
            obj.BreakfastEDesc = BreakfastEDesc;
            obj.MinVacRooms = MinVacRooms;
            obj.RateCodeCName = RateCodeCName;
            obj.RateCodeEName = RateCodeEName;
            obj.RmTypeEDesc = RmTypeEDesc;
            obj.RatePrice = RatePrice;
            obj.RateCode = RateCode;
            obj.RateDate = RateDate;
            obj.RmType = RmType;
            obj.RmTypeCDesc = RmTypeCDesc;
            obj.RmTypeCName = RmTypeCName;
            obj.VacRooms = VacRooms;
            obj.BreakfastDesc = BreakfastDesc;
            obj.NeedPay = NeedPay;
            obj.NeedGuarant = NeedGuarant;
            return obj;
        }

        #endregion
   
        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
		        var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanging(System.String propertyName) 
        {    
		        var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, new PropertyChangingEventArgs(propertyName));
        }

        protected virtual void SendPropertyChanged(System.String propertyName)
        {    
		        var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
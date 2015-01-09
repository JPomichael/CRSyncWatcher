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
    /// There are no comments for MainContext.RateCodeInfor in the schema.
    /// </summary>
    [Table(Name = @"""main"".RateCodeInfor")]
    public partial class RateCodeInfor : INotifyPropertyChanging, INotifyPropertyChanged, ICloneable
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);
        #pragma warning disable 0649

        private int _HotelId;

        private string _RateCode;

        private string _CName;

        private string _EName;

        private string _MemberType;

        private string _CardType;

        private string _IsContract;

        private string _Currency;

        private string _RateCat;

        private System.Nullable<System.DateTime> _BegDate;

        private System.Nullable<System.DateTime> _EndDate;

        private string _Isdiscount;

        private string _Market;

        private string _Source;

        private string _Weekenddays;

        private System.Nullable<int> _PtCoef;

        private string _DiscountOf;

        private string _DiscountType;

        private System.Nullable<int> _DiscountValue;

        private string _RoundType;

        private string _TargetRound;

        private string _Status;

        private string _DefaultShow;

        private System.Nullable<System.DateTime> _SellBegDate;

        private System.Nullable<System.DateTime> _SellEndDate;

        private string _BookingThrough;

        private string _AliasCn;

        private string _AliasEn;

        private System.Nullable<int> _ContractId;

        private System.Nullable<int> _Dailyinvallocation;

        private string _Descript;

        private string _Edescript;

        private string _FrtMarket;

        private string _FrtSource;

        private string _IsUpward;

        private string _Remarks;

        private string _Syncstatus;

        private int _Id;
        #pragma warning restore 0649
    
        #region Extensibility Method Definitions

        partial void OnLoaded();
        partial void OnValidate(ChangeAction action);
        partial void OnCreated();
        partial void OnHotelIdChanging(int value);
        partial void OnHotelIdChanged();
        partial void OnRateCodeChanging(string value);
        partial void OnRateCodeChanged();
        partial void OnCNameChanging(string value);
        partial void OnCNameChanged();
        partial void OnENameChanging(string value);
        partial void OnENameChanged();
        partial void OnMemberTypeChanging(string value);
        partial void OnMemberTypeChanged();
        partial void OnCardTypeChanging(string value);
        partial void OnCardTypeChanged();
        partial void OnIsContractChanging(string value);
        partial void OnIsContractChanged();
        partial void OnCurrencyChanging(string value);
        partial void OnCurrencyChanged();
        partial void OnRateCatChanging(string value);
        partial void OnRateCatChanged();
        partial void OnBegDateChanging(System.Nullable<System.DateTime> value);
        partial void OnBegDateChanged();
        partial void OnEndDateChanging(System.Nullable<System.DateTime> value);
        partial void OnEndDateChanged();
        partial void OnIsdiscountChanging(string value);
        partial void OnIsdiscountChanged();
        partial void OnMarketChanging(string value);
        partial void OnMarketChanged();
        partial void OnSourceChanging(string value);
        partial void OnSourceChanged();
        partial void OnWeekenddaysChanging(string value);
        partial void OnWeekenddaysChanged();
        partial void OnPtCoefChanging(System.Nullable<int> value);
        partial void OnPtCoefChanged();
        partial void OnDiscountOfChanging(string value);
        partial void OnDiscountOfChanged();
        partial void OnDiscountTypeChanging(string value);
        partial void OnDiscountTypeChanged();
        partial void OnDiscountValueChanging(System.Nullable<int> value);
        partial void OnDiscountValueChanged();
        partial void OnRoundTypeChanging(string value);
        partial void OnRoundTypeChanged();
        partial void OnTargetRoundChanging(string value);
        partial void OnTargetRoundChanged();
        partial void OnStatusChanging(string value);
        partial void OnStatusChanged();
        partial void OnDefaultShowChanging(string value);
        partial void OnDefaultShowChanged();
        partial void OnSellBegDateChanging(System.Nullable<System.DateTime> value);
        partial void OnSellBegDateChanged();
        partial void OnSellEndDateChanging(System.Nullable<System.DateTime> value);
        partial void OnSellEndDateChanged();
        partial void OnBookingThroughChanging(string value);
        partial void OnBookingThroughChanged();
        partial void OnAliasCnChanging(string value);
        partial void OnAliasCnChanged();
        partial void OnAliasEnChanging(string value);
        partial void OnAliasEnChanged();
        partial void OnContractIdChanging(System.Nullable<int> value);
        partial void OnContractIdChanged();
        partial void OnDailyinvallocationChanging(System.Nullable<int> value);
        partial void OnDailyinvallocationChanged();
        partial void OnDescriptChanging(string value);
        partial void OnDescriptChanged();
        partial void OnEdescriptChanging(string value);
        partial void OnEdescriptChanged();
        partial void OnFrtMarketChanging(string value);
        partial void OnFrtMarketChanged();
        partial void OnFrtSourceChanging(string value);
        partial void OnFrtSourceChanged();
        partial void OnIsUpwardChanging(string value);
        partial void OnIsUpwardChanged();
        partial void OnRemarksChanging(string value);
        partial void OnRemarksChanged();
        partial void OnSyncstatusChanging(string value);
        partial void OnSyncstatusChanged();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        #endregion

        public RateCodeInfor()
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
        /// There are no comments for RateCode in the schema.
        /// </summary>
        [Column(Name = @"rateCode", Storage = "_RateCode", CanBeNull = false, DbType = "VARCHAR NOT NULL", UpdateCheck = UpdateCheck.Never)]
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
        /// There are no comments for CName in the schema.
        /// </summary>
        [Column(Name = @"cName", Storage = "_CName", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string CName
        {
            get
            {
                return this._CName;
            }
            set
            {
                if (this._CName != value)
                {
                    this.OnCNameChanging(value);
                    this.SendPropertyChanging();
                    this._CName = value;
                    this.SendPropertyChanged("CName");
                    this.OnCNameChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for EName in the schema.
        /// </summary>
        [Column(Name = @"eName", Storage = "_EName", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string EName
        {
            get
            {
                return this._EName;
            }
            set
            {
                if (this._EName != value)
                {
                    this.OnENameChanging(value);
                    this.SendPropertyChanging();
                    this._EName = value;
                    this.SendPropertyChanged("EName");
                    this.OnENameChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for MemberType in the schema.
        /// </summary>
        [Column(Name = @"memberType", Storage = "_MemberType", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string MemberType
        {
            get
            {
                return this._MemberType;
            }
            set
            {
                if (this._MemberType != value)
                {
                    this.OnMemberTypeChanging(value);
                    this.SendPropertyChanging();
                    this._MemberType = value;
                    this.SendPropertyChanged("MemberType");
                    this.OnMemberTypeChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for CardType in the schema.
        /// </summary>
        [Column(Name = @"cardType", Storage = "_CardType", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string CardType
        {
            get
            {
                return this._CardType;
            }
            set
            {
                if (this._CardType != value)
                {
                    this.OnCardTypeChanging(value);
                    this.SendPropertyChanging();
                    this._CardType = value;
                    this.SendPropertyChanged("CardType");
                    this.OnCardTypeChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for IsContract in the schema.
        /// </summary>
        [Column(Name = @"isContract", Storage = "_IsContract", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string IsContract
        {
            get
            {
                return this._IsContract;
            }
            set
            {
                if (this._IsContract != value)
                {
                    this.OnIsContractChanging(value);
                    this.SendPropertyChanging();
                    this._IsContract = value;
                    this.SendPropertyChanged("IsContract");
                    this.OnIsContractChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Currency in the schema.
        /// </summary>
        [Column(Name = @"currency", Storage = "_Currency", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string Currency
        {
            get
            {
                return this._Currency;
            }
            set
            {
                if (this._Currency != value)
                {
                    this.OnCurrencyChanging(value);
                    this.SendPropertyChanging();
                    this._Currency = value;
                    this.SendPropertyChanged("Currency");
                    this.OnCurrencyChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RateCat in the schema.
        /// </summary>
        [Column(Name = @"rateCat", Storage = "_RateCat", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string RateCat
        {
            get
            {
                return this._RateCat;
            }
            set
            {
                if (this._RateCat != value)
                {
                    this.OnRateCatChanging(value);
                    this.SendPropertyChanging();
                    this._RateCat = value;
                    this.SendPropertyChanged("RateCat");
                    this.OnRateCatChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for BegDate in the schema.
        /// </summary>
        [Column(Name = @"begDate", Storage = "_BegDate", DbType = "DATETIME", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<System.DateTime> BegDate
        {
            get
            {
                return this._BegDate;
            }
            set
            {
                if (this._BegDate != value)
                {
                    this.OnBegDateChanging(value);
                    this.SendPropertyChanging();
                    this._BegDate = value;
                    this.SendPropertyChanged("BegDate");
                    this.OnBegDateChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for EndDate in the schema.
        /// </summary>
        [Column(Name = @"endDate", Storage = "_EndDate", DbType = "DATETIME", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<System.DateTime> EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                if (this._EndDate != value)
                {
                    this.OnEndDateChanging(value);
                    this.SendPropertyChanging();
                    this._EndDate = value;
                    this.SendPropertyChanged("EndDate");
                    this.OnEndDateChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Isdiscount in the schema.
        /// </summary>
        [Column(Name = @"isdiscount", Storage = "_Isdiscount", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string Isdiscount
        {
            get
            {
                return this._Isdiscount;
            }
            set
            {
                if (this._Isdiscount != value)
                {
                    this.OnIsdiscountChanging(value);
                    this.SendPropertyChanging();
                    this._Isdiscount = value;
                    this.SendPropertyChanged("Isdiscount");
                    this.OnIsdiscountChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Market in the schema.
        /// </summary>
        [Column(Name = @"market", Storage = "_Market", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string Market
        {
            get
            {
                return this._Market;
            }
            set
            {
                if (this._Market != value)
                {
                    this.OnMarketChanging(value);
                    this.SendPropertyChanging();
                    this._Market = value;
                    this.SendPropertyChanged("Market");
                    this.OnMarketChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Source in the schema.
        /// </summary>
        [Column(Name = @"source", Storage = "_Source", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string Source
        {
            get
            {
                return this._Source;
            }
            set
            {
                if (this._Source != value)
                {
                    this.OnSourceChanging(value);
                    this.SendPropertyChanging();
                    this._Source = value;
                    this.SendPropertyChanged("Source");
                    this.OnSourceChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Weekenddays in the schema.
        /// </summary>
        [Column(Name = @"weekenddays", Storage = "_Weekenddays", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string Weekenddays
        {
            get
            {
                return this._Weekenddays;
            }
            set
            {
                if (this._Weekenddays != value)
                {
                    this.OnWeekenddaysChanging(value);
                    this.SendPropertyChanging();
                    this._Weekenddays = value;
                    this.SendPropertyChanged("Weekenddays");
                    this.OnWeekenddaysChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for PtCoef in the schema.
        /// </summary>
        [Column(Name = @"ptCoef", Storage = "_PtCoef", DbType = "INT", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> PtCoef
        {
            get
            {
                return this._PtCoef;
            }
            set
            {
                if (this._PtCoef != value)
                {
                    this.OnPtCoefChanging(value);
                    this.SendPropertyChanging();
                    this._PtCoef = value;
                    this.SendPropertyChanged("PtCoef");
                    this.OnPtCoefChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for DiscountOf in the schema.
        /// </summary>
        [Column(Name = @"discountOf", Storage = "_DiscountOf", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string DiscountOf
        {
            get
            {
                return this._DiscountOf;
            }
            set
            {
                if (this._DiscountOf != value)
                {
                    this.OnDiscountOfChanging(value);
                    this.SendPropertyChanging();
                    this._DiscountOf = value;
                    this.SendPropertyChanged("DiscountOf");
                    this.OnDiscountOfChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for DiscountType in the schema.
        /// </summary>
        [Column(Name = @"discountType", Storage = "_DiscountType", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string DiscountType
        {
            get
            {
                return this._DiscountType;
            }
            set
            {
                if (this._DiscountType != value)
                {
                    this.OnDiscountTypeChanging(value);
                    this.SendPropertyChanging();
                    this._DiscountType = value;
                    this.SendPropertyChanged("DiscountType");
                    this.OnDiscountTypeChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for DiscountValue in the schema.
        /// </summary>
        [Column(Name = @"discountValue", Storage = "_DiscountValue", DbType = "INT", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> DiscountValue
        {
            get
            {
                return this._DiscountValue;
            }
            set
            {
                if (this._DiscountValue != value)
                {
                    this.OnDiscountValueChanging(value);
                    this.SendPropertyChanging();
                    this._DiscountValue = value;
                    this.SendPropertyChanged("DiscountValue");
                    this.OnDiscountValueChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for RoundType in the schema.
        /// </summary>
        [Column(Name = @"roundType", Storage = "_RoundType", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string RoundType
        {
            get
            {
                return this._RoundType;
            }
            set
            {
                if (this._RoundType != value)
                {
                    this.OnRoundTypeChanging(value);
                    this.SendPropertyChanging();
                    this._RoundType = value;
                    this.SendPropertyChanged("RoundType");
                    this.OnRoundTypeChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for TargetRound in the schema.
        /// </summary>
        [Column(Name = @"targetRound", Storage = "_TargetRound", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string TargetRound
        {
            get
            {
                return this._TargetRound;
            }
            set
            {
                if (this._TargetRound != value)
                {
                    this.OnTargetRoundChanging(value);
                    this.SendPropertyChanging();
                    this._TargetRound = value;
                    this.SendPropertyChanged("TargetRound");
                    this.OnTargetRoundChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Status in the schema.
        /// </summary>
        [Column(Name = @"status", Storage = "_Status", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if (this._Status != value)
                {
                    this.OnStatusChanging(value);
                    this.SendPropertyChanging();
                    this._Status = value;
                    this.SendPropertyChanged("Status");
                    this.OnStatusChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for DefaultShow in the schema.
        /// </summary>
        [Column(Name = @"defaultShow", Storage = "_DefaultShow", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string DefaultShow
        {
            get
            {
                return this._DefaultShow;
            }
            set
            {
                if (this._DefaultShow != value)
                {
                    this.OnDefaultShowChanging(value);
                    this.SendPropertyChanging();
                    this._DefaultShow = value;
                    this.SendPropertyChanged("DefaultShow");
                    this.OnDefaultShowChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for SellBegDate in the schema.
        /// </summary>
        [Column(Name = @"sellBegDate", Storage = "_SellBegDate", DbType = "DATETIME", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<System.DateTime> SellBegDate
        {
            get
            {
                return this._SellBegDate;
            }
            set
            {
                if (this._SellBegDate != value)
                {
                    this.OnSellBegDateChanging(value);
                    this.SendPropertyChanging();
                    this._SellBegDate = value;
                    this.SendPropertyChanged("SellBegDate");
                    this.OnSellBegDateChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for SellEndDate in the schema.
        /// </summary>
        [Column(Name = @"sellEndDate", Storage = "_SellEndDate", DbType = "DATETIME", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<System.DateTime> SellEndDate
        {
            get
            {
                return this._SellEndDate;
            }
            set
            {
                if (this._SellEndDate != value)
                {
                    this.OnSellEndDateChanging(value);
                    this.SendPropertyChanging();
                    this._SellEndDate = value;
                    this.SendPropertyChanged("SellEndDate");
                    this.OnSellEndDateChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for BookingThrough in the schema.
        /// </summary>
        [Column(Name = @"bookingThrough", Storage = "_BookingThrough", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string BookingThrough
        {
            get
            {
                return this._BookingThrough;
            }
            set
            {
                if (this._BookingThrough != value)
                {
                    this.OnBookingThroughChanging(value);
                    this.SendPropertyChanging();
                    this._BookingThrough = value;
                    this.SendPropertyChanged("BookingThrough");
                    this.OnBookingThroughChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for AliasCn in the schema.
        /// </summary>
        [Column(Name = @"aliasCn", Storage = "_AliasCn", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string AliasCn
        {
            get
            {
                return this._AliasCn;
            }
            set
            {
                if (this._AliasCn != value)
                {
                    this.OnAliasCnChanging(value);
                    this.SendPropertyChanging();
                    this._AliasCn = value;
                    this.SendPropertyChanged("AliasCn");
                    this.OnAliasCnChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for AliasEn in the schema.
        /// </summary>
        [Column(Name = @"aliasEn", Storage = "_AliasEn", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string AliasEn
        {
            get
            {
                return this._AliasEn;
            }
            set
            {
                if (this._AliasEn != value)
                {
                    this.OnAliasEnChanging(value);
                    this.SendPropertyChanging();
                    this._AliasEn = value;
                    this.SendPropertyChanged("AliasEn");
                    this.OnAliasEnChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for ContractId in the schema.
        /// </summary>
        [Column(Name = @"contractId", Storage = "_ContractId", DbType = "INT", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> ContractId
        {
            get
            {
                return this._ContractId;
            }
            set
            {
                if (this._ContractId != value)
                {
                    this.OnContractIdChanging(value);
                    this.SendPropertyChanging();
                    this._ContractId = value;
                    this.SendPropertyChanged("ContractId");
                    this.OnContractIdChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Dailyinvallocation in the schema.
        /// </summary>
        [Column(Name = @"dailyinvallocation", Storage = "_Dailyinvallocation", DbType = "INT", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> Dailyinvallocation
        {
            get
            {
                return this._Dailyinvallocation;
            }
            set
            {
                if (this._Dailyinvallocation != value)
                {
                    this.OnDailyinvallocationChanging(value);
                    this.SendPropertyChanging();
                    this._Dailyinvallocation = value;
                    this.SendPropertyChanged("Dailyinvallocation");
                    this.OnDailyinvallocationChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Descript in the schema.
        /// </summary>
        [Column(Name = @"descript", Storage = "_Descript", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string Descript
        {
            get
            {
                return this._Descript;
            }
            set
            {
                if (this._Descript != value)
                {
                    this.OnDescriptChanging(value);
                    this.SendPropertyChanging();
                    this._Descript = value;
                    this.SendPropertyChanged("Descript");
                    this.OnDescriptChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Edescript in the schema.
        /// </summary>
        [Column(Name = @"edescript", Storage = "_Edescript", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string Edescript
        {
            get
            {
                return this._Edescript;
            }
            set
            {
                if (this._Edescript != value)
                {
                    this.OnEdescriptChanging(value);
                    this.SendPropertyChanging();
                    this._Edescript = value;
                    this.SendPropertyChanged("Edescript");
                    this.OnEdescriptChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for FrtMarket in the schema.
        /// </summary>
        [Column(Name = @"frtMarket", Storage = "_FrtMarket", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string FrtMarket
        {
            get
            {
                return this._FrtMarket;
            }
            set
            {
                if (this._FrtMarket != value)
                {
                    this.OnFrtMarketChanging(value);
                    this.SendPropertyChanging();
                    this._FrtMarket = value;
                    this.SendPropertyChanged("FrtMarket");
                    this.OnFrtMarketChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for FrtSource in the schema.
        /// </summary>
        [Column(Name = @"frtSource", Storage = "_FrtSource", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string FrtSource
        {
            get
            {
                return this._FrtSource;
            }
            set
            {
                if (this._FrtSource != value)
                {
                    this.OnFrtSourceChanging(value);
                    this.SendPropertyChanging();
                    this._FrtSource = value;
                    this.SendPropertyChanged("FrtSource");
                    this.OnFrtSourceChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for IsUpward in the schema.
        /// </summary>
        [Column(Name = @"isUpward", Storage = "_IsUpward", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string IsUpward
        {
            get
            {
                return this._IsUpward;
            }
            set
            {
                if (this._IsUpward != value)
                {
                    this.OnIsUpwardChanging(value);
                    this.SendPropertyChanging();
                    this._IsUpward = value;
                    this.SendPropertyChanged("IsUpward");
                    this.OnIsUpwardChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Remarks in the schema.
        /// </summary>
        [Column(Name = @"remarks", Storage = "_Remarks", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string Remarks
        {
            get
            {
                return this._Remarks;
            }
            set
            {
                if (this._Remarks != value)
                {
                    this.OnRemarksChanging(value);
                    this.SendPropertyChanging();
                    this._Remarks = value;
                    this.SendPropertyChanged("Remarks");
                    this.OnRemarksChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Syncstatus in the schema.
        /// </summary>
        [Column(Name = @"syncstatus", Storage = "_Syncstatus", DbType = "VARCHAR", UpdateCheck = UpdateCheck.Never)]
        public string Syncstatus
        {
            get
            {
                return this._Syncstatus;
            }
            set
            {
                if (this._Syncstatus != value)
                {
                    this.OnSyncstatusChanging(value);
                    this.SendPropertyChanging();
                    this._Syncstatus = value;
                    this.SendPropertyChanged("Syncstatus");
                    this.OnSyncstatusChanged();
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
            RateCodeInfor obj = new RateCodeInfor();
            obj.HotelId = HotelId;
            obj.RateCode = RateCode;
            obj.CName = CName;
            obj.EName = EName;
            obj.MemberType = MemberType;
            obj.CardType = CardType;
            obj.IsContract = IsContract;
            obj.Currency = Currency;
            obj.RateCat = RateCat;
            obj.BegDate = BegDate;
            obj.EndDate = EndDate;
            obj.Isdiscount = Isdiscount;
            obj.Market = Market;
            obj.Source = Source;
            obj.Weekenddays = Weekenddays;
            obj.PtCoef = PtCoef;
            obj.DiscountOf = DiscountOf;
            obj.DiscountType = DiscountType;
            obj.DiscountValue = DiscountValue;
            obj.RoundType = RoundType;
            obj.TargetRound = TargetRound;
            obj.Status = Status;
            obj.DefaultShow = DefaultShow;
            obj.SellBegDate = SellBegDate;
            obj.SellEndDate = SellEndDate;
            obj.BookingThrough = BookingThrough;
            obj.AliasCn = AliasCn;
            obj.AliasEn = AliasEn;
            obj.ContractId = ContractId;
            obj.Dailyinvallocation = Dailyinvallocation;
            obj.Descript = Descript;
            obj.Edescript = Edescript;
            obj.FrtMarket = FrtMarket;
            obj.FrtSource = FrtSource;
            obj.IsUpward = IsUpward;
            obj.Remarks = Remarks;
            obj.Syncstatus = Syncstatus;
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

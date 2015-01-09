using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRS.Sync.Watcher.Domain.Dto
{
    public class RatePlanDTO
    {
        string RatePlanId;

        public string ratePlanId
        {
            get { return RatePlanId; }
            set { RatePlanId = value; }
        }
        int hotel_id;

        public int Hotel_id
        {
            get { return hotel_id; }
            set { hotel_id = value; }
        }
        bool h_room_rp_state;

        public bool H_room_rp_state
        {
            get { return h_room_rp_state; }
            set { h_room_rp_state = value; }
        }
        string h_room_rp_name_cn;

        public string H_room_rp_name_cn
        {
            get { return h_room_rp_name_cn; }
            set { h_room_rp_name_cn = value; }
        }
        string h_room_rp_name_en;

        public string H_room_rp_name_en
        {
            get { return h_room_rp_name_en; }
            set { h_room_rp_name_en = value; }
        }
        bool h_room_rp_is_pay_online;

        public bool H_room_rp_is_pay_online
        {
            get { return h_room_rp_is_pay_online; }
            set { h_room_rp_is_pay_online = value; }
        }
        string h_room_rp_check_in;

        public string H_room_rp_check_in
        {
            get { return h_room_rp_check_in; }
            set { h_room_rp_check_in = value; }
        }
        string h_room_rp_check_out;

        public string H_room_rp_check_out
        {
            get { return h_room_rp_check_out; }
            set { h_room_rp_check_out = value; }
        }
        int h_room_rp_least_day;

        public int H_room_rp_least_day
        {
            get { return h_room_rp_least_day; }
            set { h_room_rp_least_day = value; }
        }
        int h_room_rp_longest_day;

        public int H_room_rp_longest_day
        {
            get { return h_room_rp_longest_day; }
            set { h_room_rp_longest_day = value; }
        }
        bool h_room_rp_invoice;

        public bool H_room_rp_invoice
        {
            get { return h_room_rp_invoice; }
            set { h_room_rp_invoice = value; }
        }
        string h_room_rp_breakfast_title;

        public string H_room_rp_breakfast_title
        {
            get { return h_room_rp_breakfast_title; }
            set { h_room_rp_breakfast_title = value; }
        }
        DateTime h_room_rp_ctime;

        public DateTime H_room_rp_ctime
        {
            get { return h_room_rp_ctime; }
            set { h_room_rp_ctime = value; }
        }
        string SuffixName;

        public string suffixName
        {
            get { return SuffixName; }
            set { SuffixName = value; }
        }
        Nullable<int> CurrentAlloment;

        public Nullable<int> currentAlloment
        {
            get { return CurrentAlloment; }
            set { CurrentAlloment = value; }
        }
        string PaymentType;

        public string paymentType
        {
            get { return PaymentType; }
            set { PaymentType = value; }
        }

        string GuaranteeRuleIds;
        public string guaranteeRuleIds
        {
            get { return GuaranteeRuleIds; }
            set { GuaranteeRuleIds = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WOPHRMSystem.Models
{
    public class ChequeDetailsModel
    {
        public int Id { get; set; }
        public int RowId { get; set; }
        public string TableName { get; set; }
        public int TableHeadId { get; set; }
        public string ChequeNo { get; set; }
        public string BankDetails { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ReceivedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> CashTookDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }

    public class ChequeModel : ChequeDetailsModel { }
    public class Chequeform
    {
        public Nullable<decimal> RowTotalAmount { get; set; }
        public ChequeDetailsModel Fileds { get; set; }
        public List<ChequeModel> ChequeModelData { get; set; }
    }
}
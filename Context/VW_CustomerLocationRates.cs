//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WOPHRMSystem.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class VW_CustomerLocationRates
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public int Fk_LocatonId { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<long> LocationNumber { get; set; }
    }
}

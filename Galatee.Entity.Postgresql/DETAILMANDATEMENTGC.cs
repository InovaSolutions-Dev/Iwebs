//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Galatee.Entity.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class DETAILMANDATEMENTGC
    {
        public int FK_IDMANDATEMENT { get; set; }
        public string CENTRE { get; set; }
        public string CLIENT { get; set; }
        public string ORDRE { get; set; }
        public string PERIODE { get; set; }
        public string NDOC { get; set; }
        public string STATUS { get; set; }
        public Nullable<decimal> MONTANT { get; set; }
        public int PK_ID { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public Nullable<System.DateTime> DATEMODIFICATION { get; set; }
        public string USERCREATION { get; set; }
        public string USERMODIFICATION { get; set; }
        public Nullable<decimal> MONTANTTVA { get; set; }
        public Nullable<int> FK_IDCENTRE { get; set; }
        public Nullable<int> FK_IDLCLIENT { get; set; }
        public Nullable<int> FK_IDCLIENT { get; set; }
    
        public virtual CENTRE CENTRE1 { get; set; }
        public virtual CLIENT CLIENT1 { get; set; }
        public virtual LCLIENT LCLIENT { get; set; }
        public virtual MANDATEMENTGC MANDATEMENTGC { get; set; }
    }
}

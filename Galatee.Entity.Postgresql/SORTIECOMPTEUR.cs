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
    
    public partial class SORTIECOMPTEUR
    {
        public int PK_ID { get; set; }
        public Nullable<int> FK_IDLIVREUR { get; set; }
        public Nullable<int> FK_IDRECEPTEUR { get; set; }
        public Nullable<int> FK_IDDEMANDE { get; set; }
        public Nullable<int> FK_IDCANALISATION { get; set; }
        public bool ACTIF { get; set; }
        public string LIVRE { get; set; }
        public string RECU { get; set; }
        public System.DateTime DATECREATION { get; set; }
        public System.DateTime DATELIVRAISON { get; set; }
        public Nullable<System.DateTime> DATERECEPTION { get; set; }
    }
}

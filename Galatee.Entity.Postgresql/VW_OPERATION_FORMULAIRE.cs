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
    
    public partial class VW_OPERATION_FORMULAIRE
    {
        public System.Guid PK_ID { get; set; }
        public string CODE { get; set; }
        public string NOM { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<System.Guid> FK_ID_PARENTOPERATION { get; set; }
        public Nullable<int> FK_ID_PRODUIT { get; set; }
        public Nullable<int> FK_IDFORMULAIRE { get; set; }
        public string FORMULAIRE { get; set; }
        public string FULLNAMECONTROLE { get; set; }
        public bool CREATIONDEMANDE { get; set; }
    }
}

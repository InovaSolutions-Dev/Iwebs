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
    
    public partial class HISTORIQUE_CONNECTION
    {
        public int PK_ID { get; set; }
        public string Login { get; set; }
        public string Matricule { get; set; }
        public Nullable<int> FK_ID_USER { get; set; }
        public Nullable<System.DateTime> DateConnection { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using Inova.Tools.Utilities;
using System.Runtime.Serialization;

namespace Galatee.Structure
{
    [DataContract]
    public class CsCodeRegroupement
    {
        [DataMember]
        public string IDREGCLI { get; set; }
        [DataMember]
        public string NOM { get; set; }

    }

}










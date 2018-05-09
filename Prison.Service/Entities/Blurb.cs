using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Prison.Service
{
    [DataContract]
    public class Blurb
    {
        [DataMember]
        public int BlurbID { get; set; }

        [DataMember]
        public string BlurbHeader { get; set; }

        [DataMember]
        public string BlurbContent { get; set; }

        
        [DataMember]
        public byte[] Image { get; set; }

        
    }
}
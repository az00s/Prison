using System.Runtime.Serialization;

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
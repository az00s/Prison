namespace Prison.App.Common.Entities
{
    public class Blurb:IBlurb
    {
        public int BlurbID { get; set; }

        public string BlurbHeader { get; set; }

        public string BlurbContent { get; set; }

        public byte[] Image { get; set; }
    }
}

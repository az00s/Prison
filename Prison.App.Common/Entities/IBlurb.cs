namespace Prison.App.Common.Entities
{
    public interface IBlurb
    {
        int BlurbID { get; set; }

        string BlurbHeader { get; set; }

        string BlurbContent { get; set; }


        byte[] Image { get; set; }
    }
}

namespace PhotoMagic.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath => @"/Photos/" + FileName;
    }
}

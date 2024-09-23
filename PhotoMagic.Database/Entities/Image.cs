using System.ComponentModel.DataAnnotations;

namespace PhotoMagic.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int UserId { get; set; }
    }
}

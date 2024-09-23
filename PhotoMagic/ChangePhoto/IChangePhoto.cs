using PhotoMagic.Models;

namespace PhotoMagic.ChangePhoto
{
    public interface IChangePhoto
    {
        List<ImageModel> ChangePhoto(ImageModel image);
    }
}

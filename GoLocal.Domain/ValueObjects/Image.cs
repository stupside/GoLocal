using GoLocal.Domain.Enums;

namespace GoLocal.Domain.ValueObjects
{
    public class Image
    {
        public string Value { get; }
        
        public ImageKind Type { get; }

        public Image()
        {
        }

        public Image(string value = default, ImageKind type = ImageKind.Base64)
            : this()
        {
            Type = type;
            Value = value;
        }
    }
}
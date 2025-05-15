namespace ChamaOSindico.Domain.Entities
{
    public class CondominalService : BaseEntity
    {
        public string Title { get; private set; }
        public byte[] ProviderPhotoUrl { get; private set; }     
        public string ImageType { get; private set; }
        public string ProviderName { get; private set; }
        public string Cellphone { get; private set; }
        public string? Description { get; private set; }

        public CondominalService(string id, string title, byte[] providerPhotoUrl, string imageType, string providerName, string cellphone, string? description)
        {
            if (providerPhotoUrl.Length == 0)
                throw new ArgumentException("Provider photo url is required.");
            
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.");

            if (string.IsNullOrWhiteSpace(providerName))
                throw new ArgumentException("Provider name is required.");

            if (string.IsNullOrWhiteSpace(cellphone))
                throw new ArgumentException("Cellphone is required.");

            if(string.IsNullOrEmpty(imageType))
                throw new ArgumentException("ImageType is required.");

            if (cellphone.Length < 10 || cellphone.Length > 15)
                throw new ArgumentException("Cellphone number must be between 10 and 15 characters.");

            Id = id;
            ProviderPhotoUrl = providerPhotoUrl;
            Title = title;
            ProviderName = providerName;
            Cellphone = cellphone;
            Description = description;
            ImageType = imageType;
        }
    }
}

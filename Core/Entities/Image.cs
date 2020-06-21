namespace Core.Entities
{
    public class Image : BaseEntity
    {
        public string UrlSmall { get; set; }
        public string UrlMedium { get; set; }
        public string UrlBig { get; set; }
        public string PublicId { get; set; }
        public virtual Item Item { get; set; }
        public int ItemId { get; set; }

    }
}
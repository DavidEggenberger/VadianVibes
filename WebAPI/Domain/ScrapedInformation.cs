namespace WebAPI.Domain
{
    public class ScrapedInformation
    {
        public string ScrapedInformationFromLinkedWebsite { get; set; }
        public string ScrapedInformationFromLinkedFile { get; set; }
        public override string ToString()
        {
            return ScrapedInformationFromLinkedFile;
        }
    }
}

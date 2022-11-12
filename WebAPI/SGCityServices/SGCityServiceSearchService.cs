using System.Collections.Generic;
using WebAPI.Domain;
using WebAPI.SGCityServicesClient;
using System.Linq;
using DTOs;

namespace WebAPI.SGCityServices
{
    public class SGCityServiceSearchService
    {
        private readonly InMemoryCityServicesCollection inMemoryCityServicesCollection;
        public SGCityServiceSearchService(InMemoryCityServicesCollection inMemoryCityServicesCollection)
        {
            this.inMemoryCityServicesCollection = inMemoryCityServicesCollection;
        }

        public IEnumerable<CityService> SearchCityServices(IEnumerable<string> keywords, KeywordSearchOption? keywordSearchOption, SearchInLinkedDocumentSearchOption? searchInLinkedDocumentsSearchOption)
        {
            if(keywords == null)
            {
                return inMemoryCityServicesCollection.CityServices;
            }
            var services = (keywordSearchOption, searchInLinkedDocumentsSearchOption) switch
            {
                (KeywordSearchOption.MustMatchAllKeywords, SearchInLinkedDocumentSearchOption.SearchOnlyInLinkedDocuments) => inMemoryCityServicesCollection.CityServices.Where(service => keywords.All(k => service.ScrapedInformation.ToString()?.Contains(k) == true)),
                (KeywordSearchOption.MustMatchOnlyOneKeyword, SearchInLinkedDocumentSearchOption.SearchOnlyInLinkedDocuments) => inMemoryCityServicesCollection.CityServices.Where(service => keywords.Any(k => service.ScrapedInformation.ToString()?.Contains(k) == true)),
                (KeywordSearchOption.MustMatchAllKeywords, SearchInLinkedDocumentSearchOption.Never) => inMemoryCityServicesCollection.CityServices.Where(service => keywords.All(k => service.ToString()?.Contains(k) == true)),
                (KeywordSearchOption.MustMatchOnlyOneKeyword, SearchInLinkedDocumentSearchOption.Never) => inMemoryCityServicesCollection.CityServices.Where(service => keywords.Any(k => service.ToString()?.Contains(k) == true)),
                (KeywordSearchOption.MustMatchAllKeywords, SearchInLinkedDocumentSearchOption.Always) => inMemoryCityServicesCollection.CityServices.Where(service => keywords.All(k => service.ToString().Contains(k)) || keywords.All(k => service.ScrapedInformation.ToString()?.Contains(k) == true)),
                (KeywordSearchOption.MustMatchOnlyOneKeyword, SearchInLinkedDocumentSearchOption.Always) => inMemoryCityServicesCollection.CityServices.Where(service => keywords.Any(k => service.ToString().Contains(k)) || keywords.Any(k => service.ScrapedInformation.ToString()?.Contains(k) == true)),
                _ => inMemoryCityServicesCollection.CityServices
            };
            return services;
        }

        public IEnumerable<IGrouping<string, CityService>> SearchCityServicesGrouped(IEnumerable<string> keywords, KeywordSearchOption? keywordSearchOption, SearchInLinkedDocumentSearchOption? searchInLinkedDocumentsSearchOption, CityServiceGroupBy cityServiceGroupBy)
        {
            var services = SearchCityServices(keywords, keywordSearchOption, searchInLinkedDocumentsSearchOption);

            return cityServiceGroupBy switch
            {
                CityServiceGroupBy.art_der_dienstleistung => services.GroupBy(s => s.art_der_dienstleistung),
                CityServiceGroupBy.dienststelle => services.GroupBy(s => s.dienststelle),
                CityServiceGroupBy.thema => services.GroupBy(s => s.thema),
                _ => services.GroupBy(s => s.art_der_dienstleistung)
            };
        }
    }
}

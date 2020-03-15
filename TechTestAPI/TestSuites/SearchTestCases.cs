using System.Net;
using NUnit.Allure.Attributes;
using NUnit.Framework;
using RestSharp;
using TechTestAPI.Base;
using TechTestAPI.Base.Http;
using TechTestAPI.Helpers;
using TechTestAPI.Models;

namespace TechTestAPI.TestSuites
{
    [TestFixture]
    [AllureEpic("AllureEpic")] // kullanıma göre özelleştirilebilir
    [AllureFeature("APITests")] // kullanıma göre özelleştirilebilir
    [AllureParentSuite("AllureParentSuite")] // kullanıma göre özelleştirilebilir
    [AllureSuite("AllureSuite")] // kullanıma göre özelleştirilebilir
    [AllureTag("AllureTag", "Get")] // kullanıma göre özelleştirilebilir
    [AllureSeverity]
    public class SearchTestCases : BaseClass
    {
        [SetUp]
        public void BeforeMethod()
        {
        }

        [TearDown]
        public void After()
        {
            AfterMethod(TestContext
                .CurrentContext); // log kaydı alınması için her test sonucunda burdan base class içerisine gider
        }

        private IRestResponse _searchResponse;

        [TestCase("t", "harry potter", ParameterType.GetOrPost)]
        [Order(1)]
        public void Should_ALLTitlesContains_EnteredTitle(string name, string title, ParameterType type)
        {
            _searchResponse =
                RestHttpClient.Create()
                    .Get<SearchResponse>(new Parameter(name, title, type)); //yukarıdaki bilgiler ile istek atılır
            Assert.That(_searchResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                string.Format(Messages.HTTP_Status_Error_Message, name, title, type, _searchResponse.ResponseUri,
                    _searchResponse
                        .Content)); //donen response status kontrol edilir beklenen status dönmediyse hata alır
            var responseData =
                JsonConverter.ConvertJsonToObject<SearchResponse>(_searchResponse
                    .Content); // response content te bulunan data yı deserialize edilir
            var titleControl =
                responseData.Search.Find(x =>
                    !x.Title.Contains(
                        title)); //tüm titlelar arasında içinde harry potter geçmeyen data var mı kontrol edilir
            Assert.Null(titleControl,
                string.Format(Messages.Title_Control_Error_Message, name, title, type, _searchResponse.ResponseUri,
                    _searchResponse.Content, title, JsonConverter.ConvertObjectToJson(titleControl))
            ); //title içerisinde olmayan data varsa hata verir.
        }

        [TestCase("i", "tt0295297", ParameterType.GetOrPost)]
        [Order(2)]
        public void Should_ReturnsData_When_SearchByID(string name, string imdbId, ParameterType type)
        {
            _searchResponse =
                RestHttpClient.Create()
                    .Get<SearchResponseById>(new Parameter(name, imdbId, type)); //yukarıdaki bilgiler ile istek atılır
            Assert.That(_searchResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                string.Format(Messages.HTTP_Status_Error_Message, name, imdbId, type, _searchResponse.ResponseUri,
                    _searchResponse
                        .Content)); //donen response status kontrol edilir beklenen status dönmediyse hata alır
            var responseData = JsonConverter.ConvertJsonToObject<SearchResponseById>(_searchResponse
                .Content); // response content te bulunan data yı deserialize edilir
            Assert.That(responseData.ImdbId, Is.EqualTo(imdbId),
                string.Format(Messages.Search_By_IMDBId_Error_Message, name, imdbId, type, _searchResponse.ResponseUri,
                    _searchResponse
                        .Content)); //response da bulunan ımdb ıd ile search edilen ımdb ıd aynı değilse hata alır.
        }

        [TestCase("s", ParameterType.GetOrPost, "harry potter", "Harry Potter and the Sorcerer's Stone")]
        [Order(3)]
        public void Control_ReturnedData_When_SearchByTitle(string name, ParameterType type, string title,
            string wantedTitleResult)
        {
            _searchResponse =
                RestHttpClient.Create()
                    .Get<SearchResponse>(new Parameter(name, title, type)); //title :harry potter araması yapılır
            var responseData =
                JsonConverter.ConvertJsonToObject<SearchResponse>(_searchResponse
                    .Content); //dönen sonuc deserialize edilir 
            Assert.That(_searchResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                string.Format(Messages.HTTP_Status_Error_Message, name, title,
                    type, _searchResponse.ResponseUri,
                    _searchResponse.Content)); //dönen sonucun status kodu OK degilse hata alır. OK ise devam eder
            var wantedResult =
                new SearchResponse().FindResult(responseData.Search,
                    wantedTitleResult); //donen sonuclardan title:"Harry Potter and the Sorcerer's Stone" olan film bulunur

            var detailResponse = RestHttpClient.Create()
                .Get<SearchResponseById>(new Parameter("i", wantedResult.ImdbId,
                    type)); //bulunan filmin id si Search By ımdbId ile search edilecek şekilde istek atılır
            Assert.That(wantedResult, !Is.Null, string.Format(Messages.Wanted_Result_Control_Error_Message, name, title,
                type, detailResponse.ResponseUri,
                detailResponse.Content,
                wantedTitleResult)); //donen sonuclardan title:"Harry Potter and the Sorcerer's Stone" olan film yoksa hata alır
            var detailResponseData =
                JsonConverter.ConvertJsonToObject<SearchResponseById>(_searchResponse
                    .Content); //dönen sonuclar deserialize edilir
            Assert.Multiple(() =>
            {
                Assert.That(detailResponseData.Title, !Is.Empty,
                    string.Format(Messages.Detail_Control_Error_Message, name, title, type, detailResponse.ResponseUri,
                        detailResponse.Content, title, wantedTitleResult,
                        "Title")); // dönen sonucun 'Title' ı boşmu kontrol edilir boşsa hata alır
                Assert.That(detailResponseData.Year, !Is.Empty,
                    string.Format(Messages.Detail_Control_Error_Message, name, title, type, detailResponse.ResponseUri,
                        detailResponse.Content, title, wantedTitleResult,
                        "Year")); // dönen sonucun 'Year' ı boşmu kontrol edilir boşsa hata alır
                Assert.That(detailResponseData.Released, !Is.Empty,
                    string.Format(Messages.Detail_Control_Error_Message, name, title, type, detailResponse.ResponseUri,
                        detailResponse.Content, wantedTitleResult,
                        "Released")); // dönen sonucun 'Released' ı boşmu kontrol edilir boşsa hata alır
            });
        }
    }
}
using System.Collections.Generic;

namespace TechTestAPI.Models
{
    public class SearchResponse
    {
        public SearchResponse()
        {
            Search = new List<Results>();
        }

        public List<Results> Search { get; set; }
        public string TotalResults { get; set; }
        public string Response { get; set; }

        public Results FindResult(List<Results> list, string searchTitle)
        {
            return list.Find(x => x.Title == searchTitle);
        }
        
    }

    public class Results
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string ImdbId { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
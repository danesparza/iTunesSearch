using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using iTunesSearch.Library.Models;

namespace iTunesSearch.Library
{
    /// <summary>
    /// A wrapper for the iTunes search API.  More information here: 
    /// https://www.apple.com/itunes/affiliates/resources/documentation/itunes-store-web-service-search-api.html
    /// </summary>
    public class iTunesSearchManager
    {
        /// <summary>
        /// The base API url for iTunes search
        /// </summary>
        private string _baseSearchUrl = "https://itunes.apple.com/search?{0}";

        /// <summary>
        /// The base API url for iTunes lookups
        /// </summary>
        private string _baseLookupUrl = "https://itunes.apple.com/lookup?{0}";

        #region TV shows

        /// <summary>
        /// Get a list of episodes for a given TV show
        /// </summary>
        /// <param name="showName">The TV show name to search for</param>
        /// <param name="resultLimit">Limit the result count to this number</param>
        /// <returns></returns>
        public async Task<TVEpisodeListResult> GetTVEpisodesForShow(string showName, int resultLimit = 100)
        {
            var nvc = HttpUtility.ParseQueryString(string.Empty);

            //  Set attributes for a TV season.  
            nvc.Add("term", showName);
            nvc.Add("media", "tvShow");
            nvc.Add("entity", "tvEpisode");
            nvc.Add("attribute", "showTerm");
            nvc.Add("limit", resultLimit.ToString());

            //  Construct the url:
            string apiUrl = string.Format(_baseSearchUrl, nvc.ToString());

            //  Get the list of episodes
            var result = await MakeAPICall<TVEpisodeListResult>(apiUrl);

            return result;
        }

        /// <summary>
        /// Get a list of seasons for a given TV show
        /// </summary>
        /// <param name="showName">The TV show name to search for</param>
        /// <param name="resultLimit">Limit the result count to this number</param>
        /// <returns></returns>
        public async Task<TVSeasonListResult> GetTVSeasonsForShow(string showName, int resultLimit = 10)
        {
            var nvc = HttpUtility.ParseQueryString(string.Empty);

            //  Set attributes for a TV season.  
            nvc.Add("term", showName);
            nvc.Add("media", "tvShow");
            nvc.Add("entity", "tvSeason");
            nvc.Add("attribute", "showTerm");
            nvc.Add("limit", resultLimit.ToString());

            //  Construct the url:
            string apiUrl = string.Format(_baseSearchUrl, nvc.ToString());

            //  Get the list of episodes
            var result = await MakeAPICall<TVSeasonListResult>(apiUrl);

            return result;
        }

        /// <summary>
        /// Looks up a TV show season by its unique iTunes season id
        /// </summary>
        /// <param name="seasonId"></param>
        /// <returns></returns>
        public async Task<TVSeasonListResult> GetTVSeasonById(long seasonId)
        {
            var nvc = HttpUtility.ParseQueryString(string.Empty);

            //  Set attributes for a TV season.  
            nvc.Add("id", seasonId.ToString());

            //  Construct the url:
            string apiUrl = string.Format(_baseLookupUrl, nvc.ToString());

            //  Get the list of episodes
            var result = await MakeAPICall<TVSeasonListResult>(apiUrl);

            return result;
        }

        #endregion

        #region API helpers

        /// <summary>
        /// Makes an API call and deserializes return value to the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiCall"></param>
        /// <returns></returns>
        async private Task<T> MakeAPICall<T>(string apiCall)
        {
            HttpClient client = new HttpClient();

            //  Make an async call to get the response
            var objString = await client.GetStringAsync(apiCall).ConfigureAwait(false);

            //  Deserialize and return
            return (T)DeserializeObject<T>(objString);
        }

        /// <summary>
        /// Deserializes the JSON string to the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objString"></param>
        /// <returns></returns>
        private T DeserializeObject<T>(string objString)
        {
            using(var stream = new MemoryStream(Encoding.Unicode.GetBytes(objString)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        } 

        #endregion
    }
}

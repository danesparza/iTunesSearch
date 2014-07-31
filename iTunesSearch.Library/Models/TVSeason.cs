using System;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace iTunesSearch.Library.Models
{
    [DataContract]
    public class TVSeason
    {
        [DataMember(Name = "artistId")]
        public int ShowId { get; set; }

        [DataMember(Name = "collectionId")]
        public int SeasonId { get; set; }

        [DataMember(Name = "collectionName")]
        public string SeasonName { get; set; }

        /// <summary>
        /// The parsed season number, based on the season name
        /// </summary>
        public int SeasonNumber
        {
            get
            {
                int retval = 0;

                //  See if we can parse the season number from the season name
                try
                {
                    string newString = Regex.Replace(this.SeasonName, "[^.0-9]", "");
                    retval = Convert.ToInt32(newString);
                }
                catch(Exception)
                { /* Don't do anything */ }

                return retval;
            }
        }
    }
}

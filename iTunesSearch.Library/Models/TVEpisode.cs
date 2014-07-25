using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace iTunesSearch.Library.Models
{
    [DataContract]
    public class TVEpisode
    {
        [DataMember(Name="trackName")]
        public string Name { get; set; }

        [DataMember(Name = "shortDescription")]
        public string DescriptionShort { get; set; }

        [DataMember(Name = "longDescription")]
        public string DescriptionLong { get; set; }

        [DataMember(Name = "artistName")]
        public string ShowName { get; set; }

        [DataMember(Name = "artistId")]
        public int ShowId { get; set; }

        [DataMember(Name = "collectionId")]
        public int SeasonId { get; set; }

        [DataMember(Name = "collectionName")]
        public string SeasonName { get; set; }

        [DataMember(Name = "trackNumber")]
        public int Number { get; set; }

        [DataMember(Name = "contentAdvisoryRating")]
        public string Rating { get; set; }

        [DataMember(Name = "releaseDate")]
        public string ReleaseDate { get; set; }

        [DataMember(Name = "artworkUrl100")]
        public string ArtworkUrl { get; set; }

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

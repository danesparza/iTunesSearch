using System;
using System.Diagnostics;
using System.Linq;
using iTunesSearch.Library.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iTunesSearch.Library.Tests
{
    [TestClass]
    public class iTunesSearchTests
    {
        [TestMethod]
        public void GetEpisodesForShow_ValidShow_ReturnsEpisodes()
        {
            //  Arrange
            iTunesSearchManager search = new iTunesSearchManager();
            string showName = "Modern Family";

            //  Act
            var items = search.GetEpisodesForShow(showName, 200).Result;

            //  Assert
            Assert.IsTrue(items.Episodes.Any());
        }

        [TestMethod]
        public void GetSeasonsAndEpisodesForShow_ValidShow_ReturnsEpisodes()
        {
            //  Arrange
            iTunesSearchManager search = new iTunesSearchManager();
            string showName = "Modern Family";

            //  Act
            var items = search.GetEpisodesForShow(showName, 200).Result;
            var seasons = from episode in items.Episodes
                          orderby episode.Number
                             group episode by episode.SeasonNumber into seasonGroup
                             orderby seasonGroup.Key
                             select seasonGroup;

            //  Assert
            foreach(var seasonGroup in seasons)
            {
                Debug.WriteLine("Season number: {0}", seasonGroup.Key);

                foreach(TVEpisode episode in seasonGroup)
                {
                    Debug.WriteLine("Ep {0}: {1}", episode.Number, episode.Name);
                }
            }
        }
    }
}

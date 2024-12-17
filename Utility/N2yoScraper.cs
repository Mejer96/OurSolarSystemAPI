using System;
using HtmlAgilityPack;


namespace OurSolarSystemAPI.Utility {
    public class N2yoScraper() 
    {
        public Dictionary<string, string> ExtractSatelliteInfoFromHtml(string htmlContent) 
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);
            var satinfoDiv = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='satinfo']");

            string period = ExtractData(satinfoDiv, "Period").Remove(0, 2);
            period = period.Replace(" minutes", "");

            var satelliteInfo = new Dictionary<string, string>
            {
                { "noradId", ExtractData(satinfoDiv, "NORAD ID") },
                { "intlCode", ExtractData(satinfoDiv, "Int'l Code") },
                { "perigee", ExtractData(satinfoDiv, "Perigee").Remove(0, 2) },
                { "apogee", ExtractData(satinfoDiv, "Apogee").Remove(0, 2) },
                { "inclination", ExtractData(satinfoDiv, "Inclination").Remove(0, 2) },
                { "period", period },
                { "semiMajorAxis", ExtractData(satinfoDiv, "Semi major axis").Remove(0, 2) },
                { "rcs", ExtractData(satinfoDiv, "RCS").Remove(0, 2) },
                { "launchDate", ExtractLaunchDate(satinfoDiv) },
                { "source", ExtractData(satinfoDiv, "Source") },
                { "launchSite", ExtractData(satinfoDiv, "Launch site").Remove(0, 2) }
            };

            return satelliteInfo;
        }

        public string ExtractData(HtmlNode parentNode, string label)
        {
            var node = parentNode.SelectSingleNode($".//b[contains(text(), \"{label}\")]");
            if (node != null && node.NextSibling != null)
            {
                return node.NextSibling.InnerText.Trim();
            }
            return string.Empty;
        }

        public string ExtractLaunchDate(HtmlNode parentNode)
        {
            var dateNode = parentNode.SelectSingleNode(".//b[contains(text(), 'Launch date')]/following-sibling::a");

            if (dateNode != null)
            {
                return dateNode.InnerText.Trim();
            }

            return string.Empty;
        }
    }
}
using System.Xml.Serialization;
using NoOrangeTrees.OptionsFramework;

namespace NoOrangeTrees
{
    public class Options : IModOptions
    {
        public Options()
        {
            shore = true;
            pollution = true;
            oil = true;
            forest = true;
            fertility = true;
            ore = true;
        }

        [Checkbox("No orange/dead trees at shoreline, no shoreline ground color", "NaturalResourcesManagerDetour", "RefreshTexture")]
        public bool shore { set; get; }
        [Checkbox("No orange/dead trees in polluted areas, no polluted area ground color", "NaturalResourcesManagerDetour", "RefreshTexture")]
        public bool pollution { set; get; }
        [Checkbox("No fertility area ground color", "NaturalResourcesManagerDetour", "RefreshTexture")]
        public bool fertility { set; get; }
        [Checkbox("No ore area gound color", "NaturalResourcesManagerDetour", "RefreshTexture")]
        public bool ore { set; get; }
        [Checkbox("No oil area gound color", "NaturalResourcesManagerDetour", "RefreshTexture")]
        public bool oil { set; get; }
        [Checkbox("No forest area gound color", "NaturalResourcesManagerDetour", "RefreshTexture")]
        public bool forest { set; get; }

        [XmlIgnore]
        public string FileName => "CSL-NoOrangeTrees.xml";
    }
}
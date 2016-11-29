using System.Xml.Serialization;
using NoOrangeTrees.Detours;
using NoOrangeTrees.OptionsFramework;

namespace NoOrangeTrees
{
    public class Options : IModOptions
    {
        public Options()
        {
            shore = true;
            pollution = true;
            oil = false;
            forest = false;
            fertility = false;
            ore = false;
            burned = false;
        }

        [Checkbox("No orange/dead trees at shoreline, no shoreline ground color", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool shore { set; get; }
        [Checkbox("No orange/dead trees in polluted areas, no polluted area ground color", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool pollution { set; get; }
        [Checkbox("No fertility area ground color", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool fertility { set; get; }
        [Checkbox("No ore area ground color", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool ore { set; get; }
        [Checkbox("No oil area ground color", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool oil { set; get; }
        [Checkbox("No forest area ground color", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool forest { set; get; }
        [Checkbox("No burned area ground color", nameof(NaturalResourcesManagerDetour), nameof(NaturalResourcesManagerDetour.RefreshTexture))]
        public bool burned { set; get; }


        [XmlIgnore]
        public string FileName => "CSL-NoOrangeTrees.xml";
    }
}
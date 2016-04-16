using System.Xml.Serialization;

namespace NoOrangeTrees.OptionsFramework
{
    public interface IModOptions
    {
        [XmlIgnore]
        string FileName
        {
            get;
        }
    }
}
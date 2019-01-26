using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Utls
{
    [Serializable]
    public class MenuConfig
    {
        public MenuConfig()
        {
        }

        public MenuGroup[] MenuGroups { get; set; }
    }

    [Serializable]
    public class MenuGroup
    {
        public List<Menu> MenuArray { get; set; }

        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("icon")]
        public string Icon { get; set; }

        [XmlAttribute("permission")]
        public string Permission { get; set; }

        [XmlAttribute("info")]
        public string Info { get; set; }
    }

    [Serializable]
    public class Menu
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("url")]
        public string Url { get; set; }

        [XmlAttribute("info")]
        public string Info { get; set; }

        [XmlAttribute("permission")]
        public string Permission { get; set; }
    }
}

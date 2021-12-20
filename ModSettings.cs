// Originally written by algernon for Find It 2.
// Modified by sway
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TreeVehicleProps
{
    /// <summary>
    /// Class to hold global mod settings.
    /// </summary>
    [XmlRoot(ElementName = "TreeVehicleProps", Namespace = "", IsNullable = false)]
    internal static class Settings
    {
        internal static bool skipVanillaTrees = false;
        internal static bool skipCustomTrees = false;
        internal static bool skipVanillaVehicles = false;
        internal static bool skipCustomVehicles = false;
        internal static bool removeTreeSway = false;

        internal static List<SkippedEntry> skippedVehicleEntries = new List<SkippedEntry>();
        internal static List<SkippedEntry> skippedTreeEntries = new List<SkippedEntry>();
    }

    /// <summary>
    /// Defines the XML settings file.
    /// </summary>
    [XmlRoot(ElementName = "TreeVehicleProps", Namespace = "", IsNullable = false)]
    public class XMLSettingsFile
    {
        [XmlElement("Language")]
        public string Language
        {
            get
            {
                return Translations.Language;
            }
            set
            {
                Translations.Language = value;
            }
        }

        [XmlElement("SkipVanillaTrees")]
        public bool SkipVanillaTrees { get => Settings.skipVanillaTrees; set => Settings.skipVanillaTrees = value; }

        [XmlElement("SkipCustomTrees")]
        public bool SkipCustomTrees { get => Settings.skipCustomTrees; set => Settings.skipCustomTrees = value; }

        [XmlElement("SkipVanillaVehicles")]
        public bool SkipVanillaVehicles { get => Settings.skipVanillaVehicles; set => Settings.skipVanillaVehicles = value; }

        [XmlElement("SkipCustomVehicles")]
        public bool SkipCustomVehicles { get => Settings.skipCustomVehicles; set => Settings.skipCustomVehicles = value; }

        [XmlElement("RemoveTreeSway")]
        public bool RemoveTreeSway { get => Settings.removeTreeSway; set => Settings.removeTreeSway = value; }

        [XmlArray("SkippedVehicleEntries")]
        [XmlArrayItem("SkippedEntry")]
        public List<SkippedEntry> SkippedVehicleEntries { get => Settings.skippedVehicleEntries; set => Settings.skippedVehicleEntries = value; }

        [XmlArray("SkippedTreeEntries")]
        [XmlArrayItem("SkippedEntry")]
        public List<SkippedEntry> SkippedTreeEntries { get => Settings.skippedTreeEntries; set => Settings.skippedTreeEntries = value; }
    }

    public class SkippedEntry
    {
        [XmlAttribute("Name")]
        public string name = "";

        [XmlAttribute("Skipped")]
        public bool skipped = false;

        public SkippedEntry() { }

        public SkippedEntry(string newName) { name = newName; }
    }
}
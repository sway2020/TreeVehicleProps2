// Originally written by algernon for Find It 2.

using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace TreeVehicleProps
{
    /// <summary>
    /// XML serialization/deserialization utilities class.
    /// </summary>
    internal static class XMLUtils
    {
        internal static readonly string SettingsFileName = "TreeVehiclePropsConfig.xml";

        /// <summary>
        /// Load settings from XML file.
        /// </summary>
        internal static void LoadSettings()
        {
            try
            {
                // Check to see if configuration file exists.
                if (File.Exists(SettingsFileName))
                {
                    // Read it.
                    using (StreamReader reader = new StreamReader(SettingsFileName))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(XMLSettingsFile));
                        if (!(xmlSerializer.Deserialize(reader) is XMLSettingsFile xmlSettingsFile))
                        {
                            Debug.Log("couldn't deserialize settings file");
                        }
                    }
                }
                else
                {
                    Debug.Log("no settings file found");
                }
            }
            catch (Exception e)
            {
                Debug.Log("exception reading XML settings file");
                Debug.LogException(e);
            }
        }

        /// <summary>
        /// Save settings to XML file.
        /// </summary>
        internal static void SaveSettings()
        {
            try
            {
                // Pretty straightforward.  Serialisation is within GBRSettingsFile class.
                using (StreamWriter writer = new StreamWriter(SettingsFileName))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(XMLSettingsFile));
                    xmlSerializer.Serialize(writer, new XMLSettingsFile());
                }
            }
            catch (Exception e)
            {
                Debug.Log("exception saving XML settings file");
                Debug.LogException(e);
            }
        }
    }
}
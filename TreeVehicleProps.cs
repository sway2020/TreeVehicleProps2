using CitiesHarmony.API;
using HarmonyLib;
using ICities;
using System.Reflection;
using ColossalFramework;
using System.Collections.Generic;
using System;
using ColossalFramework.UI;
using UnityEngine;
using System.IO;
using ColossalFramework.IO;

namespace TreeVehicleProps
{
    public class Mod : IUserMod
    {
        public const string version = "2.0";
        public string Name => "Tree & Vehicle Props " + version;
        public string Description
        {
            get { return Translations.Translate("TVP_DESC"); }
        }

        public static PropInfo templateProp;

        public static Dictionary<string, bool> skippedVehicleDictionary = new Dictionary<string, bool>();
        public static Dictionary<string, bool> skippedTreeDictionary = new Dictionary<string, bool>();

        public static Dictionary<PropInfo, TreeInfo> propToTreeCloneMap = new Dictionary<PropInfo, TreeInfo>();
        public static Dictionary<PropInfo, VehicleInfo> propToVehicleCloneMap = new Dictionary<PropInfo, VehicleInfo>();

        public static HashSet<PropInfo> generatedVehicleProp = new HashSet<PropInfo>();
        public static HashSet<PropInfo> generatedTreeProp = new HashSet<PropInfo>();
        public static Dictionary<PropInfo, VehicleInfo> propVehicleInfoTable = new Dictionary<PropInfo, VehicleInfo>();
        public static Dictionary<PropInfo, VehicleInfo> duplicateVehiclePropsInfoTable = new Dictionary<PropInfo, VehicleInfo>();

        public static List<PropInfo> tcProps = new List<PropInfo>();
        public static Dictionary<PropInfo, PropInfo> cloneMap = new Dictionary<PropInfo, PropInfo>();
        public static bool PrefabsInitialized = false;

        public void OnEnabled()
        {
            HarmonyHelper.DoOnHarmonyReady(() => Patcher.PatchAll());
            XMLUtils.LoadSettings();

            foreach (SkippedEntry entry in Settings.skippedVehicleEntries)
            {
                if (skippedVehicleDictionary.ContainsKey(entry.name)) continue;
                skippedVehicleDictionary.Add(entry.name, entry.skipped);
            }

            foreach (SkippedEntry entry in Settings.skippedTreeEntries)
            {
                if (skippedTreeDictionary.ContainsKey(entry.name)) continue;
                skippedTreeDictionary.Add(entry.name, entry.skipped);
            }
        }

        public void OnDisabled()
        {
            if (HarmonyHelper.IsHarmonyInstalled) Patcher.UnpatchAll();
        }
      
        public void OnSettingsUI(UIHelperBase helper)
        {
            try
            {
                UIHelper group = helper.AddGroup(Name) as UIHelper;
                UIPanel panel = group.self as UIPanel;


                UICheckBox skipVanillaTrees = (UICheckBox)group.AddCheckbox(Translations.Translate("TVP_SET_VAN_TREE"), Settings.skipVanillaTrees, (b) =>
                {
                    Settings.skipVanillaTrees = b;
                    XMLUtils.SaveSettings();
                });
                skipVanillaTrees.tooltip = Translations.Translate("TVP_SET_VAN_TREETP");
                group.AddSpace(10);

                UICheckBox skipCustomTrees = (UICheckBox)group.AddCheckbox(Translations.Translate("TVP_SET_CUS_TREE"), Settings.skipCustomTrees, (b) =>
                {
                    Settings.skipCustomTrees = b;
                    XMLUtils.SaveSettings();
                });
                skipCustomTrees.tooltip = Translations.Translate("TVP_SET_CUS_TREETP");
                group.AddSpace(10);


                UICheckBox skipVanillaVehicles = (UICheckBox)group.AddCheckbox(Translations.Translate("TVP_SET_VAN_VEHI"), Settings.skipVanillaVehicles, (b) =>
                {
                    Settings.skipVanillaVehicles = b;
                    XMLUtils.SaveSettings();
                });
                skipVanillaVehicles.tooltip = Translations.Translate("TVP_SET_VAN_VEHITP");
                group.AddSpace(10);

                UICheckBox skipCustomVehicles = (UICheckBox)group.AddCheckbox(Translations.Translate("TVP_SET_CUS_VEHI"), Settings.skipCustomVehicles, (b) =>
                {
                    Settings.skipCustomVehicles = b;
                    XMLUtils.SaveSettings();
                });
                skipCustomVehicles.tooltip = Translations.Translate("TVP_SET_CUS_VEHITP");
                group.AddSpace(10);

                UICheckBox removeTreeSway = (UICheckBox)group.AddCheckbox(Translations.Translate("TVP_SET_CUS_TREE_SWAY"), Settings.removeTreeSway, (b) =>
                {
                    Settings.removeTreeSway = b;
                    XMLUtils.SaveSettings();
                });
                group.AddSpace(10);

                // languate settings
                UIDropDown languageDropDown = (UIDropDown)group.AddDropdown(Translations.Translate("TRN_CHOICE"), Translations.LanguageList, Translations.Index, (value) =>
                {
                    Translations.Index = value;
                    XMLUtils.SaveSettings();
                });

                languageDropDown.width = 300;
                group.AddSpace(10);

                // show path to NonTerrainConformingPropsConfig.xml
                string path = Path.Combine(DataLocation.executableDirectory, "TreeVehiclePropsConfig.xml");
                UITextField customTagsFilePath = (UITextField)group.AddTextfield(Translations.Translate("TVP_SET_CONF") + " - TreeVehiclePropsConfig.xml", path, _ => { }, _ => { });
                customTagsFilePath.width = panel.width - 30;
                group.AddButton(Translations.Translate("TVP_SET_CONFFE"), () => System.Diagnostics.Process.Start(DataLocation.executableDirectory));
            
            }
            catch (Exception e)
            {
                Debug.Log("OnSettingsUI failed");
                Debug.LogException(e);
            }
        }
    }

}

// modified from Elektrix's Tree and Vehicle Props
using System.Collections.Generic;
using ICities;
using UnityEngine;
using System;

namespace TreeVehicleProps
{
    public class Generator : AssetDataExtensionBase
    {
        private static Shader shader = Shader.Find("Custom/Props/Prop/Default");

        public static void GenerateTreeProp(TreeInfo tree)
        {
            if (tree == null) return;
            if (Settings.skipVanillaTrees && !tree.m_isCustomContent) return;
            if (Mod.skippedTreeDictionary.ContainsKey(tree.name) && Mod.skippedTreeDictionary[tree.name]) return;

            try
            {
                PropInfo propInfo = CloneProp();
                propInfo.name = tree.name.Replace("_Data", "") + " Prop_Data";
                propInfo.m_mesh = tree.m_mesh;
                propInfo.m_material = tree.m_material;
                propInfo.m_Thumbnail = tree.m_Thumbnail;
                propInfo.m_InfoTooltipThumbnail = tree.m_InfoTooltipThumbnail;
                propInfo.m_InfoTooltipAtlas = tree.m_InfoTooltipAtlas;
                propInfo.m_Atlas = tree.m_Atlas;
                propInfo.m_generatedInfo.m_center = tree.m_generatedInfo.m_center;
                propInfo.m_generatedInfo.m_uvmapArea = tree.m_generatedInfo.m_uvmapArea;
                propInfo.m_generatedInfo.m_size = tree.m_generatedInfo.m_size;
                propInfo.m_generatedInfo.m_triangleArea = tree.m_generatedInfo.m_triangleArea;
                propInfo.m_color0 = tree.m_defaultColor;
                propInfo.m_color1 = tree.m_defaultColor;
                propInfo.m_color2 = tree.m_defaultColor;
                propInfo.m_color3 = tree.m_defaultColor;
                Mod.propToTreeCloneMap.Add(propInfo, tree);
                Mod.generatedTreeProp.Add(propInfo);

                if (!Mod.skippedTreeDictionary.ContainsKey(tree.name))
                {
                    Settings.skippedTreeEntries.Add(new SkippedEntry(tree.name));
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"TreeVehicleProps exception caught: TreeInfo {tree.name} may be broken, {ex.Message}");
            }
        }

        public static void GenerateVehicleProp(VehicleInfo vehicle)
        {
            if (vehicle == null || vehicle.name == "Vortex") return;
            if (Settings.skipVanillaVehicles && !vehicle.m_isCustomContent) return;
            if (Mod.skippedVehicleDictionary.ContainsKey(vehicle.name) && Mod.skippedVehicleDictionary[vehicle.name]) return;

            try
            {
                PropInfo propInfo = CloneProp();
                propInfo.name = vehicle.name.Replace("_Data", "") + " Prop_Data";
                propInfo.m_mesh = vehicle.m_mesh;
                propInfo.m_material = UnityEngine.Object.Instantiate<Material>(vehicle.m_material);

                bool flag2 = propInfo.m_material != null;
                if (flag2)
                {
                    propInfo.m_material.shader = shader;
                }
                propInfo.m_Thumbnail = vehicle.m_Thumbnail;
                propInfo.m_InfoTooltipThumbnail = vehicle.m_InfoTooltipThumbnail;
                propInfo.m_InfoTooltipAtlas = vehicle.m_InfoTooltipAtlas;
                propInfo.m_Atlas = vehicle.m_Atlas;
                propInfo.m_color0 = vehicle.m_color0;
                propInfo.m_color1 = vehicle.m_color1;
                propInfo.m_color2 = vehicle.m_color2;
                propInfo.m_color3 = vehicle.m_color3;
                Mod.propToVehicleCloneMap.Add(propInfo, vehicle);
                Mod.propVehicleInfoTable.Add(propInfo, vehicle);

                if (!Mod.skippedVehicleDictionary.ContainsKey(vehicle.name))
                {
                    Settings.skippedVehicleEntries.Add(new SkippedEntry(vehicle.name));
                }
            }
            catch (Exception ex)
            {
                Debug.Log($"TreeVehicleProps exception caught: VehicleInfo {vehicle.name} may be broken, {ex.Message}");
            }
        }

        public static PropInfo CloneProp()
        {
            if (Mod.templateProp == null)
            {
                Debug.Log($"TreeVehicleProps error: Conversion template TVPConversionTemplate.crp was not loaded");
            }

            GameObject gameObject = UnityEngine.Object.Instantiate(Mod.templateProp.gameObject);
            gameObject.SetActive(value: false);
            PrefabInfo component = gameObject.GetComponent<PrefabInfo>();
            component.m_isCustomContent = true;
            return gameObject.GetComponent<PropInfo>();
        }

        public override void OnAssetLoaded(string name, object asset, Dictionary<string, byte[]> userData)
        {
            if (asset is PropInfo)
            {
                PropInfo propInfo = asset as PropInfo;
                if (propInfo.name.EndsWith("TVPConversionTemplate_Data"))
                {
                    Mod.templateProp = propInfo;
                }
            }
        }
    }
}

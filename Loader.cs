using ICities;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace TreeVehicleProps
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public static Shader shader = Shader.Find("Custom/Props/Prop/Default");
        public override void OnLevelLoaded(LoadMode mode)
        {
            foreach (KeyValuePair<PropInfo, VehicleInfo> keyValuePair in Mod.propToVehicleCloneMap)
            {
                PropInfo key = keyValuePair.Key;
                VehicleInfo value = keyValuePair.Value;
                try
                {
                    if (value.m_lodMesh != null)
                    {
                        key.m_lodMesh = UnityEngine.Object.Instantiate<Mesh>(value.m_lodMesh);
                    }
                    if (value.m_lodMaterial != null)
                    {
                        key.m_lodMaterial = UnityEngine.Object.Instantiate<Material>(value.m_lodMaterial);
                        key.m_lodMaterial.shader = shader;
                        key.m_lodMaterialCombined = UnityEngine.Object.Instantiate<Material>(value.m_lodMaterialCombined);
                        key.m_lodMaterialCombined.shader = shader;
                        key.m_lodColors = value.m_lodColors;
                    }
                    if (value.m_lodMeshCombined1 != null)
                    {
                        key.m_lodMeshCombined1 = UnityEngine.Object.Instantiate<Mesh>(value.m_lodMeshCombined1);
                    }
                    if (value.m_lodMeshCombined4 != null)
                    {
                        key.m_lodMeshCombined4 = UnityEngine.Object.Instantiate<Mesh>(value.m_lodMeshCombined4);
                    }
                    if (value.m_lodMeshCombined8 != null)
                    {
                        key.m_lodMeshCombined8 = UnityEngine.Object.Instantiate<Mesh>(value.m_lodMeshCombined8);
                    }
                    if (value.m_lodMeshCombined16 != null)
                    {
                        key.m_lodMeshCombined16 = UnityEngine.Object.Instantiate<Mesh>(value.m_lodMeshCombined16);
                    }
                    key.m_lodRenderDistance = value.m_lodRenderDistance;
                    key.m_maxRenderDistance = value.m_maxRenderDistance;
                    key.m_isCustomContent = value.m_isCustomContent;
                    key.m_dlcRequired = value.m_dlcRequired;
                    key.m_generatedInfo = UnityEngine.Object.Instantiate<PropInfoGen>(key.m_generatedInfo);
                    key.m_generatedInfo.name = key.name;
                    key.m_generatedInfo.m_propInfo = key;
                    key.m_generatedInfo.m_uvmapArea = value.m_generatedInfo.m_uvmapArea;
                    key.m_generatedInfo.m_triangleArea = value.m_generatedInfo.m_triangleArea;
                    if (key.m_mesh != null)
                    {
                        key.m_generatedInfo.m_size = Vector3.one * (Math.Max(key.m_mesh.bounds.extents.x, Math.Max(key.m_mesh.bounds.extents.y, key.m_mesh.bounds.extents.z)) * 2f - 1f);
                    }
                    if (key.m_material != null)
                    {
                        key.m_material.SetColor("_ColorV0", key.m_color0);
                        key.m_material.SetColor("_ColorV1", key.m_color1);
                        key.m_material.SetColor("_ColorV2", key.m_color2);
                        key.m_material.SetColor("_ColorV3", key.m_color3);
                    }
                    if (key.m_lodMaterial != null)
                    {
                        key.m_lodMaterial.SetColor("_ColorV0", key.m_color0);
                        key.m_lodMaterial.SetColor("_ColorV1", key.m_color1);
                        key.m_lodMaterial.SetColor("_ColorV2", key.m_color2);
                        key.m_lodMaterial.SetColor("_ColorV3", key.m_color3);
                    }

                    Mod.generatedVehicleProp.Add(key);
                }
                catch (Exception ex)
                {
                    Debug.Log($"TreeVehicleProps exception caught: VehicleInfo {value.name} may be broken, {ex.Message}");
                }
            }

            foreach (KeyValuePair<PropInfo, TreeInfo> keyValuePair2 in Mod.propToTreeCloneMap)
            {
                PropInfo key2 = keyValuePair2.Key;
                TreeInfo value2 = keyValuePair2.Value;
                try
                {
                    key2.m_lodMesh = value2.m_mesh;
                    key2.m_lodMaterial = value2.m_material;
                    key2.m_color0 = value2.m_defaultColor;
                    key2.m_color1 = value2.m_defaultColor;
                    key2.m_color2 = value2.m_defaultColor;
                    key2.m_color3 = value2.m_defaultColor;
                    key2.m_lodObject = key2.gameObject;
                    key2.m_generatedInfo = UnityEngine.Object.Instantiate<PropInfoGen>(key2.m_generatedInfo);
                    key2.m_generatedInfo.name = key2.name;
                    key2.m_isCustomContent = value2.m_isCustomContent;
                    key2.m_dlcRequired = value2.m_dlcRequired;
                    key2.m_generatedInfo.m_propInfo = key2;
                    if (key2.m_mesh != null)
                    {
                        if (key2.m_isCustomContent)
                        {
                            key2.m_mesh = UnityEngine.Object.Instantiate<Mesh>(value2.m_mesh);
                        }
                        key2.m_generatedInfo.m_size = Vector3.one * (Math.Max(key2.m_mesh.bounds.extents.x, Math.Max(key2.m_mesh.bounds.extents.y, key2.m_mesh.bounds.extents.z)) * 2f - 1f);
                    }
                    if (key2.m_material != null)
                    {
                        key2.m_material.SetColor("_ColorV0", key2.m_color0);
                        key2.m_material.SetColor("_ColorV1", key2.m_color1);
                        key2.m_material.SetColor("_ColorV2", key2.m_color2);
                        key2.m_material.SetColor("_ColorV3", key2.m_color3);
                    }
                    if (Settings.removeTreeSway && key2.m_isCustomContent)
                    {
                        try
                        {
                            if (key2.m_material.shader.name == "Custom/Trees/Default")
                            {
                                Color[] array2 = new Color[key2.m_mesh.vertices.Length];
                                for (int i = 0; i < array2.Length; i++)
                                {
                                    array2[i] = new Color(0f, 0f, 0f, 0f);
                                }
                                key2.m_mesh.colors = array2;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.Log($"{ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log($"TreeVehicleProps exception caught: TreeInfo {value2.name} may be broken, {ex.Message}");
                }
            }

            XMLUtils.SaveSettings();
            return;
        }

        public override void OnLevelUnloading()
        {
            Mod.PrefabsInitialized = false;
            Mod.propToVehicleCloneMap.Clear();
            Mod.propToTreeCloneMap.Clear();
            Mod.generatedVehicleProp.Clear();
            Mod.generatedTreeProp.Clear();
            Mod.propVehicleInfoTable.Clear();
            Mod.duplicateVehiclePropsInfoTable.Clear();
        }

    }

}

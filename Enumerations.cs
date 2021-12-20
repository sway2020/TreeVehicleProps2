// modified from Elektrix's Tree and Vehicle Props
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TreeVehicleProps
{
    public class Enumerations
    {
        public static IEnumerator CreateClones()
        {
            if (!(Settings.skipCustomTrees && Settings.skipVanillaTrees))
            {
                int count = PrefabCollection<TreeInfo>.LoadedCount();
                for (uint i = 0; i < count; i++)
                {
                    Generator.GenerateTreeProp(PrefabCollection<TreeInfo>.GetLoaded(i));
                    yield return null;
                }
            }
            yield return null;

            if (!(Settings.skipCustomVehicles && Settings.skipVanillaVehicles))
            {
                int count = PrefabCollection<VehicleInfo>.LoadedCount();
                for (uint i = 0; i < count; i++)
                {
                    Generator.GenerateVehicleProp(PrefabCollection<VehicleInfo>.GetLoaded(i));
                    yield return null;
                }
            }
            yield return 0;

        }

        public static IEnumerator InitializeAndBindClones()
        {
            Dictionary<string, PropInfo> customPropNamesDict = new Dictionary<string, PropInfo>();
            int count = PrefabCollection<PropInfo>.LoadedCount();
            for (uint i = 0; i < count; i++)
            {
                PropInfo prop = PrefabCollection<PropInfo>.GetLoaded(i);
                if (!prop.m_isCustomContent) continue;
                customPropNamesDict.Add(prop.name, prop);
            }

            HashSet<PropInfo> DuplicateTreeProps = new HashSet<PropInfo>();
            foreach (var i in Mod.propToTreeCloneMap)
            {
                if (customPropNamesDict.ContainsKey(i.Key.name)) DuplicateTreeProps.Add(i.Key);
            }
            foreach (var i in DuplicateTreeProps)
            {
                if (Mod.propToTreeCloneMap.ContainsKey(i)) Mod.propToTreeCloneMap.Remove(i);
            }

            HashSet<PropInfo> DuplicateVehicleProps = new HashSet<PropInfo>();
            foreach (var i in Mod.propToVehicleCloneMap)
            {
                if (customPropNamesDict.ContainsKey(i.Key.name))
                {
                    DuplicateVehicleProps.Add(i.Key);
                    Mod.duplicateVehiclePropsInfoTable.Add(customPropNamesDict[i.Key.name], i.Value);
                }
            }
            foreach (var i in DuplicateVehicleProps)
            {
                if (Mod.propToVehicleCloneMap.ContainsKey(i)) Mod.propToVehicleCloneMap.Remove(i);
            }
            yield return null;

            PrefabCollection<PropInfo>.InitializePrefabs("Tree to Prop", Mod.propToTreeCloneMap.Select((KeyValuePair<PropInfo, TreeInfo> k) => k.Key).ToArray(), null);
            yield return null;
            PrefabCollection<PropInfo>.InitializePrefabs("Vehicle to Prop", Mod.propToVehicleCloneMap.Select((KeyValuePair<PropInfo, VehicleInfo> k) => k.Key).ToArray(), null);
            yield return null;
            PrefabCollection<PropInfo>.BindPrefabs();
        }
    }
}
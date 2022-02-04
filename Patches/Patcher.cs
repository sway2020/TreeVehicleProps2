using HarmonyLib;
using System.Reflection;
using ColossalFramework;
using System;
using UnityEngine;

namespace TreeVehicleProps
{
    internal static class Patcher
    {
        private const string HarmonyId = "sway.TreeVehicleProps";

        private static bool patched = false;

        public static void PatchAll()
        {
            if (patched) return;

            UnityEngine.Debug.Log("TreeVehicleProps: Patching...");

            patched = true;

            // Harmony.DEBUG = true;
            var harmony = new Harmony("sway.TreeVehicleProps");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        public static void UnpatchAll()
        {
            if (!patched) return;

            var harmony = new Harmony(HarmonyId);
            harmony.UnpatchAll(HarmonyId);

            patched = false;

            UnityEngine.Debug.Log("TreeVehicleProps: Reverted...");
        }

    }

    [HarmonyPatch(typeof(PropInstance), "RenderInstance", new Type[]
    {
        typeof(RenderManager.CameraInfo), typeof(PropInfo), typeof(InstanceID), typeof(Vector3), typeof(float), typeof(float), typeof(Color), typeof(Vector4), typeof(bool)
    })]
    public static class RenderInstancePatch
    {
        public static void Prefix(ref bool active, ref PropInfo info)
        {
            if (Mod.generatedVehicleProp.Contains(info))
            {
                active = false;
            }
        }
    }


    [HarmonyPatch(typeof(RenderManager), "Managers_CheckReferences")]
    public static class LoadingHook
    {
        public static void Prefix()
        {
            if (!Mod.PrefabsInitialized)
            {
                Mod.PrefabsInitialized = true;
                Singleton<LoadingManager>.instance.QueueLoadingAction(Enumerations.CreateClones());
                Singleton<LoadingManager>.instance.QueueLoadingAction(Enumerations.InitializeAndBindClones());
            }
        }
    }
}

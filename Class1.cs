using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using RimWorld;
using Verse;


namespace FrauDecon
{
    [StaticConstructorOnStartup]
    internal static class HarmonyInit
    {
        static HarmonyInit()
        {
            Log.Message("Loaded ReloadCompMod");
            new Harmony("ReloadComp.Mod").PatchAll();
        }
    }
    [HarmonyPatch(typeof(ReloadableUtility), "FindSomeReloadableComponent")]
        static class Patch_ReloadableUtility_FindSomeReloadableComponent
        {
            static void Postfix(ref Pawn pawn, bool allowForcedReload, ref CompReloadable __result)
            {
                CompReloadable compReloadable = pawn.equipment.Primary.TryGetComp<CompReloadable>();
                if (compReloadable != null && compReloadable.NeedsReload(allowForcedReload))
                {
                Log.Message("reloadable weapon");
                __result = compReloadable;
                }
            Log.Message("not reloadable weapon");
            __result = null;
            }
        }
    [HarmonyPatch(typeof(ReloadableUtility), "WearerOf")]
    static class Patch_ReloadableUtility_WearerOf
    {
        static void Postfix(ref Pawn __result, ref CompReloadable comp)
        {
            Pawn_EquipmentTracker pawn_EquipmentTracker = comp.ParentHolder as Pawn_EquipmentTracker;
            if (pawn_EquipmentTracker != null)
            {
                Log.Message("WearerOf");
                __result = pawn_EquipmentTracker.pawn;
            }
            Log.Message("not WearerOf");
            __result = null;
        }
    }
}

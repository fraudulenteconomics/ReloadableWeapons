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
    [HarmonyPatch(typeof(ReloadableUtility), "FindSomeReloadableComponent")]
    static class Patch_ReloadableUtility_FindSomeReloadableComponent
    {
        static void Postfix(ref Pawn pawn, bool allowForcedReload, CompReloadable __result)
        {
            CompReloadable compReloadable = pawn.equipment.Primary.TryGetComp<CompReloadable>();
            if (compReloadable != null && compReloadable.NeedsReload(allowForcedReload))
            {
                __result = compReloadable;
            }
            __result = null;
        }
    }
}

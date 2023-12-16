using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShipFastLanding.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class ShipHubPatch
    {
        internal static GameObject shipHub = null;

        [HarmonyPatch("SetMapScreenInfoToCurrentLevel")]
        [HarmonyPrefix]
        private static void speedUpShipOnCompany(StartOfRound __instance)
        {
            if (shipHub == null)
            {
                shipHub = GameObject.Find("Environment/HangarShip");
                if (shipHub == null)
                {
                    Plugin.log.LogError("Didn't find ship!");
                    return;
                }
                Plugin.log.LogInfo("Found ship!");
            }
            if (shipHub?.GetComponent<Animator>() != null)
            {
                if (__instance.currentLevelID == 3 || Plugin.config_fastTravelToAllPlanets.Value)
                {
                    Plugin.log.LogInfo("Speeding up ship!");
                    shipHub.GetComponent<Animator>().speed = 10f;
                }
                else
                {
                    Plugin.log.LogInfo("Regular speed!");
                    shipHub.GetComponent<Animator>().speed = 1f;
                }
            }
            else
            {
                Plugin.log.LogError("Didn't find ship animator!");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprainkle.Patches
{
    [HarmonyPatch(typeof(Sprains), nameof(Sprains.Update))]
    internal class Sprains_Update
    {
        public static void Postfix(Sprains __instance)
        {
            if (!GameManager.m_IsPaused && !GameManager.s_IsGameplaySuspended && !GameManager.GetPlayerManagerComponent().m_God && (!GameManager.InCustomMode() || GameManager.GetCustomMode().m_EnableSprains) && GameManager.IsFrameValidToUpdate(GameManager.GameplayComponent.Sprains))
            {
                __instance.m_MinSecondsForSlopeRisk             = Settings.Instance.General_MinSecondsRisk;
                __instance.m_MinSlopeDegreesForSprain           = Settings.Instance.General_MinimumSlope;
                __instance.m_BaseChanceWhenMovingOnSlope        = Settings.Instance.General_BaseChanceMoving;
                __instance.m_ChanceIncreaseEncumbered           = Settings.Instance.General_EncumberanceChance;
                __instance.m_ChanceIncreaseExhausted            = Settings.Instance.General_ExhaustionChance;
                __instance.m_ChanceIncreaseSprinting            = Settings.Instance.General_SprintingChance;
                __instance.m_ChanceReduceWhenCrouchedPercent    = Settings.Instance.General_CrouchingChance;
                __instance.m_MinSecondsBeforeHidingWarning      = Settings.Instance.General_SprintUIOff;
                __instance.m_MinSecondsToShowWarning            = Settings.Instance.General_SprintUIOn;
            }
        }
    }
}

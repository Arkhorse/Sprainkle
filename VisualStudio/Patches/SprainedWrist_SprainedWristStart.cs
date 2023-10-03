using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprainkle.Patches
{
    [HarmonyPatch(typeof(SprainedWrist), nameof(SprainedWrist.SprainedWristStart), new Type[] { typeof(string), typeof(AfflictionOptions) })]
    internal class SprainedWrist_SprainedWristStart
    {
        public static void Postfix(SprainedWrist __instance)
        {
            if (GameManager.GetPlayerManagerComponent().PlayerIsDead() || InterfaceManager.IsPanelEnabled<Panel_ChallengeComplete>() || (GameManager.InCustomMode() && !GameManager.GetCustomMode().m_EnableSprains) || __instance.m_IsNoSprainWristForced || !Settings.Instance.WristSettings_Enabled)
            {
                return;
            }

            __instance.m_DurationHoursMin       = Settings.Instance.WristSettings_Duration_Minimum;
            __instance.m_DurationHoursMax       = Settings.Instance.WristSettings_Duration_Maximum;
            __instance.m_ChanceSprainAfterFall  = Settings.Instance.WristSettings_Chances_Fall;
            __instance.m_NumHoursRestForCure    = Settings.Instance.WristSettings_RestHours;
        }
    }
}

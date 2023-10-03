using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprainkle.Patches
{
    [HarmonyPatch(typeof(SprainedAnkle), nameof(SprainedAnkle.SprainedAnkleStart), new Type[] { typeof(string), typeof(AfflictionOptions) })]
    internal class SprainedAnkle_SprainedAnkleStart
    {
        public static void Postfix(SprainedAnkle __instance)
        {
            if (GameManager.GetPlayerManagerComponent().PlayerIsDead() ||
                InterfaceManager.IsPanelEnabled<Panel_ChallengeComplete>() ||
                (GameManager.InCustomMode() && !GameManager.GetCustomMode().m_EnableSprains) ||
                !Settings.Instance.AnkleSettings_Enabled)
            {
                return;
            }

            __instance.m_DurationHoursMin       = Settings.Instance.AnkleSettings_Duration_Minimum;
            __instance.m_DurationHoursMax       = Settings.Instance.AnkleSettings_Duration_Maximum;
            __instance.m_ChanceSprainAfterFall  = Settings.Instance.AnkleSettings_Chances_Fall;
            __instance.m_NumHoursRestForCure    = Settings.Instance.AnkleSettings_RestHours;
        }
    }
}

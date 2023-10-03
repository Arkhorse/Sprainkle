namespace Sprainkle
{
    public class Settings : JsonModSettings
    {
        internal static Settings Instance { get; } = new();
        public enum Presets { Vanilla, Custom, Enhanced }

        [Section("General")]

        [Name("Enable Mod")]
        public bool EnableMod = true;

        [Name("Presets")]
        [Description("Enhanced makes sprains less likely and less annoying")]
        public Presets m_Presets = Presets.Vanilla;

        #region Sprain Settings
        [Section("Sprains")]

        [Name("Minimum Slope for Sprain")]
        [Slider(0, 90, 91)]
        public int General_MinimumSlope = 30;

        [Name("Chance increase per degree above minimum")]
        [Description("How much to increase the chance of sprain while on a slope above the minimum slope")]
        [Slider(0f, 100f, 401)]
        public float General_MinimumSlopeIncrease = 1.5f;

        [Name("Base Chance while moving on slope")]
        [Slider(0f, 100f, 401)]
        public float General_BaseChanceMoving = 15f;

        [Name("Risk of Sprains from Encumberance")]
        [Slider(0f, 100f, 1001)]
        public float General_EncumberanceChance = 0.3f;

        [Name("Risk of Sprains from Exhaustion")]
        [Slider(0f, 100f, 1001)]
        public float General_ExhaustionChance = 0.3f;

        [Name("Risk of Sprains from Sprinting")]
        [Slider(0f, 100f, 1001)]
        public float General_SprintingChance = 2f;

        [Name("Risk of Sprains from Crouching")]
        [Description("The amount to reduce the chance of sprains when moving while crouched. 100: never sprain when crouched.")]
        [Slider(0f, 100f, 1001)]
        public float General_CrouchingChance = 75f;

        [Name("Minimum Seconds before risk")]
        [Description("How long the player has to move on a slope to be at risk.")]
        [Slider(0f, 60f, 241)]
        public float General_MinSecondsRisk = 1.5f;

        [Name("Chance of wrist sprain while moving")]
        [Slider(0f, 100f, 1001)]
        public float General_WristMovementChance = 50f;

        [Section("Sprain Risk UI")]

        [Name("On")]
        [Description("How long the player has to move on a slope before the warning UI is shown. Should be shorter than Min Seconds For Slope Risk.")]
        [Slider(0f, 10f, 101)]
        public float General_SprintUIOn = 0.5f;

        [Name("Off")]
        [Description("How long the player has to be off the slope before the warning UI is hidden. Should be shorter than Min Seconds For Slope Risk.")]
        [Slider(0f, 10f, 101)]
        public float General_SprintUIOff = 0.3f;
        #endregion
        #region Ankle Settings
        [Section("Ankle Options")]

        [Name("Allow Ankle Sprains")]
        [Description("When disabled, ankle sprains will not happen")]
        public bool AnkleSettings_Enabled = true;

        [Name("Duration Minimum")]
        [Slider(0f, 672f, 673)]
        public float AnkleSettings_Duration_Minimum = 48f;

        [Name("Duration Maximum")]
        [Slider(0f, 672f, 673)]
        public float AnkleSettings_Duration_Maximum = 72f;

        [Name("Hours to rest for cure")]
        [Slider(1f, 72f, 72)]
        public float AnkleSettings_RestHours = 4f;

        [Name("Chance of sprain from fall")]
        [Slider(0f, 100f, 401)]
        public float AnkleSettings_Chances_Fall = 35f;
        #endregion
        #region Wrist Settings
        [Section("Wrist Options")]

        [Name("Allow Wrist Sprains")]
        [Description("When disabled, wrist sprains will not happen")]
        public bool WristSettings_Enabled = true;

        [Name("Duration Minimum")]
        [Slider(0f, 672f, 673)]
        public float WristSettings_Duration_Minimum = 48f;

        [Name("Duration Maximum")]
        [Slider(0f, 672f, 673)]
        public float WristSettings_Duration_Maximum = 72f;

        [Name("Hours to rest for cure")]
        [Slider(0f, 72f, 73)]
        public float WristSettings_RestHours = 2f;

        [Name("Chance of sprain from fall")]
        [Slider(0f, 100f, 401)]
        public float WristSettings_Chances_Fall = 35f;
        #endregion

        // this is used to set things when user clicks confirm. If you dont need this ability, dont include this method
        protected override void OnConfirm()
        {
            PresetBuilder();
            base.OnConfirm();
        }

        // this is called whenever there is a change. Ensure it contains the null bits that the base method has
        protected override void OnChange(FieldInfo field, object? oldValue, object? newValue)
        {
            SetDisplay(Instance.EnableMod, Instance.EnableMod);
            base.OnChange(field, oldValue, newValue);
        }

        private void SetDisplay(bool active, bool preset)
        {
            SetFieldVisible(nameof(m_Presets),                          preset);
            SetFieldVisible(nameof(General_MinimumSlope),               active);
            SetFieldVisible(nameof(General_MinimumSlopeIncrease),       active);
            SetFieldVisible(nameof(General_EncumberanceChance),         active);
            SetFieldVisible(nameof(General_ExhaustionChance),           active);
            SetFieldVisible(nameof(General_SprintingChance),            active);
            SetFieldVisible(nameof(General_CrouchingChance),            active);
            SetFieldVisible(nameof(General_MinSecondsRisk),             active);
            SetFieldVisible(nameof(General_WristMovementChance),        active);

            SetFieldVisible(nameof(General_SprintUIOn),                 active);
            SetFieldVisible(nameof(General_SprintUIOff),                active);

            SetFieldVisible(nameof(AnkleSettings_Duration_Minimum),     active);
            SetFieldVisible(nameof(AnkleSettings_Duration_Maximum),     active);
            SetFieldVisible(nameof(AnkleSettings_Chances_Fall),         active);
            SetFieldVisible(nameof(AnkleSettings_RestHours),            active);

            SetFieldVisible(nameof(WristSettings_Duration_Minimum),     active);
            SetFieldVisible(nameof(WristSettings_Duration_Maximum),     active);
            SetFieldVisible(nameof(WristSettings_Chances_Fall),         active);
            SetFieldVisible(nameof(WristSettings_RestHours),            active);
        }

        private void PresetBuilder()
        {
            switch (Instance.m_Presets)
            {
                case Presets.Vanilla:
                    SetDisplay(false, true);
                    break;
                case Presets.Custom:
                    SetDisplay(true, true);
                    break;
                case Presets.Enhanced:
                    SetDisplay(false, true);
                    Instance.General_MinimumSlope               = 70;
                    Instance.General_MinimumSlopeIncrease       = 0.25f;
                    Instance.General_EncumberanceChance         = 0f;
                    Instance.General_ExhaustionChance           = 0f;
                    Instance.General_SprintingChance            = 0f;
                    Instance.General_CrouchingChance            = 100f;
                    Instance.General_MinSecondsRisk             = 60f;
                    Instance.General_WristMovementChance        = 0f;

                    Instance.General_SprintUIOn                 = 59f;
                    Instance.General_SprintUIOff                = 0f;

                    Instance.AnkleSettings_Duration_Minimum     = 0f;
                    Instance.AnkleSettings_Duration_Maximum     = 48f;
                    Instance.AnkleSettings_Chances_Fall         = 15f;
                    Instance.AnkleSettings_RestHours            = 1f;

                    Instance.WristSettings_Duration_Minimum     = 0f;
                    Instance.WristSettings_Duration_Maximum     = 24f;
                    Instance.WristSettings_Chances_Fall         = 10f;
                    Instance.WristSettings_RestHours            = 1f;
                    return;
            }
        }

        // This is used to load the settings
        internal void OnLoad()
        {
            Instance.AddToModSettings(BuildInfo.GUIName);
            PresetBuilder();
            Instance.RefreshGUI();
        }
    }
}
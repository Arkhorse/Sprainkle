namespace Sprainkle
{
    public class Main : MelonMod
    {
        public override void OnInitializeMelon()
        {
            Settings.Instance.OnLoad();
        }
    }
}

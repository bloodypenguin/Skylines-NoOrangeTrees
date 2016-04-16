using ICities;
using NoOrangeTrees.OptionsFramework;

namespace NoOrangeTrees
{
    public class Mod : IUserMod
    {
        public string Name => "No Radioactive Desert And More!";

        public string Description => "Brings sanity to ground color and makes trees lush green no matter how much pollution there";

        public void OnSettingsUI(UIHelperBase helper)
        {
            helper.AddOptionsGroup<Options>();
        }
    }
}

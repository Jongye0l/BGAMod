using System.Reflection;
using HarmonyLib;
using UnityModManagerNet;

namespace BGAMod {
    public class Main {
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static UnityModManager.ModEntry ModEntry;
        private static Harmony _harmony;
        public static bool Enabled;
        private static Assembly _assembly;

        public static void Setup(UnityModManager.ModEntry modEntry) {
            ModEntry = modEntry;
            Logger = modEntry.Logger;
            modEntry.OnToggle = OnToggle;
            _assembly = Assembly.GetExecutingAssembly();
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value) {
            Enabled = value;
            if(value) {
                _harmony = new Harmony(modEntry.Info.Id);
                _harmony.PatchAll(_assembly);
            } else {
                _harmony.UnpatchAll(modEntry.Info.Id);
            }
            return true;
        }
    }
}
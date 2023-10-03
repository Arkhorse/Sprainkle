using Il2CppSystem.Text.RegularExpressions;

namespace Sprainkle.Utilities
{
    internal class CommonUtilities
    {
        // NOTE: These "Get" methods are volitile. Ensure it is always up to date as otherwise it can break anything tied to GearItem
        // This is used to load prefab info of a GearItem
        [return: NotNullIfNotNull(nameof(name))]
        public static GearItem GetGearItemPrefab(string name) => GearItem.LoadGearItemPrefab(name);
        [return: NotNullIfNotNull(nameof(name))]
        public static ToolsItem GetToolItemPrefab(string name) => GearItem.LoadGearItemPrefab(name).GetComponent<ToolsItem>();
        [return: NotNullIfNotNull(nameof(name))]
        public static ClothingItem GetClothingItemPrefab(string name) => GearItem.LoadGearItemPrefab(name).GetComponent<ClothingItem>();

        private static string pattern = @"(?:\(\d{1,}\))";

        /// <summary>
        /// Normalizes the name given to remove extra bits using regex for most of the changes
        /// </summary>
        /// <param name="name">The name of the thing to normalize</param>
        /// <returns>Normalized name without <c>(Clone)</c> or any numbers appended</returns>
        /// <remarks>
        /// <para>Regex used: <c>@"(?:\(\d{1,}\))"</c></para>
        /// <para>Things removed: <c>(Clone)</c>, whitespace and appended numbers</para>
        /// </remarks>
        [return: NotNullIfNotNull(nameof(name))]
        public static string? NormalizeName(string name)
        {
            name.Replace("(Clone)", "", StringComparison.InvariantCultureIgnoreCase).Trim();
            name = Regex.Replace(name, pattern, "");
            return name;
        }
    }
}
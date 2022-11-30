using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace TMG.ModularInventory
{
    internal static class ResourcesProvider
    {
        internal static VisualTreeAsset ItemDefinitionUxml { get; }
        internal static VisualTreeAsset ItemModuleUxml { get; }
        
        static ResourcesProvider()
        {
            ItemDefinitionUxml = LoadAssetRelativeToScript<VisualTreeAsset>("UI/ItemDefinitionUxml.uxml");
            ItemModuleUxml = LoadAssetRelativeToScript<VisualTreeAsset>("UI/ItemModuleUxml.uxml");
        }

        private static string GetScriptParentDirectory()
        {
            string[] guids = AssetDatabase.FindAssets("t: Script ResourcesProvider");

            Assert.IsNotNull(guids);
            Assert.IsTrue(guids.Length > 0);

            string relativePath = AssetDatabase.GUIDToAssetPath(guids[0]);
            return Path.GetDirectoryName(relativePath);
        }

        private static TAsset LoadAssetRelativeToScript<TAsset>(string relativePath) where TAsset : Object
        {
            string path = GetScriptParentDirectory();
            path = Path.Combine(path, relativePath);
            return AssetDatabase.LoadAssetAtPath<TAsset>(path);
        }
    }
}
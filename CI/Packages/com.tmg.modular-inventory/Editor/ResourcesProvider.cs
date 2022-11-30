using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace TMG.ModularInventory
{
    public class ResourcesProvider
    {
        internal static VisualTreeAsset ItemDefinitionUxml { get; }
        internal static VisualTreeAsset ItemModuleUxml { get; }
        
        static ResourcesProvider()
        {
            ItemDefinitionUxml = LoadAssetRelativeToScript<ResourcesProvider, VisualTreeAsset>("UI/ItemDefinitionUxml.uxml");
            ItemModuleUxml = LoadAssetRelativeToScript<ResourcesProvider, VisualTreeAsset>("UI/ItemModuleUxml.uxml");
        }

        private static string GetScriptParentDirectory<T>()
        {
            string[] guids = AssetDatabase.FindAssets($"t: Script ResourcesProvider");

            Assert.IsNotNull(guids);
            Assert.IsTrue(guids.Length > 0);

            string relativePath = AssetDatabase.GUIDToAssetPath(guids[0]);
            return Path.GetDirectoryName(relativePath);
        }

        private static TAsset LoadAssetRelativeToScript<TScript, TAsset>(string relativePath) where TAsset : Object
        {
            string path = GetScriptParentDirectory<TScript>();
            path = Path.Combine(path, relativePath);
            return AssetDatabase.LoadAssetAtPath<TAsset>(path);
        }
    }
}
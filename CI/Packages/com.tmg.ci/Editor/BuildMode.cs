using System;

namespace Tmg.CI
{
    [Serializable]
    public struct BuildMode
    {
        public string Name;
        public string Preprocessor;

        public static BuildMode Default = new() { Name = "Default", Preprocessor = "" };
    }
}
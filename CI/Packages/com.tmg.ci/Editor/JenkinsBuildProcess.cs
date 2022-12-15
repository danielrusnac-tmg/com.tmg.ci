namespace TMG.Builder
{
    public static class JenkinsBuildProcess
    {
        /// <summary>
        /// This is called by Jenkins API
        /// </summary>
        public static void PerformBuild()
        {
            GameBuilder.BuildInBatchMode();
        }
    }
}
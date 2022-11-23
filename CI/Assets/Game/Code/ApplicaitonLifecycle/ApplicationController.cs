using System.Threading.Tasks;
using VContainer;
using VContainer.Unity;

namespace ApplicationController
{
    public class ApplicationController : LifetimeScope
    {
        protected override async void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            await InitializeServices();
            LoadGameplay();
        }

        private async Task InitializeServices()
        {
            
        }

        private void LoadGameplay()
        {
            
        }

        public void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}

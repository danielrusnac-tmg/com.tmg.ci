using Game.Gameplay.Messages;
using Game.Infrastructure.Game.Code.Infrastructure.PubSub;
using TMG.SceneLoader;
using TMG.ScreenFader;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.ApplicationLifecycle
{
    public class ApplicationInstaller : LifetimeScope
    {
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private UssScreenFader _screenFader;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(_sceneLoader).AsImplementedInterfaces();
            builder.RegisterInstance(_screenFader).AsImplementedInterfaces();
            builder.RegisterInstance(new MessageChannel<QuitApplicationMessage>()).AsImplementedInterfaces();
            builder.RegisterInstance(new MessageChannel<RestartApplicationMessage>()).AsImplementedInterfaces();
        }
    }
}
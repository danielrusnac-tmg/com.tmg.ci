using System.Collections;

namespace TMG.ScreenFader
{
    public interface IScreenFader
    {
        bool IsCurtainShown { get; }
        
        IEnumerator ShowCurtain();
        void ShowCurtainImmediate();
        IEnumerator HideCurtain();
        void HideCurtainImmediate();
    }
}
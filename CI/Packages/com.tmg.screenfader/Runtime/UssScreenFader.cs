using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace TMG.ScreenFader
{
    public class UssScreenFader : MonoBehaviour, IScreenFader
    {
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private AnimationCurve _showCurtainEase = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        [SerializeField] private AnimationCurve _hideCurtainEase = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private VisualElement _curtainElement;
        
        public bool IsCurtainShown { get; private set; }
        
        private float Opacity
        {
            get => _curtainElement.style.opacity.value;
            set => _curtainElement.style.opacity = value;
        }

        private void Awake()
        {
            _curtainElement = _uiDocument.rootVisualElement.Q("curtain");
            HideCurtainImmediate();
        }

        public IEnumerator ShowCurtain()
        {
            StopFade();

            _uiDocument.enabled = true;
            yield return LerpAlpha(1f, _duration, _showCurtainEase);
            
            OnCurtainShown();
        }

        public void ShowCurtainImmediate()
        {
            StopFade();
            OnCurtainShown();
        }

        public IEnumerator HideCurtain()
        {
            StopFade();

            _uiDocument.enabled = true;
            yield return LerpAlpha(0f, _duration, _hideCurtainEase);
            
            OnCurtainHidden();
        }

        public void HideCurtainImmediate()
        {
            StopFade();
            OnCurtainHidden();
        }

        private void StopFade()
        {
            StopAllCoroutines();
        }

        private void OnCurtainShown()
        {
            Opacity = 1f;
            _uiDocument.enabled = true;
            IsCurtainShown = true;
        }

        private void OnCurtainHidden()
        {
            _uiDocument.enabled = false;
            IsCurtainShown = false;
        }

        private IEnumerator LerpAlpha(float to, float duration, AnimationCurve ease)
        {
            float from = Opacity;
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                Opacity = Mathf.Lerp(from, to, ease.Evaluate(time / duration));
                yield return null;
            }
        }
    }
}
using System.Collections;
using System.Text.RegularExpressions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DrawYourHero.UI
{
    public class SubtitleUI : MonoBehaviour
    {
        //[SerializeField] private Button continueButton;
        [SerializeField] private TextMeshProUGUI lineText;
        //[SerializeField] private float letterPause = 0.1f;
        [SerializeField] private float fadeDuration = 0.4f;
        [SerializeField] private CanvasGroup subtitleCanvasGroup;
        [SerializeField] private GameObject holder;

        private float targetDuration;
        private float targetEndPause;
        private float targetEndFade;

        private string targetText;
        private Coroutine typingCoroutine = null;

        private Tween alphaTween;
        
        private void Awake()
        {
            //continueButton.onClick.AddListener(OnContinue);
        }
        
        private void OnContinue()
        {
            if (typingCoroutine != null) SkipTyping();
            else OnDoneSubtitle();
        }
        
        public void SkipTyping()
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
            lineText.text = targetText;
        }

        private void OnDoneSubtitle()
        {
            holder.SetActive(false);
        }
        
        public void Display(string text, float duration, float endPause = 2f, float endFade = 1f)
        {
            targetDuration = duration;
            targetEndPause = endPause;
            targetEndFade = endFade;
            targetText = text;
            
            holder.SetActive(true);
            
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            typingCoroutine = StartCoroutine(TypeSentence());
        }

        IEnumerator TypeSentence()
        {
            alphaTween?.Kill();
            subtitleCanvasGroup.alpha = 0f;
            alphaTween = subtitleCanvasGroup.DOFade(1f, fadeDuration);
            
            lineText.text = "";

            var letterPause = targetDuration / targetText.Length;

            // This regex pattern will match any TMP tags.
            string pattern = @"<.*?>";

            string[] parts = Regex.Split(targetText, pattern);

            int currentIndex = 0;
            foreach (string part in parts)
            {
                if (Regex.IsMatch(part, pattern))
                {
                    // If the part is a TMP tag, append it whole without delay.
                    lineText.text += part;
                }
                else
                {
                    // If the part is normal text, reveal it letter by letter.
                    foreach (char letter in part)
                    {
                        lineText.text += letter;
                        currentIndex++;
                        //play feedback here
                        yield return new WaitForSeconds(letterPause);
                    }
                }
            }

            if (targetEndPause > 0f) yield return new WaitForSeconds(targetEndPause);

            if (targetEndFade <= 0f) subtitleCanvasGroup.alpha = 0f;
            else alphaTween = subtitleCanvasGroup.DOFade(0f, targetEndFade);
        }
    }
}
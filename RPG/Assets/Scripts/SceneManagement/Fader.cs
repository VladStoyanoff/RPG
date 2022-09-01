using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement 
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        float fadingTime = 3f;

        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOut(float fadingTime)
        {
            while (canvasGroup.alpha < 1)
            {
                var deltaAlpha = Time.deltaTime / fadingTime;
                canvasGroup.alpha += deltaAlpha;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float fadingTime)
        {
            while (canvasGroup.alpha > 0)
            {
                var deltaAlpha = Time.deltaTime / fadingTime;
                canvasGroup.alpha -= deltaAlpha;
                yield return null;
            }
        }
    }
}


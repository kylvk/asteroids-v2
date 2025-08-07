using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlashUI : MonoBehaviour
{
    public float FlashDuration = 0.33f;
    private Image FlashImage;
    private Color imageColor;
    void Start()
    {
        FlashImage = GetComponent<Image>();
        imageColor = FlashImage.color;
    }
    public IEnumerator FlashRoutine()

    {
        float timer = 0f;
        float t = 0f;
        float alphaFrom = 1f; // fully opaque
        float alphaTo = 0f; // fully transparent
        while (t < 1f) // repeats while condition is true
        {
            timer += Time.deltaTime;
            t = Mathf.Clamp01(timer / FlashDuration);
            float alpha = Mathf.Lerp(alphaFrom, alphaTo, t);
            Color col = imageColor;
            col.a = alpha;
            FlashImage.color = col;
            yield return new WaitForEndOfFrame();
        }
    }
   
}

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ScreenShakeUI : MonoBehaviour
{
    public bool start = false;
    public float duration = 1f;

    private void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(shaking());
        }
    }
        private IEnumerator shaking()
        {
            Vector2 startPos = transform.position;
            float elaspedTime = 0f;

            while (elaspedTime < duration)
            {
                elaspedTime += Time.deltaTime;
                transform.position = startPos + Random.insideUnitCircle;
                yield return null;
            }

        transform.position = startPos;
        }
}


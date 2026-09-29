using UnityEngine;
using System.Collections;

public class StoneExplosion : MonoBehaviour
{
    public float delay = 1f;
    public float fadeDuration = 2f;

    private Renderer rend;
    private Material material;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        material = rend.material;
        originalColor = material.color;

        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(delay);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            Color color = originalColor;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            material.color = color;

            yield return null;
        }

        gameObject.SetActive(false);
    }
}
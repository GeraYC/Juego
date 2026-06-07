using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instancia;

    public Image fadeImage;
    public float duracion = 1f;

    void Awake()
    {
        instancia = this;
    }

    public IEnumerator FadeIn()
    {
        float t = 0;
        while (t < duracion)
        {
            t += Time.deltaTime;
            float alpha = t / duracion;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}

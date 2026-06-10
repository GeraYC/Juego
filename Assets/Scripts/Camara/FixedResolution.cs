using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedResolution : MonoBehaviour
{
    //blejjj
    [Tooltip("Aspecto objetivo")]
    public float aspectoObjetivo = 16f / 9f;

    private Camera cam;
    private int anchoAnterior;
    private int altoAnterior;

    void Awake()
    {
        //Hola
        cam = GetComponent<Camera>();
        AplicarAspecto();
    }

    void Update()
    {
        if (Screen.width != anchoAnterior || Screen.height != altoAnterior)
            AplicarAspecto();
    }

    void AplicarAspecto()
    {
        anchoAnterior = Screen.width;
        altoAnterior = Screen.height;

        float aspectoActual = (float)Screen.width / Screen.height;
        float escala = aspectoActual / aspectoObjetivo;

        Rect rect = new Rect(0, 0, 1, 1);

        if (escala < 1f)
        {
            // Pantalla más alta de lo esperado → barras arriba/abajo (letterbox)
            rect.height = escala;
            rect.y = (1f - escala) / 2f;
        }
        else
        {
            // Pantalla más ancha de lo esperado → barras izq/der (pillarbox)
            rect.width = 1f / escala;
            rect.x = (1f - rect.width) / 2f;
        }

        cam.rect = rect;
    }
}
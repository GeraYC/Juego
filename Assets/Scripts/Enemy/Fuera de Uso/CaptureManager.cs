using UnityEngine.UI;
using UnityEngine;



public class CaptureManager : MonoBehaviour
{
    public GameObject capturePanel;

    public Image timeBar;
    public Image escapeBar;

    public GameObject gameOverPanel;

    public Sprite[] barSprites;
    public PlayerMovement player;

    public float tiempoMaximo = 3f;

    private float tiempoRestante;

    private bool capturando;
    public int pulsacionesNecesarias = 20;

    private int pulsacionesActuales;

    


    public void StartCapture()
{
    player.capturado = true;

    tiempoRestante = tiempoMaximo;

    pulsacionesActuales = 0;

    capturando = true;

    capturePanel.SetActive(true);

    Debug.Log("Jugador capturado");
}

private void Update()
{
    if (!capturando)
        return;

    tiempoRestante -= Time.deltaTime;

    ActualizarBarraTiempo();

    if (tiempoRestante <= 0)
    {
        GameOver();
    }

    if (Input.GetKeyDown(KeyCode.Y))
{
    pulsacionesActuales++;

    ActualizarBarraEscape();

    if (pulsacionesActuales >= pulsacionesNecesarias)
    {
        Escape();
    }
}
}

void ActualizarBarraTiempo()
{
    float porcentaje =
        tiempoRestante / tiempoMaximo;

    int frame =
        Mathf.Clamp(
            Mathf.FloorToInt(
                (1f - porcentaje) * 5f
            ),
            0,
            5
        );

    timeBar.sprite = barSprites[frame];
}

void ActualizarBarraEscape()
{
    float porcentaje =
        (float)pulsacionesActuales /
        pulsacionesNecesarias;

    int frame =
        Mathf.Clamp(
            Mathf.FloorToInt(
                porcentaje * 5f
            ),
            0,
            5
        );

    escapeBar.sprite =
        barSprites[5 - frame];
}

void Escape()
{
    capturando = false;

    player.capturado = false;

    capturePanel.SetActive(false);

    Debug.Log("ESCAPÓ");
}

void GameOver()
{
    capturando = false;

    player.capturado = true;

    capturePanel.SetActive(false);

    gameOverPanel.SetActive(true);

    Debug.Log("GAME OVER");
}
}
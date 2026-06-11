using UnityEngine;

public class CaptureManager : MonoBehaviour
{
    public PlayerMovement player;

    public void StartCapture()
{
    Debug.Log(player);

    player.capturado = true;

    Debug.Log("Jugador capturado");
}
}
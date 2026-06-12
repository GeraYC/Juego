using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string escenaNuevaPartida = "Intro";
    public GameObject panelCreditos;

    

    public void Comenzar()
    {
        Time.timeScale = 1f;

        // Nueva partida: empieza desde la escena de intro o primera escena
        SceneManager.LoadScene(escenaNuevaPartida);
    }

    public void Continuar()
    {
        Time.timeScale = 1f;

        if (!SaveSystem.HasSave())
        {
            Debug.Log("No hay partida guardada");
            return;
        }

        SaveSystem.LoadSavedGame();
    }

    public void Creditos()
    {
        if (panelCreditos != null)
            panelCreditos.SetActive(true);
    }

    public void CerrarCreditos()
    {
        if (panelCreditos != null)
            panelCreditos.SetActive(false);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
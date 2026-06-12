using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPausa;

    private bool pausado;

    void Start()
    {
        menuPausa.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausado)
                Reanudar();
            else
                Pausar();
        }
    }

   public void Pausar()
{
    menuPausa.SetActive(true);

    menuPausa.transform.SetAsLastSibling();

    Time.timeScale = 0f;

    pausado = true;
}

    public void Reanudar()
{
    Debug.Log("REANUDAR PRESIONADO");

    menuPausa.SetActive(false);

    Time.timeScale = 1f;

    pausado = false;
}
    public void Guardar()
    {
        GameManager.instancia.SaveGame();
        Debug.Log("Guardado desde menú de pausa");
    }

    public void MenuPrincipal()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
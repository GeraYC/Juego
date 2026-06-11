using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public string puntoSpawn;

    public bool cargandoPartida;

    public Vector3 loadedPosition;

    public Dictionary<string,string> decisiones =
        new Dictionary<string,string>();

    private void Awake()
    {
        if(instancia == null)
        {
            instancia = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame();
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.F5))
    {
        SaveGame();
    }

    if (Input.GetKeyDown(KeyCode.F9))
{
    SaveSystem.LoadSavedGame();
}
}
}
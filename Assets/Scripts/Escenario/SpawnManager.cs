using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke(nameof(SpawnPlayer), 0.01f); // pequeño delay = seguridad extra
    }

    void SpawnPlayer()
{
    GameObject player = GameObject.FindGameObjectWithTag("Player");

    if (player == null)
    {
        Debug.LogError("No hay Player");
        return;
    }

    // AUTO CREAR GAMEMANAGER
    if (GameManager.instancia == null)
    {
        GameObject gm = new GameObject("GameManager");
        gm.AddComponent<GameManager>();

        Debug.Log("GameManager creado automáticamente");
    }

    // CARGAR PARTIDA
    if (GameManager.instancia.cargandoPartida)
    {
        player.transform.position =
            GameManager.instancia.loadedPosition;

        GameManager.instancia.cargandoPartida = false;

        Debug.Log("Posición cargada desde Save");

        return;
    }

    string id = GameManager.instancia.puntoSpawn;

    Debug.Log("Spawn ID usado: " + id);

    SpawnPoint[] puntos =
        FindObjectsByType<SpawnPoint>(
            FindObjectsSortMode.None
        );

    foreach (SpawnPoint p in puntos)
    {
        if (!string.IsNullOrEmpty(id) && p.id == id)
        {
            player.transform.position =
                p.transform.position;

            return;
        }
    }

    foreach (SpawnPoint p in puntos)
    {
        if (p.id == "Start")
        {
            player.transform.position =
                p.transform.position;

            return;
        }
    }

    Debug.LogWarning("No hay SpawnPoint válido");
}
}
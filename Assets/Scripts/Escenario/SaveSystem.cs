using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public static class SaveSystem
{
    static string savePath =
        Application.persistentDataPath +
        "/save.json";

    public static void SaveGame()
    {
        SaveData data = new SaveData();

        data.sceneName =
            SceneManager.GetActiveScene().name;

        GameObject player =
            GameObject.FindGameObjectWithTag("Player");

        data.posX = player.transform.position.x;
        data.posY = player.transform.position.y;
        data.posZ = player.transform.position.z;

        data.decisiones =
            new List<DecisionData>();

        foreach(var pair in GameManager.instancia.decisiones)
        {
            DecisionData d = new DecisionData();

            d.key = pair.Key;
            d.value = pair.Value;

            data.decisiones.Add(d);
        }

        string json =
            JsonUtility.ToJson(data, true);

        File.WriteAllText(savePath, json);

        Debug.Log("PARTIDA GUARDADA");
    }
    public static SaveData LoadGame()
{
    if (!File.Exists(savePath))
        return null;

    string json =
        File.ReadAllText(savePath);

    return JsonUtility.FromJson<SaveData>(json);
}
public static bool HasSave()
{
    return File.Exists(savePath);
}

public static void LoadSavedGame()
{
    SaveData data = LoadGame();

    if (data == null)
        return;

    GameManager.instancia.cargandoPartida = true;

    GameManager.instancia.loadedPosition =
        new Vector3(
            data.posX,
            data.posY,
            data.posZ
        );

    GameManager.instancia.decisiones.Clear();

    foreach (DecisionData d in data.decisiones)
    {
        GameManager.instancia.decisiones[d.key]
            = d.value;
    }

    SceneManager.LoadScene(
        data.sceneName
    );
}


}
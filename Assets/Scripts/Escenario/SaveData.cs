using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public string sceneName;

    public float posX;
    public float posY;
    public float posZ;

    public string puntoSpawn;

    public List<DecisionData> decisiones;
}
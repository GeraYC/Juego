using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Dialogos/Personaje")]
public class CharacterData : ScriptableObject
{
    public string nombre;

    [Header("Sprites")]
    public Sprite neutral;
    public Sprite feliz;
    public Sprite enojado;
    public Sprite triste;

}

using UnityEngine;

[CreateAssetMenu(menuName = "Dialogos/Nodo")]
public class DialogueData : ScriptableObject
{
    [TextArea(3, 10)]
    public string texto;

    public enum TipoNodo
    {
        NPC,
        Protagonista,
        Narrador
    }

    [Header("Tipo")]
    public TipoNodo tipoNodo;

    [Header("Personaje")]
    public CharacterData personaje;

    public enum Emocion
    {
        Neutral,
        Feliz,
        Enojado,
        Triste
    }

    [Header("Emocion")]
    public Emocion emocion;

    [Header("Opciones")]
    public DialogueChoice[] opciones;

    [Header("Siguiente")]
    public DialogueData siguiente;
}
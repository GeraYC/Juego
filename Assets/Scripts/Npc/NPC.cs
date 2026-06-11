using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Identificación")]
    public string npcID;

    [Header("Dialogos")]
    public DialogueData dialogoBase;
    public DialogueData dialogoDespues;

    private bool jugadorCerca = false;
    private bool dialogoActivo = false;

    private bool primerDialogoCompletado = false;

    void Update()
    {
        if (!jugadorCerca) return;
        if (dialogoActivo) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager dm = FindFirstObjectByType<DialogueManager>();

            if (dm == null)
            {
                Debug.LogError("No hay DialogueManager en la escena");
                return;
            }

            DialogueData dialogoAUsar;

            if (!primerDialogoCompletado)
            {
                dialogoAUsar = dialogoBase;
            }
            else
            {
                dialogoAUsar = dialogoDespues;
            }

            if (dialogoAUsar == null)
            {
                Debug.LogError("No hay diálogo asignado para " + npcID);
                return;
            }

            dm.IniciarDialogo(dialogoAUsar, this);

            dialogoActivo = true;
        }
    }

    public void FinalizarDialogo()
    {
        StartCoroutine(ReactivarNPC());
    }

    IEnumerator ReactivarNPC()
    {
        yield return null;
        dialogoActivo = false;
    }

    public void MarcarDialogoCompletado()
    {
        primerDialogoCompletado = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            jugadorCerca = false; //me voy a pelear con este programa alv
    }
} //no me dejaaaaa
//ñiñiñi poner mensajes a lo pendejo
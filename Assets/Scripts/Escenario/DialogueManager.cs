using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerMovement playerMovement;

    [Header("UI")]
    public GameObject panel;

    public TextMeshProUGUI txtNombre;
    public TextMeshProUGUI txtDialogo;

    public TextMeshProUGUI txtOpcion1;
    public TextMeshProUGUI txtOpcion2;

    public Image imgNPC;
    public Image RecuadroNombre;

    private DialogueData nodoActual;

    private int opcionSeleccionada = 0;

    private bool esperandoInput = false;
    private bool cambiandoNodo = false;

    private NPC npcActual;

    void Start()
    {
        panel.SetActive(false);
        LimpiarUI();
    }

    void LimpiarUI()
    {
        txtNombre.text = "";
        txtDialogo.text = "";

        txtOpcion1.text = "";
        txtOpcion2.text = "";

        txtOpcion1.gameObject.SetActive(false);
        txtOpcion2.gameObject.SetActive(false);

        imgNPC.gameObject.SetActive(false);
        RecuadroNombre.gameObject.SetActive(false);
    }

    public void IniciarDialogo(DialogueData inicio, NPC npc)
    {
        nodoActual = inicio;
        npcActual = npc;

        panel.SetActive(true);

        if (playerMovement != null)
            playerMovement.puedeMoverse = false;

        MostrarNodo();
    }

    void MostrarNodo()
    {
        if (nodoActual == null)
            return;

        txtDialogo.text = nodoActual.texto;

        switch (nodoActual.tipoNodo)
        {
            case DialogueData.TipoNodo.NPC:

                if (nodoActual.personaje != null)
                {
                    txtNombre.text = nodoActual.personaje.nombre;

                    imgNPC.gameObject.SetActive(true);
                    RecuadroNombre.gameObject.SetActive(true);

                    imgNPC.sprite = ObtenerSprite(nodoActual);
                }

                break;

            case DialogueData.TipoNodo.Protagonista:

                txtNombre.text = "";

                imgNPC.gameObject.SetActive(false);
                RecuadroNombre.gameObject.SetActive(false);

                break;

            case DialogueData.TipoNodo.Narrador:

                txtNombre.text = "";

                imgNPC.gameObject.SetActive(false);
                RecuadroNombre.gameObject.SetActive(false);

                break;
        }

        bool hayOpciones =
            nodoActual.opciones != null &&
            nodoActual.opciones.Length > 0;

        if (hayOpciones)
        {
            opcionSeleccionada = 0;

            txtOpcion1.gameObject.SetActive(true);
            txtOpcion2.gameObject.SetActive(true);

            ActualizarOpciones();
        }
        else
        {
            txtOpcion1.gameObject.SetActive(false);
            txtOpcion2.gameObject.SetActive(false);
        }

        esperandoInput = false;
        StartCoroutine(ActivarInputSeguro());
    }

    IEnumerator ActivarInputSeguro()
    {
        yield return null;

        while (Input.GetKey(KeyCode.Space))
            yield return null;

        esperandoInput = true;
    }

    void Update()
    {
        if (nodoActual == null)
            return;

        if (!esperandoInput)
            return;

        if (cambiandoNodo)
            return;

        bool hayOpciones =
            nodoActual.opciones != null &&
            nodoActual.opciones.Length > 0;

        if (!hayOpciones)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                esperandoInput = false;
                Avanzar();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                opcionSeleccionada = 0;
                ActualizarOpciones();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                opcionSeleccionada = 1;
                ActualizarOpciones();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                esperandoInput = false;
                ConfirmarSeleccion();
            }
        }
    }

    void ActualizarOpciones()
    {
        if (nodoActual.opciones.Length < 2)
            return;

        txtOpcion1.text =
            (opcionSeleccionada == 0 ? "> " : "  ")
            + nodoActual.opciones[0].texto;

        txtOpcion2.text =
            (opcionSeleccionada == 1 ? "> " : "  ")
            + nodoActual.opciones[1].texto;
    }

    void ConfirmarSeleccion()
    {
        if (nodoActual.opciones == null)
            return;

        if (nodoActual.opciones.Length == 0)
            return;

        DialogueChoice opcion =
            nodoActual.opciones[opcionSeleccionada];

        if (!string.IsNullOrEmpty(opcion.flagKey))
        {
            GameManager.instancia.decisiones[opcion.flagKey]
                = opcion.flagValue;

            Debug.Log(
                "Guardado: "
                + opcion.flagKey
                + " -> "
                + opcion.flagValue
            );
        }

        nodoActual = opcion.siguiente;

        MostrarNodo();
    }

    void Avanzar()
    {
        if (cambiandoNodo)
            return;

        cambiandoNodo = true;

        if (nodoActual.siguiente != null)
        {
            nodoActual = nodoActual.siguiente;
            MostrarNodo();
        }
        else
        {
            TerminarDialogo();
        }

        StartCoroutine(ResetCambio());
    }

    void TerminarDialogo()
    {
        panel.SetActive(false);

        LimpiarUI();

        if (playerMovement != null)
            playerMovement.puedeMoverse = true;

        if (npcActual != null)
        {
            npcActual.MarcarDialogoCompletado();

            npcActual.FinalizarDialogo();

            npcActual = null;
        }

        nodoActual = null;
    }

    IEnumerator ResetCambio()
    {
        yield return null;
        cambiandoNodo = false;
    }

    Sprite ObtenerSprite(DialogueData nodo)
    {
        if (nodo.personaje == null)
            return null;

        switch (nodo.emocion)
        {
            case DialogueData.Emocion.Feliz:
                return nodo.personaje.feliz;

            case DialogueData.Emocion.Enojado:
                return nodo.personaje.enojado;

            case DialogueData.Emocion.Triste:
                return nodo.personaje.triste;

            default:
                return nodo.personaje.neutral;
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ZonaTransicion : MonoBehaviour
{
    public string escenaDestino;
    public string puertaDestinoID;

    private bool dentro = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dentro = true;
            UIEntrada.instancia.Mostrar(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dentro = false;
            UIEntrada.instancia.Mostrar(false);
        }
    }

    void Update()
    {
        if (dentro && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Presionaste espacio"); 

            StartCoroutine(Transicionar());
        }
    }

    IEnumerator Transicionar()
    {
        Debug.Log("Iniciando transición"); 

        UIEntrada.instancia.Mostrar(false);

        yield return FadeManager.instancia.StartCoroutine(
            FadeManager.instancia.FadeIn()
        );

        Debug.Log("Fade terminado"); 

        GameManager.instancia.puntoSpawn = puertaDestinoID;

        Debug.Log("Cargando escena: " + escenaDestino); 

        SceneManager.LoadScene(escenaDestino);
    }


}

using UnityEngine;

public class UIEntrada : MonoBehaviour
{
    public static UIEntrada instancia;
    public GameObject texto;

    void Awake()
    {
        instancia = this;
    }

    public void Mostrar(bool estado)
    {
        texto.SetActive(estado);
    }
}

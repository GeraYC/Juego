using UnityEngine;

public class SeguimientodeCamara : MonoBehaviour
{
    public Transform objetivo;

    public float suavizado = 5f;
    public Vector3 offset = new Vector3(0, 5, -8);

    public float zonaMuertaX = 1.5f;
    public float zonaMuertaZ = 1.5f;

    private Vector3 posicionDeseada;

    void Start()
    {
        posicionDeseada = transform.position;
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 diferencia = objetivo.position - posicionDeseada;

        // DEAD ZONE
        if (Mathf.Abs(diferencia.x) > zonaMuertaX)
        {
            posicionDeseada.x = objetivo.position.x;
        }

        if (Mathf.Abs(diferencia.z) > zonaMuertaZ)
        {
            posicionDeseada.z = objetivo.position.z;
        }

        // Mantener altura fija
        posicionDeseada.y = offset.y;

        // Aplicar offset en Z
        Vector3 destino = new Vector3(posicionDeseada.x,posicionDeseada.y,posicionDeseada.z + offset.z);

        transform.position = Vector3.Lerp(transform.position,destino,suavizado * Time.deltaTime);
    }
}

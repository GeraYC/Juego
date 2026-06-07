using UnityEngine;

public class CameraFollowPro : MonoBehaviour
{
    public Transform objetivo;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float velocidad = 10f;

    void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 destino = objetivo.position + offset;

        transform.position = Vector3.Lerp(
            transform.position,
            destino,
            velocidad * Time.deltaTime
        );
    }
}

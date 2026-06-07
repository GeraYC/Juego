using UnityEngine;

public class Limites2 : MonoBehaviour
{
    public BoxCollider areaLimite;
    public float margen = 0.5f;

    private Vector3 minBounds;
    private Vector3 maxBounds;

    void Start()
    {
        minBounds = areaLimite.bounds.min;
        maxBounds = areaLimite.bounds.max;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minBounds.x + margen, maxBounds.x - margen);
        pos.y = Mathf.Clamp(pos.y, minBounds.y + margen, maxBounds.y - margen);

        transform.position = pos;
    }
}

using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    [HideInInspector]
    public float captureTime = 3f;

    [HideInInspector]
    public int escapePresses = 20;

   private CaptureSystem captureSystem;
    private void Awake()
{
    captureSystem = FindFirstObjectByType<CaptureSystem>();

    Debug.Log("CaptureSystem encontrado: " + captureSystem);
}

    private bool alreadyCaptured;

    private void Update()
    {
        if (player == null)
            return;

        if (alreadyCaptured)
            return;

        Vector3 targetPosition = new Vector3(
            player.position.x,
            transform.position.y,
            player.position.z
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );
    }

    private void OnTriggerEnter(Collider other)
{
    if (alreadyCaptured)
        return;

    if (!other.CompareTag("Player"))
        return;

    // Si ya hay una captura activa, no hacer nada
    if (captureSystem.IsCapturing)
        return;

    alreadyCaptured = true;

    captureSystem.StartCapture(this);
}
    
}
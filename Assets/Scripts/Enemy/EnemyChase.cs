using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    private CaptureManager captureManager;

    private void Awake()
{
    captureManager = GetComponent<CaptureManager>();
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

        if (other.CompareTag("Player"))
        {
            alreadyCaptured = true;

            captureManager.StartCapture();
        }
    }
}
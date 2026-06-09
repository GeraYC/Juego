using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour //pito pal yerestin
{
    public float velocidad = 5f; //materia de mierda

    private Rigidbody rb;
    private Animator animator;
    private Vector3 direccion;
    private float ultimoX;
    private float ultimoZ;

    public bool puedeMoverse = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        Debug.Log(animator);

        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!puedeMoverse)
        {
            direccion = Vector3.zero;
            return;
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Debug.Log("H: " + h + " V: " + v);

        direccion = new Vector3(-h, 0f, -v).normalized;



        bool moviendose = direccion != Vector3.zero;

        animator.SetBool("IsMoving", moviendose);

        if (moviendose)
        {
            ultimoX = direccion.x;
            ultimoZ = direccion.z;
        }

        animator.SetFloat("MoveX", ultimoX);
        animator.SetFloat("MoveZ", ultimoZ);

    }

    void FixedUpdate()
    {
        if (!puedeMoverse)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        rb.linearVelocity = new Vector3(
            direccion.x * velocidad,
            rb.linearVelocity.y,
            direccion.z * velocidad
        );
    }
}

using UnityEngine;

public class ActivateSound : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        
       audioSource.Play();
    
    }
    


}

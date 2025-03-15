
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] float thrustStrength = 100f;
    [SerializeField] InputAction rotation;
    [SerializeField] float rotationStrength = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem upParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;

    Rigidbody rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource=GetComponent<AudioSource> ();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrusting();
        ProcessRotation();
            
    }
    private void ProcessThrusting()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
                
            }
            if (!upParticles.isPlaying)
            {
                upParticles.Play();
            }

        }
        else
        {
            audioSource.Stop();
            upParticles.Stop();
        }
    }
    private void ProcessRotation()
    {
        float rotationinput=rotation.ReadValue<float>();
        if (rotationinput > 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward* rotationStrength*Time.fixedDeltaTime);
            rb.freezeRotation = false;
            if (!leftParticles.isPlaying)
            {
                rightParticles.Stop();
                leftParticles.Play();
            }
            else
            {
                
                leftParticles.Stop();
            }
        }
        else if (rotationinput < 0)
        {
            rb.freezeRotation = true;
            transform.Rotate(-Vector3.forward * rotationStrength * Time.fixedDeltaTime);
            rb.freezeRotation = false;
            if (!rightParticles.isPlaying)
            {
                rightParticles.Play();
                leftParticles.Stop();
            }
            else
            {
                rightParticles.Stop();
            }
        }
    }
}


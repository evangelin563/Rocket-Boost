using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    //[SerializeField] float levelloaddelay=2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip successful;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    
    bool isControllable = true;
    bool isCollidable = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            isCollidable=!isCollidable;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("All Good");
                break;
            case "Finish":
                StartSuccessSequence();

                break;

            default:
                StartCrashSequence();


                break;
        }
    }
    void StartSuccessSequence()
    {
        isControllable = false;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(successful);
        //successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", 2f);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        //crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("reloadlevel", 2f);
    }


    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)

        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }

    void reloadlevel()
    {
        int currentScene= SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    

}

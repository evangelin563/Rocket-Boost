using UnityEngine;
using UnityEditor;

public class QuitApp : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("O key pressed");
#if UNITY_EDITOR
               EditorApplication.isPlaying = false; 
#else
            Application.Quit();
#endif
        }
    }
}
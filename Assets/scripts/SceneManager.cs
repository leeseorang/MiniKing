using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    //씬 리셋
    void Start()
    {
     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

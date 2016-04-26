using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class LevelStationBehavior : MonoBehaviour
{

    public string levelToLoad;

    void OnMouseUpAsButton()
    {
        print("asdsa");
        SceneManager.LoadScene(levelToLoad);
    }
}

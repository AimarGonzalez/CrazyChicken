using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

    public Text cdtimerText;

    //public float timeRemaining;
    public float timeRemaining = 10.0F;

    // Use this for initialization
    void Start()
    {
        cdtimerText.text = "";

    }

    void Update()
    {
        // Functional game countdown timer
        timeRemaining -= Time.deltaTime;
        if (timeRemaining > 0)
        {
            cdtimerText.text = "" + (int)timeRemaining;
        }
        else {
            timeRemaining = timeRemaining + 10.0F;
            //cdtimerText.text = "TIME'S UP!";
            //GameOver();

        }
    }
}

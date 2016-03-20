using UnityEngine;
using System.Collections;

public class Events : MonoBehaviour {

    // Use this for initialization
    private float Lasteventtime;

    // Update is called once per frame
    void Start()
    {
        Lasteventtime = 0;

    }

    void Update()
    {

        if (Time.time - Lasteventtime == 10)
        {
            Lasteventtime = Time.time;

            //Choose event
            float eventRoulette = Random.Range(0f, 2.99f);
            int eventChosen = Mathf.CeilToInt(eventRoulette);

            Debug.Log(eventChosen);

            if (eventChosen == 0)
            {
                //Trigger event
            }


            else if (eventChosen == 1)
            {
                //Trigger event 
            }

            else {
                //trigger event
            }

        }

    }
}

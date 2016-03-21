using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Events : NetworkBehaviour {

	public float m_timeBetweenEvents = 10f;
	private float m_nextEventTime = 0f;
	private GameObject m_localPlayer;

    // Update is called once per frame
    void Start()
    {
		m_nextEventTime = Time.time + m_timeBetweenEvents;
    }

    void Update()
    {
		if (!isServer)
			return;

		if (Time.time >= m_nextEventTime)
        {
			m_nextEventTime = Time.time + m_timeBetweenEvents;
			findLocalPlayer ();
			m_localPlayer.GetComponent<TankItemSpawner> ().SpawnCabras ();

            //Choose event
            /*float eventRoulette = Random.Range(0f, 2.99f);
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
            }*/
        }
    }

	public void findLocalPlayer(){
		GameObject[] pollos = GameObject.FindGameObjectsWithTag ("Tank");
		foreach(GameObject pollo in pollos)
		{
			if (pollo.GetComponent<NetworkIdentity> ().isLocalPlayer) {
				m_localPlayer = pollo;
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Events : NetworkBehaviour {

	public float m_timeBetweenEvents = 10f;
	public float m_cabrasLifeTime = 10f;
	public float m_meteorLifeTime = 8f;
	public Text m_eventTitleText;
	public string[] m_eventsNames;

	private float m_nextEventTime = 0f;
	private GameObject m_localPlayer;

	private EInGameEventType m_nextEventType;

	enum EInGameEventType
	{
		EVENT_CABRAS = 0,
		EVENT_METEOR = 1,
		LAST_EVENT = EVENT_METEOR
	}

    // Update is called once per frame
	public void Awake()
    {
		SetUpNextRandomEvent ();
    }

    void Update()
    {
		if (!isServer)
			return;
		
		if (Time.time >= m_nextEventTime)
        {
			findLocalPlayer ();
			switch(m_nextEventType)
			{
				case EInGameEventType.EVENT_CABRAS:
					m_localPlayer.GetComponent<TankItemSpawner> ().SpawnCabras (m_cabrasLifeTime);
					break;

				case EInGameEventType.EVENT_METEOR:
					m_localPlayer.GetComponent<TankItemSpawner> ().SpawnMeteoro (m_meteorLifeTime);
					break;

				default:
					break;
			}

			SetUpNextRandomEvent ();
        }

    }

	private void SetUpNextRandomEvent()
	{
		m_nextEventTime = Time.time + m_timeBetweenEvents;
		m_nextEventType = (EInGameEventType)Random.Range (0, (int)(EInGameEventType.LAST_EVENT)+1);
		m_eventTitleText.text = m_eventsNames [(int)m_nextEventType];
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

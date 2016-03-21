using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TankItemSpawner : NetworkBehaviour {

	public GameObject m_maizPrefab;
	public float m_maizEffectTime = 3f;

	public GameObject m_cabrasPrefab;
	public float m_timeToDestroyCabras = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnMaiz(Vector3 position)
	{
		CmdSpawnMaiz (position);
	}

	[Command]
	public void CmdSpawnMaiz(Vector3 hitPos)
	{
		GameObject nuevoMaiz = (GameObject)Instantiate(m_maizPrefab, hitPos, Quaternion.identity);
		NetworkServer.Spawn(nuevoMaiz);
		Destroy (nuevoMaiz, m_maizEffectTime);
	}

	//public void SpawnCabras()
	//{
	//	CmdSpawnCabras ();
	//}

	//[Command]
	public void SpawnCabras()
	{
		GameObject[] cabrasSpawnPoints = GameObject.FindGameObjectsWithTag("CabrasSpawn");
		int randomIndex = Random.Range (0, cabrasSpawnPoints.Length - 1);
		GameObject nuevasCabras = (GameObject)Instantiate(m_cabrasPrefab, cabrasSpawnPoints[randomIndex].transform.position, cabrasSpawnPoints[randomIndex].transform.rotation);
		NetworkServer.Spawn(nuevasCabras);
		Destroy (nuevasCabras, m_timeToDestroyCabras);
	}
}

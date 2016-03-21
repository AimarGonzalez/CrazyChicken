using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TankItemSpawner : NetworkBehaviour {

	public GameObject m_maizPrefab;

	public GameObject m_cabrasPrefab;
	public GameObject m_meteorPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnMaiz(Vector3 position, float lifeTime)
	{
		CmdSpawnMaiz (position, lifeTime);
	}

	[Command]
	public void CmdSpawnMaiz(Vector3 hitPos, float lifeTime)
	{
		GameObject nuevoMaiz = (GameObject)Instantiate(m_maizPrefab, hitPos, Quaternion.identity);
		NetworkServer.Spawn(nuevoMaiz);
		Destroy (nuevoMaiz, lifeTime);
	}

	//public void SpawnCabras()
	//{
	//	CmdSpawnCabras ();
	//}

	//[Command]
	public void SpawnCabras(float lifeTime)
	{
		GameObject[] cabrasSpawnPoints = GameObject.FindGameObjectsWithTag("CabrasSpawn");
		int randomIndex = Random.Range (0, cabrasSpawnPoints.Length - 1);
		GameObject nuevasCabras = (GameObject)Instantiate(m_cabrasPrefab, cabrasSpawnPoints[randomIndex].transform.position, cabrasSpawnPoints[randomIndex].transform.rotation);
		NetworkServer.Spawn(nuevasCabras);
		Destroy (nuevasCabras, lifeTime);
	}

	public void SpawnMeteoro(float lifeTime)
	{
		GameObject[] meteoroSpawnPoints = GameObject.FindGameObjectsWithTag("CabrasSpawn");
		int randomIndex = Random.Range (0, meteoroSpawnPoints.Length - 1);
		GameObject newMeteor = (GameObject)Instantiate(m_meteorPrefab, meteoroSpawnPoints[randomIndex].transform.position, meteoroSpawnPoints[randomIndex].transform.rotation);
		NetworkServer.Spawn(newMeteor);
		Destroy (newMeteor, lifeTime);
	}
}

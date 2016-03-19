using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TankItemSpawner : NetworkBehaviour {

	public GameObject m_maizPrefab;

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
		Destroy (nuevoMaiz, 3.0f);
	}
}

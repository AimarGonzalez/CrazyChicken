using UnityEngine;
using System.Collections;

public class EstampidaDamageScript : MonoBehaviour {

	public float m_amountOfDamage = 10f;
	public float m_timeBetweenDamage = 0.75f;

	private float m_nextTimeToApplyDamage = Time.time;
	private BoxCollider m_boxCollider; 

	// Use this for initialization
	void Start () {
		m_boxCollider = transform.Find ("Guide/Cube").GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Time.time >= m_nextTimeToApplyDamage) {
			m_nextTimeToApplyDamage = Time.time + m_timeBetweenDamage;

			ApplyDamageToPollos ();
		}

	}

	private void ApplyDamageToPollos(){
		GameObject[] pollos = GameObject.FindGameObjectsWithTag ("Tank");

		foreach (GameObject pollo in pollos) {

			if (m_boxCollider.bounds.Contains (pollo.transform.position)) {
				pollo.GetComponent<TankHealth> ().Damage (m_amountOfDamage);

				GameObject plumasInstance = Instantiate (pollo.GetComponent<TankShooting> ().m_plumasParticles, pollo.transform.position + new Vector3 (0, 2, 0), pollo.transform.rotation) as GameObject;
				Destroy (plumasInstance, 1.0f);
			}


			/*if (distanceToPollo > 0.01 && distanceToPollo < m_areaDamageDistance) {
				bool countKill = pollo.GetComponent<TankHealth> ().Damage (m_areaDamageAmount);
				GameObject plumasInstance = Instantiate (m_plumasParticles, pollo.transform.position + new Vector3 (0, 2, 0), pollo.transform.rotation) as GameObject;
				Destroy (plumasInstance, 1.0f);

				//Add kill if necessary
				if (countKill) 
				{
					GetComponent<TankHealth> ().m_Manager.m_Kills++;
				}
			}

			transform.FindChild ("Chicken").GetComponent<Animator> ().SetTrigger ("KickTrigger");*/
		}
	}
}

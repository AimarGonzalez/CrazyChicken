using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerInput : NetworkBehaviour
{
	private GameObject m_localPlayer;
	private bool farmer1Activated = false;

	public float m_maizLifeTime = 3f;
	private float m_Farmer1TimeUntilActivation = 0f;
	public float m_Farmer1CooldownTime = 5f;
	private float m_KickTimeUntilActivation = 5f;
	public float m_KickCooldownTime = 1.5f;

    public GameObject maiz;

    Vector2[] touches = new Vector2[5];
 

    // Use this for initialization
    void Start () {
        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
    }

    // Update is called once per frame
    void Update () {

		UpdateButtonsCooldown ();

	    if(farmer1Activated)
        {
            doFarmer1();
        }

	}

	private void UpdateButtonsCooldown()
	{
		if (m_Farmer1TimeUntilActivation > 0f) {
			
			m_Farmer1TimeUntilActivation -= Time.deltaTime;

			if (m_Farmer1TimeUntilActivation <= 0f) {
				m_Farmer1TimeUntilActivation = 0f;
				//transform.FindChild ("ButtonMaiz").GetComponent<CanvasRenderer> ().SetColor (Color.white);
				transform.FindChild ("ButtonMaiz").GetComponent<Button> ().interactable = true;
			}
		}

		if (m_KickTimeUntilActivation > 0f) {
			
			m_KickTimeUntilActivation -= Time.deltaTime;

			if (m_KickTimeUntilActivation <= 0f) {
				m_KickTimeUntilActivation = 0f;
				//transform.FindChild ("ButtonKick").GetComponent<CanvasRenderer> ().SetColor (Color.white);
				transform.FindChild ("ButtonKick").GetComponent<Button> ().interactable = true;
			}
		}
	}

    public void Kick()
	{
		if (m_KickTimeUntilActivation <= 0f) {
			m_KickTimeUntilActivation = m_KickCooldownTime;
			transform.FindChild ("ButtonKick").GetComponent<Button> ().interactable = false;

			findLocalPlayer ();
			m_localPlayer.GetComponent<TankShooting> ().Kick ();
		}
	}

    public void Dash()
    {

    }

    public void Farmer1()
    {
		if (m_Farmer1TimeUntilActivation <= 0f) {
			farmer1Activated = !farmer1Activated;
		}
    }
   
    public void doFarmer1()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // foreach (Touch t in Input.touches)
           Vector3 touchPosition = Input.mousePosition;
           //Vector3 worldPoint = Camera.main.ScreenToWorldPoint(touchPosition);
           
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           RaycastHit hit;

           if (Physics.Raycast(ray, out hit, 100))
           {
                //Debug.DrawLine(ray.origin, hit.point);
                if (hit.collider.name == "Terrain")
                {
                    print("maiz!");

					findLocalPlayer ();
					m_localPlayer.GetComponent<TankItemSpawner>().SpawnMaiz(hit.point, m_maizLifeTime);

                    CancelFarmer1();
					m_Farmer1TimeUntilActivation = m_Farmer1CooldownTime;
					transform.FindChild ("ButtonMaiz").GetComponent<Button> ().interactable = false;
                }
                else
                {
                    print("arbol!");
                }
            }
        }

        /*
        if (Input.touchCount > 0 && Input.touchCount < 2)
        {
            // foreach (Touch t in Input.touches)
            Touch t = Input.touches[0];
            touches[t.fingerId] = Camera.main.ScreenToWorldPoint(Input.GetTouch(t.fingerId).position);
            if (Input.GetTouch(t.fingerId).phase == TouchPhase.Began)
            {
                hit = Physics2D.Raycast(touches[t.fingerId], Vector2.zero);
            }
            if (hit.collider.name == "Terrain")
            {
                hit.transform.position = touches[t.fingerId];
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawLine(ray.origin, hit.point);
            }

            CancelFarmer1();
        }
        */
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

    public void CancelFarmer1()
    {
        farmer1Activated = false;
        //algo mas
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerInput : NetworkBehaviour
{

    private bool farmer1Activated = false;
    private bool farmer2Activated = false;

    public GameObject maiz;

    Vector2[] touches = new Vector2[5];
 

    // Use this for initialization
    void Start () {
        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        ClientScene.RegisterPrefab(maiz);
    }

    // Update is called once per frame
    void Update () {
	    if(farmer1Activated)
        {
            CmdDoFarmer1();
        }
	}

    public void Kick()
    {
        
    }

    public void Dash()
    {

    }

    public void Farmer1()
    {
        farmer1Activated = true;
        CancelFarmer2();
    }

    [Command]
    public void CmdSpawnMaiz(Vector3 hitPos)
    {
        GameObject nuevoMaiz = (GameObject)Instantiate(maiz, hitPos, Quaternion.identity);
        NetworkServer.Spawn(nuevoMaiz);
    }

   
    public void CmdDoFarmer1()
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
                Debug.DrawLine(ray.origin, hit.point);
                if (hit.collider.name == "Terrain")
                {
                    print("maiz!");
                    CmdSpawnMaiz(hit.point);

                    CancelFarmer1();
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

    public void CancelFarmer1()
    {
        farmer1Activated = false;
        //algo mas
    }

    public void Farmer2()
    {
        farmer2Activated = true;
        CancelFarmer1();
    }

    public void CancelFarmer2()
    {
        farmer2Activated = false;
        //algo mas
    }
}

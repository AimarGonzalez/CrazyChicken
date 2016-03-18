using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TankRandomMovement : NetworkBehaviour
{
    public int m_PlayerNumber = 1;                // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public int m_LocalID = 1;

    public Rigidbody m_Rigidbody;              // Reference used to move the tank.

    public float m_Speed = 2f;
    public float m_MaizEffectDistance = 10f;

    NavMeshAgent agent;
    public Vector3 m_destination = Vector3.zero;


    public float movementDestinationMin = -15f;
    public float movementDestinationMax = 15f;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    private void Start () {
        m_destination = GetNewDestination();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void FixedUpdate()
    {
        if (!isLocalPlayer && !GameManager.DEBUG_MODE)
            return;

        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
    }



    private void Move()
    {
        // Create a movement vector based on the input, speed and the time between frames, in the direction the tank is facing.
        float distanceToTarget = Vector3.Distance(m_Rigidbody.position, agent.destination);
        if (distanceToTarget < 0.5f)
        {
            agent.destination = m_destination = GetNewDestination();
        }

        //Metodo 2:
        /*
            m_Rigidbody.AddForce(m_destination - m_Rigidbody.position);
        */
       
        //Metodo 3:
        /*
            Vector3 movement = m_Rigidbody.position - destination * m_Speed * Time.deltaTime;

            Apply this movement to the rigidbody's position.
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
        */
    }

    private Vector3 GetNewDestination()
    {
        Vector3 newDestination;

        GameObject maiz = GetClosestMaiz();
        if (maiz)
        {
            newDestination = maiz.transform.position;
        }
        else
        {
            //random
            newDestination = GetRandomPosition();
        }
        return newDestination;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 newPosition;
        do
        {
            newPosition = new Vector3(Random.Range(movementDestinationMin, movementDestinationMax), 0, Random.Range(movementDestinationMin, movementDestinationMax));

        } while (Vector3.Distance(newPosition, m_Rigidbody.position) < 5f);

        return newPosition;
    }

    private GameObject GetClosestMaiz()
    {
        GameObject[] maizes = GameObject.FindGameObjectsWithTag("Maiz");
        float minDistance = 9999;
        GameObject closestMaiz = null;
        foreach(GameObject maiz in maizes)
        {
            float distance = Vector3.Distance(maiz.transform.position, m_Rigidbody.position);
            if (distance < m_MaizEffectDistance)
            {
                if(distance < minDistance)
                {
                    closestMaiz = maiz;
                    minDistance = distance;
                }
            }
        }

        return closestMaiz;
    }

    public void SetDefaults()
    {
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
    }
}

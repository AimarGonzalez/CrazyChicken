using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TankRandomMovement : NetworkBehaviour
{
    public int m_PlayerNumber = 1;                // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public int m_LocalID = 1;

    public Rigidbody m_Rigidbody;              // Reference used to move the tank.

    public float m_Speed = 2f;

    public Vector3 m_destination = Vector3.zero;


    public float movementDestinationMin = 0f;
    public float movementDestinationMax = 15f;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
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
       

        if(Vector3.Distance(m_Rigidbody.position, m_destination) < 10)
        {
            m_destination = GetNewDestination();
        }

        m_Rigidbody.AddForce(m_destination - m_Rigidbody.position);
       
        
        //Vector3 movement = m_Rigidbody.position - destination * m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        //m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private Vector3 GetNewDestination()
    {
        return new Vector3(Random.Range(movementDestinationMin, movementDestinationMax), 0, Random.Range(movementDestinationMin, movementDestinationMax));
    }

    public void SetDefaults()
    {
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
    }
}

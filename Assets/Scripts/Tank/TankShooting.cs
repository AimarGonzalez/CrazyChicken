using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TankShooting : NetworkBehaviour
{
    public int m_PlayerNumber = 1;            // Used to identify the different players.
    public Rigidbody m_Shell;                 // Prefab of the shell.
    public Rigidbody m_AreaDamage;            // Prefab of the areaDamage.
    public Transform m_FireTransform;         // A child of the tank where the shells are spawned.
    public Slider m_AimSlider;                // A child of the tank that displays the current launch force.
    public AudioSource m_ShootingAudio;       // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
    public AudioClip m_ChargingClip;          // Audio that plays when each shot is charging up.
    public AudioClip m_FireClip;              // Audio that plays when each shot is fired.
    public float m_MinLaunchForce = 15f;      // The force given to the shell if the fire button is not held.
    public float m_MaxLaunchForce = 30f;      // The force given to the shell if the fire button is held for the max charge time.
    public float m_MaxChargeTime = 0.75f;     // How long the shell can charge for before it is fired at max force.
    public float m_areaDamageDistance = 3.0f;
    public int m_areaDamageAmount = 30;
	private float m_timeWhenKickIsAvailable;
	public float m_kickCooldownTime = 3.0f;


    [SyncVar]
    public int m_localID;

    private string m_FireButton;            // The input axis that is used for launching shells.
    private Rigidbody m_Rigidbody;          // Reference to the rigidbody component.
    [SyncVar]
    private float m_CurrentLaunchForce;     // The force that will be given to the shell when the fire button is released.
    [SyncVar]
    private float m_ChargeSpeed;            // How fast the launch force increases, based on the max charge time.
    public GameObject m_plumasParticles;

    private void Awake()
    {
        // Set up the references.
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void Start()
    {
        // The fire axis is based on the player number.
        m_FireButton = "Fire" + (m_localID + 1);

        // The rate that the launch force charges up is the range of possible forces by the max charge time.
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }

    [ClientCallback]
    private void Update()
    {
        if (!isLocalPlayer)
        {
			return;
        }
        /*else
        {
			if (Input.GetButtonDown(m_FireButton) && (Time.time >= m_timeWhenKickIsAvailable))
            {
				CmdKick();
				//m_timeWhenKickIsAvailable = Time.time + m_kickCooldownTime;
            }
        }
        */
    }

	public void Kick()
	{
		CmdKick ();
	}

	[Command]
	public void CmdKick()
	{
		RpcKick ();
	}

	[ClientRpc]
	public void RpcKick()
	{
		GameObject[] pollos = GameObject.FindGameObjectsWithTag ("Tank");
		Vector3 myPosition = GetComponent<Rigidbody> ().transform.position;


		foreach (GameObject pollo in pollos) {
			float distanceToPollo = (pollo.transform.position - myPosition).sqrMagnitude;

			if (distanceToPollo > 0.01 && distanceToPollo < m_areaDamageDistance) {
				bool countKill = pollo.GetComponent<TankHealth> ().Damage (m_areaDamageAmount);
				GameObject plumasInstance = Instantiate (m_plumasParticles, pollo.transform.position + new Vector3 (0, 2, 0), pollo.transform.rotation) as GameObject;
				Destroy (plumasInstance, 1.0f);

				//Add kill if necessary
				if (countKill) 
				{
					GetComponent<TankHealth> ().m_Manager.m_Kills++;
				}
			}

			transform.FindChild ("Chicken").GetComponent<Animator> ().SetTrigger ("KickTrigger");
		}
	}

    // This is used by the game manager to reset the tank.
    public void SetDefaults()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }
}
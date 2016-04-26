using UnityEngine;
using System.Collections;

public class CameraSagaMap : MonoBehaviour {


    //Camera Traslation
    private Camera oldCameraPosition;
    private Camera newCameraPosition;

    private float speed = 1.0F;

    //Object Rotation
    private GameObject selectedObject;

    private Vector3 ResetCamera;
    private Vector3 origin;
    private Vector3 diference;
    private Vector3 curMousePos;
    private Vector3 lastMousePos;

    private bool Drag = false;

    //Episode Change
    public Transform world;
    private Quaternion newEpisodePosition;
    private Quaternion oldEpisodePosition;

    private float rotationAmount = 0.0f;


    void Start()
    {
        ResetCamera = Camera.main.transform.position;
        ActivateCamera(Camera.main);

        oldEpisodePosition = world.transform.rotation;
        newEpisodePosition = world.transform.rotation;
    }

    void Update()
    {
        EpisodeMove();
    }


    void LateUpdate()
    {
        // Camera movement
        CameraMove();


        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.transform.gameObject.tag);
                if (hit.transform.gameObject.tag == "MapInteractive")
                {                   
                    selectedObject = hit.transform.gameObject;
                }
            }

            diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                lastMousePos = origin;
            }
        }
        else
        {
            Drag = false;
            selectedObject = null;
        }

        if (Drag == true)
        {
            if (selectedObject)
            {

                curMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3 tempRota = selectedObject.transform.rotation.eulerAngles;
                tempRota.z += -(curMousePos.x - lastMousePos.x);
                selectedObject.transform.rotation = Quaternion.Euler(tempRota);

                lastMousePos = curMousePos;

            }
        }
    }


    /// <summary>
    /// Set a new camera
    /// </summary>
    /// <param name="newPosition"></param>
    public void ActivateCamera(Camera newPosition)
    {
        print("New camera position");
        newCameraPosition = newPosition;
        oldCameraPosition = Camera.main;
    }

    /// <summary>
    /// Camera Calculation
    /// </summary>
    void CameraMove()
    {
        transform.position = Vector3.Lerp(oldCameraPosition.transform.position, newCameraPosition.transform.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.Lerp(oldCameraPosition.transform.rotation, newCameraPosition.transform.rotation, Time.deltaTime * speed);
        Camera.main.orthographicSize = Mathf.Lerp(oldCameraPosition.orthographicSize, newCameraPosition.orthographicSize, Time.deltaTime * speed);
    }

    /// <summary>
    /// Select Saga Map direction
    /// </summary>
    /// <param name="positivoUp"></param>
    public void ActivateEpisode(bool positivoUp)
    {
        if (rotationAmount >= 1.0f)
        {
            rotationAmount = 0.0f;

            oldEpisodePosition = newEpisodePosition;
            if (positivoUp)
            {
                newEpisodePosition = Quaternion.Euler(newEpisodePosition.eulerAngles.x, newEpisodePosition.eulerAngles.y + 30, newEpisodePosition.eulerAngles.z);
            }
            else
            {
                newEpisodePosition = Quaternion.Euler(newEpisodePosition.eulerAngles.x, newEpisodePosition.eulerAngles.y - 30, newEpisodePosition.eulerAngles.z);
            }


            print(oldEpisodePosition.eulerAngles.y + "   " + newEpisodePosition.eulerAngles.y);
        }
    }

    /// <summary>
    /// Movement of the saga map
    /// </summary>
    void EpisodeMove()
    {
        rotationAmount += Time.deltaTime * speed;
        world.transform.rotation = Quaternion.Lerp(oldEpisodePosition, newEpisodePosition, rotationAmount);
    }
}

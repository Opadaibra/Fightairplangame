using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingScript : MonoBehaviour
{
    public float normalSpeed = 25f;
    public float accelerationSpeed = 45f;
    public Transform cameraPosition;
    public Camera mainCamera;
    public Transform jetRoot;
    public float rotationSpeed = 2.0f;
    public float cameraSmooth = 4f;
    public float Range = 100f;
    float speed;
    Rigidbody r;
    Quaternion lookRotation;
    float rotationZ = 0;
    float mouseXSmooth = 0;
    float mouseYSmooth = 0;
    Vector3 defaultJetRotation;
    public GameObject Impact;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        r.useGravity = false;
        lookRotation = transform.rotation;
        defaultJetRotation = jetRoot.localEulerAngles;
        rotationZ = defaultJetRotation.z;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
  
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
        if (Input.GetMouseButton(1))
        {
            speed = Mathf.Lerp(speed, accelerationSpeed, Time.deltaTime * 3);
        }
        else
        {
            speed = Mathf.Lerp(speed, normalSpeed, Time.deltaTime * 10);
        }

        //Set moveDirection to the vertical axis (up and down keys) * speed
        Vector3 moveDirection = new Vector3(0, 0, speed);
        //Transform the vector3 to local space
        moveDirection = transform.TransformDirection(moveDirection);
        //Set the velocity, so you can move
        r.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);

        //Camera follow
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, cameraPosition.position, Time.deltaTime * cameraSmooth);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, cameraPosition.rotation, Time.deltaTime * cameraSmooth);

        //Rotation
        float rotationZTmp = 0;
        if (Input.GetKey(KeyCode.A))
        {
            rotationZTmp = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationZTmp = -1;
        }
        mouseXSmooth = Mathf.Lerp(mouseXSmooth, Input.GetAxis("Mouse X") * rotationSpeed, Time.deltaTime * cameraSmooth);
        mouseYSmooth = Mathf.Lerp(mouseYSmooth, Input.GetAxis("Mouse Y") * rotationSpeed, Time.deltaTime * cameraSmooth);
        Quaternion localRotation = Quaternion.Euler(-mouseYSmooth, mouseXSmooth, rotationZTmp * rotationSpeed);
        lookRotation = lookRotation * localRotation;
        transform.rotation = lookRotation;
        rotationZ -= mouseXSmooth;
        rotationZ = Mathf.Clamp(rotationZ, -45, 45);
        jetRoot.transform.localEulerAngles = new Vector3(defaultJetRotation.x, defaultJetRotation.y, rotationZ);
        rotationZ = Mathf.Lerp(rotationZ, defaultJetRotation.z, Time.deltaTime * cameraSmooth);
    }
    void Shoot()
    {

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, Range))
        {


            if (hit.rigidbody != null)
            {
        //       if(hit.rigidbody.transform.gameObject.tag =="enm")
        //        {
        //hit.rigidbody.AddForce(-hit.normal * 30);
        //        Destroy(hit.rigidbody.transform.gameObject);
        //        }
        //        hit.rigidbody.AddForce(-hit.normal * 30);
        //        Destroy(hit.rigidbody.transform.gameObject);
            }
            GameObject go = Instantiate(Impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(go, 1f);
        }


    }
}

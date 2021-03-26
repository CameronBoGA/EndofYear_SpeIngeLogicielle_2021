using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    // Camera Scrolling or moving speed
    public Slider cameraSpeedSlide;
    public ManagerScript ms;

    // Camera positions
    private float xAxis;
    private float yAxis;
    private float zoom;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if a save or load Menu has been opened
        if (ms.saveLoadMenuOpen == false)
        {
            // Catch the Player's Input
            xAxis = Input.GetAxis("Horizontal");
            yAxis = Input.GetAxis("Vertical");
            zoom = Input.GetAxis("Mouse ScrollWheel") * 10;
            
            // move the camera according to the player's input
            transform.Translate(new Vector3(xAxis * -cameraSpeedSlide.value, yAxis * -cameraSpeedSlide.value, 0.0f));
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -20, 20),
                Mathf.Clamp(transform.position.y, 20, 20),
                Mathf.Clamp(transform.position.z, -20, 20));
            if (zoom < 0 && cam.orthographicSize >= -25)
                cam.orthographicSize -= zoom * -cameraSpeedSlide.value;
            if (zoom > 0 && cam.orthographicSize <= -5)
                cam.orthographicSize += zoom * cameraSpeedSlide.value;
        }
    }
}

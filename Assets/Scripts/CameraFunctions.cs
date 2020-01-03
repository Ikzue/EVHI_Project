using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : MonoBehaviour
{
    public int nbScreens = 3;
    public int currentScreen = 0;
    public Vector3 cameraPosition;
    public Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        myCamera = Camera.main;
        cameraPosition = myCamera.transform.position;
    }

    public void moveCameraToRight() {
        currentScreen = (currentScreen + 1) % 3;
        Vector3 newPosition = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, currentScreen*15);
        myCamera.transform.position = newPosition;
    }

    public void moveCameraToLeft() {
        currentScreen -= 1;
        if (currentScreen < 0)
            currentScreen = nbScreens - 1;
        Vector3 newPosition = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, currentScreen * 15);
        myCamera.transform.position = newPosition;
    }
}

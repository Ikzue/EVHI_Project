using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFunctions : MonoBehaviour
{
    public int nbScreens = 3;
    public int currentScreen = 0;
    public GameObject UIScreen1;
    public GameObject UIScreen2;
    public GameObject UIScreen3;
    public Sprite UICurrentScreen;
    public Sprite UINotCurrentScreen;
    private Camera myCamera;
    private Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        myCamera = Camera.main;
        cameraPosition = myCamera.transform.position;
    }

    public void moveCameraToRight() {
        Debug.Log("right");
        currentScreen = (currentScreen + 1) % 3;
        Vector3 newPosition = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, currentScreen*15);
        myCamera.transform.position = newPosition;
        updateUICurrentScreen(currentScreen);
    }

    public void moveCameraToLeft() {
        Debug.Log("left");
        currentScreen -= 1;
        if (currentScreen < 0)
            currentScreen = nbScreens - 1;
        Vector3 newPosition = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y, currentScreen * 15);
        myCamera.transform.position = newPosition;
        updateUICurrentScreen(currentScreen);
    }

    private void updateUICurrentScreen(int currentScreen) {
        Image i1 = UIScreen1.GetComponent<Image>();
        Image i2 = UIScreen2.GetComponent<Image>();
        Image i3 = UIScreen3.GetComponent<Image>();
        i1.sprite = UINotCurrentScreen;
        i2.sprite = UINotCurrentScreen;
        i3.sprite = UINotCurrentScreen;
        switch (currentScreen) {
            case 0:
                i1.sprite = UICurrentScreen;
                break;
            case 1:
                i2.sprite = UICurrentScreen;
                break;
            case 2:
                i3.sprite = UICurrentScreen;
                break;
            default:
                break;
        }

    }
}

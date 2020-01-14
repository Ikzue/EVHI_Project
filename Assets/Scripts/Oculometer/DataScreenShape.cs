using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research;
using Tobii.Research.Unity;

public class DataScreenShape : MonoBehaviour
{
    public Vector4 UpperLeftZone = new Vector4(470, 570, 840, 870);
    public Vector4 BottomLeftZone = new Vector4(470, 70, 840, 370);
    public Vector4 MiddleZone = new Vector4(970, 300, 1340, 570);
    public Vector4 UpperRightZone = new Vector4(1430, 570, 1800, 870);
    public Vector4 BottomRightZone = new Vector4(1430, 70, 1800, 370);

    private int[] mostWatchedZone = new int[5];

    IEyeTracker eyetracker;
    private bool userWatching = false;
    private float watchingPosX;
    private float watchingPosY;
    private float width = 1920;
    private float height = 912;

    // Start is called before the first frame update
    void Start() {
        resetWatchedArray();
        eyetracker = EyeTrackingOperations.GetEyeTracker("tobii-ttp://TPNA1-030109626614");
        eyetracker.GazeDataReceived += EyeTracker_GazeDataReceived;
        InvokeRepeating("checkZoneWatched", 2.0f, 0.5f);
    }

    // Update is called once per frame
    void checkZoneWatched()
    {
        float x = watchingPosX;
        float y = watchingPosY;
        
        if (x > UpperLeftZone.x && x < UpperLeftZone.z && y > UpperLeftZone.y && y < UpperLeftZone.w) {
            mostWatchedZone[0] += 1;
            Debug.Log("UpperLeft");
        }
        else if (x > BottomLeftZone.x && x < BottomLeftZone.z && y > BottomLeftZone.y && y < BottomLeftZone.w) {
            mostWatchedZone[1] += 1;
            Debug.Log("BottomLeft");
        }
        else if(x > MiddleZone.x && x < MiddleZone.z && y > MiddleZone.y && y < MiddleZone.w) {
            mostWatchedZone[2] += 1;
            Debug.Log("MiddleZone");
        }
        else if(x > UpperRightZone.x && x < UpperRightZone.z && y > UpperRightZone.y && y < UpperRightZone.w) {
            mostWatchedZone[3] += 1;
            Debug.Log("UpperRight");
        }
        else if(x > BottomRightZone.x && x < BottomRightZone.z && y > BottomRightZone.y && y < BottomRightZone.w) {
            mostWatchedZone[4] += 1;
            Debug.Log("BottomRightZone");
        }
        else {
            //Debug.Log("Not looking in a zone");
            Debug.Log(string.Join(",", mostWatchedZone));
        }
        
    }
    void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e) {
        if (e.LeftEye.GazePoint.Validity.Valid()) {
            userWatching = true;
            float x = e.LeftEye.GazePoint.PositionOnDisplayArea.X + e.RightEye.GazePoint.PositionOnDisplayArea.X;
            x = x / 2 * width;
            float y = e.LeftEye.GazePoint.PositionOnDisplayArea.Y + e.RightEye.GazePoint.PositionOnDisplayArea.Y;
            y = height - y / 2 * height;
            watchingPosX = x;
            watchingPosY = y;
        }
        else {
            userWatching = false;
        }
    }

    private void resetWatchedArray() {
        for (int i = 0; i < mostWatchedZone.Length; i++)
            mostWatchedZone[i] = 0;
    }

    private void OnApplicationQuit() {
        eyetracker.GazeDataReceived -= EyeTracker_GazeDataReceived;
        EyeTrackingOperations.Terminate();
    }
}

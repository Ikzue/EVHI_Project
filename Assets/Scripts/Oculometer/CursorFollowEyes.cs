using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Research;
using Tobii.Research.Unity;

public class CursorFollowEyes : MonoBehaviour
{
    public GameObject cursor;
    private RectTransform r_cursor;
    IEyeTracker eyetracker;
    private RectTransform canvas;
    private bool userWatching = false;
    private float watchingPosX;
    private float watchingPosY;
    float width;
    float height;

    void Start() {
        eyetracker = EyeTrackingOperations.GetEyeTracker("tobii-ttp://TPNA1-030109626614");
        eyetracker.GazeDataReceived += EyeTracker_GazeDataReceived;
        r_cursor = cursor.GetComponent<RectTransform>();
        canvas = cursor.transform.parent.gameObject.GetComponent<RectTransform>();
        width = Screen.width;
        height = Screen.height;
    }

    void Update() {
        cursor.SetActive(userWatching);
        r_cursor.anchoredPosition = new Vector2(watchingPosX,watchingPosY);
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

    private void OnApplicationQuit() {
        eyetracker.GazeDataReceived -= EyeTracker_GazeDataReceived;
        EyeTrackingOperations.Terminate();
    }
}

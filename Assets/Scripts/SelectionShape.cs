﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionShape : MonoBehaviour
{
    Random rnd = new Random();
    public GameObject imageTargetShape;
    private List<GameObject> availableShapes;
    private GameObject targetShape;
    private int numberOfTargets = 0;

    private bool blockInput = false;
    private float bufferBlockInput;
    public GameObject correctAnswerImage;
    public GameObject wrongAnswerImage;


    // Update is called once per frame
    void Update()
    {
        if (blockInput) {
            bufferBlockInput -= Time.deltaTime;
            if (bufferBlockInput <= 0.0f) {
                blockInput = false;
                correctAnswerImage.SetActive(false);
                wrongAnswerImage.SetActive(false);
            }
        }
        else if (numberOfTargets > 0 && Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                ShapeComponent clickedShape = hit.transform.gameObject.GetComponent<ShapeComponent>();
                ShapeComponent target = targetShape.GetComponent<ShapeComponent>();
                if (target){
                    bufferBlockInput = 0.2f;
                    blockInput = true;
                    if (target.myShape.Equals(clickedShape.myShape) && target.myColor.Equals(clickedShape.myColor)) {
                        numberOfTargets--;
                        correctAnswerImage.SetActive(true);
                        initializeNextTarget();
                    }
                    else {
                        wrongAnswerImage.SetActive(true);
                    }
                }
            }
        }
    }

    public void initializeSelection(int n) {
        int numTargetShape;
        numberOfTargets = n;
        availableShapes = new List<GameObject>(GameObject.FindGameObjectsWithTag("Shape"));
        numTargetShape = Random.Range(0, availableShapes.Count);
        targetShape = availableShapes[numTargetShape];
        availableShapes.RemoveAt(numTargetShape);
        imageTargetShape.GetComponent<Image>().sprite = targetShape.GetComponent<ShapeComponent>().UIImage;
    }

    private void initializeNextTarget() {
        if(numberOfTargets>0){
            Debug.Log(numberOfTargets);
            int numTargetShape;
            numTargetShape = Random.Range(0, availableShapes.Count);
            targetShape = availableShapes[numTargetShape];
            availableShapes.RemoveAt(numTargetShape);
            imageTargetShape.GetComponent<Image>().sprite = targetShape.GetComponent<ShapeComponent>().UIImage;
        }
        else {
            Debug.Log("no shape left");
        }
    }

}

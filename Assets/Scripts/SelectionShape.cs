using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectionShape : MonoBehaviour
{
    Random rnd = new Random();
    public GameObject imageTargetShape;
    private List<GameObject> availableShapes;
    private GameObject targetShape;
    private int numberOfTargets;

    public GameObject correctAnswerImage;
    public GameObject wrongAnswerImage;


    // Update is called once per frame
    void Update()
    {
        if (numberOfTargets > 0 && Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                ShapeComponent clickedShape = hit.transform.gameObject.GetComponent<ShapeComponent>();
                Debug.Log(targetShape);
                ShapeComponent target = targetShape.GetComponent<ShapeComponent>();
                if (target.myShape.Equals(clickedShape.myShape) && target.myColor.Equals(clickedShape.myColor)) {
                    Debug.Log("TRUE");
                    numberOfTargets--;
                    initializeNextTarget();
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
        int numTargetShape;
        numTargetShape = Random.Range(0, availableShapes.Count);
        targetShape = availableShapes[numTargetShape];
        availableShapes.RemoveAt(numTargetShape);
        imageTargetShape.GetComponent<Image>().sprite = targetShape.GetComponent<ShapeComponent>().UIImage;
    }
}

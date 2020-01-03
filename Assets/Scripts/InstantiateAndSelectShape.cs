using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateAndSelectShape : MonoBehaviour
{
    private GameObject[] ListShapes;
    private int nbScreens = 3;
    // Start is called before the first frame update
    void Start()
    {
        ListShapes = GameObject.FindGameObjectsWithTag("Shape");
        initializeObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initializeObjects(int numberObjects = 2) {
        for(int i = 0; i < ListShapes.Length; i++) {

        }
    }
}

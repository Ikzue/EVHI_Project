using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Position = System.Collections.Generic.KeyValuePair<int, int>;

public class InitiateShapePosition : MonoBehaviour
{
    public enum Shape { Sphere, Cube, Prism, Oval, Cylinder };
    public enum Color { Red, Green, Blue };
    private GameObject[,] ListShapes;

    private int nbScreens = 3;
    private int nbPositions = 5;
    private int nbColors = 3;
    private int nbShapes = 5;

    private List<Position> availablePositions = new List<Position>();

    public GameObject ShapesScreen1;
    public GameObject ShapesScreen2;
    public GameObject ShapesScreen3;
    // Start is called before the first frame update
    void Start()
    {
        initializeShapes();
        initializeAvailablePositions();
        randomlyPositionShapes();
        GameObject go = GameObject.Find("MainLoop");
        SelectionShape s = go.GetComponent<SelectionShape>();
        s.initializeSelection(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initializeShapes() {
        int numColor, numShape;
        ListShapes = new GameObject[nbColors, nbShapes];
        GameObject[] AllShapes = GameObject.FindGameObjectsWithTag("Shape");
        foreach (GameObject shape in AllShapes) {
            numColor = (int)shape.GetComponent<ShapeComponent>().myColor;
            numShape = (int)shape.GetComponent<ShapeComponent>().myShape;
            ListShapes[numColor, numShape] = shape;
            shape.SetActive(false);
        }
    }

    private void initializeAvailablePositions() {
        for(int i=0; i < nbScreens; i++) {
            for(int j = 0; j < nbPositions; j++) {
                availablePositions.Add(new Position(i, j));
            }
        }
    }

    private void randomlyPositionShapes(int ShapesToPosition = 8) {
        //Debug.Log(availablePositions.Count);
        Random rnd = new Random();
        GameObject currentScreen;
        GameObject newShape;
        int color;
        int shape;
        int listPosition;
        Position position;
        for (int i = 0; i < ShapesToPosition; i++) {
            color = Random.Range(0, nbColors);
            shape = Random.Range(0, nbShapes);
            listPosition = Random.Range(0, availablePositions.Count);
            position = availablePositions[listPosition];
            availablePositions.RemoveAt(listPosition);

            currentScreen = getScreen(position.Key);

            newShape = Instantiate(ListShapes[color, shape]);
            newShape.SetActive(true);
            newShape.transform.parent = currentScreen.transform;
            newShape.transform.localPosition = getPosition(position);
        }
        //Debug.Log(position);
        //Debug.Log(availablePositions.Count);
    }
    /*
    private void randomlyPositionShapes(int ShapesToPosition = 16) {
        Random rnd = new Random();
        GameObject currentScreen;
        GameObject newShape;
        int nbScreen;
        int color;
        int shape;
        for (int i = 0; i < ShapesToPosition; i++) {
            nbScreen = Random.Range(0, nbScreens);
            currentScreen = getScreen(nbScreen);
            color = Random.Range(0, nbColors);
            shape = Random.Range(0, nbShapes);
            newShape = Instantiate(ListShapes[color, shape]);
            newShape.SetActive(true);
            newShape.transform.parent = currentScreen.transform;

            float newX = Random.Range(-screenWidth, screenWidth);
            float newY = 2;
            float newZ = Random.Range(-screenHeight, screenHeight);
            newShape.transform.localPosition = new Vector3(newX, newY, newZ);
        }
    }
    */
    private GameObject getScreen(int numScreen) {
        if (numScreen == 0)
            return ShapesScreen1;
        if (numScreen == 1)
            return ShapesScreen2;
        if (numScreen == 2)
            return ShapesScreen3;
        return null;
    }

    private Vector3 getPosition(Position p) {
        int screen = p.Key;
        int position = p.Value;
        float x, y, z;
        y = 2;
        switch (position) {
            case 0:
                x = 6.3f;
                z = -3.3f;
                break;
            case 1:
                x = 6.3f;
                z = 3.3f;
                break;
            case 2:
                x = 0f;
                z = 0f;
                break;
            case 3:
                x = -6.3f;
                z = -3.3f;
                break;
            case 4:
                x = -6.3f;
                z = 3.3f;
                break;
            default:
                throw new System.ArgumentException("Position doesn't exist", "p");
        }
        return new Vector3(x, y, z);


    }
}

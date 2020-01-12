using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeComponent : MonoBehaviour
{
    public enum Shape { Sphere, Cube, Prism, Oval, Cylinder};
    public enum Color { Red, Green, Blue};
    public Shape myShape;
    public Color myColor;
    public Sprite UIImage;
}

using UnityEngine;

public class Sphere_SphereTest : MonoBehaviour
{ 
    [SerializeField] private GameObject shape1;
    [SerializeField] private GameObject shape2;

    private bool isIntersect;

    private void Update() 
    {
        Sphere sphere1 = Mathematics.BoundingSphere(shape1);
        Sphere sphere2 = Mathematics.BoundingSphere(shape2);

        isIntersect = Mathematics.Sphere_SphereIntersection(sphere1, sphere2);

        if (!isIntersect)
            SetColor(shape1, Color.green);
        else
            SetColor(shape1, Color.red);
    }

    private void SetColor(GameObject shape, Color color)
    {
        shape.GetComponent<MeshRenderer>().material.color = color;
    }
}

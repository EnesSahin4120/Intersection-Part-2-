using UnityEngine;

public class Sphere : MonoBehaviour
{
    public float radius;
    public Vector3 center;

    public Sphere(float _radius, Vector3 _center) 
    {
        radius = _radius;
        center = _center;
    }
}



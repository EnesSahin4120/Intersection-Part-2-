using UnityEngine;

public class BoundingSphereTest : MonoBehaviour
{
    [SerializeField] private GameObject shape;
    private void Start()
    {
        Sphere sphere = Mathematics.BoundingSphere(shape);
        SphereCollider _collider = shape.AddComponent<SphereCollider>();
        Vector3 localCenter = shape.transform.InverseTransformPoint(sphere.center);
        _collider.center = localCenter;
        _collider.radius = .5f;
    }
}

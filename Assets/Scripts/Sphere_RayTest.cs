using UnityEngine;

public class Sphere_RayTest : MonoBehaviour
{
    [SerializeField] private Transform rayStart;
    [SerializeField] private Transform rayDirection;
    [SerializeField] private GameObject ball;

    private bool isIntersect;

    private void Update()
    {
        Coordinates startRayCoord = new Coordinates(rayStart.position.x, rayStart.position.y, rayStart.position.z);
        Coordinates directionRayCoord = new Coordinates(rayDirection.position.x, rayDirection.position.y, rayDirection.position.z);
        Line ray = new Line(startRayCoord, directionRayCoord, Line.LINETYPE.RAY);

        isIntersect = Mathematics.Sphere_RayIntersection(ball, ray);

        if (!isIntersect)
            Debug.DrawLine(startRayCoord.ToVector(), directionRayCoord.ToVector(), Color.green);
        else
            Debug.DrawLine(startRayCoord.ToVector(), directionRayCoord.ToVector(), Color.red);
    }
}

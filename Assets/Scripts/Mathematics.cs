using UnityEngine;

public class Mathematics : MonoBehaviour
{
    static public float Square(float grade)
    {
        return grade * grade;
    }

    static public float Distance(Coordinates coord1, Coordinates coord2)
    {
        float diffSquared = Square(coord1.x - coord2.x) +
            Square(coord1.y - coord2.y) +
            Square(coord1.z - coord2.z);
        float squareRoot = Mathf.Sqrt(diffSquared);
        return squareRoot;
    }

    static public float VectorLength(Coordinates vector)
    {
        float length = Distance(new Coordinates(0, 0, 0), vector);
        return length;
    }

    static public Coordinates Normalize(Coordinates vector)
    {
        float length = VectorLength(vector);
        vector.x /= length;
        vector.y /= length;
        vector.z /= length;

        return vector;
    }

    static public float Dot(Coordinates vector1, Coordinates vector2)
    {
        return (vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z);
    }

    static public Coordinates Projection(Coordinates vector1, Coordinates vector2)
    {
        float numeratorPart = Dot(vector1, vector2);
        float vector2Length = VectorLength(vector2);
        float denominatorPart = Square(vector2Length);
        Coordinates resultCoordinate = new Coordinates(vector2.x * (numeratorPart / denominatorPart), vector2.y * (numeratorPart / denominatorPart), vector2.z * (numeratorPart / denominatorPart));

        return resultCoordinate;
    }

    static public Vector3[] LocalToWorldVertices(GameObject shape)
    {
        MeshFilter meshfilter = shape.GetComponent<MeshFilter>();
        Mesh sharedMesh = meshfilter.sharedMesh;
        Matrix4x4 localtoWorldMatrix = shape.transform.localToWorldMatrix;
        Vector3[] resultVertices = new Vector3[sharedMesh.vertices.Length];
        for (int i = 0; i < resultVertices.Length; i++)
        {
            resultVertices[i] = localtoWorldMatrix.MultiplyPoint3x4(sharedMesh.vertices[i]);
        }
        return resultVertices;
    }

    static public Sphere BoundingSphere(GameObject shape) 
    {
        Vector3[] _points = LocalToWorldVertices(shape);
        Vector3 minPos_X, maxPos_X, minPos_Y, maxPos_Y, minPos_Z, maxPos_Z;
        minPos_X = minPos_Y = minPos_Z = Vector3.one * Mathf.Infinity;
        maxPos_X = maxPos_Y = maxPos_Z = Vector3.one * Mathf.NegativeInfinity;

        foreach (Vector3 current in _points)
        {
            if (current.x < minPos_X.x) minPos_X = current;
            if (current.x > maxPos_X.x) maxPos_X = current;
            if (current.y < minPos_Y.y) minPos_Y = current;
            if (current.y > maxPos_Y.y) maxPos_Y = current;
            if (current.z < minPos_Z.z) minPos_Z = current;
            if (current.z > maxPos_Z.z) maxPos_Z = current;
        }

        float center_X = (minPos_X.x + maxPos_X.x);
        float center_Y = (minPos_Y.y + maxPos_Y.y);
        float center_Z = (minPos_Z.z + maxPos_Z.z);
        Vector3 centerPos = new Vector3(center_X, center_Y, center_Z) * 0.5f;

        float maxDistance = (minPos_X - centerPos).sqrMagnitude;
        foreach (Vector3 current in _points)
        {
            float distance = (current - centerPos).sqrMagnitude;
            if (distance > maxDistance)
                maxDistance = distance;
        }
        float radiusSize = Mathf.Sqrt(maxDistance);

        Sphere result = new Sphere(radiusSize, centerPos);
        return result;
    }

    static public bool Sphere_SphereIntersection(Sphere _sphere1,Sphere _sphere2)
    {
        Coordinates diffBetweenCenters = new Coordinates(_sphere1.center.x, _sphere1.center.y, _sphere1.center.z)
                                       - new Coordinates(_sphere2.center.x, _sphere2.center.y, _sphere2.center.z);
        float sumOfRadius = _sphere1.radius + _sphere2.radius;

        bool result = Mathf.Abs(Dot(diffBetweenCenters, diffBetweenCenters)) <= sumOfRadius * sumOfRadius;
        return result;
    }

    static public bool Sphere_RayIntersection(GameObject sphereShape, Line ray) 
    {
        Sphere sphere = BoundingSphere(sphereShape);
        Coordinates sphereCenter = new Coordinates(sphere.center.x, sphere.center.y, sphere.center.z);

        Coordinates m = ray.A - sphereCenter;
        Coordinates d = Normalize(ray.v);
        float b = Dot(m, d);
        float c = Dot(m, m) - sphere.radius * sphere.radius;
        if (c > 0.0f && b > 0.0f)
            return false;

        float discr = b * b - c;
        if (discr < 0.0f)
            return false;

        return true;
    }
}

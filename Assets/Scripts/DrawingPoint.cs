using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingPoint : MonoBehaviour
{
    public bool isFilled;
    public Vector3[] polygonPoints;
    public int[] polygonTriangles;
    public float polygonRadius;
    public int polygonSides;
    public float centerRadius;
    public Object objectMesh;
    Mesh mesh;
    Material material;
    Material materialMesh;
    bool flag = false;
    Dictionary<Vector3, int> vector3s1 = new Dictionary<Vector3, int>();
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        polygonRadius = 0.1f;
        polygonSides = 24;
        material = GetComponent<Renderer>().material;
        materialMesh = Drawing.material;
        StartCoroutine(a());
    }

    // Update is called once per frame
    void Update()
    {
        if (isFilled)
        {
            DrawFilled(polygonSides, polygonRadius);
        }
        else
        {
            DrawHollow(polygonSides, polygonRadius, centerRadius);
        }
        
        if (vector3s1.ContainsKey(new Vector3(transform.parent.position.x, 0, transform.parent.position.z))
                        && vector3s1[new Vector3(transform.parent.position.x, 0, transform.parent.position.z)] == 1
                        && ControllerPers.animatorWork == false && flag == false)
        {
            //Debug.Log(transform.parent.position);
            material.SetColor("_Color", materialMesh.GetColor("_Color"));
            flag = true;
        }
    }

    IEnumerator a()
    {
        yield return new WaitUntil(() => GenerateBase.vector3s != null);
        vector3s1 = GenerateBase.vector3s;
    }

    void DrawFilled(int sides, float radius)
    {
        polygonPoints = GetCircumferencePoints(sides, radius).ToArray();
        polygonTriangles = DrawFilledTriangles(polygonPoints);
        mesh.Clear();
        mesh.vertices = polygonPoints;
        mesh.triangles = polygonTriangles;
    }

    List<Vector3> GetCircumferencePoints(int sides, float radius)
    {
        List<Vector3> points = new List<Vector3>();
        float circumferenceProgressPerStep = (float)1 / sides;
        float TAU = 2 * Mathf.PI;
        float radianProgressPerStep = circumferenceProgressPerStep * TAU;

        for (int i = 0; i < sides; i++)
        {
            float currentRadian = radianProgressPerStep * i;
            points.Add(new Vector3(Mathf.Cos(currentRadian) * radius, 0, Mathf.Sin(currentRadian) * radius));
        }
        return points;
    }

    int[] DrawFilledTriangles(Vector3[] points)
    {
        int triangleAmount = points.Length;
        List<int> triangle = new List<int>();
        for (int i = 0; i < triangleAmount; i++)
        {
            triangle.Add(0);
            triangle.Add(i + 2);
            triangle.Add(i + 1);
        }
        return triangle.ToArray();
    }

    void DrawHollow(int sides, float outerRadius, float innerRadius)
    {
        List<Vector3> pointList = new List<Vector3>();
        List<Vector3> outerPoints = GetCircumferencePoints(sides, outerRadius);
        pointList.AddRange(outerPoints);
        List<Vector3> innerPoints = GetCircumferencePoints(sides, innerRadius);
        pointList.AddRange(innerPoints);

        polygonPoints = pointList.ToArray();

        polygonTriangles = DrawHollowTriangles(polygonPoints);
        mesh.Clear();
        mesh.vertices = polygonPoints;
        mesh.triangles = polygonTriangles;
    }

    int[] DrawHollowTriangles(Vector3[] points)
    {
        int sides = points.Length / 2;
        int triangleAmount = sides * 2;

        List<int> triangle = new List<int>();
        for (int i = 0; i < sides; i++)
        {
            int outerIndex = i;
            int innerIndex = i + sides;

            //first
            triangle.Add(outerIndex);
            triangle.Add(innerIndex);
            triangle.Add((i + 1) % sides);

            //second
            triangle.Add(outerIndex);
            triangle.Add(sides + ((sides + i - 1) % sides));
            triangle.Add(outerIndex + sides);
        }
        return triangle.ToArray();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    [SerializeField] private Transform debugVisual1;
    [SerializeField] private Transform debugVisual2;
    Mesh mesh;
    public GameObject pers;
    ControllerPers persController;
    string stepPrev;

    static public Material material;
    // Start is called before the first frame update
    void Start()
    {
        persController = pers.GetComponent<ControllerPers>();
        material = GetComponent<Renderer>().material;
        mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = persController.getNowPos();//new Vector3 (0.0f, 0.0f, 0.0f);
        vertices[1] = persController.getNowPos();//new Vector3 (0.0f, 0.0f, 0.05f);
        vertices[2] = persController.getNowPos();//new Vector3 (0.05f, 0.0f, 0.05f);
        vertices[3] = persController.getNowPos();//new Vector3 (0.05f, 0.0f, 0.0f);

        uv[0] = Vector2.zero;//new Vector2(0.0f, 0.0f);
        uv[1] = Vector2.zero;//new Vector2(0.0f, 1.0f);
        uv[2] = Vector2.zero;//new Vector2 (1.0f, 1.0f);
        uv[3] = Vector2.zero;//new Vector2 (1.0f, 0.0f);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.MarkDynamic();

        GetComponent<MeshFilter>().mesh = mesh;

        

    }

    // Update is called once per frame
    void Update()
    {
        //if (isFilled)
        //{
        //    DrawFilled(polygonSides, polygonRadius);
        //}
        //else
        //{
        //    DrawHollow(polygonSides, polygonRadius, centerRadius);
        //}

        if (ControllerPers.animatorWork == true)
        {
            Vector3[] vertices = new Vector3[mesh.vertices.Length + 2];
            Vector2[] uv = new Vector2[mesh.uv.Length + 2];
            int[] triangles = new int[mesh.triangles.Length + 6];

            mesh.vertices.CopyTo(vertices, 0);
            mesh.uv.CopyTo(uv, 0);
            mesh.triangles.CopyTo(triangles, 0);

            int vIndex = vertices.Length - 4;
            int vIndex0 = vIndex + 0;
            int vIndex1 = vIndex + 1;
            int vIndex2 = vIndex + 2;
            int vIndex3 = vIndex + 3;

            Vector3 mainPersVector = (persController.getPos() - persController.getPosPrev()).normalized;
            Vector3 normal2D = new Vector3(0.0f, 0.5f, 0.0f);
            float lineThickness = 0.1f;
            Vector3 newVertexUp = persController.getNowPos() + Vector3.Cross(mainPersVector, normal2D) * lineThickness;
            Vector3 newVertexDown = persController.getNowPos() + Vector3.Cross(mainPersVector, normal2D * -1f) * lineThickness;

            //debugVisual1.position = newVertexUp;
            //debugVisual2.position = newVertexDown;

            vertices[vIndex2] = newVertexUp;
            vertices[vIndex3] = newVertexDown;

            //if (persController.getPos().x == persController.getPosPrev().x)
            //{
            //    if (stepPrev == "right_left")
            //    {
            //        if ((persController.getPos().z - persController.getPosPrev().z) > 0)
            //        {
            //            vertices[vIndex0] = new Vector3(newVertexUp.x, newVertexUp.y, newVertexUp.z - persController.moveSpace);
            //            vertices[vIndex1] = new Vector3(newVertexDown.x, newVertexDown.y, newVertexDown.z - persController.moveSpace);
            //        }
            //        else
            //        {
            //            vertices[vIndex0] = new Vector3(newVertexUp.x, newVertexUp.y, newVertexUp.z + persController.moveSpace);
            //            vertices[vIndex1] = new Vector3(newVertexDown.x, newVertexDown.y, newVertexDown.z + persController.moveSpace);
            //        }
            //    }
            //    stepPrev = "up_down";
            //}
            //else if (persController.getPos().z == persController.getPosPrev().z)
            //{
            //    if (stepPrev == "up_down")
            //    {
            //        if ((persController.getPos().x - persController.getPosPrev().x) > 0)
            //        {
            //            vertices[vIndex0] = new Vector3(newVertexUp.x - persController.moveSpace, newVertexUp.y, newVertexUp.z);
            //            vertices[vIndex1] = new Vector3(newVertexDown.x - persController.moveSpace, newVertexDown.y, newVertexDown.z);
            //        }
            //        else
            //        {
            //            vertices[vIndex0] = new Vector3(newVertexUp.x + persController.moveSpace, newVertexUp.y, newVertexUp.z);
            //            vertices[vIndex1] = new Vector3(newVertexDown.x + persController.moveSpace, newVertexDown.y, newVertexDown.z);
            //        }
            //    }
            //    stepPrev = "right_left";
            //}

            uv[vIndex2] = Vector2.zero;

            int tIndex = triangles.Length - 6;

            triangles[tIndex + 0] = vIndex0;
            triangles[tIndex + 1] = vIndex2;
            triangles[tIndex + 2] = vIndex1;

            triangles[tIndex + 3] = vIndex1;
            triangles[tIndex + 4] = vIndex2;
            triangles[tIndex + 5] = vIndex3;

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            

        }
        else
        {
            //Vector3[] vertices = new Vector3[mesh.vertices.Length + 8];
            //Vector2[] uv = new Vector2[mesh.uv.Length + 8];
            //int[] triangles = new int[mesh.triangles.Length + 24];

            //mesh.vertices.CopyTo(vertices, 0);
            //mesh.uv.CopyTo(uv, 0);
            //mesh.triangles.CopyTo(triangles, 0);

            //int[] vIndex0 = new int[8];
            //int vIndex = vertices.Length - 8;
            //for (int i = 0; i < vIndex; i++)
            //{
            //    vIndex0[i] = vIndex + i;
            //}

            //for (int i = 0; i < vIndex; i++)
            //{
            //    vertices[i] = new Vector3(persController.getPos().x + 0.05f, persController.getPos().y, persController.getPos().z + 0.05f); ;
            //}


            //vertices[vIndex3] = newVertexDown;

            //uv[vIndex2] = Vector2.zero;

            //int tIndex = triangles.Length - 6;

            //triangles[tIndex + 0] = vIndex0;
            //triangles[tIndex + 1] = vIndex2;
            //triangles[tIndex + 2] = vIndex1;

            //triangles[tIndex + 3] = vIndex1;
            //triangles[tIndex + 4] = vIndex2;
            //triangles[tIndex + 5] = vIndex3;

            //mesh.vertices = vertices;
            //mesh.uv = uv;
            //mesh.triangles = triangles;
        }
    }
}

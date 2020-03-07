using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Sphere : MonoBehaviour
{

    private struct LocalMesh{
        public List<Vector3> points;
        public List<int> faces;

        public LocalMesh(List<Vector3> p, List<int> f){
            this.points = p;
            this.faces = f;
        }
    }
    LocalMesh myMesh;
    List<Vector3> vertices;
    List<int> index;
    void Start()
    {
       List<Vector3> vertices = new List<Vector3>{
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 0, -1),
            new Vector3(0, 0, 1),
            new Vector3(0, 1, 0),
            new Vector3(0, -1, 0)
        };
        
        List<int> index = new List<int>{
            5,2,0, //Abajo Izquierda
            //3,2,1/*, //Medio
            5,1,2, //Abajo Atrás
            5,3,1, //Abajo Derecha
            5,0,3, //Abajo Adelante

            0,2,4, //Arriba Izquierda
            2,1,4, //Arriba Atrás
            1,3,4, //Arriba Derecha
            3,0,4  //Arriba Adelante
        };

        //De unity, no se puede modificar
        /*
        Mesh mesh = new Mesh();
        triangleMesh.vertices = points;
        triangleMesh.triangles = indices;
        */

        //Un mesh de nosotros, que si se puede modificar
        myMesh = new LocalMesh(vertices, index);

        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        //mr.sharedMaterial = new Material(Shader.Find("Standard"));
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        //mf.mesh = myMesh;
        mf.mesh.vertices = myMesh.points.ToArray();
        mf.mesh.triangles = myMesh.faces.ToArray();
        mf.mesh.RecalculateNormals();
        //tessell(vertices);

        for(int i=0;i<=2;i++){
            MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
            myMesh = TessellateMesh(myMesh);
            meshFilter.mesh.vertices = myMesh.points.ToArray();
            meshFilter.mesh.triangles = myMesh.faces.ToArray();
            meshFilter.mesh.RecalculateNormals();
        }

    }

    public void tessell(List<Vector3> vertices){
        for(int i=0;i<vertices.Count;i++){
            Debug.Log(vertices.ElementAt(i));
        }
    }

    LocalMesh TessellateMesh(LocalMesh inMesh){
        List<Vector3> outPoints = new List<Vector3>();
        List<int> outFaces = new List<int>();

        for(int i = 0; i < inMesh.faces.Count; i += 3){
            int inIndex0 = inMesh.faces[i+0];
            int inIndex1 = inMesh.faces[i+1];
            int inIndex2 = inMesh.faces[i+2];

            Vector3 v0 = inMesh.points[inIndex0];       //A
            Vector3 v1 = inMesh.points[inIndex1];       //B
            Vector3 v2 = inMesh.points[inIndex2];       //C
            Vector3 v3 = (0.5f * (v0+v1)).normalized;   //D
            Vector3 v4 = (0.5f * (v1+v2)).normalized;   //E
            Vector3 v5 = (0.5f * (v2+v0)).normalized;   //F

            int outIndex0 = outPoints.IndexOf(v0);
            if(outIndex0 == -1){
                outIndex0 = outPoints.Count;
                outPoints.Add(v0);
            }
            int outIndex1 = outPoints.IndexOf(v1);
            if(outIndex1 == -1){
                outIndex1 = outPoints.Count;
                outPoints.Add(v1);
            }
            int outIndex2 = outPoints.IndexOf(v2);
            if(outIndex2 == -1){
                outIndex2 = outPoints.Count;
                outPoints.Add(v2);
            }
            int outIndex3 = outPoints.IndexOf(v3);
            if(outIndex3 == -1){
                outIndex3 = outPoints.Count;
                outPoints.Add(v3);
            }
            int outIndex4 = outPoints.IndexOf(v4);
            if(outIndex4 == -1){
                outIndex4 = outPoints.Count;
                outPoints.Add(v4);
            }
            int outIndex5 = outPoints.IndexOf(v5);
            if(outIndex5 == -1){
                outIndex5 = outPoints.Count;
                outPoints.Add(v5);
            }

            outFaces.AddRange(new int[]{outIndex0, outIndex3, outIndex5});
            outFaces.AddRange(new int[]{outIndex3, outIndex4, outIndex5});
            outFaces.AddRange(new int[]{outIndex3, outIndex1, outIndex4});
            outFaces.AddRange(new int[]{outIndex5, outIndex4, outIndex2});

        }
        LocalMesh outMesh = new LocalMesh(outPoints, outFaces);
        return outMesh;


    }

    // Update is called once per frame
    void Update()
    {

    }
}

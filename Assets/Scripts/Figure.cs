using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    public List<Vector3> vertices = new List<Vector3>();

    public int[] faces;

    [SerializeField] float speed;

    void Update()
    {
        GetVertices();
        GetFaces();

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = new Color(0, 1, 0, 1f);
            foreach (Vector3 vertex in vertices)
            {
                Gizmos.DrawCube(vertex, new Vector3(0.2f, 0.2f, 0.2f));
            }

            for (int faceIndex = 0; faceIndex <= vertices.Count - 3; faceIndex += 3)
            {
                Vector3 point = GetFacePoint(faceIndex);

                Gizmos.DrawCube(point, new Vector3(0.1f, 0.1f, 0.1f));
                Gizmos.color = new Color(1, 1, 1, 1f);
                Gizmos.DrawLine(point, point + GetFaceNormal(faceIndex));
            }
        }
    }

    // Triangle Fan, Strip, Apellido ruso

    void GetVertices()
    {
        GetComponent<MeshFilter>().mesh.GetVertices(vertices);

        for (int i = 0; i < vertices.Count; i++)
        {
            vertices[i] = transform.TransformPoint(vertices[i]);
        }
    }

    void GetFaces()
    {
        faces = GetComponent<MeshFilter>().mesh.triangles;
    }

    Vector3 GetFacePoint(int faceIndex)
    {
        Vector3 point;
        point.x = (vertices[faceIndex].x + vertices[faceIndex + 1].x + vertices[faceIndex + 2].x) / 3;
        point.y = (vertices[faceIndex].y + vertices[faceIndex + 1].y + vertices[faceIndex + 2].y) / 3;
        point.z = (vertices[faceIndex].z + vertices[faceIndex + 1].z + vertices[faceIndex + 2].z) / 3;
        return point;
    }
    public Vector3 GetFaceNormal(int index)
    {
        // https://www.khronos.org/opengl/wiki/Calculating_a_Surface_Normal#:~:text=A%20surface%20normal%20for%20a,of%20the%20face%20w.r.t.%20winding).
        Vector3 firstVertex = vertices[index];
        Vector3 secondVertex = vertices[index + 1];
        Vector3 thirdVertex = vertices[index + 2];

        Vector3 normal;
        Vector3 firstSecond = secondVertex - firstVertex;
        Vector3 firstThird = thirdVertex - firstVertex;

        //Vector3 normal = Vector3.zero;
        //Vector3 normal = Vector3.Cross(secondVertex - firstVertex, thirdVertex - firstVertex).normalized;

        normal.x = (firstThird.y * firstSecond.z) - (firstThird.z * firstSecond.y);
        normal.y = (firstThird.z * firstSecond.x) - (firstThird.x * firstSecond.z);
        normal.z = (firstThird.x * firstSecond.y) - (firstThird.y * firstSecond.x);

        float magnitude = Mathf.Sqrt(Mathf.Pow(normal.x, 2) + Mathf.Pow(normal.y, 2) + Mathf.Pow(normal.z, 2));
        Vector3 normalizedNormal = normal / magnitude;

        return normalizedNormal;
    }

    public bool IsPointInside(Vector3 point, int faceIndex)
    {
        Vector3 normal = GetFaceNormal(faceIndex);
        Vector3 facePoint = GetFacePoint(faceIndex);
        //Vector3 offset;
        //offset.x = (vertices[faceIndex].x + vertices[faceIndex + 1].x + vertices[faceIndex + 2].x) / 3;
        //offset.y = (vertices[faceIndex].y + vertices[faceIndex + 1].y + vertices[faceIndex + 2].y) / 3;
        //offset.z = (vertices[faceIndex].z + vertices[faceIndex + 1].z + vertices[faceIndex + 2].z) / 3;

        return Vector3.Dot(normal, point - facePoint) > 0;
    }
}
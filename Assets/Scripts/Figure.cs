using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Figure : MonoBehaviour
{
    List<Vector3> vertices = new List<Vector3>();

    void Update()
    {
        GetVertices();
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
        {
            Gizmos.color = new Color(0, 1, 0, 1f);
            foreach (Vector3 vertice in vertices)
            {
                Gizmos.DrawCube(vertice, new Vector3(0.2f, 0.2f, 0.2f));
            }
        }
    }
    void GetVertices()
    {
        GetComponent<MeshFilter>().mesh.GetVertices(vertices);

        for(int i = 0; i < vertices.Count; i++)
        {
            vertices[i] = transform.TransformPoint(vertices[i]); 
        }
    }

}

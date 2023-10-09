using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] int depth;

    Vector3[,,] vectors;

    private void Start()
    {
        vectors = new Vector3[width + 1, height + 1, depth + 1];

        GameObject IcoSphere = GameObject.Find("IcoSphere");

        GameObject tetracontaedro = GameObject.Find("tetracontaedro");

        GetFace(IcoSphere);
        GetFace(tetracontaedro);

        //Vector3 objectCenter1 = IcoSphere.GetComponent<Renderer>().bounds.center;

        //Vector3 objectCenter2 = tetracontaedro.GetComponent<Renderer>().bounds.center;
        
        for (int i = 0; i <= height; i++)
        {
            for (int j = 0; j <= depth; j++)
            {
                for (int k = 0; k <= width; k++)
                {
                    vectors[k, i, j] = new Vector3(width / 2 - 1 * k, height / 2 - 1 * i, depth / 2 - 1 * j);
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = new Color(1, 1, 1, 0.2f);
            Gizmos.DrawCube(transform.position, new Vector3(width, height, depth));

            Gizmos.color = new Color(1, 0, 0, 0.3f);
            foreach (Vector3 vector in vectors)
            {
                Gizmos.DrawCube(vector, new Vector3(0.2f, 0.2f, 0.2f));
            }
        }
    }

    private void GetFace(GameObject figure) //Not sure if works
    {
        foreach (Transform face in figure.transform)
        {
            face.position = Vector3.up;
        }
    }

}

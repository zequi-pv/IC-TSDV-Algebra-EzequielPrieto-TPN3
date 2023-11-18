using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] public int width;
    [SerializeField] public int height;
    [SerializeField] public int depth;

    public Vector3[,,] points;
    public bool[,,] firstFigureIsCollidingPoint;
    public bool[,,] secondFigureIsCollidingPoint;

    private void Start()
    {
        Vector3 pos = transform.position;
        points = new Vector3[width + 1, height + 1, depth + 1];
        firstFigureIsCollidingPoint = new bool[width + 1, height + 1, depth + 1];
        secondFigureIsCollidingPoint = new bool[width + 1, height + 1, depth + 1];

        for (int i = 0; i <= height; i++)
        {
            for (int j = 0; j <= depth; j++)
            {
                for (int k = 0; k <= width; k++)
                {
                    points[k, i, j] = new Vector3(width / 2 - 1 * k + pos.x, height / 2 - 1 * i + pos.y, depth / 2 - 1 * j + pos.z);
                    firstFigureIsCollidingPoint[k, i, j] = false;
                    secondFigureIsCollidingPoint[k, i, j] = false;
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

            //foreach (Vector3 point in points)
            //{
            //    Gizmos.DrawCube(point, new Vector3(0.2f, 0.2f, 0.2f));
            //}


            for (int i = 0; i <= height; i++)
            {
                for (int j = 0; j <= depth; j++)
                {
                    for (int k = 0; k <= width; k++)
                    {
                        if (firstFigureIsCollidingPoint[k, i, j] && secondFigureIsCollidingPoint[k, i, j])
                            Gizmos.color = Color.blue;
                        else if (firstFigureIsCollidingPoint[k, i, j])
                            Gizmos.color = Color.yellow;
                        else if(secondFigureIsCollidingPoint[k, i, j])
                            Gizmos.color = Color.yellow;
                        else
                            Gizmos.color = Color.red;

                        Gizmos.DrawCube(points[k, i, j], new Vector3(0.2f, 0.2f, 0.2f));
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GridController grid;
    [SerializeField] GameObject[] figures;
    void Update()
    {
        for (int i = 0; i <= grid.height; i++)
        {
            for (int j = 0; j <= grid.depth; j++)
            {
                for (int k = 0; k <= grid.width; k++)
                {

                    foreach (GameObject figure in figures)
                    {
                        if (CheckPointInsideFigure(figure, i, j, k))
                        {
                            if (figure.gameObject.name == "IcoSphere")
                            {
                                grid.firstFigureIsCollidingPoint[k, i, j] = true;
                                // Debug.Log("in: " + figure.gameObject.name + i + j + k);
                            }
                            else if (figure.gameObject.name == "tetracontaedro")
                            {

                                grid.secondFigureIsCollidingPoint[k, i, j] = true;
                            }

                        }
                        else
                        {
                            if (figure.gameObject.name == "IcoSphere")
                            {
                                // Debug.Log("out: " + figure.gameObject.name + i + j + k);
                                grid.firstFigureIsCollidingPoint[k, i, j] = false;
                            }
                            else if (figure.gameObject.name == "tetracontaedro")
                                grid.secondFigureIsCollidingPoint[k, i, j] = false;

                        }
                    }
                }
            }
        }
    }

    bool CheckPointInsideFigure(GameObject figure, int i, int j, int k)
    {
        int facesQty = figure.GetComponent<MeshFilter>().mesh.vertices.Length / 3;
        for (int faceIndex = 0; faceIndex <= figure.GetComponent<Figure>().vertices.Count - 3; faceIndex += 3)
        {
            if (figure.GetComponent<Figure>().IsPointInside(grid.points[k, i, j], faceIndex))
            {
                //Debug.Log("ATRODEN" + faceIndex);
            }
            else
            {
                //Debug.Log("ARAFUE" + faceIndex);
                return false;
            }
        }

        //if (k == 0 && i == 0 && j == 0)
        //    Debug.Log(normalInside);

        return true;
    }
}



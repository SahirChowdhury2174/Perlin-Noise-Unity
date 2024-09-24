using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChild : MonoBehaviour
{
    Transform childTransform;

    void Start()
    {
        childTransform = transform.Find("MeshGeneratorPart2");

        childTransform.position = new Vector3(Random.Range(0, 50), 0, Random.Range(0, 50));
    }
}

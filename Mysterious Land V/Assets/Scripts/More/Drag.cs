using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{

    public GameObject prefab;


    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }

}

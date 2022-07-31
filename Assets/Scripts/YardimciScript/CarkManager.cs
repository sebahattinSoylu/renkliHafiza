using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarkManager : MonoBehaviour
{
    [SerializeField]
    int hiz;


    void Update()
    {
        transform.Rotate(Vector3.forward * hiz * Time.deltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Vector3 rotation;
   void Update()
    {
         transform.Rotate(new Vector3(0,45,0) * Time.deltaTime);
    }
}

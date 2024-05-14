using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    
    void Update()
    {
        transform.LookAt(target);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecenter : MonoBehaviour
{
    [SerializeField] Transform origin, head, target;

    void Start()
    {
        StartCoroutine(AutoRecenter());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RecenterCamera();
    }

    public void RecenterCamera()
    {
        Vector3 offset = head.position - origin.position;
        offset.y = 0f;
        origin.position = target.position - offset;

        float angle = Vector3.SignedAngle(head.forward, target.forward, Vector3.up);

        origin.RotateAround(head.position, Vector3.up, angle);
    }

    IEnumerator AutoRecenter()
    {
        yield return new WaitForSeconds(0.65f);
        RecenterCamera();
    }
}

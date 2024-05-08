using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraSpeed = 2f;
    public float yOffset = 0f;
    public Transform target;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 newPosx = new Vector3(target.position.x, target.position.y + yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPosx, cameraSpeed * Time.fixedDeltaTime);
    }
}

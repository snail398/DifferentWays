using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dampTime = 0.2f;
    public Transform target;
    public float offset = 1f;   

    private Camera m_camera;
    private Vector3 moveVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        m_camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x, target.position.y+offset, target.position.z) , ref moveVelocity, dampTime);
    }
}

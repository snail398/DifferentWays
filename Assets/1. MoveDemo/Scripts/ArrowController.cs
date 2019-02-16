using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    float time = 0;
    Vector3 startPosition;
    public float amplitude = 0.2f;
    public float speed = 1f;
    public string axe = "z";

    float x;
    float y;
    float z;
    // Start is called before the first frame update
    void Awake()
    {
        startPosition = transform.position;
        time = Random.Range(0, 20);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime*speed;
        switch (axe)
        {
            case "z":
                x = startPosition.x + 0;
                y = startPosition.y + 0;
                z = startPosition.z + Mathf.Sin(time) * amplitude;
                break;
            case "x":
                z = startPosition.z + 0;
                y = startPosition.y + 0;
                x = startPosition.x + Mathf.Sin(time) * amplitude;
                break;
            case "y":
                x = startPosition.x + 0;
                z = startPosition.z + 0;
                y = startPosition.y + Mathf.Sin(time) * amplitude;
                break;
        }

        transform.position = new Vector3(x, y, z);
    }
}

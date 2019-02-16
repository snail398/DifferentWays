using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RasengunController : MonoBehaviour
{
    public int damage = 10;
    public Vector3 direction;
    public int speed;
    float lifetimer=3f;
    float temp;

    void Awake()
    {
        
    }


    private void Update()
    {
        temp += Time.deltaTime;
        if (temp>lifetimer)
            Destroy(gameObject);
    }
    void FixedUpdate()
    {
        transform.position += direction * speed/10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<Character>()!=null && !other.gameObject.name.Equals("screen"))
        {
            Character hitTarget = other.gameObject.GetComponentInParent<Character>();
            hitTarget.setHP(hitTarget.getHP()-damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}

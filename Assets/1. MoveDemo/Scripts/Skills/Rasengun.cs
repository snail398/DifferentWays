using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rasengun : MonoBehaviour
{
    public int cost;
    public int damage;
    public int speed;
    public GameObject rasengun;

    Character character;
    GameObject rasengunController;

    void Awake()
    {
        character = GetComponent<Character>();

    }


    public void Fire(Vector3 direction)
    {
        if ( character.getMP() >= cost)
        {
            rasengunController = (GameObject)Instantiate(rasengun, gameObject.transform.Find("hand").transform.position, Quaternion.identity);
            RasengunController cntr = rasengunController.GetComponent<RasengunController>();
            cntr.speed = speed;
            cntr.damage = damage;
            cntr.direction = direction;
            cntr.direction.Normalize();
            character.setMP(character.getMP()-cost);
        }

    }
}

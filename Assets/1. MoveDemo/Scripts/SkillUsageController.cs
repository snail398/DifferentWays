using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUsageController : MonoBehaviour
{
    private Rasengun rasengun;
    float tempTime = 0f;
    float fireDelay = 2f;

    void Awake()
    {
        rasengun = GetComponent<Rasengun>();
    }

    void Update()
    {
        tempTime += Time.deltaTime;
        switch (gameObject.tag) {
            case "Player":
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    rasengun.Fire(GetComponent<Character>().getMovementController().ReturnTargetDirection());
                }
                break;
            case "Enemy":
                if (tempTime>fireDelay)
                {
                    rasengun.Fire(new Vector3(-1,0,0));
                    tempTime = 0;
                }
                break;
        }
       }
 }


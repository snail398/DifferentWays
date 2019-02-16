using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject player;
    Text heathText;
    int startHP;
    Text manaText;
    int startMP;

    // Start is called before the first frame update
    void Start()
    {
        heathText = GameObject.Find("Health").GetComponent<Text>();
        startHP = player.GetComponent<Character>().getHP();
        manaText = GameObject.Find("Mana").GetComponent<Text>();
        startMP = player.GetComponent<Character>().getMP();
    }

    // Update is called once per frame
    void Update()
    {
        heathText.text = player.GetComponent<Character>().getHP() + "/" + startHP;
        manaText.text = player.GetComponent<Character>().getMP() + "/" + startMP;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character:MonoBehaviour
{
    public int HP;
    public int MP;

    private CharacterMovementController movementController;
    
    private void Awake()
    {
        movementController = GetComponent<CharacterMovementController>();
    }

    public int getHP()
    {
        return HP;
    }

    public void setHP(int hp)
    {
        this.HP = hp;
    }
    
    public int getMP()
    {
        return MP;
    }

    public void setMP(int mp)
    {
        this.MP = mp;
    }

    public CharacterMovementController getMovementController()
    {
        return movementController;
    }
}

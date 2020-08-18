using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public void AttackDone()
    {
        //print("done");
        Player.instance.actionCooldown = false;
        Player.instance.weaponAnimator.speed = 1;
    }
}

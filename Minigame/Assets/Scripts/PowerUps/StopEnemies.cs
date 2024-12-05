using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEnemies : PowerUp
{
    override public void GivePowerUp()
    {
        GameManager.Instance.StopEnemiesPower();
    }
}

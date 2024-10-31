using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriShotPowerUp : PowerUp
{

    override public void GivePowerUp()
    {
        GameManager.Instance.TriShotUpgrade();
    }
}

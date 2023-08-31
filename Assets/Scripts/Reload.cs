using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    private int magSize = 30;
    private int bulletsLeftInLoad = 30;

    public void AssignMagSize(int value)
    {
        magSize = value;
    }
    
    public bool BulletCounter()
    {
        if (bulletsLeftInLoad > 0)
        {
            bulletsLeftInLoad--;
            return true;
        }
        else
        {
            return false;
        }

    }

    public void ReloadMag()
    {
        bulletsLeftInLoad = magSize;
    }

}

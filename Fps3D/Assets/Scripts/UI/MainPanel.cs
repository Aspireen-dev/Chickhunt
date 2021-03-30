using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour
{
    private Crosshair crosshair;
    
    void Start()
    {
        crosshair = GetComponentInChildren<Crosshair>();
    }

    public void Aim()
    {
        crosshair.Aim();
    }

    public void Shoot()
    {
        crosshair.Shoot();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaterEtops : BaseEnemy
{
    [SerializeField]
    WaterEnemy Waterout;


    protected override void TimerEnded()
    {
        base.TimerEnded();
        Waterout.spawnS();
    }
}

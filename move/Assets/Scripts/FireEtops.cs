using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEtops: BaseEnemy
{
    [SerializeField]
    FireEnemy fireout;

    protected override void TimerEnded()
    {
        base.TimerEnded();
        fireout.enabled = true;
    }
}

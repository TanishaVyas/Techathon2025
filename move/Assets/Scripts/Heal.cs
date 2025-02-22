using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Heal : MonoBehaviour
{
    public int health = 100;
    public enemy en;
    public colourChangePortal cs;
    public GameObject damagepopup;
public void increaseHelath(int shealth)
{
    health+=shealth;
}


public void decreaseHelath(int shealth)
{   
    health-=shealth;
    cs.setFlash();
    var obj =Instantiate(damagepopup, transform.position, Quaternion.identity);
    obj.GetComponent<TMP_Text>().text = shealth+"";
    if(health<=0)
    {
        en.stopgame();
    }
}





}

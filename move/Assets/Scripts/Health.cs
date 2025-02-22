using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float player_health;
    public float max_health = 100;
    public Image fill_image;
    private Slider slider;
    public Heal heal;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {   player_health= heal.health;
        if (slider.value<=slider.minValue)
        {
            fill_image.enabled = false; 
        }
        if (slider.value > slider.maxValue & !fill_image.enabled)
        {
            fill_image.enabled=true;
        }
        float fill_value=player_health/max_health;
        slider.value = fill_value;
        
    }
}

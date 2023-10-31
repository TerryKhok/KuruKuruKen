using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;

    //HPバーの最大値を設置
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    //HPバーの数値を設置
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    
}

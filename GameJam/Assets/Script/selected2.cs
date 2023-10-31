using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selected2 : MonoBehaviour
{
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = SharedData.playerSelect2 + "\n" + WeaponSelect(SharedData.playerSelect2);
    }

    string WeaponSelect(string weaponName)
    {
        if (weaponName == "Dagger")
        {
            return "Skill: Dash!";
        }
        else if (weaponName == "Katana")
        {
            return "Skill: Iaigiri!";
        }
        else if (weaponName == "Kusarigama")
        {
            return "Skill: Lightning!";
        }
        else if (weaponName == "Greatsword")
        {
            return "Skill: Strike!";
        }
        else if (weaponName == "Knuckle")
        {
            return "Skill: Punch!";
        }
        else if (weaponName == "Sword&Shield")
        {
            return "Skill: Guard!";
        }
        else if (weaponName == "Scythe")
        {
            return "Skill: Pull!";
        }
        else
        {
            return null;
        }
    }
}

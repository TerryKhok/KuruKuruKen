using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UImanage : MonoBehaviour
{
    [SerializeField] GameObject keyboard;
    [SerializeField] GameObject gamepad;
    [SerializeField] GameObject start;
    // Start is called before the first frame update
    void Start()
    {
       if(start != null)
        {
            start.SetActive(false);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.all.Count != 0)
        {
            gamepad.SetActive(true);
            keyboard.SetActive(false);

        }
        else
        {
            gamepad.SetActive(false);
            keyboard.SetActive(true);
        }

        if(start != null)
        {
            if (SharedData.playerSelect2 != null && SharedData.playerSelect1 != null)
            {
                start.SetActive(true);
            }
            else
            {
                start.SetActive(false);
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public Gamepad[] gamepads;
    
   //コントローラー判定、Debug用
    private void Start()
    {
        gamepads = Gamepad.all.ToArray();

        for (int i = 0; i < gamepads.Length; i++)
        {
            Debug.Log("Gamepad " + (i + 1) + " connected: " + gamepads[i].displayName);
        }
    }
   
}


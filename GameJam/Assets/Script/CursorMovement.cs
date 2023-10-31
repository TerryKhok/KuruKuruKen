using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorMovement : MonoBehaviour
{
    public Rigidbody2D rb2;
    [SerializeField]public float spd;
    public Vector2 direction;
    public bool dash = false;
    public InputAction move;
    public InputAction act;
    Gamepad[] gamepad;
    bool controller = false;
    Vector2 gamepadDir;
    Vector2 keyDir;
    Component script;
    bool KeyButton;
    bool GamepadButton;
    bool coolingdown = false;

    public bool objectSelected = false;
    public GameObject playerSelection;
    public static EventHandler DoneSelectingEvent;

    //material
    public Material lit;
    public Material sprites;

    //初期化
    void Start()
    {
        spd=10;
        rb2 = GetComponent<Rigidbody2D>();
        script = gameObject.GetComponent<MonoBehaviour>();
    }

    void Update()
    {
        //コントローラーの判定
        if (Gamepad.all.Count != 0)
        {
            gamepad = Gamepad.all.ToArray();
            controller = true;
        }

        //キーボード入力
        keyDir = move.ReadValue<Vector2>();
        KeyButton = act.IsPressed();

        if (keyDir != Vector2.zero)
        {
            direction = keyDir;
        }
        else
        {
            direction = gamepadDir;
        }

        //コントローラー入力
        if (gameObject.tag == "Player1" && controller && gamepad.Length >= 1)
        {
            gamepadDir = gamepad[0].leftStick.ReadValue();
            GamepadButton = gamepad[0].buttonEast.isPressed;
        }
        else if (gameObject.tag == "Player2" && controller && gamepad.Length >= 2)
        {
            gamepadDir = gamepad[1].leftStick.ReadValue();
            GamepadButton = gamepad[1].buttonEast.isPressed;
        }

    //==========スキルのクールダウン==========
        if ((KeyButton || GamepadButton) && !coolingdown)
        {
            script.SendMessage("SelectCharacter", gameObject);
            StartCooldown();
            KeyButton = false;
            GamepadButton = false;
        }
    }

    private void StartCooldown()
    {
        StartCoroutine(Cooldown());
    }

     private IEnumerator Cooldown()
    {
        coolingdown = true;
        yield return new WaitForSeconds(0.5f);
        coolingdown = false;
    }
    //========================================

    //カーソル移動
    private void FixedUpdate()
    {
        rb2.velocity = new Vector3(direction.x * spd, direction.y * spd);
    }

    private void OnEnable()
    {
        move.Enable();
        act.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
        act.Disable();
    }

    void setSpd0()
    {
        Debug.Log("set spd to 0");
        spd = 0;
    }
    void setSpd10()
    {
        Debug.Log("set spd to 10");
        spd = 10;
    }
}


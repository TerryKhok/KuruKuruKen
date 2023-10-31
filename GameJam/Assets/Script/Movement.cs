using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb2;
    public float spd = 10;
    public Vector2 direction;
    public bool dash = false;
    public InputAction move;
    public InputAction act;
    Gamepad[] gamepad;
    bool controller = false;
    Vector2 gamepadDir;
    Vector2 keyDir;

    //weapon
    public HingeJoint2D weaponJoint;
    public GameObject weapon;
    Component script;

    //cooldown
    bool KeyButton;
    bool GamepadButton;
    [SerializeField] float cooldownT = 5f;
    public bool coolingdown = false;


    //material
    public Material lit;
    public Material sprites;

    //knockback
    [SerializeField] float KnockbackDis = 50f;
    [SerializeField] float knockDur = 0.3f;

    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        script = weaponskill(weapon);

        //コントローラーの判定
        if (Gamepad.all.Count != 0)
        {
            gamepad = Gamepad.all.ToArray();
            controller = true;
        }
        else
        {
            controller = false;
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
        if ((KeyButton || GamepadButton) && !coolingdown && weapon != null && !PauseManu.isPaused)
        {
            script.SendMessage("Skill", gameObject);
            FindObjectOfType<AudioManager>().Play("SkillUsed");
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
        weapon.GetComponent<Renderer>().material = lit;

        yield return new WaitForSeconds(cooldownT);
        FindObjectOfType<AudioManager>().Play("SkillCharged");
        coolingdown = false;
        weapon.GetComponent<Renderer>().material = sprites;
    }
    //========================================

    //プレイヤー移動
    private void FixedUpdate()
    {
        if (!dash)
        {
            rb2.velocity = new Vector2(direction.x * spd, direction.y * spd);
        }
    }

    //装備してる武器をreturnする
    public GameObject GetCurrentWeapon(HingeJoint2D hj)
    {
        GameObject weapon = null;
        if (hj != null && hj.connectedBody != null)
        {
            weapon = hj.connectedBody.gameObject;
        }
        return weapon;
    }

    //装備してる武器スキルをreturnする
    public MonoBehaviour weaponskill(GameObject weapon)
    {
        return weapon.GetComponent<MonoBehaviour>();
    }

    //==========knockback処理==========
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Weapon") && collision.gameObject != weapon && collision.gameObject != ownkusarigama())
        {
            StartCoroutine(KnockBCoroutine(collision.gameObject));
        }
    }

    private IEnumerator KnockBCoroutine(GameObject collision)
    {
        float elapsedTime = 0;
        int damageTaken = GetComponent<player_1>().dmgTaken;
        dash = true;
        Vector2 dir = (transform.position - collision.transform.position).normalized;

        while (elapsedTime < knockDur)
        {
            rb2.AddForce(dir * KnockbackDis * damageTaken, ForceMode2D.Impulse);

            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        dash = false;
    }
    //====================================

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

    //kusarigama detect
    GameObject ownkusarigama()
    {
        if (weapon.gameObject.tag == "Kusarigama")
        {
            return weapon.GetComponent<Kusarigama>().scythe;
        }
        else
        {
            return null;
        }
    }
}

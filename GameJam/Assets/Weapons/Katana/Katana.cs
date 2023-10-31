using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : MonoBehaviour
{
    public GameObject Player;
    string enermyTag;
    [SerializeField] int MaxDmg = 15;
    [SerializeField] int MinDmg = 3;
    public int damage = 9;
    HingeJoint2D joint2D;

    // Swing

    [SerializeField] float swingDuration = 0.4f;
    [SerializeField] float swingDis = 4f;
    Rigidbody2D rb;
    bool hit = false;

    //particle
    public ParticleSystem blood;
    public ParticleSystem SwingParticle;
    public GameObject kiseki;
    public ParticleSystem spark;
    public ParticleSystem flare;
    public GameObject swingObject;
    public GameObject Saya;
    float colordownTime = 0.7f;
    private GameObject hitPlayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(joint2D.jointSpeed);
        }
    }

    private void Start()
    {
        if (Player.gameObject.tag == "Player1")
        {
            enermyTag = "Player2";

        }
        else if (Player.gameObject.tag == "Player2")
        {
            enermyTag = "Player1";
        }
        rb = GetComponent<Rigidbody2D>();
        joint2D = Player.GetComponent<HingeJoint2D>();
    }

    public void Skill(GameObject Player)
    {
        StartCoroutine(SwingCoroutine(Player));

    }

    private IEnumerator SwingCoroutine(GameObject Player)
    {
        HingeJoint2D hingeJoint = Player.GetComponent<HingeJoint2D>();
        JointMotor2D motor = hingeJoint.motor;
        motor.motorSpeed = -700f;
        hingeJoint.motor = motor;
        float elapsedTime = 0f;
        float t = elapsedTime;
        Vector2 originalAnchor = hingeJoint.connectedAnchor;
        hingeJoint.connectedAnchor = new Vector2(swingDis, 1);
        float startTime = Time.time;
        damage = 12;

        //=====================================================================================================
        this.GetComponent<Renderer>().enabled = false;

        Vector3 Pos = this.transform.position;
        Quaternion Rot = this.transform.rotation;

        Particle_Swing(flare);

        GameObject saya = Instantiate(Saya);
        saya.gameObject.AddComponent<HingeJoint2D>();
        saya.gameObject.GetComponent<HingeJoint2D>().connectedBody = Player.GetComponent<Rigidbody2D>();
        saya.transform.position = Pos;
        saya.transform.rotation = Rot;

        GameObject swing = Instantiate(swingObject, this.transform, false);

        GameObject newTrail = Instantiate(kiseki, this.transform);

        ParticleSystem newParticle = Instantiate(SwingParticle, this.transform);
        //newParticle.transform.position = Pos;
        //newParticle.transform.rotation = Rot;
        newParticle.Play();
        //=====================================================================================================

        while (elapsedTime < swingDuration && !hit)
        {
            hingeJoint.useMotor = true;
            elapsedTime = Time.time - startTime;
            t = elapsedTime;
            newParticle.transform.position = this.transform.position;
            yield return null;
        }

        Destroy(newTrail);

        Destroy(newParticle.gameObject, 0.5f);

        elapsedTime = 0f;
        motor.motorSpeed = 700f;
        hingeJoint.motor = motor;
        hingeJoint.connectedAnchor = originalAnchor;
        startTime = Time.time;

        while (elapsedTime < t)
        {
            hingeJoint.useMotor = true;
            elapsedTime = Time.time - startTime;
            yield return null;
        }

        Destroy(swing);
        Destroy(saya);
        Particle_Swing(spark);
        this.GetComponent<Renderer>().enabled = true;

        elapsedTime = 0f;
        motor.motorSpeed = -0.1f;
        hingeJoint.motor = motor;
        StartCoroutine(DisableJointAfterSwing());
        damage = 9;
    }


    void Particle_Swing(ParticleSystem particle)
    {
        ParticleSystem newParticle = Instantiate(particle);
        newParticle.transform.position = this.transform.position;
        newParticle.Play();
        Destroy(newParticle.gameObject, 1f);
    }


    private IEnumerator DisableJointAfterSwing()
    {
        yield return new WaitForSeconds(0.03f);
        HingeJoint2D hingeJoint = Player.GetComponent<HingeJoint2D>();
        JointMotor2D motor = hingeJoint.motor;
        motor.motorSpeed = 0f;
        hingeJoint.motor = motor;
        hingeJoint.useMotor = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == enermyTag)
        {
            collision.gameObject.GetComponent<player_1>().TakeDamage(DmgManage());
            Debug.Log(gameObject.tag + ":" + DmgManage());
            StartColorDown(collision);
            Particle(collision);
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            hit = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        hit = false;
    }

    //particle
    private void Particle(Collision2D collision)
    {
        ParticleSystem newParticle = Instantiate(blood);

        Vector3 hitPos = collision.contacts[0].point;
        newParticle.transform.position = hitPos;

        Quaternion lookRotation = Quaternion.LookRotation(this.transform.position - GameObject.FindWithTag(enermyTag).transform.position, Vector3.up);
        newParticle.transform.rotation = lookRotation;

        newParticle.Play();
        Destroy(newParticle.gameObject, 1.2f);
    }

    private void StartColorDown(Collision2D collision)
    {
        hitPlayer = collision.gameObject;
        hitPlayer.GetComponent<Renderer>().material.color -= new Color32(50, 50, 50, 1);
        StartCoroutine(ColorDown(hitPlayer));

    }

    private IEnumerator ColorDown(GameObject hitPlayer)
    {
        yield return new WaitForSeconds(colordownTime);
        hitPlayer.GetComponent<Renderer>().material.color += new Color32(50, 50, 50, 1);
    }

    //damage
    int DmgManage()
    {
        HingeJoint2D joint2D;
        joint2D = Player.GetComponent<HingeJoint2D>();
        float weaponSpd = Mathf.Abs(joint2D.jointSpeed) / 700; // 700 = maxSpinSpeed
        float grossDmg = weaponSpd * damage;
        if (grossDmg <= MinDmg || (int)grossDmg == 0)
        {
            return MinDmg;
        }
        else if (grossDmg < MaxDmg)
        {
            return ((int)grossDmg);
        }
        else
        {
            return MaxDmg;
        }

    }
}

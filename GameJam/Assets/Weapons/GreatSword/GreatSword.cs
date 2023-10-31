using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSword : MonoBehaviour
{
    public GameObject Player;
    string enermyTag;
    public int damage = 18;
    [SerializeField] int MaxDmg = 20;
    [SerializeField] int MinDmg = 10;
    float oldRotation;
    [SerializeField] float t = 0f;
    [SerializeField] float motorSpeed = 0f;
    float motorSpeedDir = 0f;
    [SerializeField] float skillDuration = 0f;



    //particle
    public ParticleSystem blood;
    public ParticleSystem fireA;
    public ParticleSystem fireB;
    public ParticleSystem hiteff;
    bool slash;
    float colordownTime = 0.7f;
    private GameObject hitPlayer;

    int particleDir = 0;



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
        oldRotation = gameObject.transform.rotation.eulerAngles.z;
    }

    IEnumerator Slash(GameObject player, GameObject weapon)
    {
        // Make the hinge motor rotate at speed 800
        float direction = gameObject.transform.rotation.eulerAngles.z - oldRotation;
        if (direction > 0f)
        {
            motorSpeedDir = motorSpeed * -1;
            particleDir = 0;
        }
        else if (direction < 0f)
        {
            motorSpeedDir = motorSpeed;
            particleDir = 1;
        }
        else
        {
            motorSpeedDir = motorSpeed;

        }

        oldRotation = gameObject.transform.rotation.eulerAngles.z;
        HingeJoint2D hinge = player.GetComponent<HingeJoint2D>();
        JointMotor2D motor = hinge.motor;
        motor.motorSpeed = motorSpeedDir;
        hinge.useMotor = true;
        hinge.motor = motor;
        weapon.GetComponent<Rigidbody2D>().angularDrag = 50f;

        slash = true;

        FireA();

        ParticleSystem newParticle = Instantiate(fireB);
        newParticle.Play();

        t = 0f;
        float startTime = Time.time;

        while (t <= 1.5f)
        {
            t = (Time.time - startTime);
            if (t >= skillDuration)
            {
                hinge.useMotor = false;
            }
            if (t >= 1.5f)
            {
                weapon.GetComponent<Rigidbody2D>().angularDrag = 10;
            }
            newParticle.transform.position = this.transform.position;
            newParticle.transform.rotation = this.transform.rotation;
            newParticle.transform.Rotate(0, 0, 90 + 180 * (1 - particleDir));
            yield return null;
        }
        slash = false;
        Destroy(newParticle.gameObject, 1.3f);


    }

    void FireA()
    {
        ParticleSystem newParticle = Instantiate(fireA);

        newParticle.transform.position = this.transform.position;
        newParticle.transform.rotation = this.transform.rotation;
        newParticle.transform.Rotate(180 * particleDir, 90, 0);

        newParticle.Play();
        Destroy(newParticle.gameObject, 1.2f);
    }

    public void Skill(GameObject Player)
    {
        StartCoroutine(Slash(Player, gameObject));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == enermyTag)
        {
            collision.gameObject.GetComponent<player_1>().TakeDamage(DmgManage());
            Debug.Log(gameObject.tag + ":" + DmgManage());
            StartColorDown(collision);
            Particle(collision);
            if (slash)
                Particle(collision);
        }

        else if (slash)
            SlashParticle(collision);

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

    void SlashParticle(Collision2D collision)
    {
        ParticleSystem newParticle = Instantiate(hiteff);

        Vector3 hitPos = collision.contacts[0].point;
        newParticle.transform.position = hitPos;
        newParticle.transform.rotation = collision.transform.rotation;

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
        float weaponSpd = Mathf.Abs(joint2D.jointSpeed) / 300; // 300 = maxSpinSpeed
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

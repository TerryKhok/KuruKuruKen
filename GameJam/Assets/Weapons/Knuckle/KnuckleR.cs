using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnuckleR : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] GameObject Weapon;
    [SerializeField] GameObject WeaponL;
    string enermyTag;
    public int damage = 10;
    [SerializeField] int MaxDmg = 10;
    [SerializeField] int MinDmg = 1;
    [SerializeField] float tR = 0f;
    [SerializeField] float tL = 0f;
    [SerializeField] float punchReach = 0f;
    [SerializeField] private float punchDisR = 1.34759f;
    [SerializeField] private float punchDisL = -1.35241f;
    [SerializeField] private float originalDisR;
    [SerializeField] private float originalDisL;

    //particle
    public ParticleSystem blood;
    float colordownTime = 0.7f;
    private GameObject hitPlayer;
    public string weaponLayer = "Weapon";
    public ParticleSystem spark;
    public ParticleSystem shock;
    public ParticleSystem hiteff;
    public bool punch;

    [SerializeField] float cooldownT = 5f;

    //material
    public Material lit;
    public Material sprites;

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
    }

    IEnumerator PunchR(GameObject player, GameObject weapon)
    {
        punch = true;

        ParticleSystem newParticle = Instantiate(shock, this.transform);
        Quaternion lookRotation = Quaternion.LookRotation(this.transform.position - Player.transform.position, Vector3.up);
        newParticle.transform.Rotate(0, 0, lookRotation.z);
        newParticle.Play();
        Destroy(newParticle, 1.2f);

        Debug.Log("punch");
        originalDisR = punchDisR;
        float startTime = Time.time;
        tR = 0f;
        HingeJoint2D[] hingeJoints = Player.GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D joint in hingeJoints)
        {
            if (joint.connectedBody == gameObject.GetComponent<Rigidbody2D>())
            {
                while (tR <= .5f)
                {
                    tR = Time.time - startTime;
                    if (tR <= .25f)
                    {
                        punchDisR += punchReach*Time.deltaTime;
                        joint.connectedAnchor = new Vector3(punchDisR, joint.connectedAnchor.y);

                    }
                    else if (tR > .25f && tR <= .5f)
                    {
                        punchDisR -= punchReach * Time.deltaTime;
                        joint.connectedAnchor = new Vector3(punchDisR, joint.connectedAnchor.y);
                    }
                    yield return null;
                }
                joint.connectedAnchor = new Vector3(originalDisR, joint.connectedAnchor.y);
                punchDisR = originalDisR;
            }
        }
    }

    IEnumerator PunchL(GameObject player, GameObject weapon)
    {
        StartCoroutine(PunchLParticle());

        originalDisL = punchDisL;
        float startTime = Time.time;
        tL = 0f;
        HingeJoint2D[] hingeJoints = Player.GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D joint in hingeJoints)
        {
            if (joint.connectedBody == WeaponL.GetComponent<Rigidbody2D>())
            {
                while (tL <= .75f)
                {
                    tL = (Time.time - startTime);
                    if (tL > .25f && tL <= .5f)
                    {
                        punchDisL -= punchReach * Time.deltaTime;
                        joint.connectedAnchor = new Vector3(punchDisL, joint.connectedAnchor.y);

                    }
                    else if (tL > .5f && tL <= .75f)
                    {
                        punchDisL += punchReach * Time.deltaTime;
                        joint.connectedAnchor = new Vector3(punchDisL, joint.connectedAnchor.y);
                    }
                    yield return null;
                }
                joint.connectedAnchor = new Vector3(originalDisL, joint.connectedAnchor.y);
                punchDisL = originalDisL;
            }
        }
        punch = false;
    }

    IEnumerator PunchLParticle()
    {
        yield return new WaitForSeconds(.25f);
        ParticleSystem newParticle = Instantiate(shock, WeaponL.transform);
        Quaternion lookRotation = Quaternion.LookRotation(WeaponL.transform.position - Player.transform.position, Vector3.up);
        newParticle.transform.Rotate(0, 0, lookRotation.z + 180);
        newParticle.Play();
        Destroy(newParticle, 1.2f);
    }

    public void Skill(GameObject Player)
    {
        StartCoroutine(Cooldown(WeaponL));

        StartCoroutine(PunchR(Player, Weapon));
        StartCoroutine(PunchL(Player, Weapon));
    }

    private IEnumerator Cooldown(GameObject weapon)
    {
        weapon.GetComponent<Renderer>().material = lit;

        yield return new WaitForSeconds(cooldownT);

        weapon.GetComponent<Renderer>().material = sprites;
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
        else if (collision.gameObject != WeaponL)
        {
            
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer(weaponLayer))
            {
                if (punch)
                    hitParticle(hiteff, collision);
                else
                {
                    hitParticle(spark, collision);
                }
            }
        }

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

    void hitParticle(ParticleSystem particle, Collision2D collision)
    {
        ParticleSystem newParticle = Instantiate(particle);
        Vector3 hitPos = collision.contacts[0].point;

        newParticle.transform.position = hitPos;

        newParticle.Play();

        Destroy(newParticle.gameObject, 1f);
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

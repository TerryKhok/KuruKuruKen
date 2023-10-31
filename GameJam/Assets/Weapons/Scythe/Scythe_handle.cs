using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe_handle : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] GameObject Weapon;
    string enermyTag;
    public int damage = 5;
    [SerializeField] int MaxDmg = 5;
    [SerializeField] int MinDmg = 1;
    [SerializeField] float t = 0f;
    [SerializeField] float pullReach = 6f;
    [SerializeField] private float pullDis = -1.394075f;
    [SerializeField] private float originalDis;

    //particle
    public ParticleSystem blood;
    float colordownTime = 0.7f;
    private GameObject hitPlayer;
    public ParticleSystem flashA;
    public ParticleSystem flashB;
    public ParticleSystem hand;
    public ParticleSystem sparkA;

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

    IEnumerator Pull(GameObject player, GameObject weapon)
    {
        handParticle();
        sparkParticle();

        Debug.Log("Pull");
        originalDis = pullDis;
        float startTime = Time.time;
        t = 0f;
        HingeJoint2D[] hingeJoints = Player.GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D joint in hingeJoints)
        {
            if (joint.connectedBody == gameObject.GetComponent<Rigidbody2D>())
            {
                bool particle = true;
                while (t <= .5f)
                {
                    t = (Time.time - startTime);
                    if (t <= .25f)
                    {
                        pullDis += pullReach * Time.deltaTime;
                        joint.connectedAnchor = new Vector3(joint.connectedAnchor.x, pullDis);

                    }
                    else if (t > .25f && t <= .5f)
                    {
                        if (particle)
                        {
                            flashParticle(flashA, -1f, -90f);
                            particle = false;
                        }
                        pullDis -= pullReach * Time.deltaTime;
                        joint.connectedAnchor = new Vector3(joint.connectedAnchor.x, pullDis);
                    }
                    yield return null;
                }
                joint.connectedAnchor = new Vector3(joint.connectedAnchor.x, originalDis);
                pullDis = originalDis;

                flashParticle(flashB, 1f, 90f);

            }
        }
    }

    private void flashParticle(ParticleSystem particle, float pos, float rot)
    {
        ParticleSystem newParticle = Instantiate(particle, this.transform);
        newParticle.transform.localPosition = new Vector3(0, pos, 0);
        newParticle.transform.parent = null;
        Quaternion lookRotation = Quaternion.LookRotation(newParticle.transform.position - Player.transform.position, Vector3.up);
        newParticle.transform.Rotate(lookRotation.x + rot, 0, 0);
        newParticle.Play();
        Destroy(newParticle.gameObject, 1.2f);
    }

    void handParticle()
    {
        ParticleSystem newParticle = Instantiate(hand, this.transform);
        newParticle.transform.localPosition = new Vector3(2f, -1f, 0);
        newParticle.Play();
        Destroy(newParticle, 1.5f);
    }

    void sparkParticle()
    {
        ParticleSystem newParticle = Instantiate(sparkA, Weapon.transform);

        newParticle.Play();
        Destroy(newParticle, 1.5f);
    }

    public void Skill(GameObject Player)
    {
        StartCoroutine(Cooldown(Weapon));

        StartCoroutine(Pull(Player, Weapon));
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

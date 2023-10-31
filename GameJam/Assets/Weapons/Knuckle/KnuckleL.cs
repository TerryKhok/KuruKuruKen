using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnuckleL : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] GameObject WeaponR;
    string enermyTag;
    public int damage = 10;
    [SerializeField] int MaxDmg = 10;
    [SerializeField] int MinDmg = 1;

    //particle
    public ParticleSystem blood;
    float colordownTime = 0.7f;
    private GameObject hitPlayer;
    public string weaponLayer = "Weapon";
    public ParticleSystem spark;
    public ParticleSystem hiteff;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == enermyTag)
        {
            collision.gameObject.GetComponent<player_1>().TakeDamage(DmgManage());
            Debug.Log(gameObject.tag + ":" + DmgManage());
            StartColorDown(collision);
            Particle(collision);
        }
        else if (collision.gameObject != WeaponR)
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer(weaponLayer))
            {
                if (WeaponR.GetComponent<KnuckleR>().punch)
                    hitParticle(hiteff, collision);
                else
                    hitParticle(spark, collision);
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

    void hitParticle(ParticleSystem particle, Collision2D collision)
    {
        ParticleSystem newParticle = Instantiate(particle);
        Vector3 hitPos = collision.contacts[0].point;

        newParticle.transform.position = hitPos;

        newParticle.Play();

        Destroy(newParticle.gameObject, 1f);
    }
}

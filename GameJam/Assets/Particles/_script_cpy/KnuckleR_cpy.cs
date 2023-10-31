/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnuckleR : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Weapon;
    [SerializeField] GameObject WeaponL;
    string enermyTag;
    public int damage = 10;
    [SerializeField] float tR = 0f;
    [SerializeField] float tL = 0f;
    [SerializeField] float punchReach = 0f;
    [SerializeField] private float punchDisR = 1.34759f;
    [SerializeField] private float punchDisL = -1.35241f;
    [SerializeField] private float originalDisR;
    [SerializeField] private float originalDisL;

    //particle
    public string weaponLayer = "Weapon";

    //==============================================================================================
    public ParticleSystem spark;
    public ParticleSystem shock;
    public ParticleSystem hiteff;
    public bool punch;
    //==============================================================================================


    private void Start()
    {
        if (Player.gameObject.tag == "Player1")
        {
            enermyTag = "Player2";
            Debug.Log("ds");
        }
        else if (Player.gameObject.tag == "Player2")
        {
            enermyTag = "Player1";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    IEnumerator PunchR(GameObject player, GameObject weapon)
    {
        Debug.Log("PunchR");
        // HingeJoint2D hinge = player.GetComponent<HingeJoint2D>();

        //==================================================================================================================
        punch = true;

        ParticleSystem newParticle = Instantiate(shock,this.transform);
        Quaternion lookRotation = Quaternion.LookRotation(this.transform.position - Player.transform.position, Vector3.up);
        newParticle.transform.Rotate(0, 0, lookRotation.z);
        newParticle.Play();
        Destroy(newParticle, 1.2f);

        //==================================================================================================================

        originalDisR = punchDisR;
        tR = 0f;
        HingeJoint2D[] hingeJoints = Player.GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D joint in hingeJoints)
        {
            if (joint.connectedBody == gameObject.GetComponent<Rigidbody2D>())
            {
                while (tR <= .5f)
                {
                    tR += Time.deltaTime;
                    if (tR <= .25f)
                    {
                        punchDisR += punchReach;
                        joint.connectedAnchor = new Vector3(punchDisR, joint.connectedAnchor.y);

                    }
                    else if (tR > .25f && tR <= .5f)
                    {
                        punchDisR -= punchReach;
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
        Debug.Log("PunchL");

        //==================================================================================================================
        StartCoroutine(PunchLParticle());
        //==================================================================================================================

        originalDisL = punchDisL;
        tL = 0f;
        HingeJoint2D[] hingeJoints = Player.GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D joint in hingeJoints)
        {
            if (joint.connectedBody == WeaponL.GetComponent<Rigidbody2D>())
            {
                while (tL <= .75f)
                {
                    tL += Time.deltaTime;
                    if (tL > .25f && tL <= .5f)
                    {
                        punchDisL -= punchReach;
                        joint.connectedAnchor = new Vector3(punchDisL, joint.connectedAnchor.y);

                    }
                    else if (tL > .5f && tL <= .75f)
                    {
                        punchDisL += punchReach;
                        joint.connectedAnchor = new Vector3(punchDisL, joint.connectedAnchor.y);
                    }
                    yield return null;
                }
                joint.connectedAnchor = new Vector3(originalDisL, joint.connectedAnchor.y);
                punchDisL = originalDisL;
            }
        }

        //==================================================================================================================
        punch = false;
        //==================================================================================================================
    }


    //------------------------------------------------------------------------------------------------------------------
    IEnumerator PunchLParticle()
    {
        yield return new WaitForSeconds(.25f);
        ParticleSystem newParticle = Instantiate(shock, WeaponL.transform);
        Quaternion lookRotation = Quaternion.LookRotation(WeaponL.transform.position - Player.transform.position, Vector3.up);
        newParticle.transform.Rotate(0, 0, lookRotation.z + 180);
        newParticle.Play();
        Destroy(newParticle, 1.2f);
    }
    //-------------------------------------------------------------------------------------------------------------------

    public void Skill(GameObject Player)
    {
        StartCoroutine(PunchR(Player, Weapon));
        StartCoroutine(PunchL(Player, Weapon));
    }



    //==================================================================================================================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == enermyTag)
        {
            collision.gameObject.GetComponent<player_1>().TakeDamage(damage);
        }
        else if (collision.gameObject != WeaponL)
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer(weaponLayer))
            {
                if (punch)
                    hitParticle(hiteff,collision);
                else
                    hitParticle(spark,collision);
            }
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
    //==================================================================================================================
}
*/
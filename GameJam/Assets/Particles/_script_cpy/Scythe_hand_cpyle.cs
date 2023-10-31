/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe_handle : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Weapon;
    string enermyTag;
    public int damage = 5;
    [SerializeField] float t = 0f;
    [SerializeField] float pullReach = 0f;
    [SerializeField] private float pullDis = -1.394075f;
    [SerializeField] private float originalDis;


    //==============================================================================================
    //paritlce
    public ParticleSystem flashA;
    public ParticleSystem flashB;
    public ParticleSystem hand;
    public ParticleSystem sparkA;
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

    IEnumerator Pull(GameObject player, GameObject weapon)
    {
        //==============================================================================================
        handParticle();
        sparkParticle();
        //==============================================================================================
        Debug.Log("Pull");
        originalDis = pullDis;
        t = 0f;
        HingeJoint2D[] hingeJoints = Player.GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D joint in hingeJoints)
        {
            if (joint.connectedBody == gameObject.GetComponent<Rigidbody2D>())
            {
                //==============================================================================================
                bool particle = true;
                //==============================================================================================
                while (t <= .5f)
                {
                    t += Time.deltaTime;
                    if (t <= .25f)
                    {
                        pullDis += pullReach;
                        joint.connectedAnchor = new Vector3(joint.connectedAnchor.x, pullDis);

                    }

                    else if (t > .25f && t <= .5f)
                    {
                        //==============================================================================================
                        if (particle)
                        {
                            flashParticle(flashA,-1f,-90f);
                            particle = false;
                        }
                        //==============================================================================================
                        pullDis -= pullReach;
                        joint.connectedAnchor = new Vector3(joint.connectedAnchor.x, pullDis);
                    }
                    yield return null;

                    
                }
                joint.connectedAnchor = new Vector3(joint.connectedAnchor.x, originalDis);
                pullDis = originalDis;

                //==============================================================================================
                flashParticle(flashB,1f,90f);
                //==============================================================================================
            }
        }
    }

    //==============================================================================================================================
    private void flashParticle(ParticleSystem particle,float pos,float rot)
    {
        ParticleSystem newParticle = Instantiate(particle, this.transform);
        newParticle.transform.localPosition = new Vector3(0, pos, 0);
        newParticle.transform.parent = null;
        Quaternion lookRotation = Quaternion.LookRotation(newParticle.transform.position - Player.transform.position, Vector3.up);
        newParticle.transform.Rotate(lookRotation.x + rot, 0, 0);
        newParticle.Play();
        Destroy(newParticle.gameObject, 1.2f);
    }
    //==============================================================================================================================

    //==============================================================================================
    void handParticle()
    {
        ParticleSystem newParticle = Instantiate(hand, this.transform);
        newParticle.transform.localPosition = new Vector3(2f, -1f, 0);
        newParticle.Play();
        Destroy(newParticle, 1.5f);
    }
    //==============================================================================================

    //==============================================================================================
    void sparkParticle()
    {
        ParticleSystem newParticle = Instantiate(sparkA, Weapon.transform);
       
        newParticle.Play();
        Destroy(newParticle, 1.5f);
    }
    //==============================================================================================

    public void Skill(GameObject Player)
    {
        StartCoroutine(Pull(Player, Weapon));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == enermyTag)
        {
            collision.gameObject.GetComponent<player_1>().TakeDamage(damage);
        }

    }
}
*/
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnuckleL : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject WeaponR;
    string enermyTag;
    public int damage = 10;

    //particle 
    public string weaponLayer = "Weapon";
    //=================================================================================================
    public ParticleSystem spark;
    public ParticleSystem hiteff;
    //=================================================================================================

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

    //==================================================================================================
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == enermyTag)
        {
            collision.gameObject.GetComponent<player_1>().TakeDamage(damage);
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

    void hitParticle(ParticleSystem particle, Collision2D collision)
    {
        ParticleSystem newParticle = Instantiate(particle);
        Vector3 hitPos = collision.contacts[0].point;

        newParticle.transform.position = hitPos;

        newParticle.Play();

        Destroy(newParticle.gameObject, 1f);
    }
    //==================================================================================================
}
*/
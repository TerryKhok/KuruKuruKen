/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Weapon;
    string enermyTag;
    public int damage = 10;
    [SerializeField] float t = 0f;
    [SerializeField] float motorSpeed = 0f;
    [SerializeField] float skillDuration = 0f;


    //=========================================================================================================
    //particle
    public ParticleSystem impactA;
    public ParticleSystem impactB;
    //=========================================================================================================

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
    void Update()
    {

    }

    IEnumerator Block(GameObject player, GameObject weapon)
    {
        //=========================================================================================================
        Particle(impactA);
        yield return new WaitForSeconds(0.8f);
        Particle(impactB);
        yield return new WaitForSeconds(0.1f);
        //=========================================================================================================

        Debug.Log("Block");
        GameObject shield = Instantiate(weapon,new Vector3(0,1.15f,0),Quaternion.Euler(0, 0, 90),Player.transform);
        shield.transform.localPosition=new Vector3(0,0.8f,0);

    }

    //=========================================================================================================
    private void Particle(ParticleSystem particle)
    {
        ParticleSystem newParticle = Instantiate(particle,Player.transform);
        particle.transform.localPosition = new Vector3(0, 0.8f, 0);
        newParticle.Play();
        Destroy(newParticle.gameObject,1f);
    }
    //=========================================================================================================

    public void Skill(GameObject Player)
    {
        // StartCoroutine(Block(Player, Weapon));
        StartCoroutine(Block(Player, Weapon));
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
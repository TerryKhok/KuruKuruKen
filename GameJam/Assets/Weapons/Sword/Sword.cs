using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] GameObject Weapon;
    string enermyTag;
    public int damage = 10;
    [SerializeField] int MaxDmg = 10;
    [SerializeField] int MinDmg = 1;

    //particle
    public ParticleSystem blood;
    float colordownTime = 0.7f;
    private GameObject hitPlayer;
    public ParticleSystem impactA;
    public ParticleSystem impactB;

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

    IEnumerator Block(GameObject player, GameObject weapon)
    {
        Debug.Log("block");
        ImpactParticle(impactA);
        yield return new WaitForSeconds(0.8f);
        ImpactParticle(impactB);
        yield return new WaitForSeconds(0.1f);

       
        GameObject shield = Instantiate(weapon, new Vector3(0, 1.15f, 0), Quaternion.Euler(0, 0, 90), player.transform);
        shield.GetComponent<Shield>().Player = Player;
        shield.GetComponent<Shield>().Weapon = gameObject;
        shield.transform.localPosition = new Vector3(0, 0.8f, 0);
    }



    public void Skill(GameObject Player)
    {
         StartCoroutine(Block(Player, Weapon));
        Debug.Log("Block start");
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
    private void ImpactParticle(ParticleSystem particle)
    {
        ParticleSystem newParticle = Instantiate(particle, Player.transform);
        particle.transform.localPosition = new Vector3(0, 0.8f, 0);
        newParticle.Play();
        Destroy(newParticle.gameObject, 1f);
    }

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

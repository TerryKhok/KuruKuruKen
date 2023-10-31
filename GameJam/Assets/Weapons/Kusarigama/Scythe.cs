using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    public GameObject Player;
    string enermyTag;
    public int MaxDmg = 18;
    [SerializeField] int MinDmg = 4;
    public int damage = 9;
 

    //particle
    public ParticleSystem blood;
    float colordownTime = 0.7f;
    private GameObject hitPlayer;

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

    // Update is called once per frame
    void Update()
    {
       
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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 velocity = rb.velocity;
        float speed = velocity.magnitude;
        float weaponSpd = Mathf.Abs(speed) / 69; // 69 = maxSpeed
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

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dagger : MonoBehaviour
{
    [SerializeField] GameObject Player;
    string enermyTag;
    [SerializeField] int MaxDmg = 10;
    [SerializeField] int MinDmg = 1;
    HingeJoint2D joint2D;
    public int damage = 5;
    private float dashDistance = 33f;
    private float dashDuration = 0.8f;
    
    

    //particle
    public ParticleSystem blood;

    //=====================================================================================================
    public ParticleSystem dashSteamA;
    public ParticleSystem dashSteamB;
    //=====================================================================================================

    float colordownTime = 0.7f;
    private GameObject hitPlayer;

    private void Start()
    {   
        if(Player.gameObject.tag == "Player1")
        {
            enermyTag = "Player2";
            
        }
        else if(Player.gameObject.tag == "Player2")
        {
            enermyTag = "Player1";
        }
        joint2D = Player.GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(joint2D.jointSpeed);
        }
    }
    IEnumerator Dash(GameObject player, float times)
    {
        player.GetComponent<Movement>().dash = true;

        Vector2 dashDirection = player.GetComponent<Movement>().direction.normalized;
        Vector2 targetVelocity = dashDirection * (dashDistance / dashDuration);
        player.GetComponent<Movement>().rb2.velocity = targetVelocity;

        //=====================================================================================================
        ParticleSystem newParticle = Instantiate(dashSteamA);
        newParticle.transform.position = Player.transform.position;
        newParticle.Play();
        //=====================================================================================================


        float startTime = Time.time;
        float endTime = startTime + dashDuration;
        float t = 0f;

        while (Time.time < endTime)
        {
            t = (Time.time - startTime) / dashDuration;
            player.GetComponent<Movement>().rb2.velocity = Vector2.Lerp(targetVelocity, Vector2.zero, t);
            yield return null;

            //=====================================================================================================
            newParticle.transform.position = Player.transform.position;
            //=====================================================================================================

        }

        player.GetComponent<Movement>().rb2.velocity = Vector2.zero;
        player.GetComponent<Movement>().dash = false;

        //=====================================================================================================
        Destroy(newParticle.gameObject);
        SteamParticle2();
        //=====================================================================================================

    }

    //=====================================================================================================
    void SteamParticle2()
    {
        ParticleSystem newParticle = Instantiate(dashSteamB);
        newParticle.transform.position = Player.transform.position;
        newParticle.transform.rotation = Player.transform.rotation;
        newParticle.Play();
        Destroy(newParticle.gameObject, 1.5f);
    }
    //=====================================================================================================

    public void Skill(GameObject Player)
    {
        StartCoroutine(Dash(Player,Time.deltaTime));
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
*/
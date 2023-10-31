/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kusarigama : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public int damage = 10;

    //scythe
    public GameObject scythe;
    Rigidbody2D Srb;
    [SerializeField] Rigidbody2D[] crb;
    [SerializeField] float targetDis = 2f;
    [SerializeField] float spd = 60f;

    //================================================================================================
    //particle
    public ParticleSystem thunderA;
    public ParticleSystem thunderB;
    public ParticleSystem flare;
    //================================================================================================


    private void Start()
    {
       
        Srb = scythe.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Callback(GameObject player)
    {
        float distance = Vector2.Distance(scythe.transform.position, player.transform.position);
        while (distance > targetDis)
        {
            Srb.AddForce((Player.transform.position - scythe.transform.position).normalized * spd);
            
           
            yield return null;
        }

        


    }

    void PullBack()
    {
        scythe.GetComponent<Scythe>().MaxDmg = 20;
        scythe.GetComponent<Scythe>().damage = 12;
        Srb.AddForce((Player.transform.position - scythe.transform.position).normalized * spd, ForceMode2D.Impulse);
        scythe.GetComponent<Scythe>().MaxDmg = 18;
        scythe.GetComponent<Scythe>().damage = 9;
    }
    public void Skill(GameObject Player)
    {
        //================================================================================================
        Particle_flare();
        //================================================================================================

        PullBack();
        //===============================================================================================
        StartCoroutine(Particle_thunderA());
        //===============================================================================================

    }


    //================================================================================================
    private void Particle_flare()
    {
        ParticleSystem newParticle = Instantiate(flare);
        newParticle.transform.position = scythe.transform.position;
        newParticle.transform.rotation = scythe.transform.rotation;
        newParticle.Play();
        Destroy(newParticle.gameObject, 0.9f);
    }

    IEnumerator Particle_thunderA()
    {
        yield return new WaitForSeconds(0.01f);
        ParticleSystem newParticle = Instantiate(thunderA, scythe.transform);
        newParticle.transform.position = scythe.transform.position;
        newParticle.transform.rotation = scythe.transform.rotation;
        newParticle.Play();
        Destroy(newParticle.gameObject, 2f);
        yield return new WaitForSeconds(2f);
        Particle_thunderB();
    }

    void Particle_thunderB()
    {
        ParticleSystem newParticle = Instantiate(thunderB);
        newParticle.transform.position = scythe.transform.position;
        newParticle.Play();
        Destroy(newParticle.gameObject, 1.2f);
    }
    //================================================================================================


}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject Player;
    public GameObject Weapon;
    [SerializeField] float t = 0f;
    [SerializeField] float motorSpeed = 0f;
    [SerializeField] float skillDuration = 0f;

    //particle
    public ParticleSystem impactB;
    public ParticleSystem impactC;
    public ParticleSystem steamB;
    HingeJoint2D hinge;

    private void Start()
    {
        //Player = GameObject.Find("player2");
       // Weapon = GameObject.FindWithTag("Sword");
        Rigidbody2D rb2 = GetComponent<Rigidbody2D>();
        hinge = Player.AddComponent<HingeJoint2D>();
        JointMotor2D motor = hinge.motor;
        hinge.connectedBody = rb2;

        Physics2D.IgnoreCollision(Weapon.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        HingeJoint2D[] hingeJoints = Player.GetComponents<HingeJoint2D>();
        foreach (HingeJoint2D joint in hingeJoints)
        {
            if (joint.connectedBody == gameObject.GetComponent<Rigidbody2D>())
            {
                motor.motorSpeed = motorSpeed;
                hinge.useMotor = true;
                hinge.motor = motor;
            }
        }

        StartCoroutine(destroyAfterDuration());

    }

    IEnumerator destroyAfterDuration()
    {
        ParticleSystem newParticle_steamB = Instantiate(steamB);
        newParticle_steamB.transform.position = this.transform.position;
        newParticle_steamB.Play();

        float startTime = Time.time;
        t = 0f;
        while (t <= skillDuration)
        {
            t = (Time.time - startTime);
            if (t >= skillDuration)
            {
                Destroy(newParticle_steamB.gameObject);
                Vector3 Pos = this.transform.position;
                Destroy(gameObject);
                Destroy(hinge);
                ParticleSystem newParticle = Instantiate(impactC);
                newParticle.transform.position = Pos;
                newParticle.Play();
                Destroy(newParticle.gameObject, 1f);
            }
            yield return null;
            newParticle_steamB.transform.position = this.transform.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != Weapon.gameObject)
        {
            if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Weapon"))
            {
                Particle(impactB, collision);
            }
        }
    }

    private void Particle(ParticleSystem particle, Collision2D collision)
    {
        ParticleSystem newParticle = Instantiate(particle);
        Vector3 hitPos = collision.contacts[0].point;
        newParticle.transform.position = hitPos;
        newParticle.Play();
        Destroy(newParticle.gameObject, 1f);
    }
}

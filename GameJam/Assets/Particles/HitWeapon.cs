using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWeapon : MonoBehaviour
{
    public ParticleSystem spark;

    //����̕����tag
    public string weaponLayer = "Weapon";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer(weaponLayer))
        {
            playWeaponSFX();
            ParticleSystem newParticle = Instantiate(spark);
            Vector3 hitPos = collision.contacts[0].point;
            newParticle.transform.position = hitPos;
            newParticle.Play();
            Destroy(newParticle.gameObject, 0.7f);
        } 
    }

    private void playWeaponSFX()
    {
        int rng = Random.Range(1, 4);
        switch (rng)
        {
            case 1:
                FindObjectOfType<AudioManager>().Play("Attack01");
                break;
            case 2:
                FindObjectOfType<AudioManager>().Play("Attack02");
                break;
            case 3:
                FindObjectOfType<AudioManager>().Play("Attack03");
                break;
            default: return;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_1 : MonoBehaviour
{
    public int maxHealth = 100;
    int oldHealth;
    public int dmgTaken = 0;
    public int currentHealth;
    public bool HoldingWeapon = false;

    //prticle
    public ParticleSystem explosion;

    public HealthBarScript healthBar;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        PlayDamageSFX();
        oldHealth = currentHealth;
        currentHealth -= damage;
        dmgTaken = oldHealth - currentHealth;
        if (currentHealth <= 0)
        {
            Vector3 Pos = this.transform.position;
            Destroy(gameObject);

            ParticleSystem newParticle = Instantiate(explosion);
            newParticle.transform.position = Pos;
            newParticle.Play();
            Destroy(newParticle.gameObject, 4f);
			FindObjectOfType<AudioManager>().Play("Death");
        }

        healthBar.SetHealth(currentHealth);
    }

    private void PlayDamageSFX()
    {
        int rng = Random.Range(1, 3);
        if (rng == 1)
            FindObjectOfType<AudioManager>().Play("TakeDamage01");
        else
            FindObjectOfType<AudioManager>().Play("TakeDamage02");
    }

}
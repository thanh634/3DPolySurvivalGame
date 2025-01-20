using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public string name;
    public bool playerInRange;
    public bool isDead;

    private Animator aniamtor;

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;


    [Header("Sounds")]
    [SerializeField] AudioSource soundChannel;
    [SerializeField] AudioClip hitAndScream;
    [SerializeField] AudioClip hitAndDie;

    [SerializeField] ParticleSystem bloodSplash;
    [SerializeField] GameObject bloodPuddle;

    private void Start()
    {
        currentHealth = maxHealth;
        aniamtor = GetComponent<Animator>();
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        if(!isDead)
        {
            currentHealth -= damage;

            bloodSplash.Play();

            if (currentHealth <= 0)
            {
                soundChannel.PlayOneShot(hitAndDie);
                aniamtor.SetTrigger("isDead");

                if(GetComponent<AI_Movement>())
                    GetComponent<AI_Movement>().enabled = false;

                bloodPuddle.SetActive(true);
                isDead = true;
            }
            else
            {
                soundChannel.PlayOneShot(hitAndScream);
            }
        }
    }
}

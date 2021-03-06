using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMovement : MonoBehaviour
{

    public float followSpeed;
    public float lineOfSite;
    public float shootingRange;
    public float meleeRange;
    public float chaseRange;
    public float fireRate = 1;
    private float nextFireTime;
    public Animator animator;
    
    public bool canAttack;
    public bool colliding;
    public bool damaging;

    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    public Rigidbody2D rb;

    public AudioSource audioSource;

    public AudioClip meleeAttackSound;
    public AudioClip hurtSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        canAttack = true;
        colliding = false;
        damaging = false;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
        Gizmos.DrawWireSphere(transform.position, meleeRange);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }


    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(0.4f);
        damaging = true;

        yield return new WaitForSeconds(0.45f);
        damaging = false;

        yield return new WaitForSeconds(2);
        canAttack = true;
    }

    public void hurtSFX()
    {
        audioSource.Stop();
        audioSource.clip = hurtSound;
        audioSource.Play();
    }

    public void meleeAttackSFX()
    {
        audioSource.Stop();
        audioSource.clip =meleeAttackSound;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

        string clipName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange)
        {
            rb.isKinematic = false;
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, followSpeed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && distanceFromPlayer >= chaseRange && nextFireTime < Time.time && clipName != "bossHurts")
        {
            rb.isKinematic = true;
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        else if (distanceFromPlayer < chaseRange && distanceFromPlayer > meleeRange)
        {
            rb.isKinematic = false;
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, followSpeed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= meleeRange && canAttack == true)
        {
            animator.SetTrigger("bossAttack");
            canAttack = false;
            StartCoroutine(WaitForSec());
        }

    }
}

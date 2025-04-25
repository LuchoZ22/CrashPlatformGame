using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CrashControler : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public int maxJumpCount;
    public bool isRightOriented;


    public float attackRange;
    public LayerMask destroyableLayer;
    public Transform attackPoint;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private int jumpCount;
  
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        jumpCount = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        XMovement();
        YMovement();
        CheckAttack();
    }


    private void XMovement()
    {
        
        float movX = Input.GetAxis("Horizontal");
        animator.SetFloat("move", Mathf.Abs(movX));

        if (movX != 0) {
            isRightOriented = movX > 0;
            spriteRenderer.flipX = !isRightOriented;
        }
       

        transform.Translate(new Vector3(movX * speed * Time.deltaTime, 0, 0));
    }


    private void YMovement()
    {
        
        if ((Input.GetKeyDown("up") || Input.GetKeyDown(KeyCode.W)) && jumpCount < maxJumpCount)
        {
            jumpCount++;
            rb.AddForce(new Vector2(0, jumpForce));
            animator.SetBool("jumping", true);
        }
        animator.SetBool("jumping", rb.velocity.y > 0.1f);
        animator.SetBool("falling", rb.velocity.y < -0.1f);

    }

    private void CheckAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(CheckAttack_());
        }
    }
    private IEnumerator CheckAttack_()
    {
        animator.SetTrigger("attack");

        // Wait a short time to sync with the animation (adjust as needed)
        yield return new WaitForSeconds(0.35f);

        Vector2 direction = isRightOriented ? Vector2.right : Vector2.left;
        Debug.Log(direction);

        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, direction, attackRange, destroyableLayer);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);

            // Try to play a destruction animation on the object first
            Animator hitAnimator = hit.collider.GetComponent<Animator>();
            if (hitAnimator != null)
            {
                hitAnimator.SetTrigger("destroy"); // Make sure your destroyable objects have this trigger
            }

            // Wait a bit before destroying to let the animation play
            yield return new WaitForSeconds(0.2f);
           
        }

        Debug.DrawRay(attackPoint.position, direction * attackRange, Color.cyan, 1f);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO - CHECK IF COLIDES WITH FLOOR
        if(collision.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
        else if(collision.gameObject.tag == "JumpBox")
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector3 contactNormal = contact.normal;
                if (Vector3.Dot(contactNormal, Vector3.up) > 0.5f) // Mostly from below
                {
                    Debug.Log("Player landed on top of the JumpPad");
                    rb.AddForce(new Vector2(0, jumpForce*1.5f));
                    // Apply impulse here
                }
            }
        }
       
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triger by " + collision.gameObject.name);
        
        if(collision.gameObject.tag == "WumpaFruit")
        {
            GameObject wumpa  = collision.gameObject.transform.GetChild(0).gameObject;
            wumpa.GetComponent<Animator>().SetTrigger("dissapear");
            AudioSource audioSource = wumpa.GetComponent<AudioSource>();
            if(audioSource != null)
            {
                audioSource.Play();
            }
           
            Destroy(collision.gameObject, 0.5f);
            ScoreManager.scoreManager.RaiseScoreWumpa(1);
        }

        else if (collision.gameObject.tag == "CrashToken")
        {
            GameObject crashToken = collision.gameObject;
            crashToken.GetComponent<Animator>().SetTrigger("dissapear");
            AudioSource audioSource = crashToken.GetComponent<AudioSource>();
            if (audioSource != null) 
            {
                Debug.Log("Audio PLayed");
                audioSource.Play();
            }
            Destroy(collision.gameObject, 0.5f);
            ScoreManager.scoreManager.RaiseScoreCrashToken(1);
        }
        
    }


}

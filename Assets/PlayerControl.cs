using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public float thrust;
    public Transform firePoint;
    public GameObject bulletPrefab;


    private Vector2 moveDir;
    private Animator anim;
    private Rigidbody2D RB;
    private bool canWalk;
    private bool canJump;


    // Start is called before the first frame update
    void Start()
    {
        moveDir = Vector2.zero;
        anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        canWalk = true;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {

        Walk();
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
      
            Jump();
            canJump = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    void Walk()
    {
        if(canWalk == true)
        {
            float moveX = Input.GetAxis("Horizontal");
            moveDir = new Vector2(moveX, 0);
            moveDir.Normalize();
            anim.SetFloat("Speed", Mathf.Abs(moveX));

            // walk
            transform.position += new Vector3(moveDir.x, moveDir.y, 0.0f) * moveSpeed * Time.deltaTime;
            if (moveX > 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (moveX < 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    void Jump()
    { 
        RB.AddForce(transform.up * thrust);
        anim.SetBool("Jump", true);
        StartCoroutine(Wait());
        
    }

    void Shoot()
    {
        anim.SetBool("Attack", true);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        anim.SetBool("Jump", false);
        anim.SetBool("Attack", false);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject);
        //DIE
        if (col.gameObject.tag == "Spike")
        {
            canJump = false;
            canWalk = false;
            anim.SetBool("Dead", true);
        }
        else if(col.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }

}

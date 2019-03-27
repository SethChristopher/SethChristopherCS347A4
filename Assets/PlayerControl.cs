using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float thrust = 75.0f;

    private Vector2 moveDir;
    private Animator anim;
    private Rigidbody2D RB;

    // Start is called before the first frame update
    void Start()
    {
        moveDir = Vector2.zero;
        anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Walk();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetMouseButton(0))
        {
            anim.SetBool("Attack", true);
            StartCoroutine(Wait());
        }

    }

    void Walk()
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

    void Jump()
    { 
        RB.AddForce(transform.up * thrust);
        anim.SetBool("Jump", true);
        StartCoroutine(Wait());
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.5f);
        anim.SetBool("Jump", false);
        anim.SetBool("Attack", false);
    }
    void OnCollisionEnter2d(Collision2D col)
    {
        Debug.Log("Spikase Collide");
        if (col.gameObject.name == "spikes")
        {
            anim.SetBool("Dead", true);
        }
    }

}

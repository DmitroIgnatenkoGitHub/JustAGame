using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    ///[SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 7f;
    private bool isGrounded = false;

    //private bool isAttacking;
    //private bool isRecharged;

    public Transform attackPos;
    public float attackRange;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        //isRecharged = true;
    }
    private void Run()
    {
        if (isGrounded) State = States.Run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Update()
    {
        if (isGrounded) State = States.Idle;
        if (Input.GetButton("Horizontal"))
            Run();
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();
        if (Input.GetButton("Fire1"))
            Attack();
    }
    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 0.5;

        if (!isGrounded) State = States.Jump;
    }
    private void Attack()
    {
        if (isGrounded) State = States.Attack;
    }
}

public enum States
{
    Idle,
    Run,
    Jump,
    Attack
}
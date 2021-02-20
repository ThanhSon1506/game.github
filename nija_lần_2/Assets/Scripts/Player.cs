using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;
   
    public Rigidbody2D MyRigidbody { get; set; }
   
    public bool Slide { get; set; }
    public bool Jump { get; set; }
    public bool OnGround { get; set; }
    // Start is called before the first frame update
    public override bool IsDead
    {
        get { return health <= 0; }
    }
    public override void Start()
    {
        Debug.Log("PlayerStart");
        base.Start();
        MyRigidbody = GetComponent<Rigidbody2D>();
      
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        OnGround = IsGrounded();
        Debug.Log(horizontal);
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleLayers();

    }
    private void HandleMovement(float horizontal)
    {
        if (MyRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }
        if (!Attack && !Slide && (OnGround || airControl))
        {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }
        if (Jump && MyRigidbody.velocity.y == 0)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            myAnimator.SetTrigger("attack");
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            myAnimator.SetTrigger("slide");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnimator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            myAnimator.SetTrigger("throw");
        }
      
    }
    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            changeDirection();
        }
    }


    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {

                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void HandleLayers()
    {
        if (!OnGround)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    public override void ThrowKnife(int value)
    {
        if(!OnGround&&value==1||OnGround&&value==0)
        {
            base.ThrowKnife(value); 
        }
    }
    public override IEnumerator TakeDamage()
    {
        yield return null;
    }
}


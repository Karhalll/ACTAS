using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 15f;
    [SerializeField] float jumpSpeed = 10f;

    Rigidbody2D myRigidBody = null;
    Collider2D myCollider = null;
    Animator myAnimator = null;

    bool isFliped = false;

    private void Awake() 
    {
        myRigidBody =  GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Run();
        HandleJump();
        FlipSprite();
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.layer == 8)
        myAnimator.SetBool("Jumping", false);
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        myAnimator.SetBool("Jumping", true);
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        { 
            myAnimator.SetBool("Running", playerHorizontalSpeed);
            myAnimator.SetBool("Idleing", myRigidBody.velocity.x == 0); 
        }
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetTrigger("Jump");
        }
    }

    private void FlipSprite()
        {
            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            if (playerHasHorizontalSpeed)
            {
                Debug.Log("Flip");
                transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
                Debug.Log(Mathf.Sign(myRigidBody.velocity.x));
            }
        }

    //Unity Animation Event
    public void Jump()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }
}

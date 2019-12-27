using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 15f;
    [SerializeField] float jumpSpeed = 10f;

    Rigidbody2D myRigidBody = null;
    Collider2D myCollider = null;
    Animator myAnimator = null;

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

        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        { 
            myAnimator.SetBool("Running Right", myRigidBody.velocity.x > 0);
            myAnimator.SetBool("Running Left", myRigidBody.velocity.x < 0);
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

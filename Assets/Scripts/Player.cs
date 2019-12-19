using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 15f;
    [SerializeField] float jumpSpeed = 10f;

    Rigidbody2D myRigidBody = null;
    Animator myAnimator = null;

    private void Awake() 
    {
        myRigidBody =  GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Run();
        Jump();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        // bool playerHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        // myAnimator.SetBool("Running", playerHorizontalSpeed);
        
        myAnimator.SetBool("Running Right", myRigidBody.velocity.x > 0);
        myAnimator.SetBool("Running Left", myRigidBody.velocity.x < 0);
        myAnimator.SetBool("Idleing", myRigidBody.velocity.x == 0);
    }

    private void Jump()
    {
        //if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }
}

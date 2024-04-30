using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 direction;
    float hAxis;
    float runSpeed = 6f; // Kecepatan berlari
    float currentSpeed; // Kecepatan saat ini
    float multipleJump = 1.5f;

    //wall jump property
    private bool isWallSliding; //cek apakah wall sliding bekerja atau tidak 
    private float wallSlidingSpeed = 2f; //kecepatan wall sliding
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(3f, 5f);
    private bool isFacingRight = true;

    [SeriliazeField] float climbSpeed = 3f;
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float jumppower = 5f;//power untuk melompat
    [SerializeField] bool onGround = false; //mekanik deteksi ground
    [SerializeField] bool doubleJumpReady = false; //mekanik double jump
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    

    Rigidbody2D rb;
    Collider2D groundCollider; // Collider untuk deteksi ground


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        Jump();
        
        WallSlide();
        WallJump();

        if(!isWallJumping)
        {
            Facing();
        }
    }

    void movement()
    {

        // Mendapatkan input dari sumbu horizontal dan vertikal (WASD atau panah)
        hAxis = Input.GetAxis("Horizontal");
        direction = new Vector2(hAxis, 0);

        

        //untuk berlari
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //jika menekan shift
            currentSpeed = runSpeed;

        }
        else
        {
            currentSpeed = walkSpeed;
        }

        // Menggerakkan pemain menggunakan vektor
        transform.Translate(direction * Time.deltaTime * currentSpeed);



    }

    //fungsi wall slide
   
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool isWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if(isWalled() && !isGrounded() && hAxis != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJump));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x += -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJump), wallJumpingDuration);
        }
    }

    private void StopWallJump()
    {
        isWallJumping = false;

    }

   void Jump()
    {
        //if spacebar pressed then apply velocity to yaxis
        if (Input.GetKeyDown(KeyCode.Space) && (onGround || doubleJumpReady))
        {
           
            if (onGround) //jika pemain berada di tanah maka:
            {
                rb.velocity = new Vector2(rb.velocity.x, jumppower);
                doubleJumpReady = true; //set double jump ready jika tersedia
            }

            else if (doubleJumpReady) //ketika double jump tersedia
            {
                rb.velocity = new Vector2(rb.velocity.x, jumppower * multipleJump);
                doubleJumpReady = false; //gunakan double jump dan atur kembali ke false
            }

            

        }
    }


    void Facing()
    {
        ///if player is moving left scale == -1
        if (hAxis < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingRight = true;

        }

        ///If player is moving right scale == 1
        if (hAxis > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isFacingRight = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //If trigger enter object with tag "ground" then allow jump then on ground = true
        if (col.tag == "Ground")
        {
            onGround = true;
            groundCollider = col; // Set collider ground saat ini
            doubleJumpReady = true; // Set double jump tersedia saat pemain menyentuh tanah
        }
          if (col.tag == "wall")
        {
            wall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //If trigger exit object with tag "ground" then allow jump then on ground = false
        if (col == groundCollider )
        {
            onGround = false;

        }
        if col.tag == "wall"
            {
            wall = false;
        }


    }
}

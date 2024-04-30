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

   


    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float jumppower = 5f;//power untuk melompat
    [SerializeField] bool onGround = false; //mekanik deteksi ground
    [SerializeField] bool doubleJumpReady = false; //mekanik double jump
   

    

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
        Facing();
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
            /* isFacingRight = true; */

        }

        ///If player is moving right scale == 1
        if (hAxis > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            /* isFacingRight = false; */

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //If trigger enter object with tag "ground" then allow jump then on ground = true
        if (col.tag == "Ground")
        {
            onGround = true;
            doubleJumpReady = true; // Set double jump tersedia saat pemain menyentuh tanah
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //If trigger exit object with tag "ground" then allow jump then on ground = false
        if (col.tag == "Ground" )
        {
            onGround = false;

        }

    }
}

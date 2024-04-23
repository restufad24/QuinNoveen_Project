using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    float hAxis;
    Vector2 direction;
    float walkSpeed = 3f; //speed player
    Rigidbody rb; //akses rigidbody
    float runSpeed = 10f; // Kecepatan berlari
    float currentSpeed; // Kecepatan saat ini
    float jumpPower = 4; //untuk loncat
    bool Ground = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        // Mendapatkan input dari sumbu horizontal dan vertikal (WASD atau panah)
        hAxis = Input.GetAxis("Horizontal");
        direction = new Vector2(hAxis, 0f);

        // Menggerakkan pemain menggunakan vektor
        transform.Translate(direction .normalized * currentSpeed * Time.deltaTime);


        //untuk berlari
        if(Input.GetKey(KeyCode.LeftShift))
        {
            //jika menekan shift
            currentSpeed = runSpeed;

        }
        else
        {
            currentSpeed = walkSpeed;
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPocongAI : MonoBehaviour
{
    public GameObject orang;
    [SerializeField] public float speed;
    [SerializeField] public float range;
    [SerializeField] public float maxDistance;
    private Vector2 startingPosition; // Titik awal musuh
    [SerializeField] private int facingDirection = 1; // 1 untuk menghadap ke kanan, -1 untuk menghadap ke kiri

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        Vector3 moveDirection = (orang.transform.position - transform.position).normalized;

        // Mengatur facing (arah pandang) musuh saat bergerak menuju objek
        if (moveDirection.x < 0)
        {
            facingDirection = 1; 
        }
        else if (moveDirection.x > 0)
        {
            facingDirection = -1; 
        }

        // Mengukur jarak antara musuh dan objek "orang" hanya pada sumbu X
        float distanceX = Mathf.Abs(orang.transform.position.x - transform.position.x);

        // Jika jaraknya kurang dari jangkauan tertentu dan belum mencapai batas maksimal pergerakan
        if (distanceX < range && Mathf.Abs(transform.position.x - startingPosition.x) < maxDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(orang.transform.position.x, transform.position.y), speed * Time.deltaTime);
        }
        else
        {
            // Ketika objek berada di luar jangkauan atau musuh kembali ke posisi semula,
            Vector3 directionToStart = (startingPosition - (Vector2)transform.position).normalized;
            if (directionToStart.x < 0)
            {
                facingDirection = 1; 
            }
            else if (directionToStart.x > 0)
            {
                facingDirection = -1; 
            }

            transform.position = Vector2.MoveTowards(transform.position, startingPosition, speed * Time.deltaTime);
        }

        transform.localScale = new Vector3(facingDirection, 1, 1);
    }
}

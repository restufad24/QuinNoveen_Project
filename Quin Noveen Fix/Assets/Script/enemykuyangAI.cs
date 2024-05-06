using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemykuyangAI : MonoBehaviour
{
    public GameObject orang;
    [SerializeField] public float speed;
    [SerializeField] public float range;
    private float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = (orang.transform.position - transform.position).normalized;

        // Mengatur facing (arah pandang) musuh sesuai dengan arah gerakannya
        if (moveDirection.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Menghadap ke kanan
        }
        else if (moveDirection.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Menghadap ke kiri
        }
        distance = Vector2.Distance(transform.position, orang.transform.position);
        Vector2 direction = orang.transform.position - transform.position;
        direction.Normalize();
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < range)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, orang.transform.position, speed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}

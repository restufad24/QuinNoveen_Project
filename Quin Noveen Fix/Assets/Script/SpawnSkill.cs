using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSkill : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;
    private GameObject spawnedObject;

    void Update()
    {
        // Jika tombol Space ditekan
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Hancurkan objek sebelumnya jika ada
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
            }
            
            // Menciptakan objek baru di spawn point
            spawnedObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnSkill : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;
    private GameObject spawnedObject;
    public float cooldownDuration = 30f; // Durasi cooldown dalam detik
    private float lastSpawnTime = -9999f; // Waktu terakhir objek dibuat, diatur ke nilai negatif besar agar objek dapat langsung dibuat saat pertama kali tombol ditekan
   

   
    void Update()
    {

        if (Time.time - lastSpawnTime >= cooldownDuration)
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

             //perbaharui waktu objek terakhir dibuat
             lastSpawnTime = Time.time;
            }
        }
        
    
        
    }

    
       
}
    
    


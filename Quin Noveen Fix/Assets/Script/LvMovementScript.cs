using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvMovementScript : MonoBehaviour
{
    public int sceneBuildIndex;

    //ketika masuk zona, pindah scene

    private void  OnTriggerEnter2D(Collider2D other)
    {
        print("Trigger Entered");
        if(other.tag == "Player")
        {
            print("Switched Scene to" + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }

    }
}

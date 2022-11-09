using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
   public Rigidbody rb;
   public float impulseForce = 3f; 

   private bool ignoreNextcollision;

   private Vector3 startPosition;

   public int perfectPass;

   public float superSpeed = 8;

   private bool isSuperSpeedActive;

   public int perfectPassCount = 1;



   private void Start()
   {
    startPosition =transform.position;
   }



   private void OnCollisionEnter(Collision collision)
   {
    

    if(ignoreNextcollision)
    {
        return;
    }

    
    if(isSuperSpeedActive && !collision.transform.GetComponent<GoalController>())
    {
        Destroy(collision.transform.parent.gameObject,0.2f);
    }
    else
    {
         DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
    if(deathPart)
    {
        GameManager.singleton.RestartLevel();
    }

    }


    rb.velocity =Vector3.zero;
    rb.AddForce(Vector3.up*impulseForce, ForceMode.Impulse);

    ignoreNextcollision = true;
    Invoke("AllowNextCollision", 0.2f);

    perfectPass =0;
    isSuperSpeedActive = false;
   }

   private void Update()
   {
    if(perfectPass >= perfectPassCount && !isSuperSpeedActive)
    {
        isSuperSpeedActive = true;
        rb.AddForce(Vector3.down*superSpeed,ForceMode.Impulse);
    }
   }

    private void  AllowNextCollision ()
    {
        ignoreNextcollision = false;
    }

    public void ResetBall()
        {
            transform.position =startPosition;
        }
    
}

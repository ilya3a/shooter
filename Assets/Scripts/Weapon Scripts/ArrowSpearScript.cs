using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpearScript : MonoBehaviour
{
    Rigidbody arrowbowBody;

    public float speed = 20f;
    public float deactivateTimer = 3f;
    public float damage = 15f;

    private void OnTriggerEnter(Collider target)
    {
        //after we touch the enemy -> deactive
        gameObject.SetActive(false);
        if(target.transform.parent.tag == Tags.ENEMY_TAG)
        {
            target.transform.GetComponent<HealthScript>().ApllyDamage(damage);
        }

    }
    
    void Awake()
    {
        arrowbowBody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //call this function in timer when the game object created
        Invoke("DeactivateGameObject", deactivateTimer);
    }

    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
    public void Luanch(Camera mainCamera)
    {
        //set arrow velocity to the direction "forward" of the main camera
        arrowbowBody.velocity = mainCamera.transform.forward * speed;
        //rotate the arrow 
        transform.LookAt(transform.position + arrowbowBody.velocity);
    }

}

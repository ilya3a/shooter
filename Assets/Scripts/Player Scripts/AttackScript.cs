using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public float damage = 2f;
    public float radius = 1f;
    public LayerMask layerMaskOfTheObjectIsBeenAttacked;


    private void OnTriggerEnter(Collider other)
    {
        if (transform.gameObject.tag != other.gameObject.tag)
        {
            print("we touched " + other.gameObject.tag);
            if (other.transform.GetComponent<HealthScript>() != null)
            {
                other.transform.GetComponent<HealthScript>().ApllyDamage(damage);
            }
        }
        
    }
   

  
}
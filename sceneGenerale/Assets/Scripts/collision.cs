using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    	public void OnTriggerEnter2D (Collider2D collider) 
        {
	Debug.Log(collider.gameObject.name);
	print(collider.gameObject.name);
  	}
        
    
}

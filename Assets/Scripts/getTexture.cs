using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getTexture : MonoBehaviour
{
    // Update is called once per frame
    public void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward, Color.green);
        if (Physics.Raycast(transform.position,transform.up * -1.0f,out hit ,5))
        {   
            Debug.Log(hit.textureCoord);
            if (hit.textureCoord.x > 0)
            {
                Destroy(gameObject);
            }
            
        }
    }
}

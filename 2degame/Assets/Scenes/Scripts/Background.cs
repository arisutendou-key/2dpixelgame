using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float repeatWidth;
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        repeatWidth = collider.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main.transform.position.x > transform.position.x + repeatWidth) // in the right of the centerpiece
        {
            // teleport it forward
            transform.position = new Vector3(transform.position.x + (repeatWidth * 4), transform.position.y, transform.position.z);
        } 
        if(Camera.main.transform.position.x < transform.position.x - repeatWidth) // in the left of the centerpiece
        {
            // teleport it backwards
            transform.position = new Vector3(transform.position.x - repeatWidth, transform.position.y, transform.position.z);
        }

    }
}

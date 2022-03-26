using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //speed variable of 8
    [SerializeField]
    private float _laserSpeed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up
        transform.Translate(Vector3.up * _laserSpeed * Time.deltaTime);

        //if laser position is greater than 8 on the y 
        //then destroy it

        if (transform.position.y > 8f)
        {
            //check if this object has a parent
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            //destroy the parent too
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider Enemy)
    {
        Destroy(this.gameObject);
    }
}

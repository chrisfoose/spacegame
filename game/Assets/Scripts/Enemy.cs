using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //private float _rand = Random.Range(-5.0f, 5.0f);
    
    [SerializeField]
    private float _enemySpeed = 4.0f;

    //[SerializeField]
    //private float _newPosition = new Vector3.(_rand, transform.position.y, 0f);

   

    //[SerializeField]
    //private float _enemyStartHorizontal = Random.Range(-11f, 11f);
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
    void Update()
    {
        //move down at 4 meters per second
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);

        //if bottom of screen
        //respawn at top with a new random x position
        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);         
        }

       
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is Player
        //damage Player
        //destroy us

        if (other.tag == "Player")
        {
            //damage player
            Debug.Log("Trigger with Player detected");
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
           
            Destroy(this.gameObject);
        }
        //Instantiate(Enemy, transform.position + new Vector3(randomX, 7, 0), Quaternion.identity);

        //if other is laser
        //destroy laser
        //destroy us
        if (other.tag == "Laser")
        {
            Debug.Log("Trigger with Laser detected");
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}

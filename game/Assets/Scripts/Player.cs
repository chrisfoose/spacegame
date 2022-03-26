using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //public or private reference
    //data type 
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;

    private float _canFire = -1f;

    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedBoostActive = false;

    void Start()
    {
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()

    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
        
        //if player position greater than 0
        //then y position is 0
        //else if position on the y is less than -3.8f
        //y pos = -3.8f

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        //if player on the x > 11
        //then x pos = -11
        //else if player on the x is less than -11
        //x pos 11

        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11f, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;

            if (_isTripleShotActive == true)
            {
                //instantiate for the triple shot
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, -2.0f, 0), Quaternion.identity);

            }

            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }




            //instantiate 3 lasers with power up
            //if space press fire laser
            //if isTripleShotActive is true 
            //fire 3 lasers

            //else fire 1 laser

        }
    }

    private void OnTriggerEnter(Collider Enemy)
    {
        Debug.Log("Player collides with enemy");

    }

    public void Damage()
    {
        _lives--;

        //check if dead
        //destroy us

        if (_lives < 1)
        {
            //Communicate with spawn manager

            //Let them know to stop spawining
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }


    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    //IEnumerator TripleShotPowerDownRoutine
    //Wait 5 seconds
    //set the triple shot to false

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }
}


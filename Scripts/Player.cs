using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject game_manager;
    [SerializeField]
    private float _speed; 
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isDoubleShotActive = false;
    
    public GameObject _doubleshotPrefab;
    public Text LivesUIText;
    const int MaxLives = 3;
    int lives;

    public void Init()
    {
        lives = MaxLives;

        LivesUIText.text = lives.ToString ();
        transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
    }
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }       
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 0), 0); 

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }

        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isDoubleShotActive == true)
        {
            Instantiate(_doubleshotPrefab, transform.position + new Vector3(-0.84f, 0.12f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {
        _lives--; //_lives -= 1;
        LivesUIText.text = _lives.ToString();

        if (_lives == 0)
        {
            game_manager.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.Gameover);
            _spawnManager.OnplayerDeth();
            Destroy(this.gameObject);
        }
    }

    public void DoubleShotActive()
    {
        _isDoubleShotActive = true;
        StartCoroutine(DoubleShotPowerDownRoutine());
    }

    IEnumerator DoubleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(7.0f);
        _isDoubleShotActive = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject scoreUIText;

    [SerializeField]
    private float _speed = 4f;

    private void Start()
    {
        scoreUIText = GameObject.FindGameObjectWithTag("Score");
    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //other.transform.GetComponent<Player>().Damage();
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser")
        {
            scoreUIText.GetComponent<UIManager>().Score += 100;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}

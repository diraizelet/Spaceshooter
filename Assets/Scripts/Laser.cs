using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed=8.0f;
    private bool _isEnemyLaser = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isEnemyLaser == false)
        {
            Moveup();
        }
        else
        {
            MoveDown();
        }
    }

    void Moveup()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if(transform.position.y>=18.3f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void MoveDown()
    {
        transform.Translate(Vector3.down *  _speed * Time.deltaTime);

        if(transform.position.y<=-6.5f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemy()
    {
        _isEnemyLaser = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();
            if(player == null)
            {
                Debug.LogError("Player null In laser");
            }
            player.damage();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    private Animator _anim;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        _audioSource = GetComponent<AudioSource>();
        if(_player == null)
        {
            Debug.LogError("Player is null !!!! ");
        }
        _anim = GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.LogError("Animaotor is null !!!! ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down *_speed * Time.deltaTime);

        if(transform.position.y <= -6.5)
        {
            transform.position = new Vector3(Random.Range(-9.05f, 9.28f), 18.3f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            Player player = other.transform.GetComponent<Player>();
            
            if(player!=null)
            {
                player.damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject);
        }

        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if(_player != null)
            {
                _player.addScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject,2.8f);
        }
    }

    
}

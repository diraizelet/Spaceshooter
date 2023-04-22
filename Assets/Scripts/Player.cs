using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]    // variable available to palyers but not other scripts 
    private float _speed =3.5f;     //use   "  _  "  for private
    private float _speedmultiplier = 2;
    [SerializeField]
    private GameObject _laserprefab;
    [SerializeField]
    private float _firerate = 0.5f;
    private float _canfire = -1f;
    [SerializeField]
    private int _lives = 3;
    private Spawn_manager _spawnmanager;

    [SerializeField]
    private GameObject _tripleshotprefab;
    [SerializeField]
    private bool _istripleshot = false;
    private bool _shieldactivated = false;
    [SerializeField]
    private GameObject _shieldvisualiser;

    [SerializeField]
    private int _score;
    private UIManager _uiManager;

    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _leftMovement;
    [SerializeField]
    private GameObject _rightMovement;
    private GameManager _gameManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    private AudioSource _audioSource;
    //private Animator _anim;
    public bool isplayer1 = false;
    public bool isplayer2 = false;

    void Start()
    {
        _spawnmanager = FindObjectOfType<Spawn_manager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>();
       // _anim = GetComponent<Animator>();
        
        if(_gameManager.isCoopMode == false)
        {
            //take the current position  =new position (0,0,0)
            transform.position = new Vector3(0, 2, 0);
        }

        if(_spawnmanager ==null)
        {
            Debug.LogError("The spawn manager is null");
        }
        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is null!!!");
        }
        if(_audioSource == null)
        {
            Debug.LogError("The AudioSource is null !!");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isplayer1 == true)
        {
            movementcontrol();
        }  
        
    }
    void movementcontrol()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire"))
        {
            shootlaser();
        }
        float horizontalinput = CrossPlatformInputManager.GetAxis("Horizontal");
        // test value returned -result(either 1 or -1)
        float verticalinput = CrossPlatformInputManager.GetAxis("Vertical"); //Input.GetAxis("Vertical");

        //nex Vector3(1,0,0)*5* realtime 
        transform.Translate(Vector3.right * horizontalinput * _speed * Time.deltaTime);
        //same as vector to move in user direction *1*speed meters per second in realtime
        transform.Translate(Vector3.up * verticalinput * _speed * Time.deltaTime);

        //transform.Translate (new Vector3(horizontalinput, verticalinput,0) * _speed * Time.deltaTime);

        //if player position is on y>0
        // player position is fixed in 0

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.938f)
        {
            transform.position = new Vector3(transform.position.x, -3.938f, 0);
        }

        // transform.position =new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.938f,0),0);
        if (transform.position.x >= 11.45f)
        {
            transform.position = new Vector3(-11.22f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.22f)
        {
            transform.position = new Vector3(11.45f, transform.position.y, 0);
        }
    }
    
    /*void movementcontrol2()
    {
        if(Input.GetKey(KeyCode.I))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.K))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.J))
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.L))
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        //nex Vector3(1,0,0)*5* realtime 
        //transform.Translate(Vector3.right * horizontalinput * _speed * Time.deltaTime);
        //same as vector to move in user direction *1*speed meters per second in realtime
        //transform.Translate(Vector3.up * verticalinput * _speed * Time.deltaTime);

        //transform.Translate (new Vector3(horizontalinput, verticalinput,0) * _speed * Time.deltaTime);

        //if player position is on y>0
        // player position is fixed in 0

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.938f)
        {
            transform.position = new Vector3(transform.position.x, -3.938f, 0);
        }

        // transform.position =new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.938f,0),0);
        if (transform.position.x >= 11.45f)
        {
            transform.position = new Vector3(-11.22f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.22f)
        {
            transform.position = new Vector3(11.45f, transform.position.y, 0);
        }
    }
    */

    void shootlaser()
    {
        _canfire = Time.time + _firerate;
        if(_istripleshot == true)
        {
            Instantiate(_tripleshotprefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserprefab, transform.position + new Vector3(0, 1.12f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }

    void shootlaser2()
    {
        _canfire = Time.time + _firerate;
        if(_istripleshot == true)
        {
            Debug.Log("TripleShot Fired");
            Instantiate(_tripleshotprefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserprefab, transform.position + new Vector3(0, 1.12f, 0), Quaternion.identity);
        }
        _audioSource.Play();
    }

    public void damage()
    {
        if( _shieldactivated == true)
        {
            _shieldactivated = false;
            _shieldvisualiser.SetActive(false);
            return;
        }
        _lives--;
        if(_lives ==2)
        {
            _leftEngine.SetActive(true);
        }
        else if(_lives ==1)
        {
            _rightEngine.SetActive(true);
        }
        _uiManager.UpdateLives(_lives);
        if(_lives<1)
        {
            _spawnmanager.onplayerdeath();
            _uiManager.LastSceneGameOver();
            Destroy(this.gameObject);
        }
    }

    public void tripleshotactive()
    {
        _istripleshot = true;
        StartCoroutine(tripleshotpowerdownroutine());
    }

    IEnumerator tripleshotpowerdownroutine()
    {
        yield return new WaitForSeconds(5.0f);
        _istripleshot = false;
    }

    public void speedactive()
    {
        _speed *= _speedmultiplier;
        StartCoroutine(speedboostpowerdownroutine());
    }

    IEnumerator speedboostpowerdownroutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedmultiplier;
    }
    public void shieldactive()
    {
        _shieldactivated = true;
        _shieldvisualiser.SetActive(true);
    }

    public void addScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}



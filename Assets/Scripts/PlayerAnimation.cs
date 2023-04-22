 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator _anim , _anim2;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _anim.Play("Player Turn Left");
            _anim.Play("Player Turn Left");
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _anim.Play("Player Turn Right");
            _anim.Play("Player Turn Right");
        }
    }
}

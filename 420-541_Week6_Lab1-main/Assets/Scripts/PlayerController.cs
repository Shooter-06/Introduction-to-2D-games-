using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    public GameObject groundcheckObject;
    private Rigidbody2D _playerRigidbody;
    private Animator _animator;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _animator= GetComponent<Animator>();
    }
    private void Update()
    {
        MovePlayer();
        _animator.SetBool("isRunning", _playerRigidbody.velocity.magnitude >0.0f && IsGrounded()); //Running
        _animator.SetBool("IsGrounded", IsGrounded());

        
        // if(_playerRigidbody.velocity.magnitude >0.0f && IsGrounded())
        // {
        //     _animator.SetBool("isRunning", true);

        // }else if(_playerRigidbody.velocity.magnitude ==0.0f && IsGrounded())
        // {
        //     c // I am idle

        // }else if(!IsGrounded())
        // {
        //     _animator.SetBool("IsGrounded", false);
        // }

        if (Input.GetButton("Jump") && IsGrounded())
            Jump();
    }
    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        if(horizontalInput<0){
            transform.localScale= new Vector3(-1,1,1);
        }else if (horizontalInput>0){
            transform.localScale=new Vector3(1,1,1);
        }

        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);
    }
    private void Jump() => _playerRigidbody.velocity = new Vector2( 0, jumpPower);

    private bool IsGrounded()
    {
        
        var groundCheck = Physics2D.Raycast(groundcheckObject.transform.position,Vector2.down, 0.7f);
        if (groundCheck.collider != null)
        {
            //Debug.Log(groundCheck.collider.tag);
            return groundCheck.collider.CompareTag("Ground");
        }
        return false;
        
    }

}
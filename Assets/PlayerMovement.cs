using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent((typeof(PlayerInput)))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] TextMeshPro text;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] InputAction jumpButton;
    [SerializeField] float speed, jumpForce;
    [SerializeField] float raycastLength;
    [SerializeField] LayerMask groundLayer;
    bool groundCheck;

    public void OnJump()
    {
        if(groundCheck)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Move()
    {
        var moveX = playerInput.actions.FindAction("Move").ReadValue<Vector2>().x;
        transform.Translate(Vector2.right * moveX * speed * Time.deltaTime);
    }
    void Update()
    {
        Move();
        GroundCheck();
        text.text = playerInput.playerIndex.ToString();
    }

    void GroundCheck()
    {
         groundCheck = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayer);
        Debug.DrawLine(transform.position,
        new Vector2(transform.position.x, transform.position.y - raycastLength), Color.red);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
public class PlayerAbility : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject playerInteractCircle;
    [SerializeField] InteractCursor interactCursor;
    float playerNumber;

    void OnEnable()
    {
        playerNumber = playerInput.playerIndex;
    }

    void Update()
    {
        Look();
    }

    public void UseAbility()
    {

        if(playerNumber == 0)
        {

            Debug.Log("Destroy");
        interactCursor.Destroy();
        }
        else if(playerNumber ==1)
        {
            Debug.Log("Create");
            //CREATE
        }
    }
    public void Look()
    {
       var  lookValue = playerInput.actions.FindAction("Look").ReadValue<Vector2>();
       playerInteractCircle.transform.eulerAngles = 
       new Vector3( 0,0, Mathf.Atan2(-lookValue.x, lookValue.y) * 180 / Mathf.PI);

       var lookLength = (Mathf.Abs(playerInput.actions.FindAction("Look").ReadValue<Vector2>().x) -
         Mathf.Abs(playerInput.actions.FindAction("Look").ReadValue<Vector2>().y)) * 5;

       interactCursor.transform.localPosition = new Vector2(0, Mathf.Abs(lookLength));
    }
}

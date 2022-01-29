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
            interactCursor.Create();
        }
    }
    public void Look()
    {
       var  lookValue = playerInput.actions.FindAction("Look").ReadValue<Vector2>();
       playerInteractCircle.transform.eulerAngles = 
       new Vector3( 0,0, Mathf.Atan2(-lookValue.x, lookValue.y) * 180 / Mathf.PI);

        var lookLength = Mathf.Clamp01(new Vector2(lookValue.x, lookValue.y).magnitude);

        interactCursor.transform.localPosition = new Vector2(0, Mathf.Abs(lookLength * 5));
    }
}

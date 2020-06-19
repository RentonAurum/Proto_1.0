using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_State : MonoBehaviour
{

    public enum playerStates
    {
        standing,
        walking,
        running,
        jumping,
        attacking
    }

    public playerStates Current_PlayerState;

    // Start is called before the first frame update
    void Start()
    {
        Current_PlayerState = playerStates.standing;
    }

    // Update is called once per frame
    void Update()
    {
        switch(Current_PlayerState)

        {
            case playerStates.standing:
                Debug.Log("Standing");
                break;
            case playerStates.walking:
                Debug.Log("Walking");
                break;
            case playerStates.attacking:
                Debug.Log("Attacking");
                break;
            case playerStates.running:
                Debug.Log("Running");
                break;
            case playerStates.jumping:
                Debug.Log("Jumping");
                break;
        }

    }
}

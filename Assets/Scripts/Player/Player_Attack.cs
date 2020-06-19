using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{

    public Animator Attack_Animation;
    private Player_State _PlayerState;
    private PlayerMovement _PlayerMovement;
    public GameObject AttackingSpeed;

    public TimeManager timeManager;

    public int clickCount;
    float lastClickTime;
    public float maxComboDelay = 0.9f;

    void Awake()
    {
        Attack_Animation = GetComponent<Animator>();
        _PlayerState = GetComponent<Player_State>();
        _PlayerMovement = GetComponent<PlayerMovement>();
    }
    /*public void AlertStartAttack() // EVENTO MAI IMPOSTATO - NON NECESSARIO PER VIA DELLE CONDIZIONI SOTTO.
    {
        Debug.Log("Alert Event : ATTACK!");
        //AttackingSpeed.GetComponent<PlayerMovement>().enabled = false;
    }
    public void AlertEndAttack()
    {
        Debug.Log("Alert Event : ATTACK FINISHED");
        //AttackingSpeed.GetComponent<PlayerMovement>().enabled = true;
    }*/
    void Update()
    {
        
        if (Time.time - lastClickTime > maxComboDelay)

        {
            clickCount = 0;
        }


        if (Input.GetMouseButtonDown(0))
        {
            _PlayerState.Current_PlayerState = Player_State.playerStates.attacking;
            _PlayerMovement.Speed = 0;
            lastClickTime = Time.time;
            clickCount++;

            if (clickCount == 1)
            
            {
                //Debug.Log("STO ATTACCANDO!");
                _PlayerState.Current_PlayerState = Player_State.playerStates.attacking;
                Attack_Animation.SetBool("FirstSlash", true);
            }

            clickCount = Mathf.Clamp(clickCount, 0, 3);

        }
        /*if (Input.GetMouseButtonUp(0))
        {
            Attack_Animation.SetBool("FirstSlash", false);
            _PlayerState.Current_PlayerState = Player_State.playerStates.standing;
        }*/
    }

    public void return1()
    {
        if (clickCount >=2)
        {
            _PlayerState.Current_PlayerState = Player_State.playerStates.attacking;

            Attack_Animation.SetBool("FirstSlash", false);
            Attack_Animation.SetBool("SecondSlash", true);
        }
        else
        {
            _PlayerState.Current_PlayerState = Player_State.playerStates.standing;

            Attack_Animation.SetBool("Walking", false);
            Attack_Animation.SetBool("Running", false);
            Attack_Animation.SetBool("FirstSlash", false);

            clickCount = 0;
        }
    }

    public void return2()
    {
        if (clickCount >= 3)
        {

            _PlayerMovement.Speed = 0;
            _PlayerState.Current_PlayerState = Player_State.playerStates.attacking;

            timeManager.SlowMotion();

            Attack_Animation.SetBool("SecondSlash", false);
            Attack_Animation.SetBool("ThirdSlash", true);

        }
        else
        {

            _PlayerState.Current_PlayerState = Player_State.playerStates.standing;

            Attack_Animation.SetBool("Walking", false);
            Attack_Animation.SetBool("Running", false);

            Attack_Animation.SetBool("FirstSlash", false);
            Attack_Animation.SetBool("SecondSlash", false);

            clickCount = 0;
        }
    }

    public void return3()
    {

        Time.timeScale = 1f;

        _PlayerState.Current_PlayerState = Player_State.playerStates.standing;

        Attack_Animation.SetBool("Walking", false);
        Attack_Animation.SetBool("Running", false);

        Attack_Animation.SetBool("FirstSlash", false);
        Attack_Animation.SetBool("SecondSlash", false);
        Attack_Animation.SetBool("ThirdSlash", false);
        clickCount = 0;
    }

}

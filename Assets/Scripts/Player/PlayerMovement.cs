using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;      
    private Vector3 moveDirection = Vector3.zero;
    public float Speed;
    public float RunningSpeed;
    public float WalkingSpeed;
    public float gravity;
    public float jumpHeight;
    public float descentGravity;

    public Animator AnimationMovement;

    private Player_State _PlayerState;

    public Transform MainCameraTransform;

    void Awake()

    {
        controller = GetComponent<CharacterController>();
        _PlayerState = GetComponent<Player_State>();
    }

    void Start()
    {

    }

    void Update()

    {

        Movement();
        Jump();
    }

    void Movement()

    {

        if (controller.isGrounded)

        {


            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))
            {
                AnimationMovement.SetBool("Running", false);
                AnimationMovement.SetBool("Walking", true);

                float moveX = Input.GetAxis("Horizontal");
                float moveZ = Input.GetAxis("Vertical");

                Vector3 forward = MainCameraTransform.forward;
                Vector3 right = MainCameraTransform.right;

                forward.y = 0;
                right.y = 0;

                forward.Normalize();
                right.Normalize();

                _PlayerState.Current_PlayerState = Player_State.playerStates.walking;

                moveDirection = new Vector3(moveX, 0f, moveZ);

                Vector3 DesiredMoveDirection = (forward * moveDirection.z + right * moveDirection.x).normalized;

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(DesiredMoveDirection), Speed);

                ///////////////////////////////////////////////////////////////////////////////////////////

                // ~ Controllo tramite Input del giocatore se sta correndo o se sta
                // ~ camminando e gli assegno il valore corrispondente alla camminata/corsa.

                if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))

                {
                    AnimationMovement.SetBool("Walking", false);
                    AnimationMovement.SetBool("Running", true);
                    _PlayerState.Current_PlayerState = Player_State.playerStates.running;
                    Speed = RunningSpeed;

                }

                else if (Input.GetKey(KeyCode.LeftShift))

                {
                    AnimationMovement.SetBool("Running", false);
                    Speed = WalkingSpeed;

                }

                if (Input.GetKeyUp(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))

                {
                    AnimationMovement.SetBool("Running", false);
                    Speed = WalkingSpeed;
                }

                controller.Move(DesiredMoveDirection * Speed * Time.deltaTime);

            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A))
            {
                AnimationMovement.SetBool("Walking", false);
                _PlayerState.Current_PlayerState = Player_State.playerStates.standing;
                Speed = 0;
                moveDirection *= Speed;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A)))
            {
                AnimationMovement.SetBool("Walking", false);
                AnimationMovement.SetBool("Running", false);
                _PlayerState.Current_PlayerState = Player_State.playerStates.standing;
                Speed = 0;
                moveDirection *= Speed;
            }
            else
                Speed = 10;

            if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A)))
            {
                AnimationMovement.SetBool("Walking", false);
                AnimationMovement.SetBool("Running", false);
                _PlayerState.Current_PlayerState = Player_State.playerStates.standing;
                Speed = 0;
                moveDirection *= Speed;
            }

            if (Input.GetMouseButtonDown(0) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A)))
            {
                _PlayerState.Current_PlayerState = Player_State.playerStates.attacking;
            }

            if (_PlayerState.Current_PlayerState == Player_State.playerStates.standing)
            {
                Speed = 0;
                moveDirection *= Speed;
            }
        }

    }

    void Jump()

    ////////////////////////////////////////////////////////////
    ///////// ~ SALTO IN PRIMA PERSONA DEL PERSONAGGIO./////////
    ////////////////////////////////////////////////////////////

    {
        // ~ Controllo se premo il tasto per saltare e se sono sul terreno.

        if (Input.GetButtonDown("Jump") && controller.isGrounded)

        {
            _PlayerState.Current_PlayerState = Player_State.playerStates.jumping;
            moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
        }

        // ~ Diminuisco la velocità di direzione del personaggio durante il salto
        // ~ sottraendo alla stessa direzione la gravità per il tempo.

        if (controller.isGrounded || moveDirection.y > 0)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // ~ Se l'asse delle y del personaggio è minore/pari a 0, assegno la sua gravità
        // ~ standard.

        if (moveDirection.y <= 0)
        {
            descentGravity = gravity;
            descentGravity += 1000 * Time.deltaTime;
            moveDirection.y -= descentGravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

}

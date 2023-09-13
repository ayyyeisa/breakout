using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public PlayerInput MyPlayerInput;
    private InputAction move;
    private InputAction restart;
    private InputAction quit;
    private InputAction launch;


    public float PaddleSpeed;
    private bool paddleShouldBeMoving;
    public Rigidbody2D Paddle;
    private float moveDirection;
    public BallController CurrentBallController;
    public bool CanReceiveGameInputs;

    // Start is called before the first frame update
    void Start()
    {
        MyPlayerInput.currentActionMap.Enable();
        move = MyPlayerInput.currentActionMap.FindAction("Move");
        restart = MyPlayerInput.currentActionMap.FindAction("RestartGame");
        quit = MyPlayerInput.currentActionMap.FindAction("QuitGame");
        launch = MyPlayerInput.currentActionMap.FindAction("Launch");

        move.started += Handle_MoveStarted;
        move.canceled += Handle_MoveCancelled;
        restart.performed += Handle_RestartPerformed;
        quit.performed += Handle_QuitPerformed;
        launch.performed += Handle_LaunchPerformed;
    }

    public void OnDestroy()
    {
        move.started -= Handle_MoveStarted;
        restart.performed -= Handle_RestartPerformed;
        quit.performed -= Handle_QuitPerformed;
        move.canceled -= Handle_MoveCancelled;
        launch.performed -= Handle_LaunchPerformed;
    }

    private void Handle_LaunchPerformed(InputAction.CallbackContext obj)
    {
        if (CanReceiveGameInputs == true)
        {
            CurrentBallController.Launch();
        }
    }

    private void Handle_QuitPerformed(InputAction.CallbackContext obj)
    {
        print("Handled Quit Performed");
        Application.Quit();
    }

    private void Handle_RestartPerformed(InputAction.CallbackContext obj)
    {
        print("Handled Restart Performed");
        SceneManager.LoadScene(0);
    }

    private void Handle_MoveStarted(InputAction.CallbackContext obj)
    {
        if (CanReceiveGameInputs == true)
        {
            print("Handled Move Started");
            paddleShouldBeMoving = true;
        }
    }

    private void Handle_MoveCancelled(InputAction.CallbackContext obj)
    {
        print("Handled Move Cancelled");
        paddleShouldBeMoving = false;
    }
    public void FixedUpdate()
    {
        if (paddleShouldBeMoving)
        {
            print("Paddle Should Be Moving");
            Paddle.velocity = new Vector2(PaddleSpeed * moveDirection, 0);
        }
        else
        {
            print("Paddle Should Not Be Moving");
            Paddle.velocity = Vector2.zero;
        }
    }

    public void Update()
    {
        if(paddleShouldBeMoving) {
            moveDirection = move.ReadValue<float>();
        } 
    }
}

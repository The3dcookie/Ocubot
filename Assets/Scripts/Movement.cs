using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   
    public float speed = 5f;
    public float jumpForce = 10f;
    private float inputX;
    private float inputZ;
    public JoyStickScript joystick;

    private Rigidbody ballRigidBody;
    public bool canJump = false;




    // Start is called before the first frame update
    private void Start()
    {
        
        ballRigidBody = gameObject.GetComponent<Rigidbody>();
        GetComponent<JoyStickScript>();
    }

    private void TouchControls()
    {
        inputX = JoyStickScript.Main.InputHorizontal();
        inputZ = JoyStickScript.Main.InputVertical();

        if (inputX < 0)
        {

            //Debug.Log("InputXLeftWorking");
            ballRigidBody.AddForce(Vector3.right * speed * Time.deltaTime);
        }
        else if (inputX > 0)
        {
            //Debug.Log("InputXRightWorking");
            ballRigidBody.AddForce(Vector3.left * speed * Time.deltaTime);
        }
        if (inputZ < 0)
        {
            //Debug.Log("InputZfrontWorking");

            ballRigidBody.AddForce(Vector3.forward * speed * Time.deltaTime);

        }
        else if (inputZ > 0)
        {
            //Debug.Log("InputZbackWorking");

            ballRigidBody.AddForce(Vector3.back * speed * Time.deltaTime);

        }

    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetAxis("Horizontal") < 0)
        {
            ballRigidBody.AddForce(Vector3.right * speed * Time.deltaTime);
        } 
       else if (Input.GetAxis("Horizontal") > 0)
        {
            ballRigidBody.AddForce(Vector3.left * speed * Time.deltaTime);
        }


       if (Input.GetAxis("Vertical") < 0)
        {
            ballRigidBody.AddForce(Vector3.forward * speed * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            ballRigidBody.AddForce(Vector3.back * speed * Time.deltaTime);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            ballRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        }
        TouchControls();
    }

    public void Jump()
    {
        if(canJump == true)
        {
            ballRigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        canJump = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            canJump = true;
        }

    }

    //private void OnLevelWasLoaded(int level)
    //{
    //    FindStartPos();
    //}

    //private void FindStartPos()
    //{
    //    transform.position = GameObject.FindWithTag("StartPos").transform.position;
    //}
}

using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Transform cam;

    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    public float jumpForce = 5;

    public Rigidbody rb;

    private float smoothTime = 0.1f;
    float turnVelocity;


    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f,  vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnVelocity, smoothTime);
            transform.rotation = Quaternion.Euler(0f, smoothAngle , 0f);

            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;


            if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
            {
                characterController.Move(moveDir.normalized * runSpeed * Time.deltaTime);
            }
            else
            {
                characterController.Move(moveDir.normalized * walkSpeed * Time.deltaTime);

            }
            //this.gameObject.transform.rotation = Quaternion.Euler(0, this.gameObject.transform.rotation.y, this.gameObject.transform.rotation.z);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

        


    }

    


}
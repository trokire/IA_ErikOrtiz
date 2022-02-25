using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterControl : MonoBehaviour
{
    public CharacterController controller;
    public Transform interactionZone;
    public Transform cam;
    Vector3 direction;

    public float speed = 6f;
    public float gravity = -9.8f;
    public float turnSmoothTime = 0.1f;
    float fallVelocity;
    float turnSmoothVelocity;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        SetGravity();
    }

    void SetGravity()
    {


        if (controller.isGrounded)
        {
            fallVelocity = gravity * Time.deltaTime;
            Vector3 gravityVector = new Vector3(0, fallVelocity, 0);
            controller.Move(gravityVector * Time.deltaTime);
        }
        else
        {
            fallVelocity += gravity * Time.deltaTime;
            Vector3 gravityVector = new Vector3(0, fallVelocity, 0);
            controller.Move(gravityVector * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 mouseLook;
    private Vector2 smoothV;
    private CharacterController controller;
    private Vector2 moveDir;
    public float Speed;
    public float sensitivity = 5f;
    public float smoothing = 2f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, -90, 90);

        Camera.main.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);
        

        moveDir.x = Input.GetAxis("Horizontal") * Speed;
        moveDir.y = Input.GetAxis("Vertical")* Speed;

        Move();
    }

    void Move()
    {
        controller.SimpleMove(transform.forward * moveDir.y);
        controller.SimpleMove(transform.right * moveDir.x);
    }


}

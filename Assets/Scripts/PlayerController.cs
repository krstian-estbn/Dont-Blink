using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRb;

    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 mouseInput;
    public float mouseSensitivity = 1f;

    public Transform viewCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Player Movement
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveHorizontal = transform.up * -moveInput.x;
        Vector3 moveVertical = transform.right * moveInput.y;
        theRb.velocity = (moveHorizontal + moveVertical) * moveSpeed;

        // Player View Control
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);

        viewCam.localRotation = Quaternion.Euler(viewCam.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));
    
    }
}

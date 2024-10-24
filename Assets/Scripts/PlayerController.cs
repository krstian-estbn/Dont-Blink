using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D theRb;

    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 mouseInput;
    public float mouseSensitivity = 1f;
    private float verticalLockRotation = 90f;

    public Camera viewCam;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Player Movement
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector3 moveHorizontal = transform.right * moveInput.x;
        Vector3 moveVertical = transform.up * moveInput.y;
        theRb.velocity = (moveHorizontal + moveVertical) * moveSpeed;

        // Player View Control
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z - mouseInput.x);

        verticalLockRotation += mouseInput.y;
        verticalLockRotation = Mathf.Clamp(verticalLockRotation, 0f, 180f);
        viewCam.transform.localRotation = Quaternion.Euler(-verticalLockRotation, 0f, 0f);

        // Detect Object
        Ray ray = viewCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.2f);
        if (hit.collider != null)
        {
            ItemPickup itemPickup = hit.collider.GetComponent<ItemPickup>();

            if (itemPickup != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("I'm picking up " + hit.collider.gameObject.name);
                    itemPickup.Pickup();
                }
            }
            else
            {
                Debug.Log("No item to pick up");
            }
        }
        else
        {
            Debug.Log("I'm not looking at anything");
        }
    }
}

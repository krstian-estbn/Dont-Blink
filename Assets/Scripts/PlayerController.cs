using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D theRb;

    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 mouseInput;
    public float mouseSensitivity = 1f;

    public Camera viewCam;

    public int letterCounter = 0;
    public Text counter;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        counter.text = letterCounter.ToString();
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

        viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f, mouseInput.y, 0f));

        // Detect Object
        Ray ray = viewCam.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 0.2f);
        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("I'm looking at " + hit.collider.gameObject.name);
                letterCounter += 1;
                Destroy(hit.collider.gameObject);

            }
            else
            {
                Debug.Log("Click Me");
            }
        }
        else
        {
            Debug.Log("I'm not looking at anything");
        }

        counter.text = letterCounter.ToString();
    }
}

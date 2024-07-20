using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
    [SerializeField] float RotationSpeed;
    [SerializeField] float Speed;
    CharacterController controller;
    //float cameraverticalRotation = 0f;
    //float camerahorizontalRotation = 0f;
    Vector3 movement;
    Vector3 Myrotation;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Myrotation = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        var horiAxis = Input.GetAxis("Horizontal");
        var verAxis = Input.GetAxis("Vertical"); //0-1
        //print("===> verAxis = "+ verAxis);
        movement = (horiAxis * transform.right) + (verAxis * transform.forward);
        movement = movement.normalized * Speed;
        movement.y = Physics.gravity.y;
        controller.Move(movement * Time.deltaTime);

        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");
        Myrotation.x += mouseX;
        Myrotation.y += mouseY;
        Myrotation.y = Mathf.Clamp(Myrotation.y, -20f, 20f);
        transform.eulerAngles = new Vector2(-Myrotation.y, Myrotation.x);

        //animator.SetFloat("Walk", verAxis);
        /*cameraverticalRotation -= mouseY;
        cameraverticalRotation = Mathf.Clamp(cameraverticalRotation, -60f, 60f);

        camerahorizontalRotation += mouseX;
        transform.localRotation = Quaternion.Euler(cameraverticalRotation, camerahorizontalRotation, 0f);
        transform.GetChild(0).transform.Rotate(-mouseY, 0, 0);*/
    }
}

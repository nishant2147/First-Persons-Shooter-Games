using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootController : MonoBehaviour
{
    //[Range(1,100)]
    [SerializeField]
    float hitForce;

    public Image aim;
    // Start is called before the first frame update
    void Start()
    {

    }

    Vector3 centerPos = new Vector3(.5f, .5f, 0);

    // Update is called once per frame
    void Update()
    {

        Cursor.lockState = CursorLockMode.Locked;

        var rayOrigin = Camera.main.ViewportToWorldPoint(centerPos);
        RaycastHit hit;

        Debug.DrawRay(rayOrigin, Camera.main.transform.forward, Color.green,50);

        if (Physics.Raycast(rayOrigin, Camera.main.transform.forward, out hit, 50))
        {
            print("=====> It's Ray Cast ===> " + hit.collider.gameObject.name);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }
        }
    }


}

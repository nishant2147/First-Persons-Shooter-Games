using UnityEngine;
using UnityEngine.UI;

public class ShootingControll : MonoBehaviour
{
    //[Range(1,100)]
    [SerializeField] float hitForce;
    public Image aim;
    LineRenderer laserline;
    public Transform gunPoint;
    public GameObject bulletPrefab;
    //public Transform bulletSpawn;
    public float bulletSpeed;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        laserline = GetComponent<LineRenderer>();
    }

    Vector3 centerPos = new Vector3(.5f, .5f, 0);

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        var RayOrigin = Camera.main.ViewportToWorldPoint(centerPos);
        RaycastHit hit;

        aim.color = Color.green;

        laserline.enabled = false;
        laserline.SetPosition(0, gunPoint.position);

        if (Physics.Raycast(RayOrigin, (Camera.main.transform.forward * 50), out hit))
        {
            laserline.enabled = true;
            laserline.SetPosition(1, hit.point);

            if (hit.rigidbody != null)
            {
                aim.color = Color.red;

                animator.SetBool("Shoot", false);
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("Shoot", true);

                    var bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
                    var rb = bullet.GetComponent<Rigidbody>();

                    var rr = bullet.transform.eulerAngles;
                    print("===> rr =>> " + rr);
                    print("===> gunPoint =>> " + gunPoint.transform.eulerAngles);
                    rr.y += 180;
                    rr.x -= 22;
                    bullet.transform.eulerAngles = rr;

                    gunPoint.LookAt(hit.point);

                    rb.AddForce(gunPoint.forward.normalized * bulletSpeed);

                    /*animator.SetBool("Jump", false);

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        animator.SetBool("Jump", true);
                    }*/
                    print("=====> Ray Cast hit ===> " + hit.collider.gameObject.name);
                }

            }
            else
            {

                laserline.SetPosition(1, RayOrigin + (Camera.main.transform.forward * 50));
            }

            RunningPlayer();
        }
    }

    /* private void Shoot()
     {
         GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

         Vector3 rotation = transform.rotation.eulerAngles;
         bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

         Rigidbody rb = bullet.GetComponent<Rigidbody>();
         rb.AddForce(bulletSpawn.forward * bulletSpeed);

         *//*var bullet = Instantiate(bulletPrefab, transform);
         var pos = bullet.transform.position;
         pos = bulletSpawn.transform.position;

         Vector3 rotation = transform.rotation.eulerAngles;
         bullet.transform.rotation = Quaternion.Euler(rotation.x + 90, transform.eulerAngles.y, rotation.z);

         bullet.transform.position = pos;
         bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.transform.forward * 80, ForceMode.Impulse);

         Destroy(bullet, 5f);*//*
         //rb.velocity = bulletSpawn.forward * bulletSpeed;
     }*/

    private void RunningPlayer()
    {
        bool isRunning = Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift);

        if (isRunning)
        {
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Walk", true);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reload");
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private int Sanity;
    public float speed;
    public Transform obj;
    public AudioSource Walking;
    bool Is_Movement;
    bool Is_Running;
    bool Check_Movement;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy")
            Sanity -= 10;
    }

    // Start is called before the first frame update
    void Start() {
        Walking = GetComponent<AudioSource>();
        Is_Movement = false;
        Check_Movement = false;
        Is_Running = false;
        Sanity = 100;
    }

    public void Update() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float temp_speed = 0;
        Is_Running = Input.GetKey(KeyCode.LeftShift) ? true : false;
        Vector3 tempVect = new Vector3(h, v, 0);
        temp_speed = speed;
        speed += Is_Running ? 0.5f : 0f;
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        obj.transform.position += tempVect;
        speed = temp_speed;
        // If Movement is found
        if (Mathf.Abs(v) > 0.05 || Mathf.Abs(h) > 0.05)
            Is_Movement = true;
        else
            Is_Movement = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Sanity <= 0) {
            Debug.Log("You're Dead, loser.");
            Sanity = 100;
        }
        if (Check_Movement == false) {
            if (Is_Movement) {
                Walking.Play();
                Walking.pitch = Is_Running ? 1.5f : 1f;
                Check_Movement = true;
            } else {
                Walking.Stop();
                Check_Movement = false;
            }
        } else if(Check_Movement && !Is_Movement) {
            Check_Movement = false;
        }
    }
}

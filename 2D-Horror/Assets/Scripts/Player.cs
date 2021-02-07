using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int Sanity;

    public float speed = 100;
    public Transform obj;

    public AudioSource Walking;
    bool Is_Movement;
    bool Check_Movement = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Sanity -= 10;
        }    

    }

    // Start is called before the first frame update
    void Start()
    {
        Sanity = 100;

        Walking = GetComponent<AudioSource>();
        Is_Movement = false;
    }

    public void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 tempVect = new Vector3(h, v, 0);
        tempVect = tempVect.normalized * speed * Time.deltaTime;

        obj.transform.position += tempVect;
        
        // If Movement is found
        if ((Mathf.Abs(v) > 0.05 ) || (Mathf.Abs(h) > 0.05))
        {
            Is_Movement = true;
        }
        else
        {
            Is_Movement = false;
        }
        Debug.Log(h);
        Debug.Log(v);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Sanity <= 0)
        {
            Debug.Log("You're Dead, loser.");
            Sanity = 100;
        }
        if (Check_Movement == false)
        {
            if (Is_Movement)
            {
                Walking.Play();
                Check_Movement = true;
            }
            else
            {
                Walking.Stop();
                Check_Movement = false;
            }
        }
        else if((Check_Movement == true) && (Is_Movement == false))
        {
            Check_Movement = false;
        }
    }
}

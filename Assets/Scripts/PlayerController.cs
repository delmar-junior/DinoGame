using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D customRigidBody;
    public Animator anim;
    private bool walking;
    private bool jumping;
    private bool dead;
    private int deaded;

    public GameManager manager;
    public Camera scriptCamera;

    private float jumpVelocity = 9.0f;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;
    private int countJump = 0;
    private int maxJump = 2;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        scriptCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        if (deaded == 35) {
            manager.AddLife(-1);
        }

        walking = false;
        jumping = false;

        Debug.Log("Pulo:" + customRigidBody.velocity.y);

        if (customRigidBody.velocity.y == 0) {
            countJump = 0;
        }
        
        if (Input.GetKeyUp(KeyCode.Space)) {
            // customRigidBody.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
            countJump += 1;
            if (countJump <= maxJump) {
                customRigidBody.velocity = Vector2.up * jumpVelocity;
                jumping = true;
            }
        }
        
        if (customRigidBody.velocity.y < 0) {
                customRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            } else if (customRigidBody.velocity.y > 0 && !Input.GetKeyUp(KeyCode.Space)) {
                customRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
                jumping = true;
            }

        if (Input.GetKey(KeyCode.A)) {
            FlipPlayerLeft();
            WalkPlayerLeft();
            walking = true;
        } else if (Input.GetKey(KeyCode.D)) {
            WalkPlayerRight();
            FlipPlayerRight();
            walking = true;
        }

        if (dead == true) {
            deaded += 1;
        }

        if (deaded == 1) {
            manager.DeathSound();
        }

        anim.SetBool("walking", walking);
        anim.SetBool("jumping", jumping);
        anim.SetBool("dead", dead);
    }

    void FixedUpdate()
    {
      scriptCamera.StalkCamera(transform);
    }
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Player") {
            return;
        }

        if (collider.tag == "DeathBur" || collider.tag == "DeathBase") {
            dead = true;
            deaded = 0;
            // Destroy(gameObject);
        }

        if (collider.tag == "PlataformaMovel") {
            gameObject.transform.parent = collider.transform;
        }

        if (collider.tag == "PassLvl2") {
            manager.PassLvl2();
        }

        if (collider.tag == "PassLvl3") {
            manager.PassLvl3();
        }

        if (collider.tag == "FinishGame") {
            manager.FinishGame();
        }

    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag == "PlataformaMovel") {
            gameObject.transform.parent = null;
        }
    }

    void FlipPlayerLeft() {
        Vector2 lado = transform.localScale;
        if (lado.x > 0) {
            lado.x = lado.x * -1;
        }
        transform.localScale = lado;
    }

    void FlipPlayerRight() {
        Vector2 lado = transform.localScale;
        if (lado.x < 0) {
            lado.x = lado.x * -1;
        }
        transform.localScale = lado;
    }

    void WalkPlayerLeft() {
        Vector2 pos = transform.position;
        pos.x -= 0.1f;
        transform.position = pos;
    }

    void WalkPlayerRight() {
        Vector2 pos = transform.position;
        pos.x += 0.1f;
        transform.position = pos;
    }

}

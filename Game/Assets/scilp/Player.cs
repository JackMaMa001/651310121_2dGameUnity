using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float jumpForce = 2f; // ความแรงในการกระโดด
    public TextMeshProUGUI coin;
    public TextMeshProUGUI Hp;
    public TextMeshProUGUI Game;
    public GameObject objectToCreate;

    private Vector3 initialPosition;
    private SpriteRenderer spriteRenderer;
    private int coi=0;
    private int H = 3;

    [SerializeField]
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        addPhysics2DRaycaster();
        initialPosition = transform.position;
    }

    
    void Update()
    {
        coin.text = "coin : " + coi ;
        Hp.text = "Hp : " + H;
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.F)) 
        {
            ResetToInitialPosition();
        }

        if (H <= 0)
        {
            Time.timeScale = 0f;
            Game.text = "Game Over\n F to reset";
            if (Input.GetKeyDown(KeyCode.F))
            {
                ResetToInitialPosition();
            }

        }

        if (Input.GetMouseButtonDown(1)) // ตรวจจับการคลิกเมาส์ซ้าย
        {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPosition.z = 0; // ตั้งค่าตำแหน่ง Z เพื่อให้ตำแหน่งถูกต้อง

            // สร้าง Object ใหม่ที่ตำแหน่งที่คลิก
            Instantiate(objectToCreate, clickPosition, Quaternion.identity);
        }

    }
    void addPhysics2DRaycaster()
    {
        Physics2DRaycaster physicsRaycaster = GameObject.FindObjectOfType<Physics2DRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<Physics2DRaycaster>();
        }
            
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Begin");
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position=new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag Ended");
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); 
    }

    void ResetToInitialPosition()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        transform.position = initialPosition;  
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            coi++;
            Destroy(collision.gameObject); 
        }

        if (collision.gameObject.CompareTag("Enamy"))
        {
            H--;
            transform.position = initialPosition;
        }
    }



}

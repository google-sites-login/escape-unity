using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : Damageable{
    public float moveSpeed = 5f;
    public int maxHealth = 20;
    public int currentHealth = 0;
    public Rigidbody2D rb;
    [SerializeField] Transform shootPoint;
    public Camera cam;
    public Animator anim;
    public TMP_Text ammoText;

    [SerializeField]
    GameObject damageEffect;

    public GameObject deathText;
    public GameObject shopMenu;
    [SerializeField] GameObject shopPrefab;

    public Transform itemPos;

    [SerializeField] Color selectedColor;
    [SerializeField] Color unselectedColor;
    [SerializeField] Image[] ImageBackgrounds;
    [SerializeField] Image[] itemImages;

    [SerializeField] Item[] items;
    GameObject currentItem;

    public HealthBar healthBar;
    public int itemIndex;
    int previousItemIndex = -1;

    Vector2 input;
    Vector2 mousePos;

    void Awake(){
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        foreach(Image i in ImageBackgrounds){
            i.color = unselectedColor;
        }
        EquipItem(0);
    }

    void Update(){
        for(int i = 0; i < items.Length; i++){
            if(Input.GetKeyDown((i + 1).ToString())){
                EquipItem(i);
                break;
            }
        }
        if(Input.GetKeyDown(KeyCode.R) && items[itemIndex] != null){
            items[itemIndex].Reload();
        }
        if(items[itemIndex] != null && items[itemIndex].IsHold() && items[itemIndex] != null){
            if(Input.GetMouseButton(0)){
                items[itemIndex].Use();
            }
        }else{
            if(items[itemIndex] != null && Input.GetMouseButtonDown(0)){
                items[itemIndex].Use();
            }else if(items[itemIndex] != null && Input.GetMouseButtonUp(0)){
                items[itemIndex].StopUse();
            }
        }
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        MoveCam();

        if(Input.GetKeyDown(KeyCode.E)){
            if(shopMenu.activeSelf){
                shopMenu.SetActive(false);
            }else{
                Collider2D[] Colliders = Physics2D.OverlapCircleAll(transform.position, 3);
                for(int i = 0; i < Colliders.Length; i++){
                    Debug.Log("0: " + Colliders[i].gameObject);
                    if(Colliders[i].GetComponent<ItemPickup>() != null){
                        Debug.Log("1");
                        PickUpItem(Colliders[i].GetComponent<ItemPickup>().Item, itemIndex);
                        Destroy(Colliders[i].gameObject);
                    }else if(Colliders[i].GetComponent<Shop>() != null){
                        shopMenu.SetActive(true);
                        LoadShop(Colliders[i].GetComponent<Shop>());
                    }else if(Colliders[i].GetComponent<HealthPotion>() != null){
                        currentHealth += Colliders[i].GetComponent<HealthPotion>().healingAmount;
                        healthBar.SetHealth(currentHealth);
                        Destroy(Colliders[i].gameObject);
                    }
                }
            }
        }
        anim.SetFloat("Speed", input.normalized.sqrMagnitude * moveSpeed/5f);
        if(items[itemIndex] != null){
            ammoText.text = items[itemIndex].GetAmmo();
        }else{
            ammoText.text = "";
        }
    }


    void LoadShop(Shop shop){
        foreach(Transform child in shopMenu.transform){
            Destroy(child.gameObject);
        }
        for(int i = 0; i < shop.items.Length; i++){
            GameObject obj = Instantiate(shopPrefab, Vector3.zero, Quaternion.identity, shopMenu.transform);
            obj.GetComponent<ShopDisplay>().Init(shop.items[i]);
        }
    }

    public void PickUpItem(GameObject item, int _index){
        Debug.Log("2");
        if(items[_index] != null){
            Destroy(items[_index].gameObject);
        }
        currentItem = Instantiate(item, itemPos.position, itemPos.rotation, transform);
        if(currentItem.GetComponent<SingleShotGun>() != null){
            currentItem.GetComponent<SingleShotGun>().shootPoint = shootPoint;
        }
        items[_index] = currentItem.GetComponent<Item>();

    }

    void EquipItem(int _index){
        if(_index == previousItemIndex)
            return;

        itemIndex = _index;
        if(items[itemIndex] != null){
            items[itemIndex].gameObject.SetActive(true);
        }
        ImageBackgrounds[itemIndex].color = selectedColor;

        if(previousItemIndex != -1){
            if(items[previousItemIndex] != null){
                items[previousItemIndex].gameObject.SetActive(false);
            }
            ImageBackgrounds[previousItemIndex].color = unselectedColor;
        }

        previousItemIndex = itemIndex;
    }

    void MoveCam(){
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + input.normalized * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - new Vector2(transform.position.x, transform.position.y);
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public override void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        damageEffect.SetActive(true);
        Invoke("DisableEffect", 0.2f);
        if(currentHealth <= 0){
            deathText.SetActive(true);
            Invoke("LeaveGame", 2f);
        }
    }
    void DisableEffect(){
        damageEffect.SetActive(false);
    }

    void LeaveGame(){
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
























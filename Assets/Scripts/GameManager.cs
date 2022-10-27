using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion

    public List<Item> itemList = new List<Item>();
    public List<Item> craftingRecipes = new List<Item>();
    public List<PotionEffects> potionEffectSList = new List<PotionEffects>();

    public Transform canvas;
    public GameObject itemInfoPrefab;
    private GameObject currentItemInfo = null;

    public Transform mainCaanvas;


    public float moveX = 180f;
    public float moveY = 70f;

    public Transform hotbarTransform;
    public Transform inventoryTransform;

    public GameObject massageManager;
    public GameObject massage;

    public GameObject PotionEffectsManager;
    public GameObject PotionEffectsMassage;

    public GameObject Bag;
    public GameObject Player;

    private Item item;
    public GameObject g;
    HealthController HC;

    GameObject g2;
    InventoryUI In_UI;

    IamItem iam;

    public GameObject VisibleWays;
    public GameObject FireEnemy;

    public GameObject open1;
    public GameObject open2;
        public GameObject open6;
        public GameObject Ladder;
        public GameObject apple;
    public GameObject open3;
    public GameObject open4;
    public GameObject open5;
        public GameObject follow_enemy;
    public GameObject open7;

    Scene sceneLoaded;

    public GameObject Panel;
    public Text textMassage;


    private void Start()
    {
        HC = g.GetComponent<HealthController>();
        g2 = GameObject.FindGameObjectWithTag("InventoryUI");
        In_UI = g2.GetComponent<InventoryUI>();
        CollisionManager.ladder_active_false += ladder_false;
        CollisionManager.apple_active += apple_active_2;
        CollisionManager.follow_enemy_active += active_enemy;
    }

    private void Update()
    {
        Open_menu();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2f))
            {

                if (hit.collider.GetComponent<IamItem>())
                {
                    IamItem currentItem = hit.collider.GetComponent<IamItem>();
                    Inventory.instance.AddStackebleItem(itemList[currentItem.ID]);
                    Massage(currentItem.ID);
                    OpenMapByID(currentItem.ID);
                    currentItem.gameObject.SetActive(false);
                }

                else if (hit.collider.GetComponent<IamRecipe>())
                {
                    IamRecipe currentItem = hit.collider.GetComponent<IamRecipe>();
                    string NameRecipe = currentItem.Name;
                    In_UI.SetUpCraftingRecipeONE(NameRecipe);
                    MassageOfRecipe(NameRecipe);
                    OpenMap(NameRecipe);
                    currentItem.gameObject.SetActive(false);
                }
                else if (hit.collider.GetComponent<OpenTheDoor>())
                {
                    bool tmp = OpenTheDoor._instance.isGateOpen;
                    if (tmp == true)
                    {
                        Debug.Log("Door is open");
                        textMassage.text ="WIN!!";
                        Invoke("LoadEndScene", 1);
                    }
                    else {
                        textMassage.text = "You haven't key";
                        Invoke("SetEmpty", 1);
                        Debug.Log("You have no key");
                    }
                }
            }
        }

    }


    public void LoadEndScene()
    {
        SceneManager.LoadScene(2);
    }

    public void SetTextM(string s) {
        textMassage.text = s;
        Invoke("SetEmpty", 1);
    }

    void SetEmpty() {
        textMassage.text = "";
    }

    private void Open_menu()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Panel.SetActive(true);
            ChangeCursorState(false);
        }
    }

    private void ChangeCursorState(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void ladder_false()
    {
        Ladder.SetActive(false);        
    }

    public void apple_active_2() {
        apple.SetActive(true);
    }

    public void active_enemy() {
        follow_enemy.SetActive(true);
    }


    void OpenMap(string name) {
        switch (name) { 
        case "Fire Resistance Potion ": open1.SetActive(true);  break;
        case "Health Potion Recipe": open2.SetActive(true); open6.SetActive(true); break;
        case "Air Resistance Recipe": open4.SetActive(true); break;
        case "Repair business, for dummies": open5.SetActive(true); open7.SetActive(true); break;
            default: Debug.Log("Can't find Open Map"); break;
        }
    }

    void OpenMapByID(int id) {
        if (id == 8) { 
            open3.SetActive(true);
        }
    }


    public void BagSet(Item currentItem)
    {
        iam = Bag.GetComponent<IamItem>();
        int id = itemList.IndexOf(currentItem);
        iam.SetID(id);
        Vector2 PlayerPos = Player.transform.position;
        PlayerPos.x += 0.5f;
        PlayerPos.y -= 1f;
        Instantiate(Bag, new Vector3(PlayerPos.x, PlayerPos.y), Quaternion.identity);
    }

    void PotionEffectMassage(int id, float gameTime) {
        GameObject ptMsg = Instantiate(PotionEffectsMassage);
        ptMsg.transform.SetParent(PotionEffectsManager.transform);
        Image msgImage = ptMsg.transform.GetChild(0).GetComponent<Image>();
        Text msg = ptMsg.transform.GetChild(1).GetComponent<Text>();

        PotionEffects pe = potionEffectSList[id];
        msgImage.sprite = pe.icon;
        msg.text = gameTime.ToString();

        DestroyPotionEffectMassage(ptMsg, gameTime);
        
    }
    void DestroyPotionEffectMassage(GameObject g, float time)
    {
        Destroy(g, time);
    }


    public void Massage(int id)
    {
        GameObject msgObj = Instantiate(massage);
        msgObj.transform.SetParent(massageManager.transform);
        Text msg = msgObj.transform.GetChild(1).GetComponent<Text>();
        Image msgImage = msgObj.transform.GetChild(0).GetComponent<Image>();

        Item currentItem = Inventory.instance.forMassage(itemList[id]);
        msg.text = currentItem.name;
        msgImage.sprite = currentItem.icon;
        DestroyMassage(msgObj);
    }

    void MassageOfRecipe(string NameRecipe) {
        GameObject msgObj = Instantiate(massage);
        msgObj.transform.SetParent(massageManager.transform);
        Text msg = msgObj.transform.GetChild(1).GetComponent<Text>();
        Image msgImage = msgObj.transform.GetChild(0).GetComponent<Image>();

        foreach (Item recipe in craftingRecipes)
        {
            if (recipe.name == NameRecipe)
            {
                msg.text = recipe.name;
                msgImage.sprite = recipe.icon;
                DestroyMassage(msgObj);
            }
        }
    }

    public void MassageOfCraftedItem(string Name)
    {
        GameObject msgObj = Instantiate(massage);
        msgObj.transform.SetParent(massageManager.transform);
        Text msg = msgObj.transform.GetChild(1).GetComponent<Text>();
        Image msgImage = msgObj.transform.GetChild(0).GetComponent<Image>();

        foreach (Item item in itemList)
        {
            if (item.name == Name)
            {
                msg.text ="You crafted: " + item.name;
                msgImage.sprite = item.icon;
                DestroyMassage(msgObj);
            }
        }

    }

    void DestroyMassage(GameObject g)
    {
        Destroy(g, 1);
    }

    public void OnStatItemuUse(StatItemType itemType, int amount)
    {
        Debug.Log("Consuming " + itemType + " Add id: " + amount);
        ForItem(amount);
    }

    public void ForItem(int id)
    {
        switch (id)
        {
            case 0: HC.SetHealth(100f); break; //healsh potion
            case 1: HC.SetHealth(20f); break; //apple
          //  case 3: HC.SetHealth(5f); break; //egg
            case 4: PotionEffectMassage(0, 30f);
                    FireEnemy.SetActive(false);
                    Invoke("FireEnemyTrue", 30f);
                    break; //fire resistance potion
            case 10: PotionEffectMassage(1, 30f);
                    VisibleWays.SetActive(true);
                    Invoke("InvisibleWays", 30f);
                    break; //invisible wat potion
            default: Debug.Log("Error ID Item = " + id); break;
        }

    }

    void InvisibleWays() {
        VisibleWays.SetActive(false);
    }
    void FireEnemyTrue()
    {
        VisibleWays.SetActive(false);
    }

    public void DisplayItemInfo(string itemName, string itemDescription, int count, Vector2 buttonPos)
    {
        if (currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }

        buttonPos.x -= moveX; //180f
        buttonPos.y += moveY; //70f

        currentItemInfo = Instantiate(itemInfoPrefab, buttonPos, Quaternion.identity, canvas);
        currentItemInfo.GetComponent<ItemInfo>().SetUp(itemName, itemDescription, count);

    }

    public void DestroyItemInfo()
    {
        if (currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }
    }

    private void OnDisable()
    {
        CollisionManager.ladder_active_false -= ladder_false;
        CollisionManager.apple_active -= apple_active_2;
        CollisionManager.follow_enemy_active -= active_enemy;

    }
}
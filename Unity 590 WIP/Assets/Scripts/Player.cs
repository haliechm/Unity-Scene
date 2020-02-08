using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    private GameObject winMessageObject;
    private GameObject inventoryMessageObject;
    private GameObject scoreMessageObject;
    public TextMesh winMessage;
    public TextMesh inventoryMessage;
    public TextMesh scoreMessage;

    public Camera oculusCam;

    int coinPointValue;
    int treasureChestPointValue;
    int diamondPointValue;

    int score = 0;

    private Bag inventory;
    private bool inventoryMessageOn;
    private bool scoreMessageOn;

    private int numberOfObjectsCollected;

    private Things coin;
    private Things treasureChest;
    private Things diamond;

    // Start is called before the first frame update
    void Start()
    {
        GameObject coinObj = GameObject.Find("Coin");
        GameObject treasureChestObj = GameObject.Find("Treasure Chest");
        GameObject diamondObj = GameObject.Find("Diamond");

        coin = coinObj.GetComponent<Things>();
        treasureChest = treasureChestObj.GetComponent<Things>();
        diamond = diamondObj.GetComponent<Things>();

        coinPointValue = coin.pointValue;
        treasureChestPointValue = treasureChest.pointValue;
        diamondPointValue = diamond.pointValue;

        GameObject hunter = GameObject.Find("First Player");
        inventory = hunter.GetComponent<Bag>();
        

        scoreMessage.text = "Halie\r\nScore: " + score;

        winMessageObject = GameObject.Find("Win Message");
        winMessage.text = "You Win! You have collected all 20 collectibles";
        winMessageObject.SetActive(false);
        winMessage.color = Color.green;

        inventoryMessageObject = GameObject.Find("Inventory Message");
        inventoryMessage.text = "Inventory Text!";
        inventoryMessageObject.SetActive(false);
        inventoryMessage.color = Color.black;

        inventoryMessageOn = false;
        scoreMessageOn = false;

        scoreMessageObject = GameObject.Find("Score Message");
        scoreMessageObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
         // checks to see if 20 objects have been collected (not sure if this works yet)
    int total = 0;
    foreach (int numOfCollectible in inventory.count) {
        total += numOfCollectible;
    }

    // number of collectibles in scene: 20
    if (total == 20) {
      winMessageObject.SetActive(true);
    }

// used some Raycast code from: https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
    RaycastHit hit;
       
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity )) {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
        } else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }

        if (Input.GetKeyDown("1")) {
            Debug.Log("Key Pressed: 1");
            Debug.Log("Object hit: " + hit.collider.gameObject);

            GameObject hitObject = hit.collider.gameObject;
            Destroy(hitObject);

            Things obj = (hit.collider.gameObject).GetComponent<Things>();

            score = score + obj.pointValue;
            scoreMessage.text = "Halie\r\nScore: " + score;


            switch(obj.pointValue) {
                case 1:
                    inventory.collectibles[0] = coin;
                    inventory.count[0] += 1;
                    break;
                case 10:
                    inventory.collectibles[1] = treasureChest;
                    inventory.count[1] += 1;
                    break;
                case 100:
                    inventory.collectibles[2] = diamond;
                    inventory.count[2] += 1;
                    break;
                default:
                    break;
            }

        updateInventory();

        }

        if (Input.GetKeyDown("2")) {
            Debug.Log("Key Pressed: 2");
            updateInventory();
         
            inventoryMessageOn = !inventoryMessageOn;
            inventoryMessageObject.SetActive(inventoryMessageOn);
        }

        if(Input.GetKeyDown("3")) {
            Debug.Log("Key Pressed: 3");
            scoreMessageOn = !scoreMessageOn;
            scoreMessageObject.SetActive(scoreMessageOn);
        }

        
    }

    void updateInventory() {
            inventoryMessage.text = 
            "Inventory:" + "\r\nCoin: " + inventory.count[0] + "\r\nTreasure Chest: " + inventory.count[1] + "\r\nDiamond: " + inventory.count[2];
            return;
        }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AttachmentRule{KeepRelative,KeepWorld,SnapToTarget}

public class Player : MonoBehaviour
{

    private bool justStarted;
   
    private GameObject sword;
    private GameObject shield;
    private GameObject face;

    private GameObject winMessageObject;
    // private GameObject testerObjectt;
    private GameObject inventoryMessageObject;
    // private GameObject scoreMessageObject;
    private GameObject lightInFront;
    public TextMesh winMessage;
    // public TextMesh testMessage;
    public TextMesh inventoryMessage;
    public TextMesh scoreMessage;
    public TextMesh lightInFrontMessage;

    private bool hasGrabbedShield = false;
    private bool hasGrabbedSword = false;

    private int numThingsCollected;

    // private GameObject testerObject;
    // public TextMesh testerMessage;

    public Camera oculusCam;

    // int coinPointValue;
    // int treasureChestPointValue;
    // int diamondPointValue;

    // int coinPointValueB;
    // int treasureChestPointValueB;
    // int diamondPointValueB;
    // int pearlPointValueB;

    // int score = 0;

    private Bag inventory;
    // private bool inventoryMessageOn;
    // private bool scoreMessageOn;

    // private int numberOfObjectsCollected;

    // private Things coin;
    // private Things treasureChest;
    // private Things diamond;

    // private Things coinB;
    // private Things treasureChestB;
    // private Things diamondB;
    // private Things pearlB;

    private Things scary00;
    private Things scary01;
    private Things scary02;

    public GameObject leftPointerObject;
    public GameObject rightPointerObject;
    public LayerMask collectiblesMask;
    Things thingIGrabbed;
    Vector3 previousPointerPos;

    // Start is called before the first frame update
    void Start()
    {

        justStarted = true;
        // testerObjectt.SetActive(false);
        // GameObject coinObj = GameObject.Find("Coin");
        // GameObject treasureChestObj = GameObject.Find("Treasure Chest");
        // GameObject diamondObj = GameObject.Find("Diamond");

        GameObject scaryObj00 = GameObject.Find("GrabBox00");
        GameObject scaryObj01 = GameObject.Find("GrabBox01");
        GameObject scaryObj02 = GameObject.Find("GrabBox02");

        numThingsCollected = 0;

        sword = GameObject.Find("claymore");
        shield = GameObject.Find("Shield_Mobile");
        face = GameObject.Find("ScaryBox");
        face.SetActive(false);

        // GameObject coinObjB = GameObject.Find("Coin Box");
        // GameObject treasureChestObjB = GameObject.Find("Treasure Chest Box");
        // GameObject diamondObjB = GameObject.Find("Diamond Box");
        // GameObject pearlObjB = GameObject.Find("Pearl Box");

        // coin = coinObj.GetComponent<Things>();
        // treasureChest = treasureChestObj.GetComponent<Things>();
        // diamond = diamondObj.GetComponent<Things>();

        scary00 = scaryObj00.GetComponent<Things>();
        scary01 = scaryObj01.GetComponent<Things>();
        scary02 = scaryObj02.GetComponent<Things>();

        // coinB = coinObjB.GetComponent<Things>();
        // treasureChestB = treasureChestObjB.GetComponent<Things>();
        // diamondB = diamondObjB.GetComponent<Things>();
        // pearlB = pearlObjB.GetComponent<Things>();

        // coinPointValueB = coinB.pointValue;
        // treasureChestPointValueB = treasureChestB.pointValue;
        // diamondPointValueB = diamondB.pointValue;
        // pearlPointValueB = pearlB.pointValue;

        // coinPointValue = coin.pointValue;
        // treasureChestPointValue = treasureChest.pointValue;
        // diamondPointValue = diamond.pointValue;

        GameObject hunter = GameObject.Find("First Player");
        inventory = hunter.GetComponent<Bag>();
        

        // scoreMessage.text = "Halie\r\nScore: " + score;

        lightInFront.SetActive(false);

        // winMessageObject = GameObject.Find("Win Message");
        // winMessage.text = "You Win! You have collected all 20 collectibles";
        

        winMessage = GameObject.Find("Win Message").GetComponent<TextMesh>();
        winMessage.gameObject.SetActive(false);

        scoreMessage = GameObject.Find("Score Message").GetComponent<TextMesh>();

        // winMessageObject.SetActive(false);
        // winMessage.color = Color.green;

        // inventoryMessageObject = GameObject.Find("Inventory Message");
        // inventoryMessage.text = "Inventory Text :|";
        // inventoryMessageObject.SetActive(false);
        // inventoryMessage.color = Color.black;

        // inventoryMessageOn = false;
        // scoreMessageOn = false;

        // scoreMessageObject = GameObject.Find("Score Message");
        // scoreMessage.color = Color.black;
        // scoreMessageObject.SetActive(false);

        // testerMessage.text = "good";
    }

    // Update is called once per frame
    void Update()
    {
        
        if (justStarted) {
            winMessage.gameObject.SetActive(false);
            justStarted = false;
        }
        // FOR TRAP VERSION COMMENT THIS IF STATEMENT OUT
        if (numThingsCollected >= 5) {
            winMessage.gameObject.SetActive(true);
        }
        scoreMessage.text = "# of Items Collected: " + numThingsCollected;
        
// global vector
// camera.transform.position.getY (and then go lower than that to get to fanny pack)

if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger)) {
    // inventoryMessage.text = "You pressed the right grip button!";
    Collider[] overlappingThings = Physics.OverlapSphere(rightPointerObject.transform.position, 0.01f, collectiblesMask);
    // add here to make sure its the thing that we want to be collected (so not the trees)
    if (overlappingThings.Length>0) {
        attachGameObjectToAChildGameObject(overlappingThings[0].gameObject, rightPointerObject, AttachmentRule.KeepWorld, AttachmentRule.KeepWorld, AttachmentRule.KeepWorld, true);
        thingIGrabbed = overlappingThings[0].gameObject.GetComponent<Things>();
        // scoreMessage.text = "thingIGrabbed: \r\n" + thingIGrabbed;
    }
} else if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger)) {
    letGo();
} else if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger)) {
    // inventoryMessage.text = "You pressed the left grip button";
    Collider[] overlappingThings = Physics.OverlapSphere(leftPointerObject.transform.position, 0.01f, collectiblesMask);
    // add here to make sure its the thing that we want to be collected (so not the trees)
    if (overlappingThings.Length>0) {
        attachGameObjectToAChildGameObject(overlappingThings[0].gameObject, leftPointerObject, AttachmentRule.KeepWorld, AttachmentRule.KeepWorld, AttachmentRule.KeepWorld, true);
        thingIGrabbed = overlappingThings[0].gameObject.GetComponent<Things>();
        // scoreMessage.text = "thingIGrabbed: \r\n" + thingIGrabbed;
    }
} else if(OVRInput.GetUp(OVRInput.RawButton.LHandTrigger)) {
    letGo();
}




        
    }

    void letGo(){
        if (thingIGrabbed){
      

                if(rightPointerObject.gameObject.transform.position.y < oculusCam.transform.position.y - 0.2 
                && rightPointerObject.gameObject.transform.position.y > oculusCam.transform.position.y - 0.6
                && rightPointerObject.gameObject.transform.position.x < oculusCam.transform.position.x + 0.3
                && rightPointerObject.gameObject.transform.position.x > oculusCam.transform.position.x - 0.3
                && rightPointerObject.gameObject.transform.position.z < oculusCam.transform.position.z + 0.3
                && rightPointerObject.gameObject.transform.position.z > oculusCam.transform.position.z - 0.3) {
                    
                    // score = score + thingIGrabbed.gameObject.GetComponent<Things>().pointValue;
                    detachGameObject(thingIGrabbed.gameObject,AttachmentRule.KeepWorld,AttachmentRule.KeepWorld,AttachmentRule.KeepWorld);
                    simulatePhysics(thingIGrabbed.gameObject,Vector3.zero,true);
                   
                    // scoreMessage.text = "Halie\r\nScore: " + score;
                    numThingsCollected = numThingsCollected + 1;
                

            // switch(thingIGrabbed.gameObject.GetComponent<Things>().pointValue) {
            //     case 1:
            //         inventory.collectibles[0] = coinB;
            //         inventory.count[0] += 1;
            //         break;
            //     case 10:
            //         inventory.collectibles[1] = treasureChestB;
            //         inventory.count[1] += 1;
            //         break;
            //     case 100:
            //         inventory.collectibles[2] = diamondB;
            //         inventory.count[2] += 1;
            //         break;
            //     case 500:
            //         inventory.collectibles[3] = pearlB;
            //         inventory.count[3] += 1;
            //         break;
            //     default:
            //         break;
            // }

             Destroy(thingIGrabbed.gameObject);

 
            // updateInventory();



                    thingIGrabbed=null;

                } else {
               
                detachGameObject(thingIGrabbed.gameObject,AttachmentRule.KeepWorld,AttachmentRule.KeepWorld,AttachmentRule.KeepWorld);
                simulatePhysics(thingIGrabbed.gameObject,Vector3.zero,true);
                thingIGrabbed=null;
                }
            
        }
    }

    public void attachGameObjectToAChildGameObject(GameObject GOToAttach, GameObject newParent, AttachmentRule locationRule, AttachmentRule rotationRule, AttachmentRule scaleRule, bool weld){
        // if the game objects is the sword, then make enemies appear here
        // inventoryMessage.text = "Grabbed the " + GOToAttach.gameObject;
        if (GOToAttach.gameObject == shield) {
        
            hasGrabbedShield = true;
        }

        if (GOToAttach.gameObject == sword) {
            // inventoryMessage.text = "Got the sword, now jump scare (make face visible)";
            // comment out this line to get rid of trap
           
            hasGrabbedSword = true;

            // FOR NONTRAP VERSION COMMENT THIS LINE OUT
            // face.SetActive(true);
            // now go to make trap door code and delete face/lights from scene
        }
        GOToAttach.transform.parent=newParent.transform;
        handleAttachmentRules(GOToAttach,locationRule,rotationRule,scaleRule);
        if (weld){
            simulatePhysics(GOToAttach,Vector3.zero,false);
        }
    }

     public static void detachGameObject(GameObject GOToDetach, AttachmentRule locationRule, AttachmentRule rotationRule, AttachmentRule scaleRule){
        //making the parent null sets its parent to the world origin (meaning relative & global transforms become the same)
        GOToDetach.transform.parent=null;
        handleAttachmentRules(GOToDetach,locationRule,rotationRule,scaleRule);
    }

    public static void handleAttachmentRules(GameObject GOToHandle, AttachmentRule locationRule, AttachmentRule rotationRule, AttachmentRule scaleRule){
        GOToHandle.transform.localPosition=
        (locationRule==AttachmentRule.KeepRelative)?GOToHandle.transform.position:
        //technically don't need to change anything but I wanted to compress into ternary
        (locationRule==AttachmentRule.KeepWorld)?GOToHandle.transform.localPosition:
        new Vector3(0,0,0);

        //localRotation in Unity is actually a Quaternion, so we need to specifically ask for Euler angles
        GOToHandle.transform.localEulerAngles=
        (rotationRule==AttachmentRule.KeepRelative)?GOToHandle.transform.eulerAngles:
        //technically don't need to change anything but I wanted to compress into ternary
        (rotationRule==AttachmentRule.KeepWorld)?GOToHandle.transform.localEulerAngles:
        new Vector3(0,0,0);

        GOToHandle.transform.localScale=
        (scaleRule==AttachmentRule.KeepRelative)?GOToHandle.transform.lossyScale:
        //technically don't need to change anything but I wanted to compress into ternary
        (scaleRule==AttachmentRule.KeepWorld)?GOToHandle.transform.localScale:
        new Vector3(1,1,1);
    }

     public void simulatePhysics(GameObject target,Vector3 oldParentVelocity,bool simulate){
        Rigidbody rb=target.GetComponent<Rigidbody>();
        if (rb){
            if (!simulate){
                
                Destroy(rb);
            } 
        } else{
            if (simulate){
                
                //there's actually a problem here relative to the UE4 version since Unity doesn't have this simple "simulate physics" option
                //The object will NOT preserve momentum when you throw it like in UE4.
                //need to set its velocity itself.... even if you switch the kinematic/gravity settings around instead of deleting/adding rb
                Rigidbody newRB=target.AddComponent<Rigidbody>();
                newRB.velocity=oldParentVelocity;
            }
        }
    }

    public void updateInventory() {
            // inventoryMessage.text = 
            // "Inventory:" + "\r\nCoin: " + inventory.count[0] + "\r\nTreasure Chest: " 
            // + inventory.count[1] + "\r\nDiamond: " + inventory.count[2] 
            // + "\r\nPearl: " + inventory.count[3] + "\r\nWorth of Items: " + "\r\nCoin: " + 1
            // + "\r\nTreasure Chest: " + 10 + "\r\nDiamond: " + 100 + "\r\nPearl: " + 500;
            return;
        }
}

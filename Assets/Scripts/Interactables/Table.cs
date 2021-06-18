using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    public Globals globals;
    public Orders ordersScribtable;
    [SerializeField]
    private GameObject player;
    Player playerScript;
    [SerializeField]
    private List<Order> orders = new List<Order>();
    [SerializeField]
    private GameObject speechBubbleNumberObject;
    private Image speechBubbleNumber;
    [SerializeField]
    private List<Sprite> numberImages;
    private int happiness;
    private bool playerIsNear;
    private int startTimer;
    private int orderCounter = 0;
    private float orderTimeOut;
    private float showOrderTime;

    //NPC animation
    [SerializeField]
    private List<GameObject> npcGameObjects = new List<GameObject>();
    private List<NPCAnimationController> npcControllers = new List<NPCAnimationController>();

    [SerializeField]
    private List<GameObject> fxObject = new List<GameObject>();
    private List<ParticleSystem> fx = new List<ParticleSystem>();

    //Delegate
    public delegate void MoodDelegate(bool fulfilled, int happiness);
    public MoodDelegate handleMoodChange;
    public delegate void OverAllMoodDelegate(bool positive, int happiness);
    public OverAllMoodDelegate handleOverAllMoodChange;

    //Button Highlight
    [SerializeField]
    private List<GameObject> buttons = new List<GameObject>();
    private List<Image> buttonImages = new List<Image>();

    [SerializeField]
    private List<GameObject> speechBubbleObjects = new List<GameObject>();
    private List<Image> speechBubbleImages = new List<Image>();


    private void Start() {
        playerScript = player.GetComponent<Player>();
        orderTimeOut = ordersScribtable.orderTimeOutSeconds;
        showOrderTime = ordersScribtable.showOrderTime;
        speechBubbleNumber = speechBubbleNumberObject.GetComponent<Image>();
        happiness = globals.defaultHappiness;
        GetNPCScripts();
        GetFXComponents();
        GetButtonImages();
        GetSpeechBubbleImages();
    }


    // PLAYER
    public void ServeItem(string item) {
        if (playerIsNear) {
            Debug.Log("Serving " + item + "...");
            playerScript.StopPlayerMovement();

            if (orders.Count > 0) {
                int temp = orders[0].GetAmountOfBeer();
                //Debug.Log(orders[0].GetAmountOfBeer() + "food: " + orders[0].GetAmountOfFood());

                if (temp > 0) {
                    if (playerScript.hasItem(item)) {
                        playerScript.QueueServeItem(item);
                        temp--;
                        orders[0].SetAmountOfBeer(temp);
                        IsOrderFulfilled();
                    } //else Debug.Log(item + " not in inventory");
                } 
            } else {
                //Debug.Log("No active order");
            }
        } else {
            //Debug.Log("Player is not by " + this.gameObject.name);
        }
    }

    // ORDER STUFF
    public void MakeOrder() {
        orders.Add(GenerateOrder());
        //orders[0].LogOrder();
        //Debug.Log("Orders list size: " + orders.Count);
    }
    private Order GenerateOrder() {
        int nbrOfItems = ordersScribtable.RollNbrOfItems();
        Order newOrder = new Order(nbrOfItems, 0, orderTimeOut, "OrderTimeOut" + orderCounter);
        FunctionTimer.Create(TimeoutAction, orderTimeOut, "OrderTimeOut" + orderCounter);

        ShowOrder(nbrOfItems);
        FunctionTimer.Create(HideOrder, showOrderTime, "HideOrder");

        orderCounter++;
        return newOrder;
    }

    private void IsOrderFulfilled() {
        if (orders != null && orders[0].GetAmountOfBeer() == 0 && orders[0].GetAmountOfFood() == 0) FulfillOrder(0); else Debug.Log("Order not yet fulfilled.");
    }

    private void FulfillOrder(int orderIndex) {
        FunctionTimer.StopTimer(orders[orderIndex].GetOrderName());
        orders.RemoveAt(orderIndex);
        AdjustMood(true);
        
        Debug.Log("Does happiness: " + happiness + " match max happiness: " + globals.maxHappiness + " Bool happiness >= maxHappiness: " + (happiness >= globals.maxHappiness));
        if (happiness >= globals.maxHappiness) {
            PlayParticleEffect("happy");
            PlayParticleEffect("hearts");
            ResetTable(true);
        } else {
            
        }
        Debug.Log("Order fulfilled! Happiness: " + happiness);
    }
    private void TimeoutAction() {
        orders.RemoveAt(0);
        AdjustMood(false);

        PlayParticleEffect("angry");
        PlayParticleEffect("angry");

        if (happiness <= 0) {
            ResetTable(false);
        }
    }

    private void ShowOrder(int amount) {
        foreach (Image image in speechBubbleImages)
        {
            ChangeAlphaValue(image, 1f);
        }

        speechBubbleNumber.sprite = numberImages[amount - 1];
        //speechBubbleNumberObject.transform.parent.gameObject.SetActive(true);
    }
    private void HideOrder() {
        foreach (Image image in speechBubbleImages)
        {
            ChangeAlphaValue(image, 0f);
        }
        //speechBubbleNumberObject.transform.parent.gameObject.SetActive(false);
    }


    //TABLE STUFF=======================================================================

    private void ResetTable(bool positive) {
        Debug.Log("Reset Table: " + "happiness before adjustment" + happiness);
        happiness = globals.defaultHappiness;
        handleOverAllMoodChange(positive, happiness);
        Debug.Log("happiness after adjustment: " + happiness);
    }

    private void AdjustMood(bool fulfilled) {
        if(fulfilled) happiness++;
        else happiness--;
        handleMoodChange(fulfilled, happiness);
        PlayNPCAnimaton(fulfilled);
    }


    //NPC ANIMATION STUFF=======================================================================

    private void GetNPCScripts() {
        if (npcGameObjects.Count > 0) {
            foreach (GameObject npc in npcGameObjects)
            {
                npcControllers.Add(npc.GetComponent<NPCAnimationController>());
            }
        }
    }

    private void GetFXComponents() {
        if (npcGameObjects.Count > 0) {
            foreach (GameObject temp in fxObject)
            {
                fx.Add(temp.GetComponent<ParticleSystem>());
            }
        }
    }

    private void PlayNPCAnimaton(bool happy) {
        if (happy) {
            if (npcControllers.Count > 0) {
                foreach (NPCAnimationController npc in npcControllers) {
                    npc.PlayAnim("isHappy");
                    PlayParticleEffect("happy");
                }
            }
        } else {
            if (npcControllers.Count > 0) {
                foreach (NPCAnimationController npc in npcControllers) {
                    npc.PlayAnim("isAngry");
                    PlayParticleEffect("angry");
                }
            }
        }
    }

    private void PlayParticleEffect(string type) {
        switch (type) {
            case "happy":
                fx[1].Play();
                break;
            case "hearts":
                fx[2].Play();
                break;
            default:
                fx[0].Play();
                break;
        }
    } 

    //BUTTON HIGHLIGHT

    private void GetButtonImages() {
        if (buttons.Count > 0) {
            foreach (GameObject button in buttons)
            {
                buttonImages.Add(button.GetComponent<Image>());
            }
        }
    }

    private void GetSpeechBubbleImages() {
        if (speechBubbleObjects.Count > 0) {
            foreach (GameObject temp in speechBubbleObjects)
            {
                speechBubbleImages.Add(temp.GetComponent<Image>());
            }
        }
    }

    private void OnMouseOver() {
        if (playerIsNear == false) ShowImages(0.5f);
    }

    private void OnMouseExit() {
        if (playerIsNear == false) HideImages();
    }

    private void ShowImages(float alphaValue) {
        if (buttonImages.Count > 0) {
            foreach (Image image in buttonImages)
            {
                ChangeAlphaValue(image, alphaValue);
            }
        }
    }

    private void HideImages() {
        if (buttonImages.Count > 0) {
            foreach (Image image in buttonImages)
            {
                ChangeAlphaValue(image, 0f);
            }
        }
    }

    private void ChangeAlphaValue(Image image, float alphaValue) {
        Color tempColor = image.color;
                tempColor.a = alphaValue;
                image.color = tempColor;
    }

    private void OnTriggerEnter(Collider target) {
        if (target.tag == "Player") {
            playerIsNear = true;
            ShowImages(1f);
        }
    }

    private void OnTriggerExit(Collider target) {
        if (target.tag == "Player") {
            playerIsNear = false;
            HideImages();
        }
    }

    private void SetStartTimer(int time) {
        startTimer = time;
    }

    public int GetHappiness() {
        return happiness;
    }
 }

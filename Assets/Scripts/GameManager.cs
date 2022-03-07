using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //[SerializeField]
    //GameObject ball;

    [SerializeField]
    GameObject InsufficientFunds;

    [SerializeField]
    Text UpgradeCostText;

    [SerializeField]
    Text PurchaseMinerText;


    [SerializeField]
    public Text balance; 

    //[SerializeField]
    //GameObject parent;

    [SerializeField]
    GameObject coinParent;

    [Tooltip("Doge coin prefab")]
    [SerializeField]
    public GameObject dogeCoin;

    public static GameManager Instance;

    List<GameObject> balls = new List<GameObject>();
    int multiplier = 2;
    int upgradeAmount = 2;
    double upgradeCost = 100.00;
    double minerCost = 50.00;

    bool _stopMining = false;

    bool buttonPressed = false;

    public static double DogeCoinValue = 0.1247;

    private Player player { get; set; }
    void Start()
    {
        Instance = this;
        this.player = new Player(new BankAccount("Aloof Inc."));
        UpgradeCostText.text = $"Upgrade\n{upgradeCost.ToString("C")}";
        PurchaseMinerText.text = $"Purchase Miner\n{minerCost.ToString("C")}";
        player.deposit(200);
    }


    // Update is called once per frame
    void Update()
    {
        //if (buttonPressed && balls.Count == 0)
        //    buttonPressed = false;
        //var platform = Application.platform;
        //if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.WindowsEditor)
        //{
        //    if (Input.touchCount > 0)
        //    {
        //        if (Input.GetTouch(0).phase == TouchPhase.Began && !buttonPressed)
        //        {
        //            Vector3 pos = Input.GetTouch(0).position;
        //            pos.z = 10;
        //            //GameObject go = Instantiate(ball);
        //            float h = Random.Range(0, 360);
        //            Color color = Random.ColorHSV(0, 1);
        //            color.a = 100;
        //            Debug.Log($"Color: {color.r}, {color.g}, {color.b} from Hue: {h}");
        //            SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
        //            renderer.color = color;
        //            go.transform.position = Camera.main.ScreenToWorldPoint(pos);
        //            balls.Add(go);
        //        }
        //    }
        //}
    }

    public void generateDogeCoin()
    {
        double random = System.Math.Round(new System.Random().NextDouble(), 5, System.MidpointRounding.AwayFromZero);
        double amount = random * multiplier;
        //Debug.Log($"Giving {player.getBankAccount().getName()} {amount} dogecoin which is ${amount * 0.1247}");
        player.deposit(amount * 0.1247);
        dropDogeCoin(dogeCoin);
    }

    public static void dropDogeCoin(GameObject coin)
    {
        Destroy(Instantiate(coin, Instance.coinParent.transform), 3f);
    }

    public void upgrade()
    {
        if(player.getBankAccount().getBalance().getIntBalance() >= new System.Numerics.BigInteger(upgradeCost))
        {
            multiplier += upgradeAmount;
            upgradeMiners();
            upgradeAmount = multiplier / 2;
            player.withdraw(upgradeCost);
            upgradeCost *= 1.25;
            UpgradeCostText.text = $"Upgrade\n{upgradeCost.ToString("C")}";
            return;
        }
        else
        {
            StartCoroutine(InsufficientFundsMsg());
        }
    }

    public void upgradeMiners()
    {
        if (player.getMiners().Count == 0)
            return;
        foreach(Miner miner in player.getMiners())
        {
            miner.setMultiplier(multiplier);
        }
    }

    public void purchase()
    {
        if (player.getBankAccount().getBalance().getIntBalance() >= new System.Numerics.BigInteger(minerCost))
        {
            player.withdraw(minerCost);
            minerCost *= 1.5;
            PurchaseMinerText.text = $"Purchase Miner\n{minerCost.ToString("C")}";
            if (player.getMiners().Count == 0)
            {
                StartCoroutine(StartMining());
            }
            player.addMiner(new Miner(multiplier));
            return;
        }
        else
        {
            StartCoroutine(InsufficientFundsMsg());
        }
    }

    public static void updateBalance(BankAccount account, Text balance)
    {
        balance.text = $"Balance: {account.getBalanceString()}";
        
    }

    IEnumerator StartMining()
    {
        while(!_stopMining)
        {
            Debug.Log($"Running {player.getMiners().Count} miner(s) with a yield of {player.runMiners()} dogecoins");
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator InsufficientFundsMsg()
    {
        InsufficientFunds.SetActive(true);
        yield return new WaitForSeconds(3);
        InsufficientFunds.SetActive(false);
    }

    public void deleteBalls()
    {
        buttonPressed = true;
        Debug.Log($"Balls: {balls.Count}");
        if (balls.Count == 0)
            return;
        foreach(GameObject go in balls)
        {
            Destroy(go);
        }
        balls.Clear();
    }

}

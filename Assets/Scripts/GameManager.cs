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
    Text balance; 

    //[SerializeField]
    //GameObject parent;

    [SerializeField]
    GameObject coinParent;

    [Tooltip("Doge coin prefab")]
    [SerializeField]
    GameObject dogeCoin;

    List<GameObject> balls = new List<GameObject>();
    int multiplier = 2;
    int upgradeAmount = 2;
    double cost = 100.00;

    bool buttonPressed = false;
    // Start is called before the first frame update

    private BankAccount player { get; set; }
    void Start()
    {
        player = new BankAccount("Aloof Inc.");
        UpgradeCostText.text = $"Upgrade\n{cost.ToString("C")}";
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
        Debug.Log($"Giving {player.getName()} {amount} dogecoin which is ${amount*0.1247}");
        player.addMoney(amount*0.1247);
        updateBalance();
        Destroy(Instantiate(dogeCoin, coinParent.transform), 3f);
    }

    public void upgrade()
    {
        if(player.getBalance().getIntBalance() >= new System.Numerics.BigInteger(cost))
        {
            multiplier += upgradeAmount;
            upgradeAmount = multiplier / 2;
            player.addMoney(-cost);
            cost *= 1.25;
            updateBalance();
            UpgradeCostText.text = $"Upgrade\n{cost.ToString("C")}";
            return;
        }
        else
        {
            StartCoroutine(InsufficientFundsMsg());
        }
    }

    public void updateBalance()
    {
        balance.text = $"Balance: {player.getBalanceString()}";
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

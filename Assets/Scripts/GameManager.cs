using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject ball;

    [SerializeField]
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting game...");
    }

    // Update is called once per frame
    void Update()
    {
        var platform = Application.platform;
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer || platform == RuntimePlatform.WindowsEditor)
        {
            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Vector3 pos = Input.GetTouch(0).position;
                    pos.z = 10;
                    GameObject go = Instantiate(ball, parent.transform);
                    float h = Random.Range(0, 360);
                    Color color = Random.ColorHSV(0, 1);
                    color.a = 100;
                    Debug.Log($"Color: {color.r}, {color.g}, {color.b} from Hue: {h}");
                    SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();
                    renderer.color = color;
                    go.transform.position = Camera.main.ScreenToWorldPoint(pos);
                }
            }
        }
    }

}

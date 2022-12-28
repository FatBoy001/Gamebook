using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIcontrol : MonoBehaviour
{
    public GameObject canvas;

    public Text bossHealth;
    public RectTransform bossHealthbar;

    public GameObject PlayerHealth;
    public GameObject heartLossEffect;
    public GameObject heartFull;
    public GameObject heartLost;
    public GameObject scoredBord;
    public TextMeshProUGUI endTimeText;
    public TextMeshProUGUI scoreText;
    private List<GameObject> heartRed = new List<GameObject>();

    public void Start()
    {
        initialPlayerHeartUI();
    }

    private void initialPlayerHeartUI() 
    {
        //這個true false決定了是否有要繼承parent的數值
        for (int i=0; i<GameManager.instance.player.health;i++)
        {
            heartRed.Add(Instantiate(heartFull, Vector2.zero, Quaternion.identity));
            heartRed[i].transform.SetParent(canvas.transform, false);

            if (i == 0)
                continue;

            heartRed[i].transform.localPosition = heartRed[i - 1].transform.localPosition + new Vector3(64, 0, 0);
        }
    }

    public void healthBarUpdate()
    {
        bossHealth.text = GameManager.instance.boss.health.ToString();
        float ratio = (float)GameManager.instance.boss.health / (float)GameManager.instance.boss.maxHealth;
        bossHealthbar.localScale = new Vector3(ratio, 1, 1);
    }
    public void playerHeartUpdate()
    {
        if (GameManager.instance.player.health <0) return;

        GameObject blackHeart = Instantiate(heartLost, heartRed[GameManager.instance.player.health].transform);
        blackHeart.transform.SetParent(canvas.transform, false);
        blackHeart.transform.localPosition = heartRed[GameManager.instance.player.health].transform.localPosition;
        Destroy(heartRed[GameManager.instance.player.health]);   
    }
    public void endGameScoredBord()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static float COOLDOWN_TIME = 3;
    private float cooldownTimeCounting= COOLDOWN_TIME;
    private readonly string urlAddRecord = URL.AddGameRecord;
    public Player player;
    public Boss boss;
    public UIcontrol UI;
    public bool gameEnd = false;
    private double gameStartTime;
    public string gameEndTime;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            return;
        }
        gameStartTime = Time.fixedTime;
        instance = this;
    }

    public Test test;
    public BoxSpawner boxSpawner;
    

    private void Start()
    {
        test.createTest();
        boxSpawner.spawnBox();
    }

    private void Update()
    {
        if (gameEnd) return;
        if (boss.alive == false|| player.alive==false)
        {
            gameEnd = true;
            UI.scoredBord.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            UI.scoredBord.GetComponent<Animator>().SetBool("EndGame",true);
            double time = Time.fixedTime- gameStartTime;
            int min = (int)time / 60;
            int sec = (int)time % 60;
            string gameEndTime = (min < 10 ? $"0{min}" : $"{min}") + "：" + (sec < 10 ? $"0{sec}" : $"{sec}");
            int score = gradeEndGame((int)time, player.health);

            UI.endTimeText.text = "："+ gameEndTime;
            UI.scoreText.text = "："+ score.ToString();

            StartCoroutine(addRecord(gameEndTime, score));
            return;
        }
        //test.checkAnswer()==true && 
        if (test.testLockUp ==false)
        {
            test.lockDownAnswer();
            /*
            test.distroyTest();
            boxSpawner.destroyBox();

            //時間暫停的方法 StartCoroutine(ExampleCoroutine());

            test.createTest();
            boxSpawner.spawnBox();
            */
        }
        if (test.testLockUp)
        {
            if (Mathf.FloorToInt(cooldownTimeCounting) > 0)
            {
                cooldownTimeCounting -= Time.deltaTime;
                Debug.Log("time (Second):" + Mathf.FloorToInt(cooldownTimeCounting));
                boxSpawner.destroyBox();
            }
            else
            {
                if (test.checkAnswer() == true)
                {
                    cooldownTimeCounting = COOLDOWN_TIME;
                    test.distroyTest();
                    test.createTest();
                    boxSpawner.spawnBox();
                    test.testLockUp = false;
                    boss.SendMessage("getDamage", new Damage() { health = 10 });
                    UI.healthBarUpdate();
                }
                else
                {
                    cooldownTimeCounting = COOLDOWN_TIME;
                    test.distroyTest();
                    test.createTest();
                    boxSpawner.spawnBox();
                    test.testLockUp = false;
                    player.SendMessage("getDamage", new Damage() { health = 1 });
                    UI.healthBarUpdate();
                }
            }
        }
    }
    private int gradeEndGame(int time,int health)
    {
        if (health==0) return 0;
        /*
         * 1:20為最高分4:00為0分
         * (80,100)->(240,0)
         * y=-0.625x+150
         */
        double score = time;
        if (score < 80)
            score = 100;
        else
            score = -0.625 * score + 150;

        switch (health)
        {
            case(1):
                //剩一血
                return (int)(score * 0.6);
            case (2):
                //扣一滴
                return (int)(score * 0.75);
            case (3):
                //滿血
                return (int)(score);
            default:
                return 0;
        }
    }
    //StartCoroutine(TryLogin());

    private IEnumerator addRecord(string time, int score)
    {
        string game = "mathDungeon";
        WWWForm form = new WWWForm();
        form.AddField("rStudent_oid", StudentData.getStudentId());
        form.AddField("rGame", game);
        form.AddField("rType", "Math");
        form.AddField("rEndTime", time);
        form.AddField("rFinish", "");
        form.AddField("rScore", score);
        //post檔案給node.js
        UnityWebRequest request = UnityWebRequest.Post(urlAddRecord, form);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;
            if (startTime > 10.0f)
            {
                break;
            }
            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)//僅代表成功連接
        {
            //node.js回傳的res,不會回傳資料而是有沒有成功code=0就是有找到
            Record response = JsonUtility.FromJson<Record>(request.downloadHandler.text);  
            if (response.code == 1) // login success?
            {
                Debug.Log("add it!");
            }
            else
            {
                Debug.Log("fail");
            }
        }
        else
        {
            Debug.Log("fail");
        }
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets;


public class GameController : MonoBehaviour
{
    //private GameObject[] 
    [SerializeField] private Text winTxt;
    [SerializeField] private Text remTxt;
    [SerializeField] private Text trashTxt;
    [SerializeField] private Text scoreTxt;
    [SerializeField] private Text moneyTxt;
    private GameObject[] audios;
    public int trashCount { private set; get; }
    public AudioSource currAudio { private set; get; }

    private int _money = 0;
    public int money {
        private set {
            moneyTxt.text = value.ToString();
            _money = value;
        }
        get => _money;
    }
    private int _score = 0;
    public int score {
        private set {
            scoreTxt.text = value.ToString();
            _score = value;
        }
        get => _score;
    }

    public enum Audio {
        JUMP = 0,
        COLLECT = 1,
        THROW = 2,
        WIN = 3,
        BG = 4,
    }

    public enum Throw {
        REGULAR,
        CORRECT,
        RECYCLE,
    }

    void Start()
    {
        //trashCount = GameObject.FindGameObjectsWithTag("Collectible").countIf(it => it.activeInHierarchy);
        trashCount = GameObject.FindGameObjectsWithTag(TrashController.STR_ORGANIC).countIf(it => it.activeInHierarchy);
        trashCount += GameObject.FindGameObjectsWithTag(TrashController.STR_ANORGANIC).countIf(it => it.activeInHierarchy);
        trashCount += GameObject.FindGameObjectsWithTag(TrashController.STR_RECYCLABLE).countIf(it => it.activeInHierarchy);
        audios = GameObject.FindGameObjectsWithTag("Audio");
        remTxt.text = trashCount.ToString();

        score = 0;
        money = 0;

        //print("trashCount= " + trashCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decTrash() {
        if (--trashCount <= 0) {
            winTxt.gameObject.SetActive(true);
            playAudio(GameController.Audio.WIN);
            playAudio(GameController.Audio.BG, false);
            //print("winTxt.enabled= " + winTxt.enabled);
            remTxt.gameObject.SetActive(false);
            trashTxt.gameObject.SetActive(false);
        }
        remTxt.text = trashCount.ToString();
        //print("decTrash.trashCount= " + trashCount);
    }

    public void playAudio(Audio a, bool play = true) {
        var name = "";
        switch (a) {
            case Audio.JUMP:
                name = "Audio_Jump";
                break;
            case Audio.COLLECT:
                name = "Audio_Collect";
                break;
            case Audio.THROW:
                name = "Audio_Throw";
                break;
            case Audio.WIN:
                name = "Audio_Win";
                break;
            case Audio.BG:
                name = "Audio_Bg";
                break;
        }
        foreach(var obj in audios) {
            if (obj.name == name) {
                currAudio = obj.GetComponent<AudioSource>();
                if(play) currAudio.Play();
                else currAudio.Stop();
            }
        }
    }

    public void throwTrash(Throw mode = Throw.REGULAR) {
        switch (mode) {
            case Throw.REGULAR:
                score += 1;
                break;
            case Throw.CORRECT:
                score += 5;
                break;
            case Throw.RECYCLE:
                score += 3;
                money += 2;
                break;
        }
        decTrash();
        playAudio(Audio.THROW);
    }
}

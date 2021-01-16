using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //private GameObject[] 
    [SerializeField] private Text winTxt;
    private GameObject[] audios;
    public int trashCount { private set; get; }
    public AudioSource currAudio { private set; get; }

    public enum Audio {
        JUMP = 0,
        COLLECT = 1,
        THROW = 2,
        WIN = 3,
        BG = 4,
    }

    void Start()
    {
        trashCount = GameObject.FindGameObjectsWithTag("Collectible").Length;
        audios = GameObject.FindGameObjectsWithTag("Audio");
        print("trashCount= " + trashCount);
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
            print("winTxt.enabled= " + winTxt.enabled);
        }
        print("decTrash.trashCount= " + trashCount);
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
}

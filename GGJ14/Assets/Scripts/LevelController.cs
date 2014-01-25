using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{

    public GameObject playerPrefab;
    public TextAsset[] levelFiles;
    public string[] levelMusic;

    public GameObject levelGenPrefab;


    GameObject player;

    int currentLevel = 0;

    GameObject currentLevelGeneratorGO;

    MusicController music;

    // Use this for initialization
    void Awake ()
    {
        music = GetComponent<MusicController> ();

        player = Instantiate (playerPrefab) as GameObject;

        Camera.main.GetComponent<CameraFollow> ().target = player.transform;

        StartLevel (currentLevel++);
    }

    void StartLevel(int level)
    {
        currentLevelGeneratorGO = Instantiate (levelGenPrefab) as GameObject;
        LevelGenerator levelGen = currentLevelGeneratorGO.GetComponent<LevelGenerator> ();
        
        levelGen.level = levelFiles[level];
        music.SwitchTrack (levelMusic [level]);
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (player.transform.localPosition.y < -5) {
            Destroy( currentLevelGeneratorGO);
            if (currentLevel < levelFiles.Length)
            {
                StartLevel(currentLevel++);
                player.transform.localPosition = Vector3.up * 2;
            }
            else
            {
                //win condition
            }
        }
    }
}

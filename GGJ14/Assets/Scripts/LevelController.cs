using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{

    public GameObject playerPrefab;
    public TextAsset[] levelFiles;
    public string[] levelMusic;
    public Color[] levelColors;

    public GameObject levelGenPrefab;

    public Light tintingLight;

    GameObject player;

    int currentLevel = 0;

    LevelGenerator currentLevelGenerator;

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
        GameObject LevelGeneratorGO = Instantiate (levelGenPrefab) as GameObject;
        currentLevelGenerator = LevelGeneratorGO.GetComponent<LevelGenerator> ();
        
        currentLevelGenerator.level = levelFiles[level];
        music.SwitchTrack (levelMusic [level]);
        tintingLight.color = levelColors [level];
    }
    
    // Update is called once per frame
    void Update ()
    {
        if (player.transform.localPosition.y < -5) {

            if (currentLevel < levelFiles.Length)
            {
                currentLevelGenerator.CleanUp();
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

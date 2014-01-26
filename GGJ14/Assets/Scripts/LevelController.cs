using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{

    public static LevelController instance;

    public GameObject playerPrefab;
    public TextAsset[] levelFiles;
    public string[] levelMusic;
    public Color[] levelColors;
    public int[] levelSkills;

    public bool reachedEnd = false;

    public GameObject levelGenPrefab;

    public Light tintingLight;

    GameObject player;

    int currentLevel = 0;

    LevelGenerator currentLevelGenerator;

    MusicController music;

    Vector3 playerStartPosition = new Vector3(0, 2);

    bool credits = false;

    // Use this for initialization
    void Start ()
    {
        instance = this;

        music = GetComponent<MusicController> ();

        player = Instantiate (playerPrefab, playerStartPosition, Quaternion.identity) as GameObject;

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

        PlayerControl playerC = player.GetComponent<PlayerControl> ();

        int skills = levelSkills [level];
        playerC.canJump = Utility.BitIsSet (skills, 0);
        playerC.canSmash = Utility.BitIsSet (skills, 1);
        playerC.canPay = Utility.BitIsSet (skills, 2);
        playerC.canDisappear = Utility.BitIsSet (skills, 3);
    }
    
	void ResetInstructions(){
		GameObject.FindGameObjectWithTag ("player").SendMessage ("Reset", currentLevel);
	}

    // Update is called once per frame
    void Update ()
    {
        if (player.transform.localPosition.y < -5) {
            if (reachedEnd)
            {
                if (currentLevel < levelFiles.Length)
                {
                    currentLevelGenerator.CleanUp();
                    StartLevel(currentLevel++);
                    player.transform.localPosition = playerStartPosition;
    				ResetInstructions();

                    reachedEnd = false;
                }
                else
                {
                    Camera.main.GetComponent<CameraFollow>().enabled = false;
                    credits = true;

                }

            }
            else{
                currentLevelGenerator.ResetLevel();
                player.transform.localPosition = playerStartPosition;
            }
        }
    }

    void OnGUI()
    {
        if (credits) {
            GUI.Label(new Rect(Screen.width/2 - 150, 200, 300, 300), "END");

        }
    }
}

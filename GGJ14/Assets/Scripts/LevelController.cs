using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{

    public static LevelController instance;

    public GameObject playerPrefab;
	public GameObject colourPlayerPrefab;

    public TextAsset[] levelFiles;
    public string[] levelMusic;
    public Color[] levelColors;
    public int[] levelSkills;

    public bool reachedEnd = false;

    public GameObject levelGenPrefab;

    public Light tintingLight;
	
	GameObject player;
	PlayerControl playerControl;

    int currentLevel = 0;

    LevelGenerator currentLevelGenerator;

    MusicController music;

    Vector3 playerStartPosition = new Vector3(0, 2);

    bool credits = false;
	
	GUIStyle instructionStyle;

    // Use this for initialization
    void Start ()
    {
		instructionStyle = new GUIStyle ();
		instructionStyle.fontSize = 24; 

        instance = this;

        music = GetComponent<MusicController> ();

        player = Instantiate (playerPrefab, playerStartPosition, Quaternion.identity) as GameObject;
		playerControl = player.GetComponent<PlayerControl>();

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

        int skills = levelSkills [level];
		playerControl.canJump = Utility.BitIsSet (skills, 0);
		playerControl.canSmash = Utility.BitIsSet (skills, 1);
		playerControl.canPay = Utility.BitIsSet (skills, 2);
		playerControl.canDisappear = Utility.BitIsSet (skills, 3);
    }
    
	void ResetInstructions(){
		playerControl.Reset();
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
					if(currentLevel == 5){
						Destroy(player);
						player = Instantiate(colourPlayerPrefab, playerStartPosition, Quaternion.identity) as GameObject;
						playerControl = player.GetComponent<PlayerControl>();
					}
					else{
						player.transform.localPosition = playerStartPosition;
					}
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
		GUI.color = Color.white;
        if (credits) {
			GUI.Label(new Rect(Screen.width / 2.0f - 40, 200, 300, 300), "END", instructionStyle);

        }

		GUI.color = Color.white;
		if (!playerControl.stateChange) {
			if (currentLevel == 1)
				GUI.Label (new Rect (Screen.width * 0.4f, Screen.height * 0.25f, 200, 100), "Press J to Jump", instructionStyle);
			if (currentLevel == 2)
				GUI.Label (new Rect (Screen.width * 0.4f, Screen.height * 0.25f, 200, 100), "Press H to Smash", instructionStyle);
			if (currentLevel == 3)
				GUI.Label (new Rect (Screen.width * 0.4f, Screen.height * 0.25f, 200, 100), "Press K to Insert Coin", instructionStyle);
			if (currentLevel == 4)
				GUI.Label (new Rect (Screen.width * 0.4f, Screen.height * 0.25f, 200, 100), "Press L to Disappear", instructionStyle);
			if (currentLevel == 5)
				GUI.Label (new Rect (Screen.width * 0.4f, Screen.height * 0.15f, 200, 400), "Press J to Jump \nPress H to Smash \nPress K to Insert Coin \nPress L to Disappear", instructionStyle);
			
			
		}
    }
}

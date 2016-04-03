using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Filename: LevelLoader.cs \n
 * Author: Michael Gonzalez \n
 * Date Drafted: 12/17/2015 \n
 * Description: This class wraps the default Unity Application.LoadLevel() 
 *              functions. This class will help during development and 
 *              deployment when scene indexes change or are added by giving
 *              a centralized location where all indexes are declared and 
 *              refrenced. This class has integer constants to represent
 *              a scene's real index, enumerators to refer to the levels and 
 *              worlds, and methods to actually load the levels.
 */
public class LevelLoader : MonoBehaviour {

    private const int INTRO = 0;
    private const int MAIN_MENU = 1;
    private const int SPACE_1 = 2;
    private const int SPACE_2 = 3;
    private const int SPACE_3 = 4;
    private const int SPACE_4 = 5;
    private const int SPACE_5 = 6;
    private const int SPACE_6 = 7;
    private const int SPACE_7 = 8;
    private const int SPACE_8 = 9;
    private const int SPACE_9 = 10;
    private const int SPACE_BOSS = 11;
    private const int WATER_1 = 12;
    private const int WATER_2 = 13;
    private const int WATER_3 = 14;
    private const int WATER_4 = 15;
	private const int WATER_5 = 16;
	private const int WATER_6 = 17;
    private const int WATER_7 = 18;
    private const int WATER_8 = 19;
    private const int WATER_BOSS = 20;
    private const int SPACE_BONUS = 21;

    public World world;
    public Level level;

    public void LoadLevel()
    {
        LoadLevel(world, level);
    }
    /**
     * Function Name: LoadLevel() \n
     * This function will load Level level of World world
     */
    public static void LoadLevel( World world, Level level)
    {
        switch (world)
        {
            case World.Space:
                LoadSpaceLevel(level);
                Physics2D.gravity = -9.81f * Vector2.up;
                break;
            case World.Water:
                LoadWaterLevel(level);
                Physics2D.gravity = -9.81f * Vector2.up;
                break;
            case World.MainMenu:
                SceneManager.LoadScene(MAIN_MENU);
                break;
            default:
                Debug.LogError("Something horrible happened when trying to load world: " + world);
                break;
        }
    }
    public static LevelInfo GetLevelInfo( int scene)
    {
        switch (scene)
        {
            case SPACE_1:
                return new LevelInfo(World.Space, Level.One);
            case SPACE_2:
                return new LevelInfo(World.Space, Level.Two);
            case SPACE_3:
                return new LevelInfo(World.Space, Level.Three);
            case SPACE_4:
                return new LevelInfo(World.Space, Level.Four);
            case SPACE_5:
                return new LevelInfo(World.Space, Level.Five);
            case SPACE_6:
                return new LevelInfo(World.Space, Level.Six);
            case SPACE_7:
                return new LevelInfo(World.Space, Level.Seven);
            case SPACE_8:
                return new LevelInfo(World.Space, Level.Eight);
            case SPACE_9:
                return new LevelInfo(World.Space, Level.Nine);
            case SPACE_BOSS:
                return new LevelInfo(World.Space, Level.Boss);
            case WATER_1:
                return new LevelInfo(World.Water, Level.One);
            case WATER_2:
                return new LevelInfo(World.Water, Level.Two);
            case WATER_3:
                return new LevelInfo(World.Water, Level.Three);
            case WATER_4:
                return new LevelInfo(World.Water, Level.Four);
            case WATER_5:
                return new LevelInfo(World.Water, Level.Four);
            case WATER_6:
                return new LevelInfo(World.Water, Level.Four);
            case WATER_7:
                return new LevelInfo(World.Water, Level.Four);
			case WATER_8:
                return new LevelInfo(World.Water, Level.Four);
            case WATER_BOSS:
                return new LevelInfo(World.Water, Level.Four);
            case SPACE_BONUS:
                return new LevelInfo(World.Space, Level.Bonus);
            case MAIN_MENU:
                return new LevelInfo(World.MainMenu, Level.Bonus);
            case INTRO:
                return new LevelInfo(World.Intro, Level.Bonus);
        }
        return null;
    }
    public static void LoadSpaceLevel( Level level )
    {
        switch( level )
        {
            case Level.One:
                SceneManager.LoadScene(SPACE_1);
                break;
            case Level.Two:
                SceneManager.LoadScene(SPACE_2);
                break;
            case Level.Three:
                SceneManager.LoadScene(SPACE_3);
                break;
            case Level.Four:
                SceneManager.LoadScene(SPACE_4);
                break;
            case Level.Five:
                SceneManager.LoadScene(SPACE_5);
                break;
            case Level.Six:
                SceneManager.LoadScene(SPACE_6);
                break;
            case Level.Seven:
                SceneManager.LoadScene(SPACE_7);
                break;
            case Level.Eight:
                SceneManager.LoadScene(SPACE_8);
                break;
            case Level.Nine:
                SceneManager.LoadScene(SPACE_9);
                break;
            case Level.Boss:
                SceneManager.LoadScene(SPACE_BOSS);
                break;
            case Level.Bonus:
                SceneManager.LoadScene(SPACE_BONUS);
                break;
            default:
                Debug.LogError("Tried to load a Space level that has not been implemented!");
                break;
        }
    }

    public static void LoadWaterLevel(Level level)
    {
        switch (level)
        {
            case Level.One:
                SceneManager.LoadScene(WATER_1);
                break;
            case Level.Two:
                SceneManager.LoadScene(WATER_2);
                break;
            case Level.Three:
                SceneManager.LoadScene(WATER_3);
                break;
            case Level.Four:
                SceneManager.LoadScene(WATER_4);
                break;
			case Level.Five:
				SceneManager.LoadScene (WATER_5);
				break;
			case Level.Six:
				SceneManager.LoadScene (WATER_6);
				break;
            case Level.Seven:
                SceneManager.LoadScene(WATER_7);
                break;
			case Level.Eight:
                SceneManager.LoadScene(WATER_8);
                break;
            case Level.Boss:
                SceneManager.LoadScene(WATER_BOSS);
                break;
            case Level.Bonus:
                SceneManager.LoadScene(SPACE_BONUS);
                break;
            default:
                Debug.LogError("Tried to load a Water level that has not been implemented!");
                break;
        }
    }

    // Used for stage panel generator.
    public void setLevel( World world, Level level){
		this.world = world;
		this.level = level;
	}

	public static Level intToLevel(int levelInt){
		switch (levelInt) {
		case 1:
			return Level.One;
		case 2:
			return Level.Two;
		case 3:
			return Level.Three;
		case 4:
			return Level.Four;
		case 5:
			return Level.Five;
		case 6:
			return Level.Six;
		case 7:
			return Level.Seven;
		case 8:
			return Level.Eight;
		case 9:
			return Level.Nine;
		default:
			return Level.One;
		}
	}
}

public enum World { Space, Water, MainMenu, Intro }
public enum Level { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Boss, Bonus }
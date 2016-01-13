using UnityEngine;
using System.Collections;

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

    private const int SPACE_2 = 2;
    private const int SPACE_3 = 3;
    private const int SPACE_4 = 4;
    private const int SPACE_BOSS = 5;
    private const int SPACE_BONUS = 1;

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
                break;
            default:
                Debug.LogError("Something horrible happened when trying to load world: " + world);
                break;
        }
    }
    public static void LoadSpaceLevel( Level level )
    {
        switch( level )
        {
            case Level.Two:
                Application.LoadLevel(SPACE_2);
                break;
            case Level.Three:
                Application.LoadLevel(SPACE_3);
                break;
            case Level.Four:
                Application.LoadLevel(SPACE_4);
                break;
            case Level.Boss:
                Application.LoadLevel(SPACE_BOSS);
                break;
            case Level.Bonus:
                Application.LoadLevel(SPACE_BONUS);
                break;
            default:
                Debug.LogError("Tried to load a Space level that has not been implemented!");
                break;
        }
    }
}

public enum World { Space }
public enum Level { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Boss, Bonus }
public enum Menu { Main, WorldSelection }
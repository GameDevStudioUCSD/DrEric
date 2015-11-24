using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterTable {

    private static CharacterTable instance;

    public static CharacterTable Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CharacterTable();
            }
            return instance;
        }
    }

    private IDictionary<string, Character> characters;
    private const string characterJsonPath = "characters/characterdata";

    private CharacterTable()
    {
        characters = new Dictionary<string, Character>();
        buildTable();
    }

    private Dialog.Expression translateEnum(string expression)
    {
        if (expression.ToLower().Equals("happy")) return Dialog.Expression.HAPPY;
        else if (expression.ToLower().Equals("neutral")) return Dialog.Expression.NEUTRAL;
        else if (expression.ToLower().Equals("sad")) return Dialog.Expression.SAD;

        return Dialog.Expression.NEUTRAL; //make default error expression?
    }

    private void buildTable()
    {
        TextAsset jsonAsset = Resources.Load(characterJsonPath) as TextAsset;
        string json = jsonAsset.text;

        JSObject parsed = JSONParser.parse(json);
        JSObject characters_raw = parsed["characters"];

        foreach (string character in characters_raw.Keys)
        {
            Character obj = new Character(character);
            JSObject portraits = characters_raw[character]["portraits"];
            foreach (string portrait in portraits.Keys)
            {
                Rect rect = new Rect(0, 0, 100, 100);
                Vector2 vec = new Vector2(0, 0);
                obj.addExpression(translateEnum(portrait)
                                , Sprite.Create(Resources.Load(portraits[portrait]) as Texture2D, rect, vec));
            }
            characters.Add(character, obj);
        }
    }

    public Character getCharacter(string name)
    {
        return characters[name];
    }
}

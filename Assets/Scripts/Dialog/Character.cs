using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character
{
    private string name;
    private IDictionary<Dialog.Expression, Sprite> portraits;

    public Character(string name)
    {
        this.name = name;
        portraits = new Dictionary<Dialog.Expression, Sprite>();
    }

    public void addExpression(Dialog.Expression expression, Sprite portrait)
    {
        portraits.Add(expression, portrait);
    }

    public Sprite getPortrait(Dialog.Expression expression)
    {
        if (!portraits.ContainsKey(expression))
        {
            Debug.Log("No portrait assigned to expression" + expression 
                + "for character " + name + ".");
            return null;
        }

        return portraits[expression];
    }
}
#if UNITY_2 || UNITY_3 || UNITY_4 || UNITY_5 || UNITY_6 || UNITY_7
using UnityEngine;
using Debug = UnityEngine.Debug;
#endif

using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// <para>A dynamic type implementation for JavaScript style objects and primitives in Unity C# (with no System.dynamic support) 
/// 
///       Implements some functionality of a dynamic type with the class JSObject since Unity does not support dynamic types. 
///       Designed to work with JSONParser (JSONObject parser specifically) as is, but is expandable.
/// 
///       Supports string, float, int, bool, List{JSObject} and List{string, JSObject}. Doubles are automatically converted to floats. Nests infinitely. </para>
/// <para>Usage: </para>
/// <para>Values or references can be directly assigned from or to this JSObject object. Loosely typed, so most cast errors only show up at run time as an exception. </para>
/// <para>    JSObject d = "squid"; </para>
/// <para>    string s = d;                    //No cast necessary </para>
/// <para>    d = 1;                           //Converted to int </para>
/// <para>    s = (d + 4) % 5;                 //Supports standard math and string operations + - * / % and +(concat) </para>
/// <para>    float f = d * 5;                 //and type conversions </para>
/// <para>    Console.Write(d.ToString());     //Unfortunately ToString() is necessary due to a C# bug in Console. Other uses, like Debug.Log(d), Debug.Write(d), work perfectly. </para>
/// 
/// <para> Some common methods and properties are implemented for List and Dictionary. The rest can be accessed by assigning it to a correct type reference. </para>
/// <para>     JSObject d = new Dictionary{string, JSObject}();                   </para>
/// <para>     d.Add("key", new List{JSObject});                                 </para>
/// <para>     d["key"].Count;                                          //0  Gets the Count for the list </para>
/// <para>     d.ContainsKey("key");                                    //true </para>
/// <para>     d["key"] = 15;                                           //Converts the list in the dictionary to int with value 15 </para>
/// 
/// <para>     Dictionary{string, JSObject}.KeyCollection keys = d.Keys; //Gets the usual list of keys </para>
/// <para>     Dictionary{string, JSObject} dict = d;                    //Gets the actual dictionary </para>
/// <para>     //Additional methods available after conversion </para>
/// </summary>

public class JSObject : IEnumerable
{

    enum Type { UNKNOWN, STRING, FLOAT, INT, BOOL, LIST, DICTIONARY, DOUBLE }

    private Type type;
    private readonly string stringValue;
    private readonly float floatValue;
    private readonly int intValue;
    private readonly bool boolValue;
    private readonly List<JSObject> listValue;
    private readonly Dictionary<string, JSObject> dictionaryValue;


    //Constructors
    public JSObject(double value)
    {
        type = Type.DOUBLE;
        floatValue = (float)value;
    }
    public JSObject(string value)
    {
        type = Type.STRING;
        stringValue = value;
    }
    public JSObject(int value)
    {
        type = Type.INT;
        intValue = value;
    }
    public JSObject(float value)
    {
        type = Type.FLOAT;
        floatValue = value;
    }
    public JSObject(bool value)
    {
        type = Type.BOOL;
        boolValue = value;
    }
    public JSObject(List<JSObject> value)
    {
        type = Type.LIST;
        listValue = value;
    }
    public JSObject(Dictionary<string, JSObject> value)
    {
        type = Type.DICTIONARY;
        dictionaryValue = value;
    }

    //Implicit setters

    public static implicit operator JSObject(double value) { return new JSObject(value); }
    public static implicit operator JSObject(string value) { return new JSObject(value); }
    public static implicit operator JSObject(float value) { return new JSObject(value); }
    public static implicit operator JSObject(int value) { return new JSObject(value); }
    public static implicit operator JSObject(bool value) { return new JSObject(value); }
    public static implicit operator JSObject(List<JSObject> value) { return new JSObject(value); }
    public static implicit operator JSObject(Dictionary<string, JSObject> value) { return new JSObject(value); }


    //Implicit getters

    public static implicit operator string (JSObject v)
    {
        if (v.type == Type.STRING) return v.stringValue;
        else if (v.type == Type.FLOAT || v.type == Type.DOUBLE) return v.floatValue.ToString();
        else if (v.type == Type.INT) return v.intValue.ToString();
        else if (v.type == Type.BOOL) return v.boolValue.ToString();
        else throw new InvalidCastException();
    }
    public static implicit operator float (JSObject v)
    {
        if (v.type == Type.FLOAT || v.type == Type.DOUBLE) return v.floatValue;
        else if (v.type == Type.INT) return v.intValue;
        else throw new InvalidCastException();
    }
    public static implicit operator int (JSObject v)
    {
        if (v.type == Type.INT)
            return v.intValue;
        else
            throw new InvalidCastException();
    }
    public static implicit operator bool (JSObject v)
    {
        if (v.type == Type.BOOL)
            return v.boolValue;
        else
            throw new InvalidCastException();
    }
    public static implicit operator List<JSObject>(JSObject v)
    {
        if (v.type == Type.LIST)
            return v.listValue;
        else
            throw new InvalidCastException();
    }
    public static implicit operator Dictionary<string, JSObject>(JSObject v)
    {
        if (v.type == Type.DICTIONARY)
            return v.dictionaryValue;
        else throw new InvalidCastException();
    }


    //Operators override

    public static JSObject operator +(JSObject first, JSObject second)
    {
        if (first.type == Type.INT && second.type == Type.INT)
        {
            return new JSObject(first.intValue + second.intValue);
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return new JSObject(first.floatValue + second.floatValue);
        }
        else if (first.type == Type.INT && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return new JSObject(first.intValue + second.floatValue);
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && second.type == Type.INT)
        {
            return new JSObject(first.floatValue + second.intValue);
        }
        else if (first.type == Type.STRING || second.type == Type.STRING)
        {
            return first.ToString() + second.ToString();
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator -(JSObject first, JSObject second)
    {
        if (first.type == Type.INT && second.type == Type.INT)
        {
            return new JSObject(first.intValue - second.intValue);
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return new JSObject(first.floatValue - second.floatValue);
        }
        else if (first.type == Type.INT && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return new JSObject(first.intValue - second.floatValue);
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && second.type == Type.INT)
        {
            return new JSObject(first.floatValue - second.intValue);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator *(JSObject first, JSObject second)
    {
        if (first.type == Type.INT && second.type == Type.INT)
        {
            return new JSObject(first.intValue * second.intValue);
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return new JSObject(first.floatValue * second.floatValue);
        }
        else if (first.type == Type.INT && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return new JSObject(first.intValue * second.floatValue);
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && second.type == Type.INT)
        {
            return new JSObject(first.floatValue * second.intValue);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator /(JSObject first, JSObject second)
    {
        if (first.type == Type.INT && second.type == Type.INT)
        {
            return new JSObject(first.intValue / second.intValue);
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return new JSObject(first.floatValue / second.floatValue);
        }
        else if (first.type == Type.INT && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return new JSObject(first.intValue / second.floatValue);
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && second.type == Type.INT)
        {
            return new JSObject(first.floatValue / second.intValue);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator %(JSObject first, JSObject second)
    {
        if (first.type == Type.INT && second.type == Type.INT)
        {
            return new JSObject(first.intValue % second.intValue);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator >(JSObject first, JSObject second)
    {
        if (first.type == Type.INT && second.type == Type.INT)
        {
            return first.intValue > second.intValue;
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return first.floatValue > second.floatValue;
        }
        else if (first.type == Type.INT && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return first.intValue > second.floatValue;
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && second.type == Type.INT)
        {
            return first.floatValue > second.intValue;
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator <(JSObject first, JSObject second)
    {
        if (first.type == Type.INT && second.type == Type.INT)
        {
            return first.intValue < second.intValue;
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return first.floatValue < second.floatValue;
        }
        else if (first.type == Type.INT && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return first.intValue < second.floatValue;
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && second.type == Type.INT)
        {
            return first.floatValue < second.intValue;
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator >=(JSObject first, JSObject second)
    {
        if (first.type == Type.INT && second.type == Type.INT)
        {
            return first.intValue >= second.intValue;
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return first.floatValue >= second.floatValue;
        }
        else if (first.type == Type.INT && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return first.intValue >= second.floatValue;
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && second.type == Type.INT)
        {
            return first.floatValue >= second.intValue;
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator <=(JSObject first, JSObject second)
    {
        if (first.type == Type.INT && second.type == Type.INT)
        {
            return first.intValue <= second.intValue;
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return first.floatValue <= second.floatValue;
        }
        else if (first.type == Type.INT && (second.type == Type.FLOAT || second.type == Type.DOUBLE))
        {
            return first.intValue <= second.floatValue;
        }
        else if ((first.type == Type.FLOAT || first.type == Type.DOUBLE) && second.type == Type.INT)
        {
            return first.floatValue <= second.intValue;
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator ==(JSObject first, JSObject second)
    {
        if (first.type != second.type)
        {
            return false;
        }
        else
        {
            switch (first.type)
            {
                case Type.STRING: return first.stringValue == second.stringValue;
                case Type.FLOAT: return first.floatValue == second.floatValue;
                case Type.INT: return first.intValue == second.intValue;
                case Type.BOOL: return first.boolValue == second.boolValue;
                case Type.LIST: return first.listValue == second.listValue;
                case Type.DICTIONARY: return first.dictionaryValue == second.dictionaryValue;
                default: return false;
            }
        }
    }

    public static JSObject operator !=(JSObject first, JSObject second)
    {
        if (first.type != second.type)
        {
            return true;
        }
        else
        {
            switch (first.type)
            {
                case Type.STRING: return first.stringValue != second.stringValue;
                case Type.FLOAT: return first.floatValue != second.floatValue;
                case Type.INT: return first.intValue != second.intValue;
                case Type.BOOL: return first.boolValue != second.boolValue;
                case Type.LIST: return first.listValue != second.listValue;
                case Type.DICTIONARY: return first.dictionaryValue != second.dictionaryValue;
                default: return false;
            }
        }
    }

    public static JSObject operator &(JSObject first, JSObject second)
    {
        if (first.type == Type.BOOL && second.type == Type.BOOL)
        {
            return first.boolValue && second.boolValue;
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator |(JSObject first, JSObject second)
    {
        if (first.type == Type.BOOL && second.type == Type.BOOL)
        {
            return first.boolValue || second.boolValue;
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator ++(JSObject first)
    {
        if (first.type == Type.INT)
        {
            return new JSObject(first.intValue + 1);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator !(JSObject first)
    {
        if (first.type == Type.INT)
        {
            return new JSObject(!first.boolValue);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public static JSObject operator --(JSObject first)
    {
        if (first.type == Type.INT)
        {
            return new JSObject(first.intValue - 1);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    //Shared methods

    IEnumerator IEnumerable.GetEnumerator()
    {
        if (type == Type.LIST)
        {
            return listValue.GetEnumerator();
        }
        else if (type == Type.DICTIONARY)
        {
            return dictionaryValue.GetEnumerator();
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public override string ToString()
    {
        switch (type)
        {
            case Type.STRING: return stringValue.ToString();
            case Type.FLOAT: return floatValue.ToString();
            case Type.INT: return intValue.ToString();
            case Type.BOOL: return boolValue.ToString();
            case Type.LIST: return listValue.ToString();
            case Type.DICTIONARY: return dictionaryValue.ToString();
            default: return "Error";
        }
    }

    /// <summary>
    /// Same as ==, checks value equality for value types and references for objects.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        return this == (JSObject)obj;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }


    //List and Dictionary method

    public int Count
    {
        get
        {
            if (type == Type.LIST)
            {
                return listValue.Count;
            }
            else if (type == Type.DICTIONARY)
            {
                return dictionaryValue.Count;
            }
            else
            {
                throw new ArgumentException("Incompatible types");
            }
        }
    }


    //List<JSObject> properties and methods

    public void Add(JSObject value)
    {
        if (type == Type.LIST)
        {
            listValue.Add(value);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public JSObject this[int key]
    {
        get
        {
            if (type == Type.LIST)
            {
                return listValue[key];
            }
            else
            {
                throw new ArgumentException("Incompatible types");
            }
        }
        set
        {
            if (type == Type.LIST)
            {
                listValue[key] = value;
            }
            else
            {
                throw new ArgumentException("Incompatible types");
            }
        }
    }


    public bool Contains(JSObject value)
    {
        if (type == Type.LIST)
        {
            return listValue.Contains(value);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }


    //Dictionary properties and methods

    public Dictionary<string, JSObject>.KeyCollection Keys
    {
        get
        {
            if (type == Type.DICTIONARY)
            {
                return dictionaryValue.Keys;
            }
            else
            {
                throw new ArgumentException("Incompatible types");
            }
        }
    }

    public JSObject this[string key]
    {
        get
        {
            if (type == Type.DICTIONARY)
            {
                return dictionaryValue[key];
            }
            else
            {
                throw new ArgumentException("Incompatible types");
            }
        }
        set
        {
            if (type == Type.DICTIONARY)
            {
                dictionaryValue[key] = value;
            }
            else
            {
                throw new ArgumentException("Incompatible types");
            }
        }
    }

    public void Add(string key, JSObject value)
    {
        if (type == Type.DICTIONARY)
        {
            dictionaryValue.Add(key, value);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public bool ContainsKey(string key)
    {
        if (type == Type.DICTIONARY)
        {
            return dictionaryValue.ContainsKey(key);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }

    public bool ContainsValue(JSObject value)
    {
        if (type == Type.DICTIONARY)
        {
            return dictionaryValue.ContainsValue(value);
        }
        else
        {
            throw new ArgumentException("Incompatible types");
        }
    }
}


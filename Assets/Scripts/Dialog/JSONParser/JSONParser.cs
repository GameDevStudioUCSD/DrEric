#if UNITY_2 || UNITY_3 || UNITY_4 || UNITY_5 || UNITY_6 || UNITY_7
using UnityEngine;
using Debug = UnityEngine.Debug;
#endif
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace JSONParser
{
    /// <summary>
    /// <para>Recursively parses JSON files or strings. </para>
    /// 
    /// <para>DOUBLE QUOTES ONLY. Escape as necessary. No char objects.
    /// 
    /// JSON can otherwise be slightly invalid, but results could be unreliable if so (such as elements after error not being parsed, etc).
    /// Keep in mind that in JSON anything in double quotes are strings, so only primitives outside quotes are converted to correct type.</para>
    /// 
    /// <para>All objects are converted to Dictionary{string, dynamic}, all arrays are converted to List{dynamic}, and all doubles are converted to floats
    /// 
    /// The default debugger (Unity or System.Diagnostics) is invoked on error when debugging, but no exceptions are intentionally thrown at run time.
    /// 
    /// Implemented using the JSONObject library and my dynamic library (included in project)</para>
    /// 
    /// <para>Usage: Standard square bracket syntax for dictionary, lists. </para>
    /// <para>    dynamic c = parse(JSONString);      </para>
    /// <para>    c["character_name"];                                //Objects access using field name </para>
    /// <para>    c["expressions_list"]["exp_default"];               //Read JSON file to see format.         </para>
    /// <para>    c["expressions_list"]["exp_angry"][0];               </para>
    /// <para>    c["expressions_list"]["exp_confused"][0]["hi"];     //Arrays(Lists) access with number indices </para>
    ///     
    /// <para>    c.ContainsKey("character_name");                    //Some methods and properties available directly </para>
    /// <para>    c.Keys; </para>
    /// <para>    ... </para>
    /// 
    /// <para>    Dictionary{string, dynamic} dict = c;               //Assign to actual type for other methods and uses (foreach uses c.GetEnumerator()) </para>
    /// <para>    foreach (dynamic d in dict){ ... }                   </para>
    /// </summary>
    public class JSONParser : MonoBehaviour
    {

        /// <summary>
        /// Recursively parses JSON in given file, returning a dynamic object containing Dictionary<string, dynamic>, List<dynamic>, string, int, float, bool and null objects.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>A dynamic object representing the JSON data. </returns>
        public static dynamic parseFile(string filePath)
        {
            try
            {
                return parse(File.ReadAllText(filePath));
            }
            catch (Exception)
            {
#if UNITY_2 || UNITY_3 || UNITY_4 || UNITY_5 || UNITY_6 || UNITY_7
                Debug.LogWarning
#else
                Debug.WriteLine
#endif
                        ("Unable to read file (is filepath correct?).");
            }
            return null;
        }

        /// <summary>
        /// Recursively parses given JSONString, returning a dynamic object containing Dictionary<string, dynamic>, List<dynamic>, string, int, float, bool and null objects.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>A dynamic object representing the JSON data. </returns>
        public static dynamic parse(string JSONString)
        {
            try
            {
                var c = new JSONObject(JSONString);
                return parse(new JSONObject(JSONString));
            }
            catch (Exception)
            {
#if UNITY_2 || UNITY_3 || UNITY_4 || UNITY_5 || UNITY_6 || UNITY_7
                Debug.LogWarning
#else
                Debug.WriteLine
#endif
                        ("JSON parse error.");
            }
            return null;
        }

        /// <summary>
        /// Recursively parses given JSONObject, returning a dynamic object containing Dictionary<string, dynamic>, List<dynamic>, string, int, float, bool and null objects.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>A dynamic object representing the JSON data. </returns>
        public static dynamic parse(JSONObject obj)
        {
            try
            {
                switch (obj.type)
                {
                    case JSONObject.Type.OBJECT:
                        Dictionary<string, dynamic> d = new Dictionary<string, dynamic>();
                        for (int i = 0; i < obj.list.Count; i++)
                        {
                            d.Add(obj.keys[i], parse(obj.list[i]));
                        }
                        return d;
                    case JSONObject.Type.ARRAY:
                        List<dynamic> l = new List<dynamic>();
                        foreach (JSONObject j in obj.list)
                        {
                            l.Add(parse(j));
                        }
                        return l;
                    case JSONObject.Type.STRING:
                        return obj.str;
                    case JSONObject.Type.NUMBER:
                        if (obj.n % 1 == 0)
                        {
                            return (int)obj.n;
                        }
                        return obj.n;
                    case JSONObject.Type.BOOL:
                        return obj.b;
                    case JSONObject.Type.NULL:
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
#if UNITY_2 || UNITY_3 || UNITY_4 || UNITY_5 || UNITY_6 || UNITY_7
                Debug.LogWarning
#else
                Debug.WriteLine
#endif
                        ("JSON parse error.");
            }
            return null;
        }
    }
}

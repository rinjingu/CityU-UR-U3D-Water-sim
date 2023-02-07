using SharpConfig;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Vector3StringConverter : TypeStringConverter<Vector3>
{
    public override string ConvertToString(object value)
    {
        return value.ToString();
    }

    // This method is responsible for converting a string to a Color object.
    public override object TryConvertFromString(string value, Type hint)
    {
        var split = value.Trim('(', ')').Split(',');

        Vector3 thisVect = new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]));


        return thisVect;
    }
}
using SharpConfig;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ColorStringConverter : TypeStringConverter<Color>
{
    public override string ConvertToString(object value)
    {
        var color = (Color)value;
        return color.ToString();
    }

    // This method is responsible for converting a string to a Color object.
    public override object TryConvertFromString(string value, Type hint)
    {
        var split = value.Trim('R','G','B','A','(', ')').Split(',');

        Color color = new Color(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));


        return color;
    }
}
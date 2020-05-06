using System;
using UnityEngine;

public static class ColorHelper
{
	/// <summary>
	/// 指定した色の補色を取得します(RGB色相環)
	/// </summary>
	/// <param name="baseColor">ベースの色</param>
	/// <returns>補色</returns>
	public static Color GetComplementaryColor(Color baseColor)
	{
	    float hue = 0;
	    float saturation = 0;
	    float value = 0;
	    Color.RGBToHSV(baseColor, out hue, out saturation, out value);
	    hue += 0.5f;
	    if (hue > 1.0f)
	    {
	        hue -= 1.0f;
	    }
	    Color outColor = Color.HSVToRGB(hue, saturation, value);
	    return outColor;
	}
	/// <summary>
	/// 指定した色の類似色を取得します(RGB色相環)
	/// </summary>
	/// <param name="baseColor">ベースの色</param>
	/// <param name="offsetHue">色相オフセット (-3~+3)</param> 
	/// <returns>類似色</returns>
	public static Color GetAnalogousColor(Color baseColor, float offsetHue)
	{
	    float hue = 0;
	    float saturation = 0;
	    float value = 0;
	    Color.RGBToHSV(baseColor, out hue, out saturation, out value);
	    float offsetDeg = (360.0f / 24.0f) * offsetHue;
	    hue += offsetDeg / 360;
	    if (hue > 1.0f)
	    {
	        hue -= 1.0f;
	    }
	    else if (hue < 0.0f)
	    {
	        hue += 1.0f;
	    }
	    Color outColor = Color.HSVToRGB(hue, saturation, value);
	    return outColor;
	}
	/// <summary>
	/// 指定した色の明度を変更します(RGB色相環)
	/// </summary>
	/// <param name="baseColor">ベースの色</param>
	/// <param name="brightness">明度</param> 
	/// <returns>変更後の色</returns>
	public static Color SetBrightNess(Color baseColor, float brightness)
	{
	    float hue = 0;
	    float saturation = 0;
	    float value = 0;
	    Color.RGBToHSV(baseColor, out hue, out saturation, out value);
	    Color outColor = Color.HSVToRGB(hue, saturation, brightness);
	    return outColor;
	}
	/// <summary>
	/// 指定した色の明度を加算します(RGB色相環)
	/// </summary>
	/// <param name="baseColor">ベースの色</param>
	/// <param name="brightness">明度の加算値</param> 
	/// <returns>変更後の色</returns>
	public static Color AddBrightNess(Color baseColor, float brightness)
	{
	    float hue = 0;
	    float saturation = 0;
	    float value = 0;
	    Color.RGBToHSV(baseColor, out hue, out saturation, out value);
	    value = Math.Min(Math.Max(value + brightness, 0), 1);
	    Color outColor = Color.HSVToRGB(hue, saturation, value);
	    return outColor;
	}
	/// <summary>
	/// 指定した色の彩度を変更します(RGB色相環)
	/// </summary>
	/// <param name="baseColor">ベースの色</param>
	/// <param name="inSaturation">彩度の加算値</param> 
	/// <returns>変更後の色</returns>
	public static Color SetSaturation(Color baseColor, float inSaturation)
	{
	    float hue = 0;
	    float saturation = 0;
	    float value = 0;
	    Color.RGBToHSV(baseColor, out hue, out saturation, out value);
	    Color outColor = Color.HSVToRGB(hue, inSaturation, value);
	    return outColor;
	}
	/// <summary>
	/// 指定した色の彩度を加算します(RGB色相環)
	/// </summary>
	/// <param name="baseColor">ベースの色</param>
	/// <param name="inSaturation">彩度の加算値</param> 
	/// <returns>変更後の色</returns>
	public static Color AddSaturation(Color baseColor, float inSaturation)
	{
	    float hue = 0;
	    float saturation = 0;
	    float value = 0;
	    Color.RGBToHSV(baseColor, out hue, out saturation, out value);
	    saturation = Math.Min(Math.Max(saturation + inSaturation, 0), 1);
	    Color outColor = Color.HSVToRGB(hue, saturation, value);
	    return outColor;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EaseFunctions
{

	public enum EasingFunctions
	{
		SmoothStart2, SmoothStart3, SmoothStart4, SmoothStart5,
		SmoothStop2, SmoothStop3, SmoothStop4, SmoothStop5,
		SmoothStep2, SmoothStep3, SmoothStep4, SmoothStep5
	}

	public static Func<float, float> GetFunction(EasingFunctions easing)
	{
		switch (easing)
		{
			case EasingFunctions.SmoothStart2: return SmoothStart2;
			case EasingFunctions.SmoothStart3: return SmoothStart3;
			case EasingFunctions.SmoothStart4: return SmoothStart4;
			case EasingFunctions.SmoothStart5: return SmoothStart5;

			case EasingFunctions.SmoothStop2: return SmoothStop2;
			case EasingFunctions.SmoothStop3: return SmoothStop3;
			case EasingFunctions.SmoothStop4: return SmoothStop4;
			case EasingFunctions.SmoothStop5: return SmoothStop5;

			case EasingFunctions.SmoothStep2: return SmoothStep2;
			case EasingFunctions.SmoothStep3: return SmoothStep3;
			case EasingFunctions.SmoothStep4: return SmoothStep4;
			case EasingFunctions.SmoothStep5: return SmoothStep5;

			default:
				return (f) => f;
		}
	}

	public static Func<float, float> GetFunc(this EasingFunctions easing)
	{
		return GetFunction(easing);
	}



	public static float SmoothStart2(float t) => t * t;
	public static float SmoothStart3(float t) => t * t * t;
	public static float SmoothStart4(float t) => t * t * t * t;
	public static float SmoothStart5(float t) => t * t * t * t * t;

	public static float SmoothStop2(float t) => 1 - (1 - t) * (1 - t);
	public static float SmoothStop3(float t) => 1 - (1 - t) * (1 - t) * (1 - t);
	public static float SmoothStop4(float t) => 1 - Mathf.Pow((1 - t), 4);
	public static float SmoothStop5(float t) => 1 - Mathf.Pow((1 - t), 5);

	public static float SmoothStep2(float t) => 1 - CrossFade(SmoothStart2, SmoothStop2, t);
	public static float SmoothStep3(float t) => 1 - CrossFade(SmoothStart3, SmoothStop3, t);
	public static float SmoothStep4(float t) => 1 - CrossFade(SmoothStart4, SmoothStop4, t);
	public static float SmoothStep5(float t) => 1 - CrossFade(SmoothStart5, SmoothStop5, t);


	public static float Mix(Func<float, float> a, Func<float, float> b, float bWeight, float t) => (1 - bWeight) * a(t) + (bWeight) * b(t);

	public static float CrossFade(Func<float, float> a, Func<float, float> b, float t) => (1 - t) * a(t) + (t) * b(t);
}

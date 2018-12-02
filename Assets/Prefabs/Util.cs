
using System;
using UnityEngine;
using Object = System.Object;

public static class Util
{
	public static readonly Vector2 NullVector = new Vector2(3214546, 546213);
	
	public static Vector2 Step(this Vector2 shootVector, int steps, float offset)
	{
		float angle = Vector2.Angle(Vector2.up, shootVector);
		if (shootVector.x < 0) {
			angle = 360 - angle;
		}
		int angleStepSize = 360 / steps;
		float newAngle = (int) (angle / angleStepSize) * angleStepSize + offset;
		double newAngleRadian = newAngle / 180 * Math.PI;
	
		return new Vector2((float) Math.Sin(newAngleRadian), (float) Math.Cos(newAngleRadian));
	}

	public static Vector2 RotateDegree(this Vector2 v, float degrees)
	{
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

		float tx = v.x;
		float ty = v.y;
		v.x = cos * tx - sin * ty;
		v.y = sin * tx + cos * ty;
		return v;
	}

	public static Quaternion AsZRotation(this Vector2 vector)
	{
		return Quaternion.Euler(0, 0, 180.0f / Mathf.PI * Mathf.Atan2(vector.y, vector.x));
	}

	public static Quaternion AsZRotation(this Vector3 vector)
	{
		return Quaternion.Euler(0, 0, 180.0f / Mathf.PI * Mathf.Atan2(vector.y, vector.x));
	}

	public static float SmoothToOne(float raw)
	{
		var inv = 1.0f - raw;
		var smooth = inv * inv * inv;
		return 1.0f - smooth;
	}
	
	public static bool HasComponent<T> (this GameObject obj, out T result) where T : Component
	{
		result = obj.GetComponent<T>();
		return result != null;
	}
	
	public static bool HasComponent<T> (this GameObject obj) where T : Component
	{
		var result = obj.GetComponent<T>();
		return result != null;
	}

	public static Vector2 GetScreenSpaceDirectionToCenterOf(this Transform transform, RectTransform rectTransform)
	{
		Canvas canvas = rectTransform.GetComponentInParent<Canvas>();
		var direction =
			(new Vector2(1, -1) * rectTransform.sizeDelta * canvas.scaleFactor / 2 + (Vector2) rectTransform.position -
			 (Vector2) transform.position) / Screen.height;
		return direction;
	}

	public static int Clamp(int min, int value, int max)
	{
		return Math.Max(min, Math.Min(value, max));
	}

	public static float Clamp(float min, float value, float max)
	{
		return Mathf.Max(min, Mathf.Min(value, max));
	}
}
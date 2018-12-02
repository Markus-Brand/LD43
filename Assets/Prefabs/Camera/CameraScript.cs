using System;
using Light2D;
using UnityEngine;
using Util;
using Util.Input;
using Random = UnityEngine.Random;

public class CameraScript : MonoBehaviour
{
	public PlayerBehaviour followed;

	public float deadZoneSize;

	public float softZoneSize;

	public float speed;

	//camera focus point weights
	public int playerWeight;
	public int aimWeight;
	public int enemyWeight;
	public int bulletWeight;

	private Vector2 cameraPosition; //current position, without shake applied
	private float timeShaking = 99999;
	private float shakeIntensity = 0;
	private float shakeDuration = 1;
	private Vector2 shakeDirection = new Vector2(0, 0); //null if unidirectional

	private Vector2 _overrideTargetPosition = MathUtil.NullVector;

	void Start()
	{
		cameraPosition = transform.position;
	}

	void Update()
	{
		Vector3 targetPosition = GetWorldFocusPoint();
		Vector2 direction = targetPosition - transform.position;

		float speedSoftener =
			System.Math.Max(System.Math.Min(1, (direction.magnitude - deadZoneSize) / softZoneSize), 0);
		cameraPosition += direction.normalized * speed * speedSoftener * Time.deltaTime;


		timeShaking += Time.deltaTime;
		float relativeTimeShaking = timeShaking / shakeDuration;
		Vector2 random = new Vector2(0, 0);
		if (relativeTimeShaking < 1) {
			float currentIntensity = (1 - relativeTimeShaking) * shakeIntensity;
			float timeFactor = (float) Math.Sqrt(Time.timeScale);
			if (shakeDirection.sqrMagnitude > 0) {
				//directional
				random = shakeDirection * (Random.value * 2 - 1) * currentIntensity * timeFactor;
			} else {
				//un-directional
				random = new Vector2((Random.value * 2 - 1) * currentIntensity * timeFactor,
					(Random.value * 2 - 1) * currentIntensity * timeFactor);
			}
		} else {
			shakeDuration = 0.0000001f;
			shakeIntensity = 0;
		}


		Vector2 actualPosition = cameraPosition + random;
		transform.position = new Vector3(actualPosition.x, actualPosition.y, -10);
	}

	/**
	 * the weighted center of all the important things on the map --> where the camera should look at
	 */
	private Vector2 GetWorldFocusPoint()
	{
		if (_overrideTargetPosition != MathUtil.NullVector) {
			return _overrideTargetPosition;
		}
		
		Vector2 sum = new Vector2(0, 0);
		float count = 0;

		count += playerWeight;
		Vector2 playerPos = followed.transform.position;
		var followedPosition = playerPos;
		sum += followedPosition * playerWeight;

		count += aimWeight;
		Vector2 aimPosition = followedPosition + followed.GetAttackVector(false);
		sum += aimPosition * aimWeight;

		var allEnemies = StaticAccess.GetEnemySpawner();
		if (allEnemies != null && allEnemies.Enemies != null && allEnemies.Enemies.Count > 0) {
			Vector2 enemyPositionSum = new Vector2(0, 0);
			float enemyWeights = 0;

			try {
				foreach (var enemy in allEnemies.Enemies) {
					if (!enemy) continue;
					Vector2 pos = enemy.transform.position;
					float distance = Vector2.Distance(playerPos, pos);
					const float outerRadius = 15f;
					const float radiusSmooth = 3f;
					float factor = Math.Max(0, Math.Min((outerRadius - distance) / radiusSmooth, 1));
					float weight = Mathf.SmoothStep(0, 1, factor);
					enemyPositionSum += pos * weight;
					enemyWeights += weight;
				}
			} catch (MissingReferenceException) {
				Debug.LogError("Deleted enemy while accessing for camera positioning");
			}

			float actualEnemyWeight = enemyWeight * Math.Min(1f, enemyWeights);

			if (actualEnemyWeight > 0) {
				count += actualEnemyWeight;
				sum += enemyPositionSum / enemyWeights * actualEnemyWeight;				
			}
		}

		return sum / count;
	}

	public void MakeUndirectedShake(float intensity, float duration)
	{
		MakeDirectedShake(intensity, duration, new Vector2(0, 0));
	}

	public void MakeDirectedShake(float intensity, float duration, Vector2 direction)
	{
		if (intensity > shakeIntensity) {
			shakeIntensity = intensity;
			shakeDuration = duration;
			shakeDirection = direction;
			timeShaking = 0;
		}
	}

	public void SetOverrideTargetPosition(Vector2 overridePosition)
	{
		_overrideTargetPosition = overridePosition;
	}

	public void ReleaseOverrideTargetPosition()
	{
		SetOverrideTargetPosition(MathUtil.NullVector);
		StaticAccess.GetBlackBars().Hide();
		InputManager.AllowInputsAgain();
	}

	public void SetOverrideTargetPositionFor(Vector2 overridePosition, float time)
	{
		InputManager.BlockAllInputs();
		StaticAccess.GetBlackBars().Show();
		SetOverrideTargetPosition(overridePosition);
		Invoke(nameof(ReleaseOverrideTargetPosition), time);
	}

	public void JumpTo(Vector2 targetPosition)
	{
		cameraPosition = targetPosition;
	}

	public void SetBrightness(float brightness)
	{
		var lighting = GetComponent<LightingSystem>();
		
		const float minimalGray = 0.05f;
		const float maximalGray = 0.45f;
		const float grayRange = maximalGray - minimalGray;

		float gray = minimalGray + brightness * grayRange;
		lighting.LightCamera.backgroundColor = new Color(gray, gray, gray, 0);

		const float maximalFlashlight = 0.32f;
		float flashGray = MathUtil.SmoothToOne(1f - brightness) * maximalFlashlight;
		
		var playerLight = StaticAccess.GetPlayerObject()?.transform.Find("Light")?.GetComponent<LightSprite>();
		if (playerLight != null) playerLight.Color = new Color(1, 1, 1, flashGray);
	}
}

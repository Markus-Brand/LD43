using System.Collections.Generic;
using UnityEngine;

namespace Postprocessing
{
	public class ShockWaveRenderer : MonoBehaviour
	{
		public Material Mat;

		private readonly Vector4[] hitPointWorldPositions = new Vector4[10];
		private readonly Vector4[] hitPointData = new Vector4[10];
		private int hitPointHead;

		private Camera Camera => GetComponent<Camera>();

		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, Mat);
		}

		private void Start()
		{
			for (var i = 0; i < hitPointWorldPositions.Length; i++) {
				hitPointWorldPositions[i] = new Vector4(0, 0, 999999, 0);
				hitPointData[i] = new Vector4(0, 0, 1, 0);
			}
			Mat.SetVectorArray("_HitPointData", hitPointData);
		}

		private void Update()
		{
			Vector4[] hitPointPositions = new Vector4[10];
			for (var i = 0; i < hitPointWorldPositions.Length; i++) {
				hitPointWorldPositions[i].z += Time.deltaTime;
				Vector2 viewportSpace = Camera.main.WorldToViewportPoint(hitPointWorldPositions[i]);
				hitPointPositions[i] = new Vector4(viewportSpace.x, viewportSpace.y, hitPointWorldPositions[i].z, hitPointWorldPositions[i].w);
			}
			

			Mat.SetVectorArray("_HitPointPositions", hitPointPositions);
			Mat.SetVectorArray("_HitPointData", hitPointData);
		}

		public void MakeScreamWave(Vector2 worldPosition)
		{
			MakeWave(worldPosition, 0.25f, 0.7f , 1.2f, 2, 1.3f);
		}

		public void MakeSmallHitWave(Vector2 worldPosition)
		{
			MakeWave(worldPosition, 0.4f, 0.3f , 1.1f, 0, 0.3f);
		}

		public void MakeHitWave(Vector2 worldPosition)
		{
			MakeWave(worldPosition, 0.4f, 0.4f , 1.3f, 0, 0.4f);
		}

		public void MakeDeathWave(Vector2 worldPosition)
		{
			MakeWave(worldPosition, 0.4f, 0.6f , 1.5f, 0, 0.6f);
		}

		public void MakeExplosionWave(Vector2 worldPosition, float explosionIntensity)
		{
			MakeWave(worldPosition, 0.5f, 1f * explosionIntensity, 1.7f, 1, 0.9f);
		}
		
		public void MakeWave(Vector2 worldPosition, float donutWidth = 1.0f, float radius = 1f, float speed = 1f, float randomness = 0, float strength = 1)
		{
			hitPointWorldPositions[hitPointHead].x = worldPosition.x;
			hitPointWorldPositions[hitPointHead].y = worldPosition.y;
			hitPointWorldPositions[hitPointHead].z = 0; //age
			hitPointWorldPositions[hitPointHead].w = strength;
			hitPointData[hitPointHead].x = randomness;
			hitPointData[hitPointHead].y = donutWidth;
			hitPointData[hitPointHead].z = radius;
			hitPointData[hitPointHead].w = speed;

			hitPointHead++;
			if (hitPointHead >= hitPointData.Length) hitPointHead = 0;
		}
	}
}

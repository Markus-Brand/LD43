using UnityEngine;

namespace Postprocessing
{
	public class VignetteRenderer : MonoBehaviour
	{
		public Material Mat;

		private float remainingTime = 0;
		private float fadeOutInterval = 1;

		private void OnRenderImage(RenderTexture src, RenderTexture dest)
		{
			Graphics.Blit(src, dest, Mat);
		}

		private void Start()
		{
			Mat.SetFloat("intensity", 0);
		}

		private void Update()
		{
			if (remainingTime > 0) {
				remainingTime -= Time.deltaTime;
				Mat.SetFloat("intensity", 1 - remainingTime / fadeOutInterval);
			}
		}

		public void FadeOutIn(float time)
		{
			remainingTime = time;
			fadeOutInterval = time;
		}
	}
}

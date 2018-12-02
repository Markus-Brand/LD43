using UnityEngine;
using Util.Input;

public class ShockWaveMouseDebugger : MonoBehaviour
{
	public float width;
	public float radius;
	public float speed;
	public float randomness;
	public float strength;

	private void Update()
	{
		if (InputManager.OnDown(InputAction.Attack)) {
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			StaticAccess.GetShockWaveRenderer().MakeWave(mousePos, width, radius, speed, randomness, strength);
		}
	}
}
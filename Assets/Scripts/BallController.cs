using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
	public delegate void OnKillAction();

	public OnKillAction OnKill;

	private float LIFETIME = 4.0f;
	private float RANDOM_FORCE = 200.0f;

	private float timer;

	public void Start ()
	{
		this.GetComponent<Rigidbody>().AddForce(Vector3.right * Random.Range(-RANDOM_FORCE, RANDOM_FORCE));
	}

	public void Update ()
	{
		timer += Time.deltaTime;
		
		if (timer > LIFETIME / 2)
		{
			float percentage = 1 - (timer - LIFETIME / 2) / (LIFETIME / 2);

			this.transform.localScale = Vector3.one * percentage;
		}

		if (timer > LIFETIME)
		{
			GameObject.Destroy(this.gameObject);
		}
	}
}
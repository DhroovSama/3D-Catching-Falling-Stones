using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public delegate void OnCollectBallAction();

	public OnCollectBallAction OnCollectBall;

	private float SPEED = 800;

	public Camera mainCamera;

	private Vector3 leftBound;
	private Vector3 rightBound;

	public void Start ()
	{
		leftBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, -mainCamera.transform.localPosition.z));
		rightBound = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, -mainCamera.transform.localPosition.z));
	}

	public void Update ()
	{
		ProcessInput();
		KeepInBounds();
	}

	private void KeepInBounds ()
	{
		if (this.transform.position.x < leftBound.x) { this.transform.position = new Vector3(leftBound.x, this.transform.position.y, this.transform.position.z); }
		if (this.transform.position.y > leftBound.y) { this.transform.position = new Vector3(this.transform.position.x, leftBound.y, this.transform.position.z); }
		if (this.transform.position.x > rightBound.x) { this.transform.position = new Vector3(rightBound.x, this.transform.position.y, this.transform.position.z); }
		if (this.transform.position.y < rightBound.y) { this.transform.position = new Vector3(this.transform.position.x, rightBound.y, this.transform.position.z); }
	}

	private void ProcessInput ()
	{
		if (Input.GetKey("left") || Input.GetKey("a")) { this.GetComponent<Rigidbody>().AddForce(Vector3.left * SPEED * Time.deltaTime); }
		if (Input.GetKey("right") || Input.GetKey("d")) { this.GetComponent<Rigidbody>().AddForce(Vector3.right * SPEED * Time.deltaTime); }
	}

	public void OnCollisionEnter (Collision collision)
	{
		if (collision.gameObject.GetComponent<BallController>() != null)
		{
			if (OnCollectBall != null)
			{
				OnCollectBall();
			}

			GameObject.Destroy(collision.gameObject);
		}
	}
}
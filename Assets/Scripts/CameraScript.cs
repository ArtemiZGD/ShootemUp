using UnityEngine;

public class CameraScript : MonoBehaviour
{
	[SerializeField] private float _moveSpeed;

	private GameObject _player;
	private Vector3 _startPosition;

	private void Start()
	{
		_startPosition = transform.position;
		_player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		transform.position = Vector3.Lerp(transform.position, _player.transform.position + _startPosition, Time.deltaTime * _moveSpeed);
	}
}

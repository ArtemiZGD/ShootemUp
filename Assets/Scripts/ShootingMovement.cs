using UnityEngine;

enum AnimateEnum
{
	isIdle,
	isRun,
	isRunBackward
}

[RequireComponent(typeof(CharacterController))]
public class ShootingMovement : MonoBehaviour
{
	[SerializeField] private Animator _animator;

	[SerializeField] private float _speed;

	float xSpeed;
	float ySpeed;

	CharacterController character;

	AnimateEnum animEnum;

	

	private void Start()
	{
		character = GetComponent<CharacterController>();
		animEnum = AnimateEnum.isIdle;
	}

	private void Update()
	{
		if (!SettingsScript.Pause)
		{
			Rotate();

			Move();

			Animate();
		}
	}

	private void Rotate()
	{
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
		{
			Vector3 point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
			transform.rotation = Quaternion.LookRotation(point - transform.position);

			Vector2 rotate = new Vector2(point.x - transform.position.x, point.z - transform.position.z).normalized;
			Vector2 move = new Vector3(xSpeed, ySpeed);
			
			float angle = Vector2.Angle(rotate, Vector2.up);

			if (rotate.x < 0)
			{
				angle = -angle;
			}

			move = new Vector2(move.x * Mathf.Cos(angle * Mathf.PI / 180) - move.y * Mathf.Sin(angle * Mathf.PI / 180), 
				move.x * Mathf.Sin(angle * Mathf.PI / 180) + move.y * Mathf.Cos(angle * Mathf.PI / 180));

			_animator.SetFloat("X", move.x);
			_animator.SetFloat("Y", move.y);
		}
	}

	private void Animate()
	{
		if (xSpeed == 0 && ySpeed == 0 && animEnum != AnimateEnum.isIdle )
		{
			animEnum = AnimateEnum.isIdle;
		}
		else if ((xSpeed != 0 || ySpeed != 0) && animEnum != AnimateEnum.isRun)
		{
			animEnum = AnimateEnum.isRun;
		}
	}

	private void Move()
	{
		xSpeed = Input.GetAxisRaw("Horizontal");
		ySpeed = Input.GetAxisRaw("Vertical");

		Vector3 move = new Vector3(xSpeed, 0, ySpeed);

		character.Move(move.normalized * _speed * Time.deltaTime);
	}
}

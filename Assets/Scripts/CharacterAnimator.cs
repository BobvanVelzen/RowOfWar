using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {

	[Tooltip("the possition the left hand needs to move to")]
	public Transform leftHandTarget;
	[Tooltip("the possition the right hand needs to move to")]
	public Transform rightHandTarget;

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
		
	///<summary>
	/// The method is called once per frame and moves the characters hands
	/// to the target possition.
	/// </summary>
	/// <param name=" name="layerIndex"">Animation layer</param>
	void OnAnimatorIK(int layerIndex)
	{

		animator.SetLookAtWeight (1, 1, 0, 1, 1);
		Vector3 position = new Vector3 (leftHandTarget.position.x, 0.1f, leftHandTarget.position.z);
		animator.SetLookAtPosition (position);

		animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
		animator.SetIKPosition (AvatarIKGoal.LeftHand, leftHandTarget.position);

		animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
		animator.SetIKPosition (AvatarIKGoal.RightHand, rightHandTarget.position);
	}


	// true = left 1, valse = right 2
	///<summary>
	/// The method sets the animation depending on the side and direction
	/// </summary>
	/// /// <param name=" name="direction"">Walk direction back/forward</param>
	/// <param name=" name="isLeft"">Character side</param>
	public void Move(float direction, bool isLeft)
    {
        if (isLeft) {
			animator.SetFloat ("direction", direction);
		} else {
			animator.SetFloat ("direction",  - direction);
		}
	}
}

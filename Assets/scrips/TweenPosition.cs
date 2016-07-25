using UnityEngine;
using System.Collections;

public class TweenPosition : TweenAnimation {

	protected override void AnimationBuffer (Vector3 iFrom, Vector3 iTo, float iProgress){
		this.transform.position = Vector3.Lerp (iFrom, iTo, iProgress);
	}

	protected override void StartAnimation (Vector3 iFrom){
		this.transform.position = iFrom;
	}
}

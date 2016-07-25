using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class TweenAnimation : MonoBehaviour {

	[SerializeField] AnimationCurve m_curve;

	[SerializeField] Vector3 m_from;

	[SerializeField] Vector3 m_to;

	public Vector3 To{
		get{return m_to;}
		set{ m_to = value;}
	}

	[SerializeField] float m_split = 1.0f;

	public float Split{
		get{return m_split;}
		set{ m_split = value;}
	}

	const int kFirstNumber = 1;
	float[] m_endTime;

	float m_time = 0.0f;
	int m_number = 1;
	// Use this for initialization
	void Awake () {
		
		var keyframe = m_curve.keys;

		m_endTime = new float[keyframe.Length];
		for (int i = 0; i < keyframe.Length; ++i) {
			m_endTime [i] = keyframe [i].time;
		}
	}

	public void Play(bool isLoop){
		StartCoroutine (Animation (isLoop));
	}

	//
	IEnumerator Animation(bool isLoop){
		StartAnimation (m_from);
		m_time = 0.0f;
		m_number = kFirstNumber;

		while (true) {
			m_time += (m_endTime[m_number] - m_endTime[m_number-1])/ m_split;

			float progress = m_curve.Evaluate (m_time);
			AnimationBuffer (m_from, m_to, progress);

			if (CheckTime ()){
				if (isLoop) {
					m_time = 0.0f;
					m_number = kFirstNumber;
					StartAnimation (m_from);
				} else {
					yield break;  // 終了
				}
			}

			yield return null;
		}

	}

	//
	bool CheckTime(){
		if (m_time >= m_endTime [m_number]) {
			m_number += 1;

			// 上限を超えていたら終了
			if (m_number >= m_endTime.Length) {
				return true;
			}
		}
		return false;
	}

	protected virtual void AnimationBuffer (Vector3 iFrom, Vector3 iTo, float iProgress){return;}

	protected virtual void StartAnimation (Vector3 iFrom){return;}
}

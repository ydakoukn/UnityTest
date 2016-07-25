using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		var hoge = GetComponent<TweenPosition> ();
		hoge.Play (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}

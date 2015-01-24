using UnityEngine;
using System.Collections;

public class UVTexture : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Material material = this.gameObject.renderer.material;
		material.SetTextureOffset ("_MainTex", new Vector2 (0.0f, 0.0f));
	}
}

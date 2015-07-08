using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

    public GameObject[ ] m_destroyThisSceneGos;

    public static LoadScene Instance;

	void Start () {
        Instance = this;
	}

    public void free ( ) {
        Debug.Log("free");
        Resources.UnloadUnusedAssets( );
    }

    public void DestroyInitSceneGos ( ) {
        for (int i = 0; i < m_destroyThisSceneGos.Length; i++) {
            Destroy(m_destroyThisSceneGos[i]);
        }
        //free( );
    }

}

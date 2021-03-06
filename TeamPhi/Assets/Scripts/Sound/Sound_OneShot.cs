using UnityEngine;
using System.Collections;


/*
 *	ワンショットのSEを再生
 *	Maruchu
 */
public		class		Sound_OneShot			: MonoBehaviour {


	private		float		m_fDelaySec				= 0.0f;		//遅延秒数
	private		bool		m_bStarted				= false;	//再生開始フラグ


	//遅延再生処理
	public		void	SetPlay( float fDelaySec=0) {

		m_fDelaySec			= fDelaySec;
		m_bStarted			= false;
	}

	//実行関数
	protected	void	Update() {

		//再生前
		if( !m_bStarted) {
			m_fDelaySec	-= Time.deltaTime;
			if( m_fDelaySec <= 0) {
				//再生開始
				audio.Play();
				m_bStarted	= true;
			}
			return;
		}

		//再生中
		if( !audio.isPlaying) {
			Destroy( gameObject);
		}
	}



}


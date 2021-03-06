using UnityEngine;
using System.Collections;


/*
	Sound_Manager.GetInst().PlayMusic( eMusicID.Hogehoge);			//BGM 再生
	Sound_Manager.GetInst().StopMusic();							//BGM 停止

	Sound_Manager.GetInst().PlaySound( eSoundID.Hogehoge);			//効果音 再生
*/



//BGM ID
public	enum	eMusicID {
	 None
	,Title
	,Game
	,Result
	,EnumMax
}

//サウンドID
public	enum	eSoundID {
	 None
	,System_Decide		//決定音
	,System_Cancel		//キャンセル音
	,System_Select		//選択音
	,System_NG			//おしちゃダメ
	,EnumMax
}




/*
 *	サウンド管理
 *	Maruchu
 */
public		class		Sound_Manager			: MonoBehaviour {



private	static	Sound_Manager	g_srcInst	= null;		//インスタンス

//インスタンス取得
public	static	Sound_Manager	GetInst() {
	return g_srcInst;
}

//起動時
private			void			Awake() {
	g_srcInst	= this;
}



private							float					m_fVolume_Music			= 0.5f;						//音量	BGM
private							float					m_fVolume_Sound			= 1.0f;						//音量	効果音



public							AudioClip[]				m_acMusicPrefab			= new AudioClip[ (int)eMusicID.EnumMax];		//音楽
public							AudioClip[]				m_acSoundPrefab			= new AudioClip[ (int)eSoundID.EnumMax];		//効果音


private							eMusicID				m_eRecentMusicID		= eMusicID.None;			//最近再生したBGMのID

public							GameObject				m_goSound_OneShot		= null;						//サウンドを鳴らすプレハブ





//音楽 再生
public		void	PlayMusic( eMusicID eID) {

	//前と違う？
	if( m_eRecentMusicID!=eID) {
		//停止
		StopMusic();

		//履歴を更新
		m_eRecentMusicID	= eID;
	}

	//ないあるよ
	if( eMusicID.None==eID) {
		//停止
		audio.Stop();
		return;
	}

	//クリップ書き換え
	audio.clip		= m_acMusicPrefab[ (int)(eID)];
	audio.loop		= true;
	audio.volume	= m_fVolume_Music;

	//再生
	audio.Play();
}
//音楽 停止
public		void	StopMusic() {

	//停止
	audio.Stop();
}


//効果音 再生
public		void	PlaySound( eSoundID eID, float fWaitSec=0) {

	//ないあるよ
	if( eSoundID.None==eID) {
		return;
	}
	if( m_acSoundPrefab.Length < (int)eID) {
		Debug.LogError( "Sound is NULL 1 : ID = "+ eID +" > "+ m_acSoundPrefab.Length);
		return;
	}
	if( null==m_acSoundPrefab[ (int)eID]) {
		Debug.LogError( "Sound is NULL 2 : ID = "+ eID);
		return;
	}

	GameObject	goTemp		= Instantiate( m_goSound_OneShot)		as GameObject;
	AudioSource	asTemp		= goTemp.GetComponent<AudioSource>();
	if( null==asTemp) {
		return;
	}
	asTemp.clip		= m_acSoundPrefab[ (int)(eID)];
	asTemp.loop		= false;
	asTemp.volume	= m_fVolume_Sound;
	goTemp.transform.parent	= transform;

	Sound_OneShot	srcTemp		= goTemp.GetComponent<Sound_OneShot>();
	srcTemp.SetPlay( fWaitSec);
}




}

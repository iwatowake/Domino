using UnityEngine;
using System.Collections;




//! モードの種類
public enum		MenuKind {
	 None	= -1
	,First						//!< 最初の説明
	,Title						//!< タイトル
	,Game						//!< ゲーム本編
	,Result						//!< リザルト
	,EnumMax
};



/*
 *	メニュー管理
 *	Maruchu
 */
public	class		System_Manager		: MonoBehaviour {





/*
	System_Manager.GetInst()

	System_Manager.GetInst().SetMenuRequest( MenuKind.Game);			//メニュー切り替え
*/

private	static	System_Manager		g_srcInst	= null;		//インスタンス

//インスタンス取得
public	static	System_Manager	GetInst() {
	return g_srcInst;
}

//起動時
private		void	Awake() {
	g_srcInst	= this;
}




//! 実行ステップ
private		enum	MainStep {
	 Initialize					//!< 初期化
	,ModeChange					//!< モード切り替え
	,DeletePrefab				//!< プレハブ削除
	,CreatePrefab				//!< プレハブ作成
	,ControlWait				//!< ユーザー操作待機
}


//! 各モードのプレハブのパス
private		static readonly		string[]						m_strMenuPrefabPathList			= new string[ (int)MenuKind.EnumMax] {
	 "First/First"		//First
	,"Title/Title"		//Title
	,"Game/Game"		//Game
	,"Result/Result"	//Result
};

private							MainStep						m_eMainStep						= (MainStep)0;						//メインステップ

public							MenuKind						m_eDEBUG_MenuKind_Default		= MenuKind.None;					//現在選択中のメニュー
private							MenuKind						m_eMenuKind_Now					= MenuKind.None;					//現在選択中のメニュー
private							MenuKind						m_eMenuKind_Req					= MenuKind.First;					//遷移のリクエスト

public							GameObject						m_goFadePrefab					= null;								//フェード用のプレハブ
public							GameObject						m_goCoverPrefab					= null;								//カバー用のプレハブ

private		static readonly		float							sc_fFadePlane_Alpha_Max			= 1.0f;								//Alphaの最大
private		static readonly		float							sc_fFadePlane_Alpha_Margin		= 0.01f;							//Alphaを0とみなす許容
private							float							m_fFadePlane_Alpha_Now			= sc_fFadePlane_Alpha_Max;




//! 実行関数
private		void Update () {

	MainStep	eStep	= m_eMainStep;
	switch( eStep) {
	case MainStep.Initialize:
		Initialize();
		eStep++;
		break;
	case MainStep.ModeChange:
		ModeChange();
		eStep++;
		break;
	case MainStep.DeletePrefab:
		DeletePrefab();
		eStep++;
		break;
	case MainStep.CreatePrefab:
		CreatePrefab();
		eStep++;
		break;
	case MainStep.ControlWait:
		if( ControlWait()) {
			//メニュー入れ替え
			eStep	=MainStep.ModeChange;
		}
		break;
	}

	if( m_eMainStep!= eStep) {
		m_eMainStep = eStep;
	}
}







/////////////////////////////////////////////////////////////////////

/*
 *	初期化処理 開始
 */
private	void		Initialize() {
	if( null==m_goFadePrefab) {
		Debug.LogError( "Initialize : Fade");
	}
	if( null==m_goCoverPrefab) {
		Debug.LogError( "Initialize : Cover");
	}

	//最初は真黒にする
	SetFadeBlack();
	//カバーON
	SetCover( true);

	//デバッグ機能
	if( MenuKind.None!=m_eDEBUG_MenuKind_Default) {
		m_eMenuKind_Req	= m_eDEBUG_MenuKind_Default;
	}
}



/////////////////////////////////////////////////////////////////////

/*
 *	メニュー遷移のリクエストを送る
 */
public		void		SetMenuRequest( MenuKind eKind) {
	//他にリクエスト来てる？
	if( IsMenuRequest()) {
		//NG
		Debug.LogError( "System_Manager.SetMenuRequest : 多重リクエスト "+ eKind);
		return;
	}
	//リクエスト発行
	m_eMenuKind_Req	= eKind;
}
/*
 *	メニュー遷移のリクエスト確認
 */
private		bool		IsMenuRequest() {
	bool	bFlag	= false;
	//何かリクエストが来ている？
	if( m_eMenuKind_Now!=m_eMenuKind_Req) {
		bFlag	= true;
	}
	return bFlag;
}


/////////////////////////////////////////////////////////////////////

/*
 *	モード切り替え
 */
private	void		ModeChange() {
	//入れ替え
	m_eMenuKind_Now		= m_eMenuKind_Req;
}
/*
 *	プレハブ削除
 */
private	void		DeletePrefab() {
	//ヒエラルキー全体が持っているゲームオブジェクトを抽出
	GameObject[]	goList		= FindObjectsOfType( typeof( GameObject))	as GameObject[];
	foreach( GameObject goTemp in goList) {
		//根元(ヒエラルキーの最上位、親がnull)以外は無視
		if( null!=goTemp.transform.parent) {
			continue;
		}
		//ノード名をチェック
		if( IsDestroyOK( goTemp.name)) {
			Destroy( goTemp);
		}
	}
}
/*
 *	削除してはいけないものかチェック
 */
private		bool	IsDestroyOK( string strName) {

	bool	bFlag	= false;
	//ノード名をチェック
	switch( strName) {
	case "System_Manager":
		break;
	default:
		//消して良い
		bFlag	= true;
		break;
	}
	return bFlag;
}


/*
 *	プレハブ作成
 */
private	void		CreatePrefab() {
	int	iID		= (int)m_eMenuKind_Now;
	if( iID < 0) {
		Debug.LogError( "System_Manager : 遷移できないメニュー "+ m_eMenuKind_Now);
		return;
	}
	//プレハブを生成
	Instantiate(	Resources.Load( ""+ m_strMenuPrefabPathList[ iID]),	Vector3.zero,	Quaternion.identity);
}
/*
 *	操作待機
 */
private	bool		ControlWait() {
	bool	bReq	= false;
	//メニュー変更のリクエストが来た？
	if( IsMenuRequest()) {
		bReq	= true;
	}
	return bReq;
}



/////////////////////////////////////////////////////////////////////


/*
 *	⊿時間を取得
 */
protected				float	GetDeltaTime() {
	return	Time.deltaTime;
}



/*
 *	メニュー開始時のフェード 初期化(真黒にする)
 */
protected	void	SetFadeBlack() {
	m_fFadePlane_Alpha_Now	= sc_fFadePlane_Alpha_Max;
	SetFadeAlpha( sc_fFadePlane_Alpha_Max);
}
/*
 *	メニュー開始時のフェードイン指示
 */
public		void	SetFadeIn() {
	m_fFadePlane_Alpha_Now	= sc_fFadePlane_Alpha_Max;
}
/*
 *	メニュー開始時のフェードイン待機
 */
public		bool	IsFadeInEnd() {
	m_fFadePlane_Alpha_Now	= (m_fFadePlane_Alpha_Now	-GetDeltaTime());
	SetFadeAlpha( m_fFadePlane_Alpha_Now /sc_fFadePlane_Alpha_Max);
	if( m_fFadePlane_Alpha_Now<=sc_fFadePlane_Alpha_Margin) {
		return true;
	}
	return false;
}
/*
 *	メニュー開始時のフェードアウト指示
 */
public	void	SetFadeOut() {
	m_fFadePlane_Alpha_Now	= 0.0f;
}
/*
 *	メニュー開始時のフェードアウト待機
 */
public		bool	IsFadeOutEnd() {
	m_fFadePlane_Alpha_Now	= (m_fFadePlane_Alpha_Now	+GetDeltaTime());
	SetFadeAlpha( m_fFadePlane_Alpha_Now /sc_fFadePlane_Alpha_Max);
	if( m_fFadePlane_Alpha_Now>=(sc_fFadePlane_Alpha_Max -sc_fFadePlane_Alpha_Margin)) {
		return true;
	}
	return false;
}
/*
 *	フェードのα更新
 */
private		void		SetFadeAlpha( float fPercent) {
	//色は黒でアルファを弄る
	float	fValue	= Mathf.Clamp01( fPercent);
	if( fValue <= 0.01f) {
		m_goFadePrefab.renderer.enabled				= false;
	} else {
		Color	colTemp	= m_goFadePrefab.renderer.material.color;
		colTemp.a		= fValue;
		m_goFadePrefab.renderer.enabled				= true;
		m_goFadePrefab.renderer.material.color		= colTemp;
	}
}


/*
 *	カバーコリジョンの有効/無効切り替え
 */
public		void		SetCover( bool bFlag) {
	if( m_goCoverPrefab) {
		m_goCoverPrefab.collider.enabled		= bFlag;
	}
}






}

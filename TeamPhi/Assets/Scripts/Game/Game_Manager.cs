using UnityEngine;
using System.Collections;








/*
 *	ゲーム管理
 */
public	class	Game_Manager				: MonoBehaviour	{



//メインステップ
private		enum	eMainStep {
	 FadeI_Init			//フェードイン
	,FadeI_Wait			//フェードイン

	,Main_Init			//メイン処理
	,Main_Wait			//メイン処理

	,FadeO_Init			//フェードアウト
	,FadeO_Wait			//フェードアウト
	,End				//終了
}

private					eMainStep				m_eMainStep					= (eMainStep)0;	//ステップ


/*
 *	実行処理
 */
protected			void	Update() {
	eMainStep	eStep		= m_eMainStep;

	//ステップ遷移
	switch( eStep) {
	case eMainStep.FadeI_Init:			//フェードイン
		System_Manager.GetInst().SetFadeIn();
		Sound_Manager.GetInst().PlayMusic( eMusicID.Game);			//BGM 再生
		eStep++;
		break;
	case eMainStep.FadeI_Wait:			//フェードイン
		if( System_Manager.GetInst().IsFadeInEnd()) {
			eStep++;
		}
		break;

	case eMainStep.Main_Init:		//メイン
		Main_Init();
		eStep++;
		break;
	case eMainStep.Main_Wait:		//メイン
		if( Main_Wait()) {
			eStep++;
		}
		break;

	case eMainStep.FadeO_Init:			//フェードアウト
		System_Manager.GetInst().SetFadeOut();
		eStep++;
		break;
	case eMainStep.FadeO_Wait:			//フェードアウト
		if( System_Manager.GetInst().IsFadeOutEnd()) {
			System_Manager.GetInst().SetMenuRequest( MenuKind.Result);
			eStep++;
		}
		break;
	case eMainStep.End:				//終了
		break;
	default:
		Debug.LogError( "ERROR!");
		break;
	}

	if( eStep!=m_eMainStep) {
		m_eMainStep	= eStep;
	}
}


/*
 *	メイン処理 初期化
 */
protected	void	Main_Init() {
}
/*
 *	メイン処理 待機
 *
 *	終了して良ければtrue
 */
protected	bool	Main_Wait() {

	bool	bEnd	= false;
	//http://docs.unity3d.com/Documentation/ScriptReference/KeyCode.html
	if( Input.GetKeyDown( KeyCode.Mouse0)) {
		//クリックされたら次へ
		Sound_Manager.GetInst().PlaySound( eSoundID.System_Decide);		//SE
		bEnd	= true;
	}
	return	bEnd;
}






}//class


using UnityEngine;
using System.Collections;








/*
 *	最初のチュートリアル管理
 *	Maruchu
 */
public	class	First_Manager				: MonoBehaviour	{



//メインステップ
private		enum	eMainStep {
	 FadeI_Init1			//フェードイン
	,FadeI_Wait1			//フェードイン

	,Main_Init1			//スタッフ
	,Main_Wait1			//スタッフ

	,FadeO_Init1			//フェードアウト
	,FadeO_Wait1			//フェードアウト
	
	,FadeI_Init2			//フェードイン
	,FadeI_Wait2			//フェードイン
	
	,Main_Init2			//チュートリアル
	,Main_Wait2			//チュートリアル
		
	,FadeO_Init2			//フェードアウト
	,FadeO_Wait2			//フェードアウト

	,End				//終了
}


private					eMainStep				m_eMainStep					= (eMainStep)0;	//ステップ
public					GameObject				m_tutorial;
public					GameObject				m_staff;			
private 				float						m_iTime;
/*
 *	実行処理
 */
protected			void	Update() {
eMainStep	eStep		= m_eMainStep;

	//ステップ遷移
	switch( eStep) {
	case eMainStep.FadeI_Init1:			//フェードイン
		m_tutorial.SetActive(false);
		System_Manager.GetInst().SetFadeIn();
		eStep++;
		break;
	case eMainStep.FadeI_Wait1:			//フェードイン
		if( System_Manager.GetInst().IsFadeInEnd()) {
			eStep++;
		}
		break;

	case eMainStep.Main_Init1:		//メイン
		Main_Init1();
		eStep++;
		break;
	case eMainStep.Main_Wait1:		//メイン
		if( Main_Wait1()) {
			eStep++;
		}
		break;

	case eMainStep.FadeO_Init1:			//フェードアウト
		System_Manager.GetInst().SetFadeOut();
		eStep++;
		break;
	case eMainStep.FadeO_Wait1:			//フェードアウト
		if( System_Manager.GetInst().IsFadeOutEnd()) {
			m_staff.SetActive(false);
			eStep++;
		}
		break;
	case eMainStep.FadeI_Init2:			//フェードイン
		m_tutorial.SetActive (true);
		System_Manager.GetInst().SetFadeIn();
		eStep++;
		break;
	case eMainStep.FadeI_Wait2:			//フェードイン
		if( System_Manager.GetInst().IsFadeInEnd()) {
			eStep++;
		}
		break;
			
	case eMainStep.Main_Init2:		//メイン
		Main_Init2();
		eStep++;
		break;
	case eMainStep.Main_Wait2:		//メイン
		if( Main_Wait2()) {
			eStep++;
		}
		break;
			
	case eMainStep.FadeO_Init2:			//フェードアウト
		System_Manager.GetInst().SetFadeOut();
		eStep++;
		break;
	case eMainStep.FadeO_Wait2:			//フェードアウト
		if( System_Manager.GetInst().IsFadeOutEnd()) {
			System_Manager.GetInst().SetMenuRequest( MenuKind.Title);
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
 *	スタッフ 初期化
 */
protected	void	Main_Init1() {
}
/*
 *	スタッフ 待機
 *
 *	終了して良ければtrue
 */
protected	bool	Main_Wait1() {
	bool bEnd = false;
	m_iTime += Time.deltaTime;
	
	if (m_iTime > 4.0f) {
		bEnd = true;
	}
	
	return	bEnd;
}

	/*
 *	チュートリアル 初期化
*/
protected	void	Main_Init2() {
		m_staff.SetActive (false);
}
	/*
 *	チュートリアル 待機
 *
 *	終了して良ければtrue
 */
protected	bool	Main_Wait2() {
	bool bEnd = false;
	
	if (Input.GetMouseButtonDown (0))
			bEnd = true;
	
	return	bEnd;
}







}//class


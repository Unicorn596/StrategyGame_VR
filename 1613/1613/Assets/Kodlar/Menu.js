var menuresim : Texture2D;
var menuskin : GUISkin;

private var macbasliyor : int;

function OnGUI () {
	GUI.skin = menuskin;
	GUI.DrawTexture(Rect(0,0,Screen.width,Screen.height), menuresim, ScaleMode.StretchToFill, true);
	if (macbasliyor == 0 && GUI.Button(Rect(Screen.width / 2 + 70 ,Screen.height / 2 + 60,350,80), "MAÇA BAŞLA")) {
		macbasliyor = 1;
		oyunabasla();
	}
	else if (macbasliyor == 1) GUI.Label (Rect (Screen.width / 2 + 20,Screen.height / 2 + 60, 500, 100), "Hazırlanın! Maç başlıyor!");
}

function oyunabasla() {
	yield WaitForSeconds(3);
	Application.LoadLevel("oyun");
	//macbasliyor = 0;
}
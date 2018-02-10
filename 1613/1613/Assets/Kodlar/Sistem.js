var skorbord : Texture2D;
var gool : Texture2D;
var macsonaerdi : Texture2D;
var Top : Transform;
var Takim1Gol : int;
var Takim2Gol : int;
var MacTime : float;

private var gol1 : int;
private var gol2 : int;
private var golGUI : int;

var mySkin : GUISkin;
var TimeSkin : GUISkin;
var TimeShadow : GUISkin;

private var Bitirdik : int;

function OnGUI () {
	if (MacTime <= 0.5) {
		if (Bitirdik != 1) BitirdikMaci();
		GUI.DrawTexture(Rect(0,0,Screen.width,Screen.height), macsonaerdi, ScaleMode.StretchToFill, true);
		if (GUI.Button(Rect(Screen.width / 2 - 60 ,Screen.height / 2 + 30,120,35), "Tekrar Oyna")) {
			Application.LoadLevel("menu");
		}
		return;
	}
	GUI.skin = mySkin;
	GUI.DrawTexture(Rect(Screen.width/2 - 142,10,284,95), skorbord, ScaleMode.ScaleToFit, true);
	if (golGUI == 1) GUI.DrawTexture(Rect(Screen.width/2 - 333,Screen.height/2 - 80,666,159), gool, ScaleMode.ScaleToFit, true);
	GUI.Label (Rect (Screen.width/2-17, 32, 100, 30), Takim1Gol.ToString());
	GUI.Label (Rect (Screen.width/2+5, 32, 100, 30), Takim2Gol.ToString());
	GUI.skin = TimeShadow;
	GUI.Label (Rect (Screen.width/2-25, 61, 50, 30), Mathf.Round(MacTime).ToString());
	GUI.skin = TimeSkin;
	GUI.Label (Rect (Screen.width/2-26, 60, 50, 30), Mathf.Round(MacTime).ToString());
}

function Start () {
	MacTime = 100;
}

function FixedUpdate () {
	GolKontrol();
	if (Takim1Gol != Takim2Gol) MacTime -= Time.deltaTime;
}

function GolKontrol() {
	if (gol2 == 0 && Top.position.z < 3 && Top.position.z > -1.6 && Top.position.x > 65 && Top.position.x < 83 && Top.position.y < 5.5) {
		gol2 = 1;
		Gol2();
	}
	
	if (gol1 == 0 && Top.position.z > 217.15 && Top.position.z < 222 && Top.position.x > 67 && Top.position.x < 85 && Top.position.y < 5.5) {
		gol1 = 1;
		Gol1();
	}
}

function Gol1() {
	var Rand = Mathf.Round(Random.Range(0.6,3.4));
	spiker.Sound("gol"+Rand);
	golGUI = 1;
	Takim1Gol += 1;
	yield WaitForSeconds(2);
	TopuSabitle();
	gol1 = 0;
	Yenidenbaslat();
}

function Gol2() {
	var Rand = Mathf.Round(Random.Range(0.6,3.4));
	spiker.Sound("gol"+Rand);
	golGUI = 1;
	Takim2Gol += 1;
	yield WaitForSeconds(4);
	TopuSabitle();
	gol2 = 0;
	Yenidenbaslat();
}

function Yenidenbaslat() {
	golGUI = 0;
	TopuSabitle();
	Top.position = Vector3(73.51942,1,110.006);
	Top.GetComponent("Top").SonDokunus = null;
	TopuSabitle();
	//Player1
	for (i=0;i<=10;i++) {
		GameObject.Find("Player1").GetComponent("Kontroller").Topcular[i].position = GameObject.Find("Player1").GetComponent("Kontroller").Topcular[i].GetComponent("Futbolcu").BaslamaPozisyonu;
	}
	//Player1
	for (i=0;i<=10;i++) {
		GameObject.Find("Player2").GetComponent("Kontroller").Topcular[i].position = GameObject.Find("Player2").GetComponent("Kontroller").Topcular[i].GetComponent("Futbolcu").BaslamaPozisyonu;
	}
	yield WaitForSeconds(6);
	var Rand = Mathf.Round(Random.Range(0.6,2.4));
	spiker.Sound("tekrarbasladi"+Rand);
}

function TopuSabitle() {
	Top.GetComponent.<Rigidbody>().velocity.x = 0; Top.GetComponent.<Rigidbody>().velocity.y = 0; Top.GetComponent.<Rigidbody>().velocity.z = 0;
}

function BitirdikMaci() {
	if (Takim1Gol > Takim2Gol) spiker.Sound("macbitti1");
	else spiker.Sound("macbitti2");
	Bitirdik = 1;
}

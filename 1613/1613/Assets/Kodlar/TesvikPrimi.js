var TesvikParasi : float;
var Takim : float;
var RakipTakim : float;
private var nextTesvik : float;
var TesvikTextures : Texture2D[];

private var nextModifier : float;
private var modifier : float;

function FixedUpdate () {
	if (nextModifier < Time.time) { nextModifier = Time.time + 1; modifier = Random.Range(0.01,0.5); }
	if (TesvikParasi < 5) TesvikParasi += Time.deltaTime * modifier;
	if (TesvikParasi > 5) TesvikParasi = 5;
	TesvikSistemi();
}

function OnGUI() {
	if (GameObject.Find("Players").GetComponent("Sistem").MacTime <= 0.5) return;
	if (Takim == 1) var TesvikGUI = 20;
	else TesvikGUI = Screen.width - 370;
	if (Mathf.Round(TesvikParasi)-1 >= 0) GUI.DrawTexture(Rect(TesvikGUI,20,350,60), TesvikTextures[Mathf.Round(TesvikParasi)-1], ScaleMode.ScaleToFit, true);
}

function TesvikSistemi (){
	if (Input.GetButton("O"+Takim+"_Tesvik") && nextTesvik < Time.time) {
		nextTesvik = Time.time + 1;
		if (TesvikParasi >= 4.5) {
			var Rand = Mathf.Round(Random.Range(0.6,3.4));
			spiker.Sound("tesvik"+Rand);
			TesvikParasi = 0;
			for (i=0;i<=10;i++) {
				var RakipTransform = GameObject.Find("Player"+RakipTakim).GetComponent("Kontroller").Topcular[i];
				if (Random.Range(0,10) > 5) RakipTransform.SendMessage("TesvikAl");
			}
		}
	}
}
var aktifFutbolcu : Transform;
var Top : Transform;
var RakipKale : Transform;
var Topcular : Transform[];
var Takim : float;

private var sonuzaklik : float;
private var bisonrakidegisme : float;
private var topauzaklik : float;
private var targetFutbolcu : Transform;

function FixedUpdate () {
if (Input.GetButton("O"+Takim+"_AdamDegis")) OyuncuDegis();
if (aktifFutbolcu.GetComponent("Futbolcu").TesvikTime < Time.time) { 
	TopSur();
	SutHazirlan();
	PasHazirlan();
	Press();
}
}

function AnimasyonuDuzelt() {
	if (aktifFutbolcu.GetComponent.<Animation>().IsPlaying("kos") || !aktifFutbolcu.GetComponent.<Animation>().isPlaying) {
		var RandomAnim = Mathf.Round(Random.Range(1,3));
		AnimasyonOynat("isin"+RandomAnim);
	}
}

function TopSur() {
	if (aktifFutbolcu.GetComponent.<Animation>().IsPlaying("sut")) return;
	if (aktifFutbolcu.GetComponent("Futbolcu").sutFix == 0 && Mathf.Abs(Input.GetAxis("O"+Takim+"_Vertical")) > 0.2 || Mathf.Abs(Input.GetAxis("O"+Takim+"_Horizontal")) > 0.2) {
			var oyuncuRot = Vector3(Input.GetAxis("O"+Takim+"_Vertical")*100,0,Input.GetAxis("O"+Takim+"_Horizontal")*100) - Vector3.zero;
			if (oyuncuRot != Vector3.zero) aktifFutbolcu.rotation = Quaternion.LookRotation(oyuncuRot);
			aktifFutbolcu.transform.Translate(Vector3.forward * Time.deltaTime * 11);
			AnimasyonOynat("kos");
	}
	else if (!Input.GetButton("O"+Takim+"_TopaGit")) AnimasyonuDuzelt();
}

function AnimasyonOynat (animadi : String) {
	if (!aktifFutbolcu.GetComponent.<Animation>().IsPlaying("sut")) {
		if (!aktifFutbolcu.GetComponent.<Animation>().IsPlaying(animadi)) 
			{ 
			aktifFutbolcu.GetComponent.<Animation>().CrossFadeQueued(animadi, 0.3, QueueMode.PlayNow);
			if (animadi == "sut") aktifFutbolcu.GetComponent("Futbolcu").sutFix = 1;
		}
	}
}


var temp_dist : float = 999.0;
var temp_i : int = 0;
function OyuncuDegis() {
	sonuzaklik = 999;
	
	if (bisonrakidegisme > Time.time) return;
	
	for (i=0;i<=10;i++) {
		if (Topcular[i].GetComponent("Futbolcu").Kaleci == 0 && aktifFutbolcu != Topcular[i] && Topcular[i].GetComponent("Futbolcu").sutFix == 0) {
			temp_dist = Vector3.Distance(Top.position, Topcular[i].position);
			if(temp_dist < sonuzaklik) {
				sonuzaklik = temp_dist;
				temp_i = i;
			}
		}
	}
	
	if (aktifFutbolcu != Topcular[temp_i]) {
		aktifFutbolcu = Topcular[temp_i];
		bisonrakidegisme = Time.time + 0.5;
	}
}

function PasHazirlan() {
	if (Input.GetButton("O"+Takim+"_Pas")) {
		if (aktifFutbolcu.GetComponent("Futbolcu").bisonrakipas < Time.time) {
			aktifFutbolcu.GetComponent("Futbolcu").bisonrakipas = Time.time + 0.1;
			aktifFutbolcu.GetComponent("Futbolcu").PasGucu = 0;
			IlkPas();
		}
		else {
			aktifFutbolcu.GetComponent("Futbolcu").PasGucu += Time.deltaTime * 100;
		}
	}
}

function PasAt() {
	if (Vector3.Distance(aktifFutbolcu.position,Top.position) < 6 && aktifFutbolcu.GetComponent("Futbolcu").PasTamam == 1) {
		var topRot =  GameObject.Find(aktifFutbolcu.name+"pas").transform.position - aktifFutbolcu.position;
		Top.transform.rotation = Quaternion.LookRotation(topRot);
		var kuvvet = aktifFutbolcu.GetComponent("Futbolcu").PasGucu * 100;
		Top.GetComponent("Top").yolla(kuvvet,0);
		Top.GetComponent("Top").SonDokunus = aktifFutbolcu.gameObject;
	}
	aktifFutbolcu.GetComponent("Futbolcu").PasTamam = 0;
	aktifFutbolcu.GetComponent("Futbolcu").PasGucu = 0;
}

function IlkPas() {
	yield WaitForSeconds(0.1);
	aktifFutbolcu.GetComponent("Futbolcu").PasTamam = 1;
	PasAt();
}

function SutHazirlan() {
	if (Input.GetButton("O"+Takim+"_Sut")) {
		if (aktifFutbolcu.GetComponent("Futbolcu").bisonrakisut < Time.time) {
			aktifFutbolcu.GetComponent("Futbolcu").SutAcisi = SutAcisi();
			aktifFutbolcu.GetComponent("Futbolcu").bisonrakisut = Time.time + 5;
			aktifFutbolcu.GetComponent("Futbolcu").SutGucu = 0;
			AnimasyonOynat("sut");
			IlkSut();
		}
		else {
			aktifFutbolcu.GetComponent("Futbolcu").SutGucu += Time.deltaTime * 50;
		}
	}
}

function SutAt() {
	if (Vector3.Distance(aktifFutbolcu.position,Top.position) < 6 && aktifFutbolcu.GetComponent("Futbolcu").SutTamam == 1) {
			var topRot = RakipKale.position - Top.position;
			Top.transform.rotation = Quaternion.LookRotation(topRot);
			var kuvvet = aktifFutbolcu.GetComponent("Futbolcu").SutGucu * 250;
			var SutModifier = aktifFutbolcu.GetComponent("Futbolcu").SutAcisi;
			Top.GetComponent("Top").yolla(kuvvet,SutModifier);
	}
	aktifFutbolcu.GetComponent("Futbolcu").SutGucu = 0;
	aktifFutbolcu.GetComponent("Futbolcu").SutTamam = 0;
}

function IlkSut() {
	if (Random.Range(0,10)>7) {
		var SutRand = Mathf.Round(Random.Range(0.6,2.4));
		spiker.Sound("Sut"+SutRand);
	}
	yield WaitForSeconds(0.7);
	aktifFutbolcu.GetComponent("Futbolcu").SutTamam = 1;
	SutAt();
}

function SutAcisi() {
	var angle1 = Quaternion.Angle(aktifFutbolcu.rotation, RakipKale.rotation);
	var targetDir = aktifFutbolcu.position - RakipKale.position;
	var forward = aktifFutbolcu.transform.forward;
	var angle2 = Vector3.Angle(targetDir, forward);
	var sutMesafesi = 200-Vector3.Distance(aktifFutbolcu.position, RakipKale.position);
	sutModifier = 180-angle1 + angle2 - sutMesafesi*1.3;
	if (sutModifier < 0) sutModifier = 0;
	return sutModifier;
}

function Press() {
	if (Input.GetButton("O"+Takim+"_TopaGit") && !aktifFutbolcu.GetComponent.<Animation>().IsPlaying("sut") && Mathf.Abs(Input.GetAxis("O"+Takim+"_Vertical")) < 0.2 && Mathf.Abs(Input.GetAxis("O"+Takim+"_Horizontal")) < 0.2) {
		var TopaUzaklik = Vector3.Distance(Top.position,aktifFutbolcu.position);
		if (TopaUzaklik > 2) {
			var OyuncuRot = Top.position - aktifFutbolcu.position;
			aktifFutbolcu.rotation = Quaternion.LookRotation(Vector3(OyuncuRot.x,0,OyuncuRot.z));
			aktifFutbolcu.transform.Translate(Vector3.forward * Time.deltaTime * 11);
			AnimasyonOynat("kos");
		}
	}
}
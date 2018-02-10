var bisonrakisut : float;
var bisonrakipas : float;
var myMesh : GameObject;
var Takim : float;
var DigerTakim : float;
var sutFix : float;
var SutAcisi : float;
// Kaleci
var Kaleci : float;
var BaslamaPozisyonu : Vector3;

private var nextDikis : float;
private var kosRot : Vector3;

private var SutGucu : float;
private var PasGucu : float;
private var PasTamam : float;
private var SutTamam : float;

// TesvikPrimi
private var TesvikTime : float;

function Start() {
	BaslamaPozisyonu = transform.position;
	AssignMyMesh();
}

function FixedUpdate() {
	if (TesvikTime < Time.time) {
		if (Kaleci != 1) CheckForAnim();
		if (Kaleci != 1) TaktikKosusu();
		if (Kaleci == 1) KaleciFunc();
	}
}

function AssignMyMesh() {
	yield WaitForSeconds(0.1);
	myMesh = GameObject.Find(gameObject.name+"mesh");
}

function sutfix() {
	transform.Translate(Vector3.forward * 6);
}

function CheckForAnim() {
	if (GetComponent.<Animation>().IsPlaying("sut")) return;
	if (sutFix == 1) {
		sutfix();
		sutFix = 0;
	}
	if (!GetComponent.<Animation>().isPlaying) {
		var RandomAnim = Mathf.Round(Random.Range(1,3));
		GetComponent.<Animation>().CrossFadeQueued("isin"+RandomAnim, 0.3, QueueMode.PlayNow);
	}
}

function TaktikKosusu() {
	if (sutFix == 0 && GameObject.Find("Player"+Takim).GetComponent("Kontroller").aktifFutbolcu != transform ) {
		if (GameObject.Find("Top").GetComponent("Top").SonDokunus) {
			TopNerde = GameObject.Find("Top").GetComponent("Top").SonDokunus.GetComponent("Futbolcu").Takim;
			if (Takim == 1) {
					if (TopNerde == 2 && Vector3.Distance(transform.position ,BaslamaPozisyonu) < 2 ) return;
					if (TopNerde == 1 && Vector3.Distance(transform.position ,BaslamaPozisyonu) > 80) return;
			}
			else if (Takim == 2) {
					if (TopNerde == 1 && Vector3.Distance(transform.position ,BaslamaPozisyonu) < 2 ) return;
					if (TopNerde == 2 && Vector3.Distance(transform.position ,BaslamaPozisyonu) > 80) return;
			}
			if (TopNerde == Takim) kosRot = GameObject.Find("Takim"+Takim+"Atak").transform.position - transform.position;
			else kosRot = BaslamaPozisyonu - transform.position;
			if (kosRot != Vector3.zero) {	transform.rotation = Quaternion.LookRotation(kosRot);
			transform.Translate(Vector3.forward * Time.deltaTime * 11);
			if (!GetComponent.<Animation>().IsPlaying("kos")) GetComponent.<Animation>().CrossFadeQueued("kos", 0.3, QueueMode.PlayNow); }
			
		}
	}
}

function KaleciFunc() {
 	var TopaUzaklik = Vector3.Distance(GameObject.Find("Top").transform.position,transform.position);
 	var KaleUzaklik = Vector3.Distance(BaslamaPozisyonu,transform.position);
 	var TopKale = Vector3.Distance(GameObject.Find("Top").transform.position,BaslamaPozisyonu);
	if (TopaUzaklik < 25 && KaleUzaklik < 25 && TopKale < 25) {
		if (TopaUzaklik>0) {
			var KaleRot = GameObject.Find("Top").transform.position - transform.position;
			transform.rotation = Quaternion.LookRotation(Vector3(KaleRot.x,0,KaleRot.z));
			transform.Translate(Vector3.forward * Time.deltaTime * 13);
			if (!GetComponent.<Animation>().IsPlaying("kos")) GetComponent.<Animation>().CrossFadeQueued("kos", 0.3, QueueMode.PlayNow);
		}
	}
	else if (KaleUzaklik > 3 && TopaUzaklik > 2) {
			var AnaPos = BaslamaPozisyonu - transform.position;
			transform.rotation = Quaternion.LookRotation(Vector3(AnaPos.x,0,AnaPos.z));
			transform.Translate(Vector3.forward * Time.deltaTime * 7);
			if (!GetComponent.<Animation>().IsPlaying("kos")) GetComponent.<Animation>().CrossFadeQueued("kos", 0.3, QueueMode.PlayNow);
	}
	else if (TopaUzaklik > 2) {
		var RandomAnim = Mathf.Round(Random.Range(1,3));
		if (GetComponent.<Animation>().IsPlaying("kos") || !GetComponent.<Animation>().isPlaying) GetComponent.<Animation>().CrossFadeQueued("isin"+RandomAnim, 0.3, QueueMode.PlayNow);
	}
	if (TopaUzaklik <= 2 && TesvikTime < Time.time && nextDikis < Time.time) {
		var topRot = GameObject.Find("Player"+Takim).GetComponent("Kontroller").RakipKale.position - GameObject.Find("Top").transform.position;
		GameObject.Find("Top").transform.rotation = Quaternion.LookRotation(topRot);
		var kuvvet = 5000;
		GameObject.Find("Top").GetComponent("Top").yolla(kuvvet,1);
		nextDikis = Time.time + 1;
	}
}

function TesvikAl() {
	if (TesvikTime == 0) {
		TesvikTime = Time.time + Random.Range(10,20);
		var dollar = Instantiate(Resources.Load("sikeParticle",Transform),Vector3(0,-100,0),Quaternion.Euler(Vector3.zero));
		dollar.GetComponent("DestroyTimer").theTime = TesvikTime - Time.time;
		dollar.transform.parent = transform;
		dollar.transform.localPosition = Vector3.zero;
		var benjamin = Instantiate(Resources.Load("benjamin",Transform),Vector3(0,-100,0),Quaternion.Euler(Vector3(0,90,0)));
		benjamin.GetComponent("DestroyTimer").theTime = TesvikTime - Time.time;
		benjamin.transform.parent = transform;
		benjamin.transform.localPosition = Vector3.zero + Vector3(0,0.25,0);
		yield WaitForSeconds(TesvikTime - Time.time);
		TesvikTime = 0;
	}
}
var HangiOyuncu : float;

function FixedUpdate () {
	var targetOyuncu = GameObject.Find("Player"+HangiOyuncu).GetComponent("Kontroller").aktifFutbolcu;
	transform.position = targetOyuncu.position + Vector3(0,7,0);
}
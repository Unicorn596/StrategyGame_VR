var theTime : float;

function Start() {
	theTime += Time.time;
}

function FixedUpdate () {
	if (theTime < Time.time) Destroy(gameObject);
}
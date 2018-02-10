var Hedef : Transform;

private var UzaklikX : float;
private var UzaklikY : float;
private var UzaklikZ : float;

function Start () {
	UzaklikX = transform.position.x;
	UzaklikY = transform.position.y;
	UzaklikZ = transform.position.z;
}

function FixedUpdate() {
	transform.position = Vector3(Hedef.position.x + 70 , Hedef.position.y + UzaklikY , Hedef.position.z + 1.5 );
}
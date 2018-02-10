var SonDokunus : GameObject;

function FixedUpdate() {
	HighVelocityFix();
	YVelocityFix();
}

function OnCollisionEnter(collision : Collision) {
	for ( var contact : ContactPoint in collision.contacts ) {
		if ( contact.otherCollider.tag != "Field") {
				if ( contact.otherCollider.tag == "Kale") {
					if (Mathf.Abs(GetComponent.<Rigidbody>().velocity.y) > 0) GetComponent.<Rigidbody>().velocity.y = GetComponent.<Rigidbody>().velocity.y / 3;
					if (Mathf.Abs(GetComponent.<Rigidbody>().velocity.z) > 0) GetComponent.<Rigidbody>().velocity.z = GetComponent.<Rigidbody>().velocity.z / 3;
					if (Mathf.Abs(GetComponent.<Rigidbody>().velocity.x) > 0) GetComponent.<Rigidbody>().velocity.x = GetComponent.<Rigidbody>().velocity.x / 3;
					return;
				}
				if (Mathf.Abs(GetComponent.<Rigidbody>().velocity.y) > 0) GetComponent.<Rigidbody>().velocity.y = GetComponent.<Rigidbody>().velocity.y * 0.90;
				if (Mathf.Abs(GetComponent.<Rigidbody>().velocity.x) > 0) GetComponent.<Rigidbody>().velocity.x = GetComponent.<Rigidbody>().velocity.x * 0.90;
				if (Mathf.Abs(GetComponent.<Rigidbody>().velocity.z) > 0) GetComponent.<Rigidbody>().velocity.z = GetComponent.<Rigidbody>().velocity.z * 0.90;
		}
		
	}
}

function HighVelocityFix() {
	if (Mathf.Abs(GetComponent.<Rigidbody>().velocity.y) > 25) GetComponent.<Rigidbody>().velocity.y = GetComponent.<Rigidbody>().velocity.y/1.01;
	if (Mathf.Abs(GetComponent.<Rigidbody>().velocity.x) > 30) GetComponent.<Rigidbody>().velocity.x = GetComponent.<Rigidbody>().velocity.x/1.01;
	if (Mathf.Abs(GetComponent.<Rigidbody>().velocity.z) > 30) GetComponent.<Rigidbody>().velocity.z = GetComponent.<Rigidbody>().velocity.z/1.01;
}

function YVelocityFix() {
	if (transform.position.y < 1 && Mathf.Abs(GetComponent.<Rigidbody>().velocity.y) > 0) GetComponent.<Rigidbody>().velocity.y = GetComponent.<Rigidbody>().velocity.y*0.97;
}

function yolla(kuvvet : float,Modifier : float) {
	if (Modifier != 0) {
		GetComponent.<Rigidbody>().velocity.y = 0; GetComponent.<Rigidbody>().velocity.x = 0; GetComponent.<Rigidbody>().velocity.z = 0;
		GetComponent.<Rigidbody>().AddForce(Vector3(Random.Range(-Modifier,Modifier),Random.Range(-Modifier,Modifier),Random.Range(-Modifier,Modifier)));
	}
	GetComponent.<Rigidbody>().AddForce(transform.forward * kuvvet);
	GetComponent.<Rigidbody>().AddForce(Vector3(Random.Range(-kuvvet/3,kuvvet/3),Random.Range(-kuvvet/12,kuvvet/6),Random.Range(-kuvvet/3,kuvvet/3)));
}
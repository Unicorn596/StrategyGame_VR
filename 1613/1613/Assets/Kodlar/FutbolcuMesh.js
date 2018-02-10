var Bekle : int;

function Start () {
	gameObject.name = transform.parent.name + "mesh"; 
}

function OnCollisionEnter(collision : Collision) {
	for ( var contact : ContactPoint in collision.contacts ) {
		if ( contact.otherCollider.tag == "Top") {
			Bekle = 0;
			contact.otherCollider.GetComponent("Top").SonDokunus = transform.parent.gameObject;
			if (Random.Range(0,50) > 46) spiker.Sound("bircalim2calim");
		}
	}
}

function OnCollisionExit(collision : Collision) {
	for ( var contact : ContactPoint in collision.contacts ) {
		if ( contact.otherCollider.tag == "Top") {
			if (contact.otherCollider.GetComponent("Top").SonDokunus == transform.parent.gameObject)
			Bekle = 1;
			SonDokunusBekle();
		}
	}
}

function SonDokunusBekle() {
	yield WaitForSeconds(3);
	if (Bekle == 1)
	GameObject.Find("Top").GetComponent("Top").SonDokunus = null;
}
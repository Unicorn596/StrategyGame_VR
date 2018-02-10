public class spiker {

public static function Sound(Ses : String) {
	if (GameObject.Find("Spiker").GetComponent.<AudioSource>().clip.name.Contains("macbitti") && GameObject.Find("Spiker").GetComponent.<AudioSource>().isPlaying) return;
	if (!Ses.Contains("macbitti") && GameObject.Find("Spiker").GetComponent.<AudioSource>().clip.name.Contains("gol") && GameObject.Find("Spiker").GetComponent.<AudioSource>().isPlaying) return;
	if (Resources.Load("Sesler/Spiker/"+Ses,AudioClip).name == GameObject.Find("Spiker").GetComponent.<AudioSource>().clip.name && GameObject.Find("Spiker").GetComponent.<AudioSource>().isPlaying) return;
	GameObject.Find("Spiker").GetComponent.<AudioSource>().Stop();
	GameObject.Find("Spiker").GetComponent.<AudioSource>().clip = Resources.Load("Sesler/Spiker/"+Ses,AudioClip);
	GameObject.Find("Spiker").GetComponent.<AudioSource>().Play();
}

}
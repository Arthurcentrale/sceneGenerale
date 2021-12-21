using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePecheur : MonoBehaviour
{

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void dialoguePecheur(Text texte)
    {
        texte.text = "Di, mon garçon. Tu la préfères comment ta truite ? Avec ou sans plastique ? Quelle chance pour toi, sur cette île tu as le choix ! Mais ça va dépendre de toi aussi.";
    }

    public void animParler()
    {
        animator.SetTrigger("arreter");
        animator.SetTrigger("parler");
    }

    IEnumerator timeGestionDialogue()
    {
        yield return new WaitForSeconds(1.5f);
    }
}

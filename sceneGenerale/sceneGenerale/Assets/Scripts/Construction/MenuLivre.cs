using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// C'est le script que j'utilise pour lancer les animations du livre
public class MenuLivre : MonoBehaviour
{
    //public AudioClip jspquoi;            éventuellement pour rajouter des clips audio pour ouverture du livre, tourner une page etc..

    public GameObject CanvasAnimLivre, PanelLivre;
    public Button boutonLivre;
    private Animator animator;
    public BoutonsMenuConstruction BoutonsMenuConstruction;  //j'importe


    void Start()
    {
        animator = PanelLivre.transform.GetChild(0).GetComponent<Animator>();
        boutonLivre = boutonLivre.GetComponent<Button>();  // boutonLivre est pas directement un bouton, c'est un sprite auquel on a rajouté un component button, c'est lui qu'on récupère ici
    }

    public void BoutonLivre() //pour afficher le menu principal, donc livre ouvert à moitié
    {
        animator.SetTrigger("ouverture_moitie");
        BoutonsMenuConstruction.menuPrincipal(); //on affiche le menu principal
    }

    public void BoutonFermerLivreDepuisMoitie()
    {
        animator.SetTrigger("fermeture"); //Est-ce que ya différentes anims de fermeture (Une depuis le livre à moitié ouvert, l'autre quand il est complétement ouvert?)
        BoutonsMenuConstruction.CloseMenuPrincipal();
    }

    public void BoutonOuvertureCompletDepuisMoitie()
    {
        animator.SetTrigger("ouverture_complete");
        BoutonsMenuConstruction.MenuConstructions();
    }

    public void BoutonFermerLivreDepuisComplet()
    {
        animator.SetTrigger("fermeture"); //Est-ce que ya différentes anims de fermeture (Une depuis le livre à moitié ouvert, l'autre quand il est complétement ouvert?)
        BoutonsMenuConstruction.CloseMenuConstructions(); //on fait déjà la différence entre les différentes pages dans le script
    }

    public void BoutonRetourLivreMoitieDepuisComplet()
    {
        //animator.SetTrigger("fermeture"); //fermeture puis ouverture en moitié? ou reverse l'anim?
        animator.SetTrigger("retour_moitie");
        BoutonsMenuConstruction.CloseMenuConstructions();
        BoutonsMenuConstruction.menuPrincipal();
    }

    //anim pour tourner une page???
    



    //animator.SetTrigger("ouverture_moitie");
    //animator.SetTrigger("ouverture_complete");
    //animator.SetTrigger("retour_moitie");
    //animator.SetTrigger("fermeture");
}

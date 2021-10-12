using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace classes
{

    public class Ressource
    {
        public int Nombre { get; }
        public string Item { get; }

        public Ressource(int nombre, string item){
            Nombre = nombre;
            Item = item;      
            }
            
    }
    public class Batiment
    {
        public int tempsConstruction { get; set; }
        public Ressource[] ressourcesConstruction { get; set; }
        public Ressource[] ressourcesDeposees { get; set; }
        public bool ouvrierIci { get; set; }
        public bool enTravail { get; set; }
        public Ressource[] ressourcesProduction { get; set; }
        public string nomOuvrier { get; set; }
    }

}
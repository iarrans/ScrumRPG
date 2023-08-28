using ORKFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarSuperrol : MonoBehaviour
{
    ORKFramework.PlayerHandler playerHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void elegirRoles()
    {
        Group grupoactual = this.playerHand.ActiveGroup;
        string id_grupo = grupoactual.GUID;
        int cantidad_combatientes = grupoactual.Size;
        for(int i= cantidad_combatientes-1; i>=0;i--){
            Combatant personaje = grupoactual.MemberAt(i);
            string nombrePJ = personaje.GetName();
            Debug.Log($"Recorriendo el personaje------------------- {nombrePJ}");
        };
    }
   
}

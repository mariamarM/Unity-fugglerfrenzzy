using UnityEngine;
//CLASE PARA MANEJAR EL ESTADO DE ESPERA
public interface IdleState 
{
       void Enter();
    void Update();
    void Exit();
    void OnMouseDown();
    void OnMouseUp();

}

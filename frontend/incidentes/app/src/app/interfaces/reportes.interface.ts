export interface ReporteBugsProyecto {
  
    Nombre?:string;
    Id?:number;
    CantidadDeIncidentes?:number;
}


export interface ReporteBugsDesarrollador {
  
    Id?:number;
    NombreUsuario:string;
    Nombre?:string;
    Apellido?:string;
    Email?:string;
    RolUsuario?:number;
    IncidentesResueltos?:number;    
    ValorHora?:number;

}
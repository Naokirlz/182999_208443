import { Usuario } from "./dtoUsuario.interface";
import { Incidente } from "./incidente.interface";

export interface Proyecto {
    id?:         number;
    nombre:     string;
    duracion?:   number;
    costo?:      number;
    incidentes?: Incidente[];
    tareas?:     any[];
    asignados?:  Usuario[];
}



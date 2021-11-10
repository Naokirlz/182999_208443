export interface Incidente {
    id?:              number;
    Id?:              number;
    nombre?:          string;
    proyectoId?:      number;
    descripcion?:     string;
    version?:         string;
    estadoIncidente?: number;
    desarrolladorId?: number;
    usuarioId?:       number;
    duracion?:        number;
}
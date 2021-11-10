export interface Incidente {
    id?:              number;
    Id?:              number;
    nombre?:          string;
    Nombre?:          string;
    proyectoId?:      number;
    ProyectoId?:      number;
    descripcion?:     string;
    Descripcion?:     string;
    version?:         string;
    Version?:         string;
    estadoIncidente?: number;
    EstadoIncidente?: number;
    desarrolladorId?: number;
    DesarrolladorId?: number;
    usuarioId?:       number;
    UsuarioId?:       number;
    duracion?:        number;
    Duracion?:        number;
}
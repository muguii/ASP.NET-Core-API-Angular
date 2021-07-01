import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable({
  providedIn: 'root'
})
export class EventoService {

  baseURL = 'https://localhost:44343/api/Evento';

  constructor(private http: HttpClient) { }

  getAllEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL);
  }

  getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`);
  }

  getEventosByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${tema}`);
  }

  postEvento(evento: Evento) {
    return this.http.post<Evento>(this.baseURL, evento);
  }

  putEvento(evento: Evento) {
    return this.http.put<Evento>(`${this.baseURL}/${evento.id}`, evento);
  }

  deleteEvento(eventoId: number) {
    return this.http.delete(`${this.baseURL}/${eventoId}`);
  }
}

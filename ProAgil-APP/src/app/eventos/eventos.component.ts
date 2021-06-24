import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  _filtroLista: string;
  eventosFiltrados: Evento[];
  eventos: Evento[];
  imagemLargura: number;
  imagemMargem: number;
  mostrarImagem: boolean;
  modalRef: BsModalRef | any;

  constructor(private eventoService: EventoService, private modalService: BsModalService) {
    this._filtroLista = '';
    this.eventosFiltrados = [];
    this.eventos = [];
    this.imagemLargura = 50;
    this.imagemMargem = 2;
    this.mostrarImagem = false;
  }

  ngOnInit(): void {
    this.getEventos();
  }
  
  getEventos() {
    this.eventoService.getAllEventos().subscribe((_eventos: Evento[]) => {
      this.eventos = _eventos;
      this.eventosFiltrados = this.eventos;
      console.log(_eventos);
    }, error => {
      console.log(error);
    });
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  alternarImagem() {
    this.mostrarImagem = !this.mostrarImagem;
  }

  filtrarEventos(filtrarPor: string): Evento[] {
    return this.eventos.filter(evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor.toLocaleLowerCase()) !== -1)
  }

  get filtroLista(): string {
    return this._filtroLista;
  }

  set filtroLista(filtraPor: string) {
    this._filtroLista = filtraPor;
    this.eventosFiltrados = this._filtroLista ? this.filtrarEventos(this._filtroLista) : this.eventos;
  }
}

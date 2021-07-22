import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '../models/Evento';
import { ToastrService } from 'ngx-toastr';
import { EventoService } from '../services/evento.service';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ptBrLocale } from 'ngx-bootstrap/locale';
defineLocale('pt-br', ptBrLocale);

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  titulo: string;

  _filtroLista: string;
  eventosFiltrados: Evento[];
  eventos: Evento[];
  imagemLargura: number;
  imagemMargem: number;
  mostrarImagem: boolean;
  registerForm: FormGroup | any;
  dataEvento: string;

  bFlInsert: number;
  eventoId: number;

  bodyDeletarEvento: string;

  file: File[];

  constructor(private eventoService: EventoService, private modalService: BsModalService, private formBuilder: FormBuilder, private localeService: BsLocaleService, private toastr: ToastrService) {
    this.titulo = 'Eventos';
    this._filtroLista = '';
    this.eventosFiltrados = [];
    this.eventos = [];
    this.imagemLargura = 50;
    this.imagemMargem = 2;
    this.mostrarImagem = false;
    this.dataEvento = '';

    this.localeService.use('pt-br');

    this.bFlInsert = 1;
    this.eventoId = -1;

    this.bodyDeletarEvento = '';

    this.file = [];
  }

  ngOnInit(): void {
    this.validation();
    this.getEventos();
  }
  
  getEventos() {
    this.eventoService.getAllEventos().subscribe((_eventos: Evento[]) => { //ESTUDAR MELHOR ESSE subscribe
      this.eventos = _eventos;
      this.eventosFiltrados = this.eventos;
      console.log(_eventos);
    }, error => {
      this.toastr.error(`Erro ao carregar eventos. ${error}`);
    });
  }

  openModal(template: any) {
    this.registerForm.reset();
    template.show();
  }

  editarEvento(evento: Evento, template: any) {
    this.bFlInsert = 0;
    this.openModal(template);
    this.eventoId = evento.id;

    const eventoCopy = Object.assign({}, evento);
    eventoCopy.imagemURL = '';

    this.registerForm.patchValue(eventoCopy); //ESTUDAR MELHOR ESSE patchValue
  }

  novoEvento(confirmDelete: any) {
    this.bFlInsert = 1;
    this.openModal(confirmDelete);
  }

  excluirEvento(evento: Evento, confirmDelete: any) {
    confirmDelete.show();
    this.eventoId = evento.id;
	  this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}?`;
  }

  confirmeDelete(template: any) {
    this.eventoService.deleteEvento(this.eventoId).subscribe(
		  () => {
	    	  template.hide();
        this.getEventos();
        this.toastr.success('Deletado com sucesso!');
      }, error => {
        this.toastr.error('Erro ao tentar deletar.');
	    	console.log(error);
	  	}
	  );
  }

  salvarAlteracao(template: any) {
    if (this.registerForm.valid) {
      if (this.bFlInsert === 1) {
        const evento = Object.assign({}, this.registerForm.value);

        const nomeArquivo = evento.imagemURL.split('\\', 3);
        evento.imagemURL = nomeArquivo[2];
        this.eventoService.postUpload(this.file, evento.imagemURL).subscribe();

        this.eventoService.postEvento(evento).subscribe( //ESTUDAR MELHOR ESSE subscribe
          (novoEvento: Evento) => {
            console.log(novoEvento);
            template.hide();
            this.getEventos();
            this.toastr.success('Inserido com sucesso!');
          }, error => {
            this.toastr.error(`Erro ao inserir. ${error}`);
            console.log(error);
          }
        );
      }
      else {
        const evento = Object.assign({ id: this.eventoId }, this.registerForm.value);

        const nomeArquivo = evento.imagemURL.split('\\', 3);
        evento.imagemURL = nomeArquivo[2];
        this.eventoService.postUpload(this.file, evento.imagemURL).subscribe();

        this.eventoService.putEvento(evento).subscribe( //ESTUDAR MELHOR ESSE subscribe
          (eventoAlterado: Evento) => {
            console.log(eventoAlterado);
            template.hide();
            this.getEventos();
            this.toastr.success('Editado com sucesso!');
          }, error => {
            this.toastr.error(`Erro ao editar. ${error}`);
            console.log(error);
          }
        );
      }
    }
  }

  onFileChange(event: any) {
if (event.target.files && event.target.files.length) {
      this.file = event.target.files;
      console.log(this.file);
    }
  }

  validation() {
    this.registerForm = this.formBuilder.group({ //ESTUDAR MELHOR ESSE formBuilder.group
      tema:       ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local:      ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      imagemURL:  ['', Validators.required],
      telefone:   ['', Validators.required],
      email:      ['', [Validators.required, Validators.email]]
    })
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

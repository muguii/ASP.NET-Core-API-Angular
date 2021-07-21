import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-contatos',
  templateUrl: './contatos.component.html',
  styleUrls: ['./contatos.component.css']
})
export class ContatosComponent implements OnInit {

  @Input() titulo: string;

  constructor() {
    this.titulo = 'Contatos';
  }

  ngOnInit(): void {
  }

}

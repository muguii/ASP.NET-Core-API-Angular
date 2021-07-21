import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-palestrantes',
  templateUrl: './palestrantes.component.html',
  styleUrls: ['./palestrantes.component.css']
})
export class PalestrantesComponent implements OnInit {

  @Input() titulo: string;

  constructor() {
    this.titulo = 'Palestrantes';
  }

  ngOnInit(): void {
  }

}

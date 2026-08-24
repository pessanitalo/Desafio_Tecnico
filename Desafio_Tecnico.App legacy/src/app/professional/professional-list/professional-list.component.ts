import { Component, OnInit } from '@angular/core';
import { Professional } from '../models/professional';
import { ProfessionalService } from '../services/professionar.service';

import { RouterLink } from '@angular/router';
import { NgxMaskDirective, NgxMaskPipe } from 'ngx-mask';

@Component({
    selector: 'app-professional-list',
    imports: [RouterLink, NgxMaskDirective, NgxMaskPipe],
    templateUrl: './professional-list.component.html',
    styleUrl: './professional-list.component.css'
})
export class ProfessionalListComponent implements OnInit {

  profissionais!: Professional[];

  constructor(
    private professionalService: ProfessionalService,
  ) { }



  ngOnInit(): void {
    this.listaprofissionais();
  }

  listaprofissionais() {
    this.professionalService.list().subscribe(
      profissional => {
        this.profissionais = profissional;
      },
      error => console.log('Erro:', error)
    );
  }

}

import { Component } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { OrderService } from '../service/order.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2'


@Component({
  selector: 'app-insert',
  templateUrl: './insert.component.html',
  styleUrls: ['./insert.component.css'],
})
export class InsertComponent {
  formOrdem!: FormGroup;
  
  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private orderService: OrderService, 
    ) { }

  ngOnInit() {
    this.createForm();
  }

  createForm() {
    this.formOrdem = this.formBuilder.group({
      OrderName: [],
      Ativo: []
    })
  }

  onSubmit() {
    var order = this.formOrdem.value;

    if(order.Ativo != undefined && order.Ativo)
      order.Ativo = 1;
    else
      order.Ativo = 0;

    this.orderService.insert(order)
    .subscribe(
      () => {
        Swal.fire(
          'Sucesso',
          'Inseriu com sucesso!',
          'success'
        )
        this.formOrdem.reset();
        this.router.navigate(['']);
      }
    );
  }
  
}

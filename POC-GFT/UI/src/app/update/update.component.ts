import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { OrderService } from '../service/order.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Order } from '../Model/order';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent {
  formOrdem!: FormGroup;
  codigo: number = 0;
  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private orderService: OrderService 
    ) { }


    ngOnInit() {
      this.route.params.subscribe(params => {
        this.codigo = parseInt(params['codigo']);
      });

      if(this.codigo == 0)
        return;
      
        this.formOrdem = this.formBuilder.group({
          Codigo: [0],
          OrderName: [""],
          Ativo: [false]
        })
      
      this.updateForm();
    }

  updateForm() {

    this.orderService.getById(this.codigo)
    .subscribe(
      (ordem:Order) => {

        this.formOrdem.patchValue({
          Codigo: ordem.codigo,
          OrderName: ordem.orderName,
          Ativo: ordem.ativo
        });
      }
    );    
  }

  onSubmit(){
    let order  = this.formOrdem.value;

    if(order.Ativo)
      order.Ativo = 1;
    else
      order.Ativo = 0;
    
    this.orderService.update(order)
    .subscribe(
      () => {
        Swal.fire('Alterado com sucesso! (outro tipo de formatação de mensagem)');

        this.formOrdem.reset();
        this.router.navigate(['']);
      }
    );
  }

}

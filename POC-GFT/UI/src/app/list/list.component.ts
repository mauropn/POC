import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, shareReplay } from 'rxjs';
import { Order } from '../Model/order';
import { OrderService } from '../service/order.service';


@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent {
  title = 'front';
  orderList!: Order[];
  orders$: Observable<Order[]> | undefined;
  dataSource: any;
  displayedColumns: String[] = ['codigo', 'orderName', 'dataCadastro', 'ativo', 'opcoes'];

  
  constructor(
    private router: Router, 
    private orderService: OrderService) 
    { }

  ngOnInit() {
 

    this.reload();
  }

  reload() {
    //Option 01 -> Usando observables....
    this.orders$ = this.orderService.findAllOrders()
    .pipe(
      shareReplay()
    )

    

    this.orderService
      .findAllOrders()
      .subscribe((orders) => {
        this.dataSource = orders
      }, err => {
        console.log(err.message);
      }
      );
  }

  Inserir(){
    this.router.navigate(['insert']);
  }
}

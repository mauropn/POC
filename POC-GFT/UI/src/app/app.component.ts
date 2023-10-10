import { Component } from '@angular/core';
import { OrderService } from './service/order.service';
import { map, shareReplay, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Order } from './Model/order';
import {MatTableModule} from '@angular/material/table';
import { Router } from '@angular/router';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})



export class AppComponent {

}

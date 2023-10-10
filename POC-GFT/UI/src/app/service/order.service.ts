import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, catchError, map, retry } from 'rxjs';
import { Order } from '../Model/order';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  
  constructor( private http: HttpClient ) { }
  baseUrl = environment.apiUrl;
  
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  findAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.baseUrl + 'Order')
    .pipe(retry(1));
  }

  insert(order: Order) {
    return this.http.post(this.baseUrl + 'Order/Insert', order);
  }

  update(order: Order) {
    return this.http.post(this.baseUrl + 'Order/Update', order);
  }

  getById(Codigo: number) {
    return this.http.get<Order>(this.baseUrl + 'Order/' + Codigo)
    .pipe(retry(1));
  }
  
}

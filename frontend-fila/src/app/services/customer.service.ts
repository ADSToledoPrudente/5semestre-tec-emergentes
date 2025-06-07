import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Customer {
  Name: string;
  Priority: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  private baseUrl = 'https://localhost:7038/api/customer';

  constructor(private http: HttpClient) {}

  addToQueue(queueId: number, customer: Customer): Observable<any> {
    return this.http.post(`${this.baseUrl}/addtoqueue/${queueId}`, customer);
  }

  removeFromQueue(customerId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/removefromqueue/${customerId}`);
  }
}

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QueueService {
  private baseUrl = 'https://localhost:7038/api/queue';

  constructor(private http: HttpClient) {}

  create(name: string): Observable<any> {
    return this.http.post(`${this.baseUrl}/new/${name}`, {});
  }

  getAll(): Observable<any> {
    return this.http.get<any[]>(`${this.baseUrl}/getall`);
  }

  getById(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}/getbyid/${id}`);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/delete/${id}`);
  }
}

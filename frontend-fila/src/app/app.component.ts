import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LoginComponent } from "./view/login/component/component.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoginComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit{
  title = 'frontend-fila';

  public logado: boolean | null = false;

  ngOnInit(): void {
    this.logado = this.verify();
    console.log(this.logado);
  }


  verify() {
    return sessionStorage.getItem('token') != "" && sessionStorage.getItem('token') != null;
  }
}

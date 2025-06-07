import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoginRequest, UserService} from '../../../services/user.service';
import { Router } from '@angular/router';
import { timeout } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './component.component.html',
  styleUrl: './component.component.scss'
})
export class LoginComponent implements OnInit{
loginForm!: FormGroup;

  constructor(
    private fb: FormBuilder
  , private userService: UserService
  , private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.loginForm.valid) {
      const { email, senha } = this.loginForm.value;
      let login: LoginRequest = {
        Email: email,
        Password: senha
      }

      this.userService.login(login).subscribe({
        next: (data) => {
          if(data.success) {
            this.armazenarToken();
            this.router.navigate(['queues']);
          }
          else {
            this.loginForm.reset();
            alert('Usuário ou senha inválidos');
          }
        }});
    } else {
      this.loginForm.markAllAsTouched();
    }
  }

  armazenarToken() {
    sessionStorage.setItem('token', 'autenticado');
  }
}

import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { QueueService } from '../../../services/queue.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-queues',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './queues.component.html',
  styleUrl: './queues.component.scss'
})
export class QueuesComponent implements OnInit{
  private fb = inject(FormBuilder);
  queues: any[] = [];
  formFila: FormGroup = this.fb.group({
    name: ['', Validators.required]
  });
  constructor(
    private router: Router,
    private queueService: QueueService
  ) {}

  ngOnInit(): void {
    this.getAll();
  }

  abrirFila(id: number) {
    this.router.navigate(['/queue', id]);
  }

  getAll() {
    this.queueService.getAll().subscribe({
      next: (data) => {
        this.queues = data.data;
      }
    })
  }

  modalAberto = false;

  abrirModal() {
    this.modalAberto = true;
  }

  fecharModal() {
    this.modalAberto = false;
    this.formFila.reset();
  }

  criarFila() {
    if (this.formFila.valid) {
      const nome = this.formFila.value.name;
      this.queueService.create(nome).subscribe(() => {
        this.fecharModal();
        this.getAll();
      });
    }
  }
}

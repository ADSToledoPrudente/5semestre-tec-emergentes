import { CommonModule, Location } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { QueueService } from '../../../services/queue.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CustomerService } from '../../../services/customer.service';

@Component({
  selector: 'app-queue-view',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './queue-view.component.html',
  styleUrl: './queue-view.component.scss'
})
export class QueueViewComponent implements OnInit {
  queueId!: number;
  customers: any[] = [];
  queue: any = {};
  private fb = inject(FormBuilder);

  formUsuario: FormGroup = this.fb.group({
    name: ['', Validators.required],
    priority: [false]
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private queueService: QueueService,
    private customerService: CustomerService
  ) {}

  ngOnInit() {
    this.queueId = Number(this.route.snapshot.paramMap.get('id'));
    this.getCustomers();
  }

  getCustomers() {
    this.queueService.getById(this.queueId).subscribe({
      next: (data) => {
        this.queue = data.data;
        this.customers = data.data.customer;
      }
    });
  }

  finalizar(id: number) {
    this.removeFromQueue(id);
  }

  cancelar(id: number) {
    this.removeFromQueue(id);
  }

  removeFromQueue(customerId: number) {
    this.customerService.removeFromQueue(customerId).subscribe({
      next: () => {
        this.getCustomers();
      }
    })
  }

  voltar() {
    this.router.navigate(['/queues']);
  }

  modalAberto = false;

  abrirModal() {
    this.modalAberto = true;
  }

  fecharModal() {
    this.modalAberto = false;
    this.formUsuario.reset({ Priority: false });
  }

  addUserToQueue() {
    if (this.formUsuario.valid && this.queueId) {
      const data = this.formUsuario.value;
      this.customerService.addToQueue(this.queueId, data).subscribe(() => {
        this.fecharModal();
        this.getCustomers();
      });
    }
  }
}
